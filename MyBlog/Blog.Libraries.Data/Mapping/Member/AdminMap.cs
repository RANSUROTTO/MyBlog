using Blog.Libraries.Data.Domain.Member;

namespace Blog.Libraries.Data.Mapping.Member
{

    public class AdminMap : CustomEntityTypeConfiguration<Admin>
    {

        public AdminMap()
        {
            this.ToTable("Member_Admin");

            this.Property(p => p.AdminName).HasMaxLength(30);
            this.Property(p => p.Passwrod).HasMaxLength(20);

            //foreign key
            this.HasRequired(p => p.Customer).WithOptional(p => p.Admin).Map(p => p.MapKey("Customer_Id"));

            //nvaigation properties
            this.HasOptional(p => p.UserRole).WithRequired(p => p.Admin);
        }

    }

}
