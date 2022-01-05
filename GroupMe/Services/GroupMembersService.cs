using System;
using GroupMe.Models;
using GroupMe.Repositories;

namespace GroupMe.Services
{
  public class GroupMembersService
  {
    private readonly GroupMembersRepository _repo;

    public GroupMembersService(GroupMembersRepository repo)
    {
      _repo = repo;
    }

    public GroupMember Create(GroupMember newGroupMember)
    {
      GroupMember inGroup = _repo.GetGroupMemberIfExists(newGroupMember);
      if (inGroup != null)
      {
        throw new Exception("Already in that group.");
      }
      return _repo.Create(newGroupMember);
    }
    // NOTE DTO Method
    // public List<GroupMemberViewModel> GetMembersByGroupId(int groupId)
    // {
    //   return _repo.GetMembersByGroupId(groupId);
    // }
  }
}