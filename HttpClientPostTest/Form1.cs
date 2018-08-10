using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpClientPostTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            /* var replyString = SendTelegramMessage("testing post");
            string rs = ConvertUnicodeToAscii(replyString.ToString());

            Console.WriteLine("from HERE> " + rs);

            nop();
            */

            List<String> contentList = new List<string>();

            contentList = MakeStringList();
            CancellationToken ct = new CancellationToken();
            await PostBasicAsync(contentList, ct);


        }

        public List<String> MakeStringList()
        {

            List<String> returnList = new List<string>();

            returnList.Add("Jim");
            returnList.Add("Celia");
            returnList.Add("Toby");
            
            return returnList;
        }
        public async Task<string> SendTelegramMessage(string text)
        {
            using (var client = new HttpClient())
            {

                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("PARAM1", "VALUE1");
                dictionary.Add("PARAM2", text);

                string json = JsonConvert.SerializeObject(dictionary);
                var requestData = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(String.Format("http://localhost:7071/api/DirtyRCV?name=XXX"), requestData);

                
                var result = await response.Content.ReadAsStringAsync();
                
                return result;
            }
        }
        private async Task PostBasicAsync(object content, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:7071/api/DirtyRCV"))
            {
                var json = JsonConvert.SerializeObject(content);
                using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    var response = await client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                        .ConfigureAwait(false);
                    {
                        string ostring = await response.Content.ReadAsStringAsync();

                        
                        Console.WriteLine(ostring);
                        var statusCode = response.StatusCode;

                    }


                }
            }
          
        }
        public void nop()
        {

        return;

        }

        public string ConvertUnicodeToAscii(System.String unicodeString)
        {
            
            {
                

                // Create two different encodings.
                Encoding ascii = Encoding.ASCII;
                Encoding unicode = Encoding.Unicode;

                // Convert the string into a byte array.
                byte[] unicodeBytes = unicode.GetBytes(unicodeString);

                // Perform the conversion from one encoding to the other.
                byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

                // Convert the new byte[] into a char[] and then into a string.
                char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
                ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
                string asciiString = new string(asciiChars);

                return asciiString;
            }

        }


    }
}
