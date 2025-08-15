/*
 * 文件名: HealthSnapshot.cs
 * 描述: 健康快照实体类，定义健康快照的数据库表结构和关联关系
 * 功能: 
 *   - 定义健康快照的数据库表映射
 *   - 整合用户的身体测量、活动记录等健康信息
 *   - 建立与其他健康相关实体的关联关系
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSupervision.Entities
{
    /// <summary>
    /// 健康快照实体类
    /// 对应数据库中的Health_Snapshot表，整合用户的多种健康指标数据
    /// 作为健康数据的汇总视图，便于健康状态分析和趋势追踪
    /// </summary>
    [Table("Health_Snapshot")] // 指定数据库表名，使用下划线命名法符合Oracle数据库规范
    public class HealthSnapshot
    {
        /// <summary>
        /// 快照唯一标识符（主键）
        /// 使用GUID字符串作为主键，确保全局唯一性
        /// 在对象创建时自动生成，避免ID冲突
        /// </summary>
        [Key]                           // 标识为主键
        [MaxLength(50)]                 // 限制最大长度为50个字符
        public string SnapshotID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 用户ID（外键）
        /// 关联到UserAccount表，建立快照与用户的从属关系
        /// 使用ForeignKey特性明确指定外键关系
        /// </summary>
        [ForeignKey("UserAccount")]     // 指定外键关系，关联到UserAccount导航属性
        public string UserID { get; set; }

        /// <summary>
        /// 用户账户导航属性
        /// Entity Framework使用此属性建立与UserAccount实体的关联
        /// 支持延迟加载和关联查询
        /// </summary>
        public UserAccount UserAccount { get; set; }

        /// <summary>
        /// 快照生成时间
        /// 自动设置为UTC时间，记录健康快照创建的确切时间
        /// 用于时间序列分析和历史数据追踪
        /// </summary>
        public DateTime GenerateTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 身体测量记录ID（外键）
        /// 关联到UserBodyMeasurement表，引用最新的身体测量数据
        /// 可以为空，表示用户可能没有身体测量记录
        /// </summary>
        [ForeignKey("UserBodyMeasurement")] // 指定外键关系，关联到UserBodyMeasurement导航属性
        public string MeasurementID { get; set; }

        /// <summary>
        /// 身体测量记录导航属性
        /// 建立与UserBodyMeasurement实体的关联
        /// 支持延迟加载和关联查询
        /// </summary>
        public UserBodyMeasurement UserBodyMeasurement { get; set; }

        /// <summary>
        /// 活动记录ID（外键）
        /// 关联到UserActivityRecord表，引用当日的活动数据
        /// 可以为空，表示用户可能没有活动记录
        /// </summary>
        [ForeignKey("UserActivityRecord")] // 指定外键关系，关联到UserActivityRecord导航属性
        public string ActivityID { get; set; }

        /// <summary>
        /// 活动记录导航属性
        /// 建立与UserActivityRecord实体的关联
        /// 支持延迟加载和关联查询
        /// </summary>
        public UserActivityRecord UserActivityRecord { get; set; }

        /// <summary>
        /// 快照数据 - 用户当前体重
        /// 使用decimal类型确保精度，单位为公斤
        /// 用于体重变化趋势分析和健康状态评估
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 快照数据 - 身体质量指数(BMI)
        /// 使用decimal类型确保精度，计算公式：体重(kg) / 身高(m)²
        /// 用于评估用户的体重是否健康，是重要的健康指标
        /// </summary>
        public decimal BMI { get; set; }

        /// <summary>
        /// 快照数据 - 今日步数
        /// 使用decimal类型，记录用户当天的活动量
        /// 用于评估用户的日常活动水平和健康生活方式
        /// </summary>
        public decimal Steps { get; set; }
    }
}