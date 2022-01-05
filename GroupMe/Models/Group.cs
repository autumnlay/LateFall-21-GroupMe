namespace GroupMe.Models
{
  public class Group : DbItem<int>
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public string Picture { get; set; }
    public string CreatorId { get; set; }
    public Profile Owner { get; set; }
  }

  public class GroupMemberViewModel : Group
  {
    public int GroupMemberId { get; set; }

    public string GroupMemberCreatorId { get; set; }
  }
}