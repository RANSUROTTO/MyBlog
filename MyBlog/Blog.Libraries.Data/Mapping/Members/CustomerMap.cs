using Blog.Libraries.Data.Domain.Members;

namespace Blog.Libraries.Data.Mapping.Members
{

    public class CustomerMap : CustomEntityTypeConfiguration<Customer>
    {

        public CustomerMap()
        {
            this.Property(p => p.Username).IsRequired().HasMaxLength(50);
            this.Property(p => p.Email).HasMaxLength(200);
            this.Property(p => p.LastIpAddress).HasMaxLength(200);

            //foreign key
            this.HasRequired(p => p.CustomerProfile).WithRequiredPrincipal();

            //nvaigation properties
            this.HasOptional(p => p.Admin).WithRequired(p => p.Customer);
            this.HasMany(p => p.Logs).WithOptional(p => p.Customer);
            this.HasMany(p => p.Articles).WithRequired(p => p.Customer);
            this.HasMany(p => p.CustomerPasswords).WithRequired(p => p.Customer);
        }

    }

}
