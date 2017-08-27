using System;

namespace Blog.Libraries.Data.Domain.Tasks
{

    /// <summary>
    /// 代表一个计划任务
    /// </summary>
    public class ScheduleTask
    {

        /// <summary>
        /// 获取或设置计划任务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置运行时间
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// 获取或设置适当的任务的类的类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示是否启用任务
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示遇到错误是否停用任务
        /// </summary>
        public bool StopOnError { get; set; }


        /// <summary>
        /// Gets or sets the machine name (instance) that leased this task. It's used when running in web farm (ensure that a task in run only on one machine). It could be null when not running in web farm.
        /// </summary>
        public string LeasedByMachineName { get; set; }

        /// <summary>
        /// Gets or sets the datetime until the task is leased by some machine (instance). It's used when running in web farm (ensure that a task in run only on one machine).
        /// </summary>
        public DateTime? LeasedUntilUtc { get; set; }

        /// <summary>
        /// 获取或设置任务最后一次启用时间
        /// </summary>
        public DateTime? LastStartAt { get; set; }

        /// <summary>
        /// 获取或设置任务最后一次执行时间(无论成功或失败)
        /// </summary>
        public DateTime? LastEndAt { get; set; }

        /// <summary>
        /// 获取或设置任务最后一次成功完成时间
        /// </summary>
        public DateTime? LastSuccessAt { get; set; }

    }

}
