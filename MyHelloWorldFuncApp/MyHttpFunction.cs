using Microsoft.Azure.Functions.Worker;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyHelloWorldFuncApp
{
    public class MyHttpFunction
    {

        [Function("MyHttpFunction")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            return new OkObjectResult("Welcome to Azure Functions");
        }
    }
}
