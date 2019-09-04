﻿using System;
using System.Net.Http.Headers;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace WebApi.Auth
{
    public static class HttpAuthChallengeContextExtension
    {
        public static void ChallengeWith(this HttpAuthenticationChallengeContext context, string scheme)
        {
            ChallengeWith(context, new AuthenticationHeaderValue(scheme));
        }

        public static void ChallengeWith(this HttpAuthenticationChallengeContext context, string scheme, string parameter)
        {
            ChallengeWith(context, new AuthenticationHeaderValue(scheme, parameter));
        }

        public static void ChallengeWith(this HttpAuthenticationChallengeContext context, AuthenticationHeaderValue challenge)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Result = new UnauthorizedResult(challenge, context.Result);
        }
    }
}