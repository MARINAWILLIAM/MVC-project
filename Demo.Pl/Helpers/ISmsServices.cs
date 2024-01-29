using Demo.DAL.Models;
using Twilio.Rest.Api.V2010.Account;

namespace Demo.Pl.Helpers
{
    public interface ISmsServices
    {
        MessageResource Send(SmsMessage sms);
    }
}
