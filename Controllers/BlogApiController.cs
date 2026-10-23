using System.Security.Claims;
using Blog.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers;

[ApiController]
public abstract class BlogApiController : ControllerBase
{
    protected ActorContext CurrentActor => new(
        User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new InvalidOperationException("Идентификатор пользователя не найден."),
        User.IsInRole(RoleNames.Admin),
        User.IsInRole(RoleNames.Moderator));

    protected IActionResult FromResult<T>(OperationResult<T> result) => result.Status switch
    {
        OperationStatus.Success => Ok(result.Value),
        OperationStatus.NotFound => NotFound(result.Errors),
        OperationStatus.Forbidden => Forbid(),
        OperationStatus.Conflict => Conflict(result.Errors),
        _ => BadRequest(result.Errors)
    };
}
