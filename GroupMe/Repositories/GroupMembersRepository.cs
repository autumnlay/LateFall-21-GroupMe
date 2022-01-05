using System.Data;
using Dapper;
using GroupMe.Models;

namespace GroupMe.Repositories
{
  public class GroupMembersRepository
  {
    private readonly IDbConnection _db;

    public GroupMembersRepository(IDbConnection db)
    {
      _db = db;
    }

    internal GroupMember Create(GroupMember newGroupMember)
    {
      string sql = @"
      INSERT INTO groupMembers
        (groupId, accountId)
      Values
        (@GroupId, @AccountId);
      SELECT LAST_INSERT_ID()
      ;";
      newGroupMember.Id = _db.ExecuteScalar<int>(sql, newGroupMember);
      return newGroupMember;
    }

    internal GroupMember GetGroupMemberIfExists(GroupMember newGroupMember)
    {
      string sql = @"
        SELECT 
        *
        FROM groupMembers
        WHERE groupId = @GroupId AND accountId = @AccountId
      ;";
      return _db.QueryFirstOrDefault<GroupMember>(sql, newGroupMember);
    }
    // NOTE DTO Method
    // internal List<GroupMemberViewModel> GetMembersByGroupId(int groupId)
    // {
    //   string sql = @"
    //   SELECT 
    //     gm.*,
    //     a.*,
    //     g.*
    //   FROM groupMembers gm
    //   JOIN accounts a ON a.id = gm.accountId
    //   JOIN groups g ON g.id = gm.groupId
    //   WHERE gm.groupId = @groupId
    //   ;";
    //   return _db.Query<GroupMemberViewModel, Profile, Group, GroupMemberViewModel>(sql, (groupMember, profile, group) =>
    //   {
    //     groupMember.Profile = profile;
    //     groupMember.Group = group;
    //     return groupMember;
    //   }, new { groupId }).ToList();
    // }
  }
}