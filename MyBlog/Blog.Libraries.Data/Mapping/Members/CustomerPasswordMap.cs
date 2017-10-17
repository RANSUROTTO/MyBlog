using Blog.Libraries.Data.Domain.Members;

namespace Blog.Libraries.Data.Mapping.Members
{

    public class CustomerPasswordMap : CustomEntityTypeConfiguration<CustomerPassword>
    {

        public CustomerPasswordMap()
        {
            this.Property(p => p.Password).HasMaxLength(32);

            //foreign key
            this.HasRequired(p => p.Customer).WithMany(p => p.CustomerPasswords).Map(p => p.MapKey("Customer_Id"));

        }

    }

}
