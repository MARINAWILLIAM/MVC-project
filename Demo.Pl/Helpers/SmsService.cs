using Demo.DAL.Models;
using Demo.Pl.Settings;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Demo.Pl.Helpers
{
    public class SmsService : ISmsServices
    {
        private readonly TwilioSettings options;

        public SmsService(IOptions<TwilioSettings> options)
        {
            this.options = options.Value;
        }
        public MessageResource Send(SmsMessage sms)
        {
            TwilioClient.Init(options.AccountSID, options.AuthToken);
            var result = MessageResource.Create(
                body: sms.Body,
                from: new Twilio.Types.PhoneNumber(options.TwilioPhoneNumber),
                to: sms.phoneNumber
                );
            return result;
        }
    }
}
