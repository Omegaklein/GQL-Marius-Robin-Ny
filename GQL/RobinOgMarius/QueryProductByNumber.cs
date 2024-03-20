using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RobinOgMarius
{
    internal class QueryProductByNumber
    {
        public static async Task QueryProductByNumberAsync(string graphqlUrl, string token, int CCC)
        {
            // Read supplierName from user input (example: 101)
            Console.Write("Enter Product Number: ");
            string productNo = Console.ReadLine();


            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var graphQLQuery = new
                {
                    query = $@"
                    {{
                      useCompany(no: 3542724) {{
                      useCompany({CCC}) {{
                        product(filter: {{ productNo: {{ _like: ""{productNo}"" }} }}) 
                        {{
                          items {{
                            description
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
