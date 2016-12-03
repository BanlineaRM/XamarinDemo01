using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BanlineaTest.Contacts.Net
{
    public class RequestResult : RequestResult<object>
    {
        public RequestResult(HttpResponseMessage response) : base(response) {
        }
    }

    public class RequestResult<TResponseType>
    {
        public RequestResult(HttpResponseMessage response) {
            this.IsSuccessful = response.IsSuccessStatusCode;
            this.StatusCode = response.StatusCode;
            this.Message = string.Empty;
            this.Data = default(TResponseType);
        }

        public System.Net.HttpStatusCode StatusCode { get; set; }

        public bool IsSuccessful { get; set; }

        public string Message { get; set; }

        public TResponseType Data { get; set; }
    }
}