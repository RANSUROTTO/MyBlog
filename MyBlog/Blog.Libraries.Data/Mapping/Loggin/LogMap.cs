using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Data.Domain.Logging;

namespace Blog.Libraries.Data.Mapping.Loggin
{

    public class LogMap : CustomEntityTypeConfiguration<Log>
    {

        public LogMap()
        {
            this.ToTable("Log");
            this.HasKey(p => p.Id);
            this.Property(p => p.ShortMessage).IsRequired();
            this.Property(p => p.IpAddress).HasMaxLength(200);
            

        }

    }

}
