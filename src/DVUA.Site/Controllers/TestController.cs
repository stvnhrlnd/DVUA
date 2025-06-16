using Microsoft.AspNetCore.Mvc;

namespace DVUA.Site.Controllers;

#if DEBUG
[ApiController]
[Route("/api/test")]
public class TestController : Controller
{
    [HttpGet]
    public string Get()
    {
        return "test";
    }
}
#endif
