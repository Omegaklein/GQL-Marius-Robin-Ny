using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RobinOgMarius
{
    internal class PostSupplier
    {
        public static async Task PostSupplierAsync(string graphqlUrl, string token, int CCC)
        {
            // Collect necessary information from user input
            Console.Write("Enter Suppliers Name: ");
            string Name = Console.ReadLine();

            Console.Write("Enter Suppliers Email: ");
            string Email = Console.ReadLine();

            Console.Write("Enter Suppliers Address: ");
            string AddressLine = Console.ReadLine();

            Console.Write("Enter Suppliers PostCode: ");
            string PostCode = Console.ReadLine();

            Console.Write("Enter Suppliers Postal Area: ");
            string PostalArea = Console.ReadLine();

            Console.Write("Enter Suppliers Phone Number: ");
            string PhoneNumber = Console.ReadLine();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var graphQLQuery = new
                {
                    query = @$"
                    mutation CreateSupplier {{
                       useCompany(no: {CCC}) {{
                          associate_create(
                             values: [{{
                                name: ""{Name}"",
                                emailAddress: ""{Email}"",
                                postCode: ""{PostalArea}"",
                                postalArea: ""{PostalArea}"",
                                addressLine1: ""{AddressLine}"",
                                phone: ""{PhoneNumber}""
                             }}],
                             suggest: {{
                                supplierNo: {{
                                   from: 50000,
                                   to: 59000
                                }}
                             }}
                          ) {{
                             affectedRows
                             items {{
                                associateNo
                                supplierNo
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