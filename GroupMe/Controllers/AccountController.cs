using System;
using System.Threading.Tasks;
using GroupMe.Models;
using GroupMe.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GroupMe.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;

    private readonly GroupsService _groupsService;

    public AccountController(AccountService accountService, GroupsService groupsService)
    {
      _accountService = accountService;
      _groupsService = groupsService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("groups")]
    [Authorize]
    public async Task<ActionResult<List<GroupMemberViewModel>>> GetMyGroups()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_groupsService.GetGroupsByAccount(userInfo.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}