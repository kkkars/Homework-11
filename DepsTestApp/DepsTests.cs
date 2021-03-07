using DepsTestApp.Contracts;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace DepsTestApp
{
    static class DepsTests
    {
        private const string registerPath = "register";

        public static async Task RegisterTest(HttpClient client)
        {
            Console.WriteLine("Test: RegisterTest\n");

            var uri = client.BaseAddress + registerPath;

            var loginData = new LoginDataDto
            {
                Login = "lucky_spirit002",
                Password = "forget_the_fear"
            };

            var response = await client.PostAsync(uri, loginData, new JsonMediaTypeFormatter());

            var expectedContent = "{\"statusCode\":100,\"message\":\"System exception: something went wrong at the server side\"}";
            var actualContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"  Request: {{{registerPath}}}\n  Expected-> Status code: {{200}}\n           Content: {expectedContent}\n  Actual-> Status code: {{{(int)response.StatusCode}}}\n           Content: {actualContent}\n  Success: {(int)response.StatusCode == 200 && expectedContent == actualContent}");
             
            Console.WriteLine();
        }

        public static async Task GetConvertedCurrencyTest(HttpClient client)
        {
            Console.WriteLine("Test: GetConvertedCurrency\n");

            Console.WriteLine("[SameCurrencyCase(WithDefaultAmount)]");
            await CurrencyConvertTest(client, "Rates/EUR/EUR", "1");

            Console.WriteLine("[DifferentCurrencyCase(WithDefaultAmount)]");
            await CurrencyConvertTest(client, "Rates/EUR/UAH", "33.075");

            Console.WriteLine("[DifferentCurrencyCase(WithSettedAmount)]");
            await CurrencyConvertTest(client, "Rates/EUR/UAH?amount=1000", "33075.000");

            Console.WriteLine("[NonExistingCurrencyCase(WithDefaultAmount)]");
            await CurrencyConvertTest(client, "Rates/asR/lll", "Invalid currency code", 400);
        }

        private static async Task CurrencyConvertTest(HttpClient client, string requestUri, string expectedContent, int expectedStatusCode = 200)
        {
            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, client.BaseAddress + requestUri));

            var actualContent = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"  Request: {{/{requestUri}}}\n  Expected-> Status code: {{{expectedStatusCode}}}\n             Content:{expectedContent}\n  Actual-> Status code: {{{(int)response.StatusCode}}}\n           Content: {actualContent}\n  Success: {(int)response.StatusCode == expectedStatusCode && expectedContent == actualContent}");

            Console.WriteLine();
        }
    }
}
