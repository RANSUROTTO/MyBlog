using Blog.Libraries.Data.Domain.Member;

namespace Blog.Libraries.Data.Mapping.Member
{

    public class CustomerProfileMap : CustomEntityTypeConfiguration<CustomerProfile>
    {

        public CustomerProfileMap()
        {
            this.ToTable("Member_CustomerProfile");

            this.Property(p => p.NickName).HasMaxLength(25);
            this.Property(p => p.Gender).HasMaxLength(2);
            this.Property(p => p.Province).HasMaxLength(30);
            this.Property(p => p.City).HasMaxLength(30);
            this.Property(p => p.Industry).HasMaxLength(30);
            this.Property(p => p.Introduction).HasMaxLength(100);
            this.Property(p => p.Description).HasMaxLength(255);
            this.Property(p => p.Phone).HasMaxLength(11);

            //foreign key
            this.HasRequired(p => p.Customer).WithRequiredDependent().Map(p => p.MapKey("Customer_Id"));
        }

    }

}
