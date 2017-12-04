using Blog.Libraries.Data.Domain.Permissions;

namespace Blog.Libraries.Data.Mapping.Permissions
{

    public class UserRoleMap : CustomEntityTypeConfiguration<UserRole>
    {

        public UserRoleMap()
        {
            this.ToTable("Permissions_UserRole");

            this.Property(p => p.Description).HasMaxLength(255);

            //foreign key
            this.HasOptional(p => p.RoleGroup).WithMany(p => p.UserRoles).Map(p => p.MapKey("RoleGroup_Id"));
            this.HasRequired(p => p.Admin).WithOptional(p => p.UserRole).Map(p => p.MapKey("Admin_Id"));
        }

    }

}
