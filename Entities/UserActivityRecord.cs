/*
 * 文件名: UserActivityRecord.cs
 * 描述: 用户活动记录实体类，定义用户日常活动数据的数据库表结构
 * 功能: 
 *   - 定义活动记录数据的数据库表映射
 *   - 存储用户的步数、运动记录等日常活动数据
 *   - 支持活动量分析和健康生活方式评估
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSupervision.Entities
{
    /// <summary>
    /// 用户活动记录实体类
    /// 对应数据库中的User_Activity_Record表，记录用户的日常活动数据
    /// 用于活动量分析、健康生活方式评估和运动建议生成
    /// </summary>
    [Table("User_Activity_Record")] // 指定数据库表名，使用下划线命名法符合Oracle数据库规范
    public class UserActivityRecord
    {
        /// <summary>
        /// 活动记录唯一标识符（主键）
        /// 使用GUID字符串作为主键，确保全局唯一性
        /// 在对象创建时自动生成，避免ID冲突
        /// </summary>
        [Key]                           // 标识为主键
        [MaxLength(50)]                 // 限制最大长度为50个字符
        public string ActivityID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 用户ID（外键）
        /// 关联到UserAccount表，建立活动记录与用户的从属关系
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
        /// 记录日期
        /// 自动设置为UTC时间的日期部分，不包含具体时间
        /// 使用.Date属性确保只记录日期，便于按日期分组和统计
        /// 用于日度活动量分析和健康生活方式评估
        /// </summary>
        public DateTime RecordDate { get; set; } = DateTime.UtcNow.Date;

        /// <summary>
        /// 当日步数
        /// 使用int类型，记录用户在特定日期的总步数
        /// 用于评估用户的日常活动水平和运动习惯
        /// 是健康生活方式的重要指标之一
        /// </summary>
        public int Steps { get; set; }
    }
}




