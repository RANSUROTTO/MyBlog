using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Data.Domain.Members;

namespace Blog.Libraries.Data.Mapping.Members
{

    public class CustomerProfileMap : CustomEntityTypeConfiguration<CustomerProfile>
    {

        public CustomerProfileMap()
        {
            this.Property(p => p.NickName).HasMaxLength(25);
            this.Property(p => p.Gender).HasMaxLength(2);
            this.Property(p => p.Province).HasMaxLength(30);
            this.Property(p => p.City).HasMaxLength(30);
            this.Property(p => p.Industry).HasMaxLength(30);
            this.Property(p => p.Introduction).HasMaxLength(100);
            this.Property(p => p.Description).HasMaxLength(255);
            this.Property(p => p.Phone).HasMaxLength(11);

            //this.HasRequired(p => p.Customer);
        }

    }

}
