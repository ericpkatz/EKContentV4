using System;
using System.Collections.Generic;
using System.IO;

using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using EKContent.bus.Entities;
using EKContent.bus.ThirdParty.Twitter;
using Newtonsoft.Json.Linq;

namespace EKContent.bus.Concrete
{
    public class TweetProvider 
    {

        private ServiceProviderDescription ServiceProviderDescription()
        {

            return new ServiceProviderDescription

            {
                AccessTokenEndpoint = new MessageReceivingEndpoint("     https://api.twitter.com/oauth/access_token", HttpDeliveryMethods.PostRequest),
                RequestTokenEndpoint = new MessageReceivingEndpoint("     https://api.twitter.com/oauth/request_token", HttpDeliveryMethods.PostRequest),
                UserAuthorizationEndpoint = new MessageReceivingEndpoint("https://api.twitter.com/oauth/authorize", HttpDeliveryMethods.PostRequest),
                TamperProtectionElements = new ITamperProtectionChannelBindingElement[] { new HmacSha1SigningBindingElement() },
                ProtocolVersion = ProtocolVersion.V10a
            };

        }

        public Newtonsoft.Json.Linq.JArray Tweets(TwitterKeys keys)
        {

            var cache = System.Web.HttpContext.Current.Cache;
            JArray tweets = (JArray)cache["Tweets"];

            if (tweets == null)
            {

                var token = keys.ApplicationAuthorizationKey;
                try
                {

                    var consumer = new DotNetOpenAuth.OAuth.WebConsumer(ServiceProviderDescription(),
                                                                        new LongTermTokenProvider());

                    var endpoint = new MessageReceivingEndpoint("https://api.twitter.com/1.1/statuses/home_timeline.json",
                                                                HttpDeliveryMethods.GetRequest);

                    var request = consumer.PrepareAuthorizedRequest(endpoint, token);

                    var response = request.GetResponse();
                    var data = (new StreamReader(response.GetResponseStream())).ReadToEnd();

                    tweets = JArray.Parse(data);
                    cache.Insert("Tweets", tweets, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero);
                }
                catch (Exception ex)
                {
                    tweets = new JArray();
                }
            }

            return tweets;
        }
    }
}