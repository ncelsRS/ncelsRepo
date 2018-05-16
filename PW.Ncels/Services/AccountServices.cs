using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using PW.Ncels.Models;
using PW.Ncels.Database.DataModel;

namespace PW.Ncels.Services {
	public class AccountServices {
		private static AccountServices _instance;

		public static AccountServices Instance {
			get {
				if (_instance == null) {
					_instance = new AccountServices();
				}
				return _instance;
			}
		}

		public MessageModel CreateUser(string email, string password) {
			//string password = Membership.GeneratePassword(8, 2);
			//string password = "123456";
			if (email != null && Membership.GetUser(email) == null) {
				try {
					Membership.CreateUser(email, password);
					return new MessageModel() {IsError = false, Message = password};
				}
				catch (Exception e) {
					return new MessageModel() {IsError = true, Message = e.Message};
				}

			}
			return new MessageModel() {IsError = true, Message = "Пользователь с таким логином уже есть!"};
		}




		public MessageModel PostRequest(ncelsEntities context, UnitModel request, int personType) {

			Unit org = null;
			string login = null;
			string name = null;

	

			try {
                switch (personType)
                {
                    // Рез ФЛ
                    case 0:
                        login = request.Iin;
                        Guid id = Guid.Parse("de593b72-44a7-439e-a422-000000000000");
                        org = context.Units.First(x => x.Id == id);
                        name = $"{request.LastName} {request.FirstName} {request.MiddleName}";
                        break;
                    // Рез ЮЛ
                    case 1:
                        login = request.Iin;
                        id = Guid.Parse("de593b72-44a7-439e-a422-000000000001");
                        org = context.Units.First(x => x.Id == id);
                        name = request.Name;
                        break;
                    // Ино ФЛ
                    case 2:
                        login = request.Iin;//request.Email;
                        id = Guid.Parse("de593b72-44a7-439e-a422-000000000002");
                        org = context.Units.First(x => x.Id == id);
                        break;
                    // Ино ЮЛ
                    case 3:
                        login = request.Iin;//request.Email;
                        id = Guid.Parse("de593b72-44a7-439e-a422-000000000003");
                        org = context.Units.First(x => x.Id == id);
                        name = request.Name;
                        break;
                }

                var unit = new Unit() {
					Id = Guid.NewGuid(),
					CreatedDate = DateTime.Now,
					ModifiedDate = DateTime.Now,
					Type = 2,
					PositionState = 0,
					PositionStaff = 0,
					Rank = 0,
					Name = name,
					DisplayName = name,
                    Bin = request.Bin,
					ParentId = org.Id
				};

				context.Units.Add(unit);

				context.SaveChanges();

				var employee = new Employee() {
					Id = Guid.NewGuid(),
					LastName = request.LastName,
					FirstName = request.FirstName,
					MiddleName = request.MiddleName,
					DisplayName = $"{request.LastName} {request.FirstName} {request.MiddleName}",
					CreatedDate = DateTime.Now,
					ModifiedDate = DateTime.Now,
					Email = request.Email,
					Login = login,
					Sex = 0,
					Iin = request.Iin,
					TimeAgreement = 0,
					FamilyStatus = 0,
					ExperienceTotal = 0,
					ExperienceSpec = 0,
					IsDegree = false,
					IsGuide = false,
					MilitaryType = 0,
					OrganizationId = unit.Id,
					PositionId = unit.Id,
					IsAcademic = false
				};
				context.Employees.Add(employee);
				context.SaveChanges();

				return new MessageModel() {Message = "Запрос на регистрацию отправлен!", IsError = false};
			}
			catch (Exception ex) {
				return new MessageModel() {Message = ex.Message, IsError = true};
			}
		}
	}
}