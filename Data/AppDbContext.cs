/*
 * 文件名: AppDbContext.cs
 * 描述: 应用程序数据库上下文，定义Entity Framework Core的数据库映射和配置
 * 功能: 
 *   - 定义数据库表与实体类的映射关系
 *   - 配置表名、外键关系等数据库结构
 *   - 提供数据库访问的入口点
 */

using Microsoft.EntityFrameworkCore;
using HealthSupervision.Entities;

namespace HealthSupervision.Data
{
    /// <summary>
    /// 应用程序数据库上下文类
    /// 继承自DbContext，是Entity Framework Core的核心类
    /// 负责管理数据库连接、实体跟踪、变更保存等数据库操作
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// 构造函数，接收数据库配置选项
        /// 通过依赖注入获取连接字符串等配置信息
        /// </summary>
        /// <param name="options">数据库配置选项，包含连接字符串、数据库提供程序等</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// 用户账户表映射
        /// DbSet<T>表示数据库中的一个表，T是实体类型
        /// Entity Framework会自动处理CRUD操作
        /// </summary>
        public DbSet<UserAccount> UserAccounts { get; set; }

        /// <summary>
        /// 用户身体测量表映射
        /// 存储用户的身高、体重、BMI等身体指标数据
        /// </summary>
        public DbSet<UserBodyMeasurement> UserBodyMeasurements { get; set; }

        /// <summary>
        /// 健康快照表映射
        /// 存储用户健康状态的快照数据，整合多个健康指标
        /// </summary>
        public DbSet<HealthSnapshot> HealthSnapshots { get; set; }

        /// <summary>
        /// 用户活动记录表映射
        /// 存储用户的日常活动数据，如步数、运动记录等
        /// </summary>
        public DbSet<UserActivityRecord> UserActivityRecords { get; set; }

        /// <summary>
        /// 模型创建时的配置方法
        /// 在Entity Framework创建数据库模型时调用，用于自定义表结构
        /// </summary>
        /// <param name="modelBuilder">模型构建器，用于配置实体和关系</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 配置表名映射，将C#实体类名映射到数据库表名
            // 使用下划线命名法，符合Oracle数据库的命名规范
            modelBuilder.Entity<UserAccount>().ToTable("User_Account");
            modelBuilder.Entity<UserBodyMeasurement>().ToTable("User_Body_Measurement");
            modelBuilder.Entity<HealthSnapshot>().ToTable("Health_Snapshot");
            modelBuilder.Entity<UserActivityRecord>().ToTable("User_Activity_Record");
            
            // 注意：外键关系通过实体类中的ForeignKey特性自动配置
            // Entity Framework会根据导航属性和外键属性自动建立关系
        }
    }
}
