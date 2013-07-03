using System;
using DotNetOpenAuth.OAuth.ChannelElements;

namespace EKContent.bus.ThirdParty.Twitter
{
    public class LongTermTokenProvider : TwitterBaseClass,  IConsumerTokenManager
    {
        public string ConsumerKey
        {
            get { return base.ConsumerKey; }
        }

        public string ConsumerSecret
        {
            get { return base.ConsumerSecret; }
        }


        public string GetTokenSecret(string token)
        {

            return Keys().ApplicationAuthorizationSecret;
        }

        public void ExpireRequestTokenAndStoreNewAccessToken(string consumerKey, string requestToken, string accessToken, string accessTokenSecret)
        {
            throw new NotImplementedException();
        }


        public TokenType GetTokenType(string token)
        {
            throw new NotImplementedException();
        }

        public void StoreNewRequestToken(DotNetOpenAuth.OAuth.Messages.UnauthorizedTokenRequest request, DotNetOpenAuth.OAuth.Messages.ITokenSecretContainingMessage response)
        {
            throw new NotImplementedException();
        }
    }
}