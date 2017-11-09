using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Timers;
using Aspose.Pdf;
using Newtonsoft.Json;
using PW.Ncels.Database.Models;
using PW.Ncels.Database.DataModel;
using PW.Ncels.Database.Helpers;
using SignalR;
using Stimulsoft.Report;

namespace PW.Prism.Hubs
{
    public sealed class ConnectionCache
    {
		
        private static readonly ConnectionCache instance
            = new ConnectionCache();

        private Dictionary<string, UserCredential> _connections;
        private Timer _timer;

        static ConnectionCache()
        {

        }

        private ConnectionCache()
        {
            _connections = new Dictionary<string, UserCredential>();
            _timer = new Timer();
            _timer.Interval = 70000;
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _timer.AutoReset = true;
        }

        public static ConnectionCache Instance
        {
            get
            {
                return instance;
            }
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            PingClients();
        }

	    private string GetTask(string userId)
	    {
		    ncelsEntities contextEntities = UserHelper.GetCn();
		    Employee employee = contextEntities.Employees.FirstOrDefault(o => o.Login == userId);
		    if (employee != null)
		    {
			    string id = employee.Id.ToString();
			    DateTime date = DateTime.Now.AddHours(-1);
			    int countAll = contextEntities.Tasks.Include("Document").Count(o => o.Document.IsDeleted ==false && o.State == 0 && o.ExecutorId == id && o.IsActive);
				int count = contextEntities.Tasks.Include("Document").Count(o => o.Document.IsDeleted == false && o.State == 0 && o.ExecutorId == id && o.CreatedDate > date && o.IsActive);
		        int countRegister = 0;
		        int countWorkNote = 0;
                if (IsAllow(employee.Id, "IsMenuOutDocVisibility"))
                {
                    countRegister += contextEntities.Documents.Count(o => o.IsDeleted == false && o.DocumentType == 1 && (o.OutgoingType == 1 || o.OutgoingType == 2) && o.StateType == 0 && o.OrganizationId == employee.OrganizationId);
                }
                if (IsAllow(employee.Id, "IsMenuInitiativeOutDocVisibility"))
                {
                    countRegister += contextEntities.Documents.Count(o => o.IsDeleted == false && o.DocumentType == 1 && o.OutgoingType == 0 && o.StateType == 0 && o.OrganizationId == employee.OrganizationId);
                }
                if (IsAllow(employee.Id, "IsMenuInnerDocVisibility"))
                {
                    countWorkNote = contextEntities.Documents.Count(o => o.IsDeleted == false && o.DocumentType == 5 && o.ApplicantType == 1 && o.StateType == 0 && o.OrganizationId == employee.OrganizationId);
                }
                return JsonConvert.SerializeObject(new { countAll, count, countRegister , countWorkNote });
		    }
			return string.Empty;
	    }

        private bool IsAllow(Guid employeeId, string key) {
            var permission =  EmployePermissionHelper.IsVisibility(key, employeeId);
            return permission;
        }

        private void PingClients()
        {
			


            var hubContext =
                    GlobalHost
                        .ConnectionManager
                            .GetHubContext<ServerSideTimerHub>();

	        
            foreach (var item in _connections.Values)
            {
                if(item==null)
                    continue;
                TimeSpan span =
                    new TimeSpan(item.GetSessionLengthInTicks());
                ConnectionSession session =
                    item.Sessions[item.Sessions.Count - 1];
	            var tasks = GetTask(item.UserId);
                hubContext.Clients[session.ConnectionId]
					.tick(session.ConnectionId, item.UserId, tasks);
            }
        }

        private void CreateNewUserSession(string userId, string connectionId)
        {
            UserCredential currentCred = new UserCredential
            {
                ConnectionStatus = ConnectionStatus.Connected,
                UserId = userId
            };
            currentCred.Sessions.Add(new ConnectionSession
            {
                ConnectionId = connectionId,
                ConnectedTime = DateTime.Now.Ticks,
                DisconnectedTime = 0L,
                ParentUser = currentCred
            });
            if (!_connections.ContainsKey(userId))
            {
                _connections.Add(userId, currentCred);
            }
        }   

        private void UpdateUserSession(string userId, 
            string connectionId, ConnectionStatus status)
        {
            UserCredential currentCred = _connections[userId];

            ExpireSession(currentCred);
            currentCred.Sessions.Add(new ConnectionSession
            {
                ConnectionId = connectionId,
                ConnectedTime = DateTime.Now.Ticks,
                DisconnectedTime = 0L,
                ParentUser = currentCred
            });
            currentCred.ConnectionStatus = status;
        }

        private static void ExpireSession(UserCredential currentCred)
        {
            ConnectionSession currentSession =
                currentCred.Sessions.Find(sess =>
                    sess.DisconnectedTime == 0);
            if (currentSession != null
                && currentSession.DisconnectedTime == 0)
            {
                currentSession.DisconnectedTime = DateTime.Now.Ticks;
            }
        }

        internal void UpdateCache(string userId,
            string connectionId, ConnectionStatus status)
        {
            if (_connections.ContainsKey(userId)
                && !string.IsNullOrEmpty(userId))
            {
                UpdateUserSession(userId, connectionId, status);
            }
            else
            {
                if (!_timer.Enabled)
                {
                    _timer.Enabled = true;
                    _timer.Start();
                }
                CreateNewUserSession(userId, connectionId);
            }
        }

        internal void Disconnect(string connectionId)
        {
            ConnectionSession session = null;
            if (_connections.Values.Count > 0)
            {
                foreach (var currentCredi in _connections.Values)
                {
                    session = currentCredi.Sessions.Find(ss =>
                        ss.ConnectionId == connectionId);
                    if (session != null)
                    {
                        session.DisconnectedTime = DateTime.Now.Ticks;
                        break;
                    }
                }
            }
        }

        internal void Logout(string userId)
        {
            ExpireSession(this._connections[userId]);
            // Save to DB If required
            this._connections.Remove(userId);
            if (this._connections.Count == 0)
            {
                _timer.Enabled = false;
                _timer.Stop();
            }
        }
    }


}