using Blog.Libraries.Data.Domain.Jurisdiction;

namespace Blog.Libraries.Data.Mapping.Jurisdiction
{

    public class RoleGroupMap : CustomEntityTypeConfiguration<RoleGroup>
    {

        public RoleGroupMap()
        {
            this.Property(p => p.Name).HasMaxLength(200);
            this.Property(p => p.Description).HasMaxLength(255);

            this.HasMany(p => p.UserRoles).WithOptional(p => p.RoleGroup);
        }

    }

}
