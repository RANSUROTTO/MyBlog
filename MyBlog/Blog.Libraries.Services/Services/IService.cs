using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Services.Services
{

    public interface IService<T> where T : BaseEntity, new()
    {



    }

}
