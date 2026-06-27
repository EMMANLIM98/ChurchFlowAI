using ChurchFlow.Application.Members.CreateMember;
using ChurchFlow.Application.Members.GetMembers;

using Microsoft.AspNetCore.Mvc;

namespace ChurchFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly CreateMemberHandler _createMemberHandler;
    private readonly GetMembersHandler _getMembersHandler;

    public MembersController(
        CreateMemberHandler createMemberHandler,
        GetMembersHandler getMembersHandler)
    {
        _createMemberHandler = createMemberHandler;
        _getMembersHandler = getMembersHandler;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetMembersQuery query, CancellationToken cancellationToken)
    {
        var result = await _getMembersHandler.Handle(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var result = await _createMemberHandler.Handle(request, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok();
    }
}
