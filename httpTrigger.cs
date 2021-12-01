using System;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace backend.Function
{
    public static class httpTrigger
    {

        [FunctionName("httpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            HttpClient client = new HttpClient(); 
            log.LogInformation("C# HTTP trigger function received a request.");

            //extract url from HTTP request
            string url = req.Query["url"];
            
            //blank string
            string response = "";

            //if there is no URL, return a string
            if (url == null) {
                response = "Backend. Please add a url query. Eg. /api/httpTrigger?url=https://xkcd.com/info.0.json";
            } else {
                response = await client.GetStringAsync(url);
            }

            //checks response on console
            //Console.WriteLine(response);

            return new OkObjectResult(response);
        }
    }
}
