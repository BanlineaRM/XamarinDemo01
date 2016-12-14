using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanlineaTest.Contacts.Net
{
    public class RequestParameters : List<KeyValuePair<string, object>>
    {
        #region Methods

        public RequestParameters Add(string key, object value) {
            var item = new KeyValuePair<string, object>(key, value);
            this.Add(item);

            return this;
        }

        public string ToQueryString() {
            if (this.Count == 0)
                return string.Empty;

            var temp = this.Select(w => string.Format("{w.Key}={w.Value}"));
            var result = string.Join("&", temp.ToArray());

            return result;
        }

        #endregion
    }
}