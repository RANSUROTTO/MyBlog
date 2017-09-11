using Blog.Libraries.Data.Domain.Members;

namespace Blog.Libraries.Data.Mapping.Members
{

    public class CustomerMap : CustomEntityTypeConfiguration<Customer>
    {

        public CustomerMap()
        {
            this.Property(p => p.Username).IsRequired().HasMaxLength(50);
            this.Property(p => p.Password).IsRequired().HasMaxLength(20);
            this.Property(p => p.Email).HasMaxLength(200);
            this.Property(p => p.LastIpAddress).HasMaxLength(200);

            //nvaigation properties
            this.HasMany(p => p.Logs).WithOptional(p => p.Customer);
            this.HasOptional(p => p.Admin).WithRequired(p => p.Customer);
            this.HasMany(p => p.Articles).WithRequired(p => p.Customer);
        }

    }

}
