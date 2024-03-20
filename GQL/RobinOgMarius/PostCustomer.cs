using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RobinOgMarius
{
    internal class PostCustomer
    {
        public static async Task PostCustomerAsync(string graphqlUrl, string token, int CCC)
        {
            // Collect necessary information from user input
            Console.Write("Enter Customers Name: ");
            string Name = Console.ReadLine();

            Console.Write("Enter Customers Email: ");
            string Email = Console.ReadLine();

            Console.Write("Enter Customers Postal Code: ");
            string PostCode = Console.ReadLine();

            Console.Write("Enter Customers Postal Area: ");
            string PostalArea = Console.ReadLine();

            Console.Write("Enter Customers Address Line: ");
            string AddressLine = Console.ReadLine();

            Console.Write("Enter Customers Phone Number: ");
            string PhoneNumber = Console.ReadLine();


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var graphQLQuery = new
                {
                    query = @$"
                    mutation CreateCustomer {{
                       useCompany(no: {CCC}) {{
                          associate_create(
                             values: [{{
                                name: ""{Name}"",
                                emailAddress: ""{Email}"",
                                postCode: ""{PostCode}"",
                                postalArea: ""{PostalArea}"",
                                addressLine1: ""{AddressLine}"",
                                phone: ""{PhoneNumber}""
                             }}],
                             suggest: {{
                                customerNo: {{
                                   from: 10000,
                                   to: 19000
                                }}
                             }}
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