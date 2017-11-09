using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PW.Photon.Web.Helpers {
	public class AccessDeniedAuthorizeAttribute : AuthorizeAttribute {
		public override void OnAuthorization(AuthorizationContext filterContext) {
			base.OnAuthorization(filterContext);

			if (filterContext.Result is HttpUnauthorizedResult) {
				filterContext.Result = new RedirectResult("~/Error/Unauthorized");
			}
		}
	}
}