using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//using Google.Apis.Auth.OAuth2;
//using Google.Apis.Books.v1;
//using Google.Apis.Books.v1.Data;
//using Google.Apis.Services;
//using Google.Apis.Util.Store;



using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2;

////using Google.Apis.Authentication.OAuth2;
////using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
////using Google.Apis.Calendar.v3;
//using Google.Apis.Util;
//using Google.Apis.Authentication.OAuth2;
//using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;

namespace SchoolTours
{
    public partial class GoogleAuth : System.Web.UI.Page
    {
        private static string _clientId = ConfigurationManager.AppSettings["ClientId"];
        private static string _clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
        private static string _redirectUri = ConfigurationManager.AppSettings["RedirectUri"];

        private static string[] _scopes = new[] { "https://www.googleapis.com/auth/calendar", "https://mail.google.com/" };


        protected void Page_Load(object sender, EventArgs e)
        {
            //GetAuthorizationUrl("itsabacus1@gmail.com");
        }


        //public static string GetAuthorizationUrl(string emailAddress)
        //{
        //    var provider = new NativeApplicationClient(GoogleAuthenticationServer.Description, _clientId, _clientSecret);
        //    IAuthorizationState authorizationState = new AuthorizationState(_scopes);
        //    authorizationState.Callback = new Uri(_redirectUri);

        //    UriBuilder builder = new UriBuilder(provider.RequestUserAuthorization(authorizationState));
        //    NameValueCollection queryParameters = HttpUtility.ParseQueryString(builder.Query);

        //    queryParameters.Set("access_type", "offline");
        //    queryParameters.Set("approval_prompt", "force");
        //    queryParameters.Set("user_id", emailAddress);

        //    builder.Query = queryParameters.ToString();
        //    return builder.Uri.ToString();
        //}

        //private GAuthenticator GetAuthenticator()
        //{
        //    var authenticator = (GAuthenticator)Session["authenticator"];
        //    if (authenticator == null || !authenticator.IsValid)
        //    {
        //        // Get a new Authenticator using the Refresh Token            
        //        authenticator = GAuthorizationHelper.RefreshAuthenticator(refreshTokenID);
        //        Session["authenticator"] = authenticator;
        //    }
        //    return authenticator;
        //}



    }
}