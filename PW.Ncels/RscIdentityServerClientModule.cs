using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Web;

namespace PW.Ncels
{
    public class RscIdentityServerClientModule : IHttpModule
    {
        public void Dispose() { }

        public void Init(HttpApplication app)
        {
            app.BeginRequest += (source, args) =>
            {
                HttpApplication application = (HttpApplication)source;
                HttpContext context = application.Context;

                var urlParam = context.Request.Params["AuthData"];
                if (urlParam != null)
                {
                    if (context.Request.Cookies["authData"] != null)
                        context.Request.Cookies.Remove("authData");
                    context.Request.Cookies.Add(new HttpCookie("authData", urlParam));
                }

                var filename = "./identity.crt";
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
                var cert = new X509Certificate2(path);

                var cookieAuth = context.Request.Cookies["authData"];
                if (cookieAuth == null) return;

                VerifyToken(cookieAuth.Value, cert);
            };

            app.EndRequest += (source, args) =>
            {
                HttpApplication application = (HttpApplication)source;
                HttpContext context = application.Context;

                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Redirect;

                    var identitySrvUrl = ConfigurationManager.AppSettings["IdSrvUrl"];
                    var idSrvClientId = ConfigurationManager.AppSettings["IdSrvClientId"];
                    var idSrvClientSecret = ConfigurationManager.AppSettings["IdSrvClientSecret"];

                    var returnUrl = context.Request.Url;
                    var returnUrlParam = $"{returnUrl.Scheme}://{returnUrl.Authority}{returnUrl.AbsolutePath}";
                    var query = HttpUtility.ParseQueryString(returnUrl.Query);
                    if (query["authData"] != null)
                        query.Remove("authData");
                    if (query.Count > 0)
                        returnUrlParam += "?" + query;
                    query = HttpUtility.ParseQueryString("");
                    query.Add("returnUrl", returnUrlParam);
                    query.Add("clientId", idSrvClientId);
                    query.Add("clientSecret", idSrvClientSecret);

                    context.Response.Redirect(identitySrvUrl + "?" + query);
                }
            };
        }

        private IPrincipal VerifyToken(string authData, X509Certificate2 cert)
        {
            var auth = JsonConvert.DeserializeObject<AuthData>(authData);

            var jwt = new JwtSecurityTokenHandler();
            var token = jwt.ReadToken(auth.AccessToken);
            var validateParams = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new X509SecurityKey(cert)
            };
            try
            {
                var res = jwt.ValidateToken(auth.AccessToken, validateParams, out token);
            }
            catch (SecurityTokenSignatureKeyNotFoundException)
            {
                return null;
            }
            return null;
        }
    }

    public class AuthData
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }
    }

}