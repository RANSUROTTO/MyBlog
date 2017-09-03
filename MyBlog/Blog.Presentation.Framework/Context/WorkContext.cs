using System;
using System.Web;
using Blog.Libraries.Core.Context;
using Blog.Libraries.Core.Domain.Localization;
using Blog.Libraries.Core.Domain.Members;
using Blog.Libraries.Core.Fakes;
using Blog.Libraries.Data.Domain.Members;

namespace Blog.Presentation.Framework.Context
{

    public class WorkContext : IWorkContext
    {

        #region Fields

        private readonly HttpContextBase _httpContext;

        private Customer _cacheCustomer;

        #endregion

        #region Constructor

        public WorkContext()
        {

        }

        #endregion



        #region Properties

        public virtual IGuest Guest
        {
            get
            {


                throw new NotImplementedException();
            }
        }

        public ICustomer Customer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IAdmin Admin { get; }

        public Language Language { get; set; }

        #endregion

    }

}
