using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace PickPoint.RestApi.Controllers;

[OpenApiIgnore]
[Route("/")]
public class HomeController : ControllerBase
{
    [HttpGet]
    [ProducesDefaultResponseType]
    public ActionResult Index()
    {
        return Redirect("/swagger");
    }
}