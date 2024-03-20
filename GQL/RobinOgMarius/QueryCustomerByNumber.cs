using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobinOgMarius
{
    internal class QueryCustomerByNumber
    {
        public static async Task QueryCustomerByNumberAsync(string graphqlUrl, string token, int CCC)
        {
            // Read supplierNo from user input (example: 10005)
            Console.Write("Enter Customer Number: ");
            string customerNumber = Console.ReadLine();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var graphQLQuery = new
                {
                    query = $@"
                    {{
                      useCompany(no: {CCC}) {{
                        associate(filter: {{ customerNo: {{ _eq: {customerNumber} }}}}) 
                        {{
                          items {{
                            customerNo
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

