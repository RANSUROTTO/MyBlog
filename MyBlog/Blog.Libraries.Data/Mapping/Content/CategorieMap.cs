using Blog.Libraries.Data.Domain.Content;

namespace Blog.Libraries.Data.Mapping.Content
{

    public class CategorieMap : CustomEntityTypeConfiguration<Categorie>
    {

        public CategorieMap()
        {
            this.Property(p => p.Title).HasMaxLength(200);

            //nvaigation properties
            this.HasMany(p => p.Articles).WithRequired(p => p.Categorie);
        }

    }

}
