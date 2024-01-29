using Demo.DAL.Models;

namespace Demo.Pl.Helpers
{
    public interface IEmailSettings
    {
        public void SendEmail(Email email);

    }
}
