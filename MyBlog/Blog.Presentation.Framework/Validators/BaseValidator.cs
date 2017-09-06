using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Infrastructure;
using Blog.Libraries.Data.Context;
using FluentValidation;

namespace Blog.Presentation.Framework.Validators
{

    public abstract class BaseValidator<T> : AbstractValidator<T> where T : class
    {

        /// <summary>
        /// Ctor
        /// </summary>
        protected BaseValidator()
        {
            Initialize();
        }

        /// <summary>
        /// 通过继承覆盖该方法可以为构造函数添加一些自定义的初始化代码
        /// </summary>
        protected virtual void Initialize()
        { }
        





    }

}
