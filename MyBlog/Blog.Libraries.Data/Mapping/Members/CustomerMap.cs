using Blog.Libraries.Data.Domain.Members;

namespace Blog.Libraries.Data.Mapping.Members
{

    public class CustomerMap : CustomEntityTypeConfiguration<Customer>
    {

        public CustomerMap()
        {
            this.Property(p => p.Username).IsRequired().HasMaxLength(50).IsUnicode();
            this.Property(p => p.Password).IsRequired().HasMaxLength(20);
            this.Property(p => p.Email).HasMaxLength(200);
            this.Property(p => p.LastIpAddress).HasMaxLength(200);

            this.HasMany(p => p.Logs).WithOptional(p => p.Customer);
        }

    }

}
