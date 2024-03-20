using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RobinOgMarius
{
    internal class PostProduct
    {
        public static async Task PostProductAsync(string graphqlUrl, string token, int CCC)
        {
            // Collect necessary information from user input
            Console.Write("Enter The Product Number: ");
            string ProductNumber = Console.ReadLine();

            Console.Write("Enter The Product Description: ");
            string ProductDescription = Console.ReadLine();

            Console.Write("Enter Tax And Accounting Group: ");
            string TaxAndAccountingGroup = Console.ReadLine();


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var graphQLQuery = new
                {
                    query = @$"
                    mutation CreateProduct {{
                       useCompany(no: {CCC}) {{
                          product_create(
                             values: [{{
                                productNo: ""{ProductNumber}"",
                                description: ""{ProductDescription}"",
                                taxAndAccountingGroup: {TaxAndAccountingGroup}
                             }}],
                             suggest: {{
                                taxAndAccountingGroup: true
                             }}
                          ) {{
                             affectedRows
                             items {{
                                productNo
                                description
                                taxAndAccountingGroup
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