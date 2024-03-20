using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RobinOgMarius
{
    internal class QueryAllSuppliers

    {

        public static async Task QueryAllSuppliersAsync(string graphqlUrl, string token, int CCC)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var graphQLQuery = new
                {
                    query = $@"
                    {{
                      useCompany(no: {CCC}) {{
                        associate(filter: {{ supplierNo: {{ _gt: 0 }} }}) {{
                          items {{
                            externalId
                            supplierNo
                            companyNo
                            associateNo
                            name
                            addressLine1
                            addressLine2
                            addressLine3
                            addressLine4
                            postCode
                            postalArea
                            emailAddress
                            phone
                            paymentTermsForSupplier
                            paymentMethodForSupplier
                            mobilePhone
                            bankAccount
                            generalLedgerAccountNoForSupplier 
                            documentDeliveryMethod1 
                            crossAccountNoForSupplier 
                            countryNo 
                            languageNo 
                            currencyNo 
                            taxCodeForSupplier 
                            information1
                            information2
                            information3
                            information4
                            information5
                            information6
                            information7
                            information8
                            remittancePriority 
                            creditLimit 
                            createdDateTime 
                            createdByUser
                            changedDateTime 
                            changedByUser
                            swiftCode
                            swiftAddress1
                            swiftAddress2
                            swiftAddress3
                            swiftAddress4
                            iban
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
