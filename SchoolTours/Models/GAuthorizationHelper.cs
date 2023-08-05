using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

using DotNetOpenAuth.OAuth2;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;
//using Google.Apis.Calendar.v3;
using Google.Apis.Util;
using System.Collections.Specialized;


namespace SchoolTours.Models
{
    public static class GAuthorizationHelper
    {
        private static string _clientId = ConfigurationManager.AppSettings["ClientId"];
        private static string _clientSecret = ConfigurationManager.AppSettings["ClientSecret"];
        private static string _redirectUri = ConfigurationManager.AppSettings["RedirectUri"];
        //private static string[] _scopes = new[] { CalendarService.Scopes.Calendar.GetStringValue() };
        //private static string[] _scopes = new[] { "https://www.googleapis.com/auth/calendar", "https://mail.google.com/" };
        private static string[] _scopes = new[] { "https://mail.google.com/" };

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

        //public static GAuthenticator GetAuthenticator(string authorizationCode)
        //{
        //    var client = new NativeApplicationClient(GoogleAuthenticationServer.Description, _clientId, _clientSecret);
        //    IAuthorizationState state = new AuthorizationState() { Callback = new Uri(_redirectUri) };
        //    state = client.ProcessUserAuthorization(authorizationCode, state);

        //    var auth = new OAuth2Authenticator<NativeApplicationClient>(client, (c) => state);
        //    auth.LoadAccessToken();

        //    return new GAuthenticator(auth);
        //}

        //public static GAuthenticator RefreshAuthenticator(string refreshToken)
        //{
        //    var state = new AuthorizationState(_scopes)
        //    {
        //        RefreshToken = refreshToken
        //    };

        //    var client = new NativeApplicationClient(GoogleAuthenticationServer.Description, _clientId, _clientSecret);
        //    bool result = client.RefreshToken(state);

        //    var auth = new OAuth2Authenticator<NativeApplicationClient>(client, (c) => state);
        //    auth.LoadAccessToken();

        //    return new GAuthenticator(auth);
        //}
    }
}