using Blog.Libraries.Data.Domain.Permissions;

namespace Blog.Libraries.Data.Mapping.Permissions
{

    public class RoleGroupMap : CustomEntityTypeConfiguration<RoleGroup>
    {

        public RoleGroupMap()
        {
            this.ToTable("Permissions_RoleGroup");

            this.Property(p => p.Name).HasMaxLength(200);
            this.Property(p => p.Description).HasMaxLength(255);

            this.HasMany(p => p.UserRoles).WithOptional(p => p.RoleGroup);
        }

    }

}
