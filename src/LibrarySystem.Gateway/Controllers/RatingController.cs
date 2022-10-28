using LibrarySystem.Gateway.DTO;
using LibrarySystem.Gateway.Models;
using LibrarySystem.Gateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Gateway.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RatingController : ControllerBase
{
    private readonly ILogger<RatingController> _logger;
    private readonly RatingService _ratingService;

    public RatingController(ILogger<RatingController> logger, RatingService ratingService)
    {
        _logger = logger;
        _ratingService = ratingService;
    }

    [HttpGet]
    public async Task<ActionResult<UserRatingResponse>> Get([FromHeader(Name = "X-User-Name")] string xUserName)
    {
        _logger.LogInformation("Requested rating for user {UserName}", xUserName);
        var response = await _ratingService.GetUserRatingAsync(xUserName);
        return response;
    }
}