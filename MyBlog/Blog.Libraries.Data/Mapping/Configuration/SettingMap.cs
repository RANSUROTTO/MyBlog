using Blog.Libraries.Data.Domain.Configuration;

namespace Blog.Libraries.Data.Mapping.Configuration
{

    public class SettingMap : CustomEntityTypeConfiguration<Setting>
    {

        public SettingMap()
        {
            this.ToTable("Configuration_Setting");

            this.Property(p => p.Name).IsRequired().IsUnicode();
            this.Property(p => p.Value).IsRequired();
        }

    }

}
