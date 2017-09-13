using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Data.Domain.Content;

namespace Blog.Libraries.Services.Content
{

    public class CategorieService : ICategorieService
    {

        #region Fields

        private IRepository<Categorie> _categorieRepository;

        #endregion

        #region Constructor

        public CategorieService(IRepository<Categorie> categorieRepository)
        {
            _categorieRepository = categorieRepository;
        }

        #endregion

        #region Methods

        public IList<Categorie> GetAllCategorie()
        {
            throw new NotImplementedException();
        }

        public Categorie GetCategorieById(long categorieId)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}
