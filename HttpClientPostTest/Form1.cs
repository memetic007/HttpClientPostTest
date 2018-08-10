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
       
        private async Task PostBasicAsync(object content, CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, "https://yikesyikesyikes.azurewebsites.net/api/DirtyRCV"))
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

       


    }
}
