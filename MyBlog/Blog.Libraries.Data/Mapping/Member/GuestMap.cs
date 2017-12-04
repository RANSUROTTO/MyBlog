using Blog.Libraries.Data.Domain.Member;

namespace Blog.Libraries.Data.Mapping.Member
{

    public class GuestMap : CustomEntityTypeConfiguration<Guest>
    {

        public GuestMap()
        {
            this.ToTable("Member_Guest");

            this.Property(p => p.UserAgent).HasMaxLength(300);
        }

    }

}
