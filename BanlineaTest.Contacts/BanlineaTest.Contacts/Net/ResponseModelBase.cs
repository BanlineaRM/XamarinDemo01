using System.Net;

namespace BanlineaTest.Contacts.Net
{
    public class ResponseModelBase
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string HttpStatusMessage { get; set; }
    }
}