using ChurchFlow.Application.Members.CreateMember;

using Microsoft.AspNetCore.Mvc;

namespace ChurchFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly CreateMemberHandler _handler;

    public MembersController(CreateMemberHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMemberCommand request)
    {
        var result = await _handler.Handle(request);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }
}