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

            this.Property(p => p.LogLevel)
                .HasColumnName("LogLevel_Id");
            this.HasOptional(p => p.Customer)
                .WithMany()
                .Map(p => p.MapKey("Customer_Id"));
        }

    }

}
