using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

using System.Linq;
using System.Web;
using PW.Ncels.Database.Helpers;
using EntityState = System.Data.Entity.EntityState;

namespace PW.Ncels.Database.DataModel {
	public partial class ncelsEntities
	{

		public ncelsEntities(string cn)
		{
			Database.Connection.ConnectionString = cn; 
		}
		public override int SaveChanges()
		{

			ChangeTracker.DetectChanges(); // Important!

			ObjectContext ctx = ((IObjectContextAdapter)this).ObjectContext;

			List<ObjectStateEntry> objectStateEntryList =
				ctx.ObjectStateManager.GetObjectStateEntries(EntityState.Added
														   | EntityState.Modified
														   | EntityState.Deleted)
				.ToList();

			foreach (ObjectStateEntry entry in objectStateEntryList) {
				if (!entry.IsRelationship) {
					switch (entry.State) {
						case EntityState.Added:
							if (entry.Entity is Document) {
								Document item = (Document)entry.Entity;
								item.ModifiedDate = DateTime.Now;
								item.CreatedDate = DateTime.Now;
								if (item.SortNumber == 0) {
									item.SortNumber = 99999;
								}
								item.ModifiedUser = UserHelper.GetCurrentName();
								item.OwnerId = UserHelper.GetCurrentEmployee().Id.ToString();
								item.OwnerValue= UserHelper.GetCurrentEmployee().DisplayName.ToString();
								item.Ip = HttpContext.Current.Request.UserHostAddress;
								item.DisplayName = BuildDisplayName(item);
								if (item.OrganizationId == Guid.Empty)
								{
									item.OrganizationId = UserHelper.GetCurrentEmployee().OrganizationId;
								}
							}
							if (entry.Entity is Activity) {
								Activity item = (Activity)entry.Entity;
								item.ModifiedDate = DateTime.Now;
								item.CreatedDate = DateTime.Now;
								item.ModifiedUser = UserHelper.GetCurrentName();
								item.Ip = HttpContext.Current.Request.UserHostAddress;
							}

							if (entry.Entity is Task) {
								Task item = (Task)entry.Entity;
								item.ModifiedDate = DateTime.Now;
								item.CreatedDate = DateTime.Now;
								item.ModifiedUser = UserHelper.GetCurrentName();
								item.Ip = HttpContext.Current.Request.UserHostAddress;
							}
							if (entry.Entity is Report) {
								Report item = (Report)entry.Entity;
								item.ModifiedDate = DateTime.Now;
								item.ModifiedUser = UserHelper.GetCurrentName();
								item.Ip = HttpContext.Current.Request.UserHostAddress;
							}
							if (entry.Entity is ExtensionExecution) {
								ExtensionExecution item = (ExtensionExecution)entry.Entity;
								item.CreatedDate = DateTime.Now;
								item.ModifiedDate = DateTime.Now;
								item.AutorId = UserHelper.GetCurrentEmployee().Id.ToString();
								item.AutorValue = UserHelper.GetCurrentEmployee().DisplayName.ToString();
	
							}

                            if (entry.Entity is PriceProject) {
                                PriceProject item = (PriceProject)entry.Entity;
                                item.CreatedDate = DateTime.Now;
                                var employee = UserHelper.GetCurrentEmployee();
                                if(employee != null)
                                    item.OwnerId = employee.Id;

                            }
                            if (entry.Entity is RegisterProject) {
                                RegisterProject item = (RegisterProject)entry.Entity;
                                item.CreatedDate = DateTime.Now;
                                item.OwnerId = UserHelper.GetCurrentEmployee().Id;
                            }
                            // write log...
                            break;
						case EntityState.Deleted:
							// write log...
							break;
						case EntityState.Modified: {
								if (entry.Entity is Document) {
									Document item = (Document)entry.Entity;
									item.ModifiedDate = DateTime.Now;
									item.ModifiedUser = UserHelper.GetCurrentName();
									item.Ip = HttpContext.Current.Request.UserHostAddress;
									item.DisplayName = BuildDisplayName(item);
								}
								if (entry.Entity is Activity) {
									Activity item = (Activity)entry.Entity;
									item.ModifiedDate = DateTime.Now;
									item.ModifiedUser = UserHelper.GetCurrentName();
									item.Ip = HttpContext.Current.Request.UserHostAddress;
								}

								if (entry.Entity is Task) {
									Task item = (Task)entry.Entity;
									item.ModifiedDate = DateTime.Now;
									item.ModifiedUser = UserHelper.GetCurrentName();
									item.Ip = HttpContext.Current.Request.UserHostAddress;
								}
								if (entry.Entity is Report) {
									Report item = (Report)entry.Entity;
									item.ModifiedDate = DateTime.Now;
									item.ModifiedUser = UserHelper.GetCurrentName();
									item.Ip = HttpContext.Current.Request.UserHostAddress;
								}
                                if (entry.Entity is PriceProject) {
                                    PriceProject item = (PriceProject)entry.Entity;
                                    item.CreatedDate = DateTime.Now;

                                }
                                if (entry.Entity is RegisterProject) {
                                    RegisterProject item = (RegisterProject)entry.Entity;
                                    item.CreatedDate = DateTime.Now;

                                }
                                break;
							}
					}
				}
			}
			//foreach (ObjectStateEntry entry in this.GetObjectStateEntries(EntityState.Added | EntityState.Modified)) {
			//	// Validate the objects in the Added and Modified state
			//	// if the validation fails throw an exeption.
			//}
			try {
				return base.SaveChanges();
			}
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
		private string BuildDisplayName(Document document) {
			string number = "Б/Н";
			string date = string.Empty;

			if (!string.IsNullOrEmpty(document.Number))
				number = document.Number;

			if (document.DocumentDate != null)
				date = string.Format("{0:dd.MM.yyyy}", document.DocumentDate);

			return string.Format("{0} от {1}", number, date);
		}

	}


}