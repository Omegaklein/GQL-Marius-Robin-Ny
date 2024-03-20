using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RobinOgMarius
{
    internal class PutSupplier
    {
        public static async Task PutSupplierAsync(string graphqlUrl, string token, int CCC)
        {
            // Collect necessary information from user input
            Console.Write("Enter The Supplier Number To Change The Supplier Information: ");
            int SupplierNumber = Convert.ToInt32(Console.ReadLine());

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
                    mutation PutSupplier {{
                       useCompany(no: {CCC}) {{
                          associate_update(
                             filters : {{supplierNo : {{_eq : {SupplierNumber}}}}},
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