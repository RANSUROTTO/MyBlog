using Blog.Libraries.Data.Domain.Member;

namespace Blog.Libraries.Data.Mapping.Member
{

    public class CustomerPasswordMap : CustomEntityTypeConfiguration<CustomerPassword>
    {

        public CustomerPasswordMap()
        {
            this.ToTable("Member_CustomerPassword");

            this.Property(p => p.Password).HasMaxLength(32);

            //foreign key
            this.HasRequired(p => p.Customer).WithMany(p => p.CustomerPasswords).Map(p => p.MapKey("Customer_Id"));

        }

    }

}
