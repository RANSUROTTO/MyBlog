using Blog.Libraries.Data.Domain.Content;

namespace Blog.Libraries.Data.Mapping.Content
{

    public class ArticleMap : CustomEntityTypeConfiguration<Article>
    {

        public ArticleMap()
        {
            this.Property(p => p.Title).HasMaxLength(200);

            //foreign key
            this.HasRequired(p => p.Customer).WithMany().Map(p => p.MapKey("Customer_Id"));
            this.HasRequired(p => p.Categorie).WithMany(p => p.Articles).Map(p => p.MapKey("Categorie_Id"));
        }

    }

}
