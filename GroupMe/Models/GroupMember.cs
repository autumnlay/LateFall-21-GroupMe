namespace GroupMe.Models
{
  public class GroupMember
  {
    public int Id { get; set; }
    public int GroupId { get; set; }
    public string AccountId { get; set; }
  }

  // NOTE DTO Method
  // public class GroupMemberViewModel : GroupMember
  // {
  //   public Profile Profile { get; set; }
  //   public Group Group { get; set; }
  // }
}