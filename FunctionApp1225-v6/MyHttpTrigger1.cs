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
using System.Collections.Generic;

namespace FunctionApp1225_v6
{
    public static class MyHttpTrigger1
    {


        private static List<Player> players = new List<Player>
        {
            new Player { Id = 1, Name = "Elchin", Surname = "Guliyev" ,Score=100,TeamId=1},
            new Player { Id = 2, Name = "Amin", Surname = "Atakishiyev" ,Score=90,TeamId=2},
            new Player { Id = 3, Name = "Murad", Surname = "Azizov" ,Score=80,TeamId=3},
        };
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

        [FunctionName("MyHttpTrigger4")]
        public static async Task<IActionResult> DeletePlayerById(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Players/Delete/{id}")] HttpRequest req,
        ILogger log, int id)
        {
            // Add your logic to delete the player with the given ID
            return new OkObjectResult($"Player with ID {id} has been deleted.");
        }
    }
}
