namespace Blog.Libraries.Core.Infrastructure
{

    /// <summary>
    /// 应用启动时运行的任务实现接口
    /// </summary>
    public interface IStartupTask
    {

        /// <summary>
        /// 启动时执行方法
        /// </summary>
        void Execute();

        /// <summary>
        /// 启动任务顺序
        /// </summary>
        int Order { get; }

    }

}
