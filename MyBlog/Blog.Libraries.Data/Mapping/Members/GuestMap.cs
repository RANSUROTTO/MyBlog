using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Data.Domain.Members;

namespace Blog.Libraries.Data.Mapping.Members
{

    public class GuestMap : CustomEntityTypeConfiguration<Guest>
    {

        public GuestMap()
        {
            this.Property(p => p.UserAgent).HasMaxLength(300);
        }

    }

}
