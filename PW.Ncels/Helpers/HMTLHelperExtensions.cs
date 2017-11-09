using System;
using System.Web.Mvc;

namespace PW.Ncels.Helpers {
	public static class HMTLHelperExtensions {
		public static string IsSelected(this HtmlHelper html, string action = null, string controller = null,
			string cssClass = null) {

			if (String.IsNullOrEmpty(cssClass))
				cssClass = "active";

			string currentAction = (string) html.ViewContext.RouteData.Values["action"];
			string currentController = (string) html.ViewContext.RouteData.Values["controller"];

			if (String.IsNullOrEmpty(controller))
				controller = currentController;

			if (String.IsNullOrEmpty(action))
				action = currentAction;

			return controller == currentController && action == currentAction
				? cssClass
				: String.Empty;
		}

		public static string PageClass(this HtmlHelper html) {
			string currentAction = (string) html.ViewContext.RouteData.Values["action"];
			return currentAction;
		}

	}
}