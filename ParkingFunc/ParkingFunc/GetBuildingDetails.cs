using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ParkingFunc.Models;

namespace ParkingFunc
{
    public static class GetBuildingDetails
    {
        [FunctionName("GetBuildingDetails")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string id = req.Query["Id"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            if(string.IsNullOrWhiteSpace(id))
            {
                return new  BadRequestObjectResult("Please pass id value.");

            }

            DbConnect dbConnect = new DbConnect();
            BuildingResponse response= dbConnect.GetBuildingDetails(Convert.ToInt32(id));

            string responseMessage = "GetAvailableSlots function executed successfully.";

            if (response.IsSuccess)
            {
                response.ErrorMessage = responseMessage;
                return new OkObjectResult(response.Floors);
            }
            else
            {
                return new BadRequestObjectResult(response.ErrorMessage);
            }           
        }

        [FunctionName("GetAvailableSlots")]
        public static async Task<IActionResult> Run2(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string buildId = req.Query["buildId"];
            string floorId = req.Query["floorId"];

            if (string.IsNullOrWhiteSpace(buildId))
            {
                return new BadRequestObjectResult("Please pass buildId value.");
            }

            if (string.IsNullOrWhiteSpace(floorId))
            {
                return new BadRequestObjectResult("Please pass floorId value.");
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            DbConnect dbConnect = new DbConnect();
            SlotResponse response = dbConnect.GetAvailableSlots(Convert.ToInt32(buildId), Convert.ToInt32(floorId));

            string responseMessage = "GetAvailableSlots function executed successfully.";

            if (response.IsSuccess)
            {
                response.ErrorMessage= responseMessage;
                return new OkObjectResult(response.Slots);
            }
            else
            {
                return new BadRequestObjectResult(response.ErrorMessage);
            }
        }
    }
}
