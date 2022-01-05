using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using GroupMe.Models;
using GroupMe.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupMe.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class GroupMembersController : ControllerBase
  {
    private readonly GroupMembersService _groupMemberService;
    private readonly GroupsService _groupService;

    public GroupMembersController(GroupMembersService groupMemberService, GroupsService groupService)
    {
      _groupMemberService = groupMemberService;
      _groupService = groupService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<GroupMember>> Create([FromBody] GroupMember newGroupMember)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newGroupMember.AccountId = userInfo.Id;
        return Ok(_groupMemberService.Create(newGroupMember));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }



    }
  }
}