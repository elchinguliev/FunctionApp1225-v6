using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionApp1225_v6.Models;

namespace FunctionApp1225_v6
{
    public static class MyHttpTrigger1
    {
        [FunctionName("MyHttpTrigger1")]
        public static async Task<IActionResult> GetPlayerById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "Players/{id}")] HttpRequest req,
            ILogger log,int id )
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? $" ID {id} This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("MyHttpTrigger2")]
        public static async Task<IActionResult> GetPlayers(
           [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "Players")] HttpRequest req,
           ILogger log)
        {
            //log.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";
            string id = req.Query["id"];
            return new OkObjectResult(id);
        }

        [FunctionName("MyHttpTrigger3")]
        public static async Task<IActionResult> AddPlayer(
         [HttpTrigger(AuthorizationLevel.Function, "post", Route = "Players/Add")] HttpRequest req,
         ILogger log)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Player player = JsonConvert.DeserializeObject<Player>(requestBody);
            return new OkObjectResult(player);
        }
    }
}
