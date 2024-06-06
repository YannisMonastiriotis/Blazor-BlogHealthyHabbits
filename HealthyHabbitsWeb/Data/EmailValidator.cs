namespace HealthyHabbitsWeb.Data
{
    using System;
    using System.Net;
    using System.Net.Mail;
    using System.Net.Sockets;
    using System.Reflection;

    public class EmailValidator
    {
        public bool HasValidMxRecords(string email)
        {
            try
            {
                var domain = email.Split('@')[1];
                var ips = Dns.GetHostAddresses(domain);
                foreach (var ip in ips)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork || ip.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
               
            }
            return false;
        }
      
    }
    public class EmailService
    {
      
    }
}
