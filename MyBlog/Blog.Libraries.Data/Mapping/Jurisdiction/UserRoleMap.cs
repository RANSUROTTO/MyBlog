using Blog.Libraries.Data.Domain.Jurisdiction;

namespace Blog.Libraries.Data.Mapping.Jurisdiction
{

    public class UserRoleMap : CustomEntityTypeConfiguration<UserRole>
    {

        public UserRoleMap()
        {
            this.Property(p => p.Description).HasMaxLength(255);

            this.HasOptional(p => p.RoleGroup).WithMany(p => p.UserRoles).Map(p => p.MapKey("RoleGroup_Id"));
        }

    }

}
