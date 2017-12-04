using Blog.Libraries.Data.Domain.Blog;

namespace Blog.Libraries.Data.Mapping.Content
{

    public class ArticleMap : CustomEntityTypeConfiguration<Domain.Blog.BlogPost>
    {

        public ArticleMap()
        {
            this.ToTable("Content_Article");

            this.Property(p => p.Title).HasMaxLength(200);

            //foreign key
            this.HasRequired(p => p.Customer).WithMany().Map(p => p.MapKey("Customer_Id"));
            this.HasRequired(p => p.BlogCategorie).WithMany(p => p.Articles).Map(p => p.MapKey("Categorie_Id"));
        }

    }

}
