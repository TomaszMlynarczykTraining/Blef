using Microsoft.AspNetCore.Mvc;

namespace Blef.Bootstrapper.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public object Get()
    {
        return new
        {
            Aplication = "Blef",
            ApiSpecification = "/swagger/index.html",
            Repository = "https://github.com/ArturWincenciak/Blef",
            DockerHub = "https://hub.docker.com/repository/docker/teovincent/blef",
            RequestTime = DateTime.UtcNow,
        };
    }
}