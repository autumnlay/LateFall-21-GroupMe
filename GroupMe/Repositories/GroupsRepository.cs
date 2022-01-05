using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using GroupMe.Interfaces;
using GroupMe.Models;

namespace GroupMe.Repositories
{
  public class GroupsRepository : IRepository<Group>
  {
    private readonly IDbConnection _db;

    public GroupsRepository(IDbConnection db)
    {
      _db = db;
    }

    public Group Create(Group data)
    {
      var sql = @"
            INSERT INTO groups(name, description, picture, creatorId)
            VALUES(@Name, @Description, @Picture, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
      var id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    public List<Group> GetAll()
    {
      string sql = @"
                SELECT 
                    g.*,
                    a.*
                FROM groups g
                JOIN accounts a ON g.creatorId = a.id;
            ";
      return _db.Query<Group, Profile, Group>(sql, (g, p) =>
      {
        g.Owner = p;
        return g;
      }).ToList();
    }

    public Group GetById(int id)
    {
      string sql = @"
                SELECT 
                    g.*,
                    a.*
                FROM groups g
                JOIN accounts a ON g.creatorId = a.id
                WHERE g.id = @id;
            ";

      return _db.Query<Group, Profile, Group>(sql, (g, p) =>
        {
          g.Owner = p;
          return g;
        }, new { id }).FirstOrDefault();
    }

    internal List<GroupMemberViewModel> GetGroupsByAccount(string id)
    {
      string sql = @"
      SELECT 
        g.*,
        gm.id AS groupMemberId,
        gm.accountId AS groupMemberCreatorId,
        a.*
      FROM groupMembers gm
      JOIN  groups g ON g.id = gm.groupId
      JOIN accounts a ON a.id = g.creatorId
      WHERE gm.accountId = @id
      ;";
      return _db.Query<GroupMemberViewModel, Profile, GroupMemberViewModel>(sql, (groupMember, profile) =>
      {
        groupMember.Owner = profile;
        return groupMember;
      }, new { id }).ToList();
    }

    public Group Update(Group data)
    {
      var sql = @"
                UPDATE groups
                    SET
                    name = @Name,
                    picture = @Picture,
                    description = @Description
                WHERE id = @Id LIMIT 1;
            ";
      var rowsAffected = _db.Execute(sql, data);
      if (rowsAffected > 1)
      {
        // in reality alert the sysadmin
        throw new Exception("Ahhhhh that was probably really bad");
      }
      if (rowsAffected < 1)
      {
        throw new Exception("woops update didn't work no idea why probably a bad id");
      }
      return data;
    }

    public void Delete(int id)
    {
      var sql = "DELETE FROM groups WHERE id = @id";
      _db.Execute(sql, new { id });
    }
  }
}