using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RobinOgMarius;

namespace RobinOgMarius
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string tokenUrl = "https://connect.visma.com/connect/token";
            string clientId = "isv_VBNxtTest";
            string clientSecret = "khLz8JnCD67vyRtZMohhclwZznPQ9LyOw1FUHXEGH6lhd7Lmnac26sm0gPW97vgN";
            string graphqlUrl = "https://business.visma.net/api/graphql-service";
            int CCC = 5073085;
            int suppliernumber = 51007;

            var token = await GetAccessToken(tokenUrl, clientId, clientSecret);
            Console.WriteLine($"Access Token: {token}");

            if (!string.IsNullOrEmpty(token))
            {
                while (true)
                {
                    Console.WriteLine("Choose an action to execute:");
                    Console.WriteLine("1. Get All Suppliers");
                    Console.WriteLine("2. Get Supplier by number");
                    Console.WriteLine("3. Get Supplier by name");
                    Console.WriteLine("4. Get Customer by number");
                    Console.WriteLine("5. Get Customer by name");
                    Console.WriteLine("6. Get Product by number");
                    Console.WriteLine("7. Get Product by name");
                    Console.WriteLine("8. Post Supplier");
                    Console.WriteLine("9. Post Customer");
                    Console.WriteLine("10. Post Product");
                    Console.WriteLine("11. Update Supplier");
                    Console.WriteLine("12. Update Customer");
                    Console.WriteLine("13. Update Product");
                    Console.WriteLine("0. Exit");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            await QueryAllSuppliers.QueryAllSuppliersAsync(graphqlUrl, token, CCC);
                            break;

                        case "2":
                            await QuerySupplierByNumber.QuerySupplierByNumberAsync(graphqlUrl, token, CCC);
                            break;

                        case "3":
                            await QuerySupplierByName.QuerySupplierByNameAsync(graphqlUrl, token, CCC);
                            break;

                        case "4":
                            await QueryCustomerByNumber.QueryCustomerByNumberAsync(graphqlUrl, token, CCC);
                            break;

                        case "5":
                            await QueryCustomerByName.QueryCustomerByNameAsync(graphqlUrl, token, CCC);
                            break;

                        case "6":
                            await QueryProductByNumber.QueryProductByNumberAsync(graphqlUrl, token, CCC);
                            break;

                        case "7":
                            await QueryProductByName.QueryProductByNameAsync(graphqlUrl, token, CCC);
                            break;

                        case "8":
                            await PostSupplier.PostSupplierAsync(graphqlUrl, token, CCC);
                            break;

                        case "9":
                            await PostCustomer.PostCustomerAsync(graphqlUrl, token, CCC);
                            break;

                        case "10":
                            await PostProduct.PostProductAsync(graphqlUrl, token, CCC);
                            break;

                        case "11":
                            await PutSupplier.PutSupplierAsync(graphqlUrl, token, CCC);
                            break;

                        case "12":
                            await PutCustomer.PutCustomerAsync(graphqlUrl, token, CCC);
                            break;

                        case "13":
                            await PutProduct.PutProductAsync(graphqlUrl, token, CCC);
                            break;

                        case "0":
                            Console.WriteLine("Exiting the program.");
                            return;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Failed to fetch token.");
            }
        }

        static async Task<string> GetAccessToken(string tokenUrl, string clientId, string clientSecret)
        {
            using (var httpClient = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                });

                var response = await httpClient.PostAsync(tokenUrl, content);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                return tokenResponse.access_token;
            }
        }
    }
}
