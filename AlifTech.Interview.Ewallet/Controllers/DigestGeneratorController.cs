using AlifTech.Interview.Ewallet.Models;
using AlifTech.Interview.Ewallet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AlifTech.Interview.Ewallet.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class DigestGeneratorController : ControllerBase
{
    /// <summary>
    /// This method is used to generate digest for user for testing purposes.
    /// </summary>
    /// <param name="userService"></param>
    /// <param name="digestGenerator"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("generate")]
    [AllowAnonymous]
    public async Task<IActionResult> GenerateDigestAsync(
        [FromServices] IUserService userService,
        [FromServices] IDigestGenerator digestGenerator,
        [FromBody] GenerateDigestRequest request)
    {
        if (request == null)
            return BadRequest();

        var secret = await userService.GetUserSecretKeyAsync(request.UserId);

        if (string.IsNullOrEmpty(secret))
            return NotFound("User not found");

        var jsonString = JsonConvert.SerializeObject(JObject.Parse(request.Body));

        var digest = await digestGenerator.GenerateDigestAsync(message: jsonString, secret: secret);

        return Ok(digest);
    }
}