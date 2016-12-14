using BanlineaTest.Contacts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BanlineaTest.Contacts.Net
{
    public class ServiceDataProvider
    {
        public static async Task<bool> RegisterContact(RegisterRequest registerRequest)
        {
            ServiceClient client = new ServiceClient(NetResources.ServiceBaseUrl);
            RequestResult result = await client.Post(NetResources.RegisterContactUrl, registerRequest);
            return result.IsSuccessful;
        }

        public static async Task<List<Country>> GetCountriesList()
        {
            ServiceClient client = new ServiceClient(NetResources.ServiceBaseUrl);
            var result = await client.Get<Country[]>(NetResources.GetCountriesUrl);

            Debug.WriteLine($"We've just received {result.Data.Length} countries from the server" );

            return result.Data.ToList();
        }
    }
}
