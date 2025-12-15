using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Interview_Test.Models;

[Table("UserRoleMappingTb")]
public class UserRoleMappingModel
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid UserRoleMappingId { get; set; }
    [ForeignKey("UserId")]
    public UserModel User { get; set; }
    [ForeignKey("RoleId")]
    public RoleModel Role { get; set; }
}