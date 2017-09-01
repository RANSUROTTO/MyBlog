using System;

namespace Blog.Libraries.Core.Data
{
    public abstract class BaseEntity
    {

        #region Constructor

        protected BaseEntity()
        {
            this.Guid = new Guid();
            this.CreateAt = DateTime.UtcNow;
            this.IsDeleted = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 实体主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 全球唯一标识符
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// 实体创建时间
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// 并发标识
        /// </summary>
        public byte[] TimeStamp { get; set; }

        /// <summary>
        /// 实体是否已被删除
        /// </summary>
        public bool IsDeleted { get; set; }

        #endregion

        #region Methods

        public static bool operator ==(BaseEntity x, BaseEntity y) => Equals(x, y);

        public static bool operator !=(BaseEntity x, BaseEntity y) => !(x == y);

        public override bool Equals(object obj)
        {
            return Equals(obj as BaseEntity);
        }

        public virtual bool Equals(BaseEntity other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (object.Equals(this.Id, other.Id))
            {
                return this.GetType() == other.GetType();
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        #endregion

    }

}
