using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Blog.Libraries.Core.Configuration;
using Blog.Libraries.Core.Infrastructure;
using Blog.Libraries.Core.Infrastructure.TypeFinder;

namespace Blog.Presentation.Framework
{

    /// <summary>
    /// 依赖注册
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, WebConfig config)
        {



        }

        public int Order
        {
            get { return 0; }
        }

    }

}
