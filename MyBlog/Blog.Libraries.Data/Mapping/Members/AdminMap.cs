using Blog.Libraries.Data.Domain.Members;

namespace Blog.Libraries.Data.Mapping.Members
{

    public class AdminMap : CustomEntityTypeConfiguration<Admin>
    {

        public AdminMap()
        {
            this.Property(p => p.AdminName).HasMaxLength(30);
            this.Property(p => p.Passwrod).HasMaxLength(20);

            //foreign key
            this.HasRequired(p => p.Customer).WithOptional().Map(p => p.MapKey("Customer_Id"));
        }

    }

}
