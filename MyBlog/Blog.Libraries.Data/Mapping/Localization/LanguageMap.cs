using Blog.Libraries.Core.Domain.Localization;

namespace Blog.Libraries.Data.Mapping.Localization
{

    public class LanguageMap : CustomEntityTypeConfiguration<Language>
    {

        public LanguageMap()
        {
            this.ToTable("Localization_Language");

            this.Property(p => p.Name).IsRequired().HasMaxLength(100);
            this.Property(p => p.LanguageCulture).IsRequired().HasMaxLength(20);
            this.Property(p => p.UniqueSeoCode).HasMaxLength(4);

            //navigation properties
            this.HasMany(p => p.LocaleStringResources).WithRequired(p => p.Language);
        }

    }

}
