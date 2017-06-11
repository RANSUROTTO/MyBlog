namespace Blog.Libraries.Core.Infrastructure
{
    /// <summary>
    /// 应用启动时运行的任务实现接口
    /// </summary>
    public interface IStartupTask
    {
        /// <summary>
        /// 执行方法
        /// </summary>
        void Execute();

        /// <summary>
        /// 顺序
        /// </summary>
        int Order { get; }
    }
}
