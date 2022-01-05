using System.Collections.Generic;
using GroupMe.Models;
using GroupMe.Repositories;

namespace GroupMe.Services
{
  public class GroupsService
  {
    private readonly GroupsRepository _groupsRepo;

    public GroupsService(GroupsRepository groupsRepo)
    {
      _groupsRepo = groupsRepo;
    }

    internal List<Group> GetGroups()
    {
      return _groupsRepo.GetAll();
    }

    public Group Create(Group data)
    {
      return _groupsRepo.Create(data);
    }

    public Group GetGroupById(int id)
    {
      var group = _groupsRepo.GetById(id);
      if (group == null)
      {
        throw new System.Exception("Bad Group Id");
      }
      return group;
    }

    public Group Update(string userId, Group data)
    {
      var group = IsGroupOwner(userId, data.Id);
      group.Name = data.Name ?? group.Name;
      group.Picture = data.Picture ?? group.Picture;
      group.Description = data.Description ?? group.Description;
      return _groupsRepo.Update(group);
    }


    private Group IsGroupOwner(string userId, int id)
    {
      var group = GetGroupById(id);
      if (group.CreatorId != userId)
      {
        throw new System.Exception("You are not the owner of the group");
      }
      return group;
    }

    public List<GroupMemberViewModel> GetGroupsByAccount(string id)
    {
      return _groupsRepo.GetGroupsByAccount(id);
    }

    public Group DeleteGroup(string userId, int groupId)
    {

      var group = IsGroupOwner(userId, groupId);
      _groupsRepo.Delete(groupId);
      return group;
    }
  }
}