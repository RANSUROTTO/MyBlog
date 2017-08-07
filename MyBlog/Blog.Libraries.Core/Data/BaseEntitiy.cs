using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Libraries.Core.Data
{
    public class BaseEntitiy
    {

        /// <summary>
        /// 实体主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 实体创建时间
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// 实体是否已被删除
        /// </summary>
        public bool IsDelete { get; set; }

    }

}
