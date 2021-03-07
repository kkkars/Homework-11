using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using static DepsTestApp.DepsTests;

namespace DepsTestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();

            var data = GetData();

            client.BaseAddress = new Uri(data.BaseAddress);

            await GetConvertedCurrencyTest(client);
            await RegisterTest(client);
        }

        static public Data GetData()
        {
            if (!File.Exists("data.json"))
            {
                throw new FileNotFoundException();
            }

            var json = File.ReadAllText("data.json");
            var data = JsonSerializer.Deserialize<Data>(json);

            if (data == null)
            {
                throw new ArgumentNullException();
            }

            return data;
        }
    }
}
