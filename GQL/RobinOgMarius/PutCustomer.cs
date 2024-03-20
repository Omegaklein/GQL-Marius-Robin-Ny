using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RobinOgMarius
{
    internal class PutCustomer
    {
        public static async Task PutCustomerAsync(string graphqlUrl, string token, int CCC)
        {
            // Collect necessary information from user input
            Console.Write("Enter The Customer Number To Change The Customer Information: ");
            int CustomerNumber = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Customer Name: ");
            string Name = Console.ReadLine();

            Console.Write("Enter Customer Email: ");
            string Email = Console.ReadLine();

            Console.Write("Enter Customer Address: ");
            string AddressLine = Console.ReadLine();

            Console.Write("Enter Customer PostCode: ");
            string PostCode = Console.ReadLine();

            Console.Write("Enter Customer Postal Area: ");
            string PostalArea = Console.ReadLine();

            Console.Write("Enter Customer Phone Number: ");
            string PhoneNumber = Console.ReadLine();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var graphQLQuery = new
                {
                    query = @$"
                    mutation PutCustomer {{
                       useCompany(no: {CCC}) {{
                          associate_update(
                             filters : {{customerNo : {{_eq : {CustomerNumber}}}}},
                             values: [{{
                                name: ""{Name}"",
                                emailAddress: ""{Email}"",
                                postCode: ""{PostCode}"",
                                postalArea: ""{PostalArea}"",
                                addressLine1: ""{AddressLine}"",
                                phone: ""{PhoneNumber}""
                             }}]
                          ) {{
                             affectedRows
                             items {{
                                associateNo
                                customerNo
                                name
                                emailAddress
                                postCode
                                postalArea
                                addressLine1
                                phone
                             }}
                          }}
                       }}
                    }}"
                };

                var jsonContent = JsonConvert.SerializeObject(graphQLQuery);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(graphqlUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                var deserializedResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                var formattedResponse = JsonConvert.SerializeObject(deserializedResponse, Formatting.Indented);

                Console.WriteLine($"GraphQL Response: {formattedResponse}");
            }
        }
    }
}