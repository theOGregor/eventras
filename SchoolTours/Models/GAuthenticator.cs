﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolTours.Models;
using Google.Apis.Authentication;
using Google.Apis.Authentication.OAuth2;
using Google.Apis.Authentication.OAuth2.DotNetOpenAuth;

namespace SchoolTours.Models
{
    public class GAuthenticator
    {
        //private OAuth2Authenticator<NativeApplicationClient> _authenticator;

        //public GAuthenticator(OAuth2Authenticator<NativeApplicationClient> authenticator)
        //{
        //    _authenticator = authenticator;
        //}

        //internal IAuthenticator Authenticator
        //{
        //    get { return _authenticator; }
        //}

        //public bool IsValid
        //{
        //    get
        //    {
        //        return _authenticator != null &&
        //            DateTime.Compare(DateTime.Now.ToUniversalTime(), _authenticator.State.AccessTokenExpirationUtc.Value) < 0;
        //    }
        //}

        //public string RefreshToken
        //{
        //    get { return _authenticator.State.RefreshToken; }
        //}
    }
}