using System.Collections.Generic;
using System.Threading.Tasks;
using GroupMe.Models;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GroupMe.Services;

namespace GroupMe.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class GroupsController : ControllerBase
  {

    private readonly GroupsService _gs;

    private readonly GroupMembersService _gms;

    public GroupsController(GroupsService gs, GroupMembersService gms)
    {
      _gs = gs;
      _gms = gms;
    }

    [HttpGet]
    public ActionResult<List<Group>> GetGroups()
    {
      try
      {
        List<Group> groups = _gs.GetGroups();
        return Ok(groups);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{groupId}")]
    public ActionResult<Group> GetGroup(int groupId)
    {
      try
      {
        Group group = _gs.GetGroupById(groupId);
        return Ok(group);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Group>> CreateGroup([FromBody] Group data)
    {
      try
      {
        var userInfo = await HttpContext.GetUserInfoAsync<Account>();
        data.CreatorId = userInfo.Id;
        Group group = _gs.Create(data);
        group.Owner = userInfo;
        return Ok(group);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [Authorize]
    [HttpPut("{groupId}")]
    public async Task<ActionResult<Group>> EditGroup(int groupId, [FromBody] Group gData)
    {
      try
      {
        gData.Id = groupId;
        var userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Group group = _gs.Update(userInfo.Id, gData);
        return Ok(group);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [Authorize]
    [HttpDelete("{groupId}")]
    public async Task<ActionResult<Group>> DeleteGroup(int groupId)
    {
      try
      {
        var userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Group group = _gs.DeleteGroup(userInfo.Id, groupId);
        return Ok(group);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // NOTE DTO Method
    // [HttpGet("{groupId}/members")]
    // public ActionResult<List<GroupMemberViewModel>> GetGroupMembers(int groupId)
    // {
    //   try
    //   {
    //     return Ok(_gms.GetMembersByGroupId(groupId));
    //   }
    //   catch (System.Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }

  }

}