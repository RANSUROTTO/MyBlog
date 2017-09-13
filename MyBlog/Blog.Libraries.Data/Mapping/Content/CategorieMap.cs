using Blog.Libraries.Data.Domain.Blog;

namespace Blog.Libraries.Data.Mapping.Content
{

    public class CategorieMap : CustomEntityTypeConfiguration<BlogCategorie>
    {

        public CategorieMap()
        {
            this.Property(p => p.Title).HasMaxLength(200);

            //nvaigation properties
            this.HasMany(p => p.Articles).WithRequired(p => p.BlogCategorie);
        }

    }

}
