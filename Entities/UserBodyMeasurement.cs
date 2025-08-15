/*
 * 文件名: UserBodyMeasurement.cs
 * 描述: 用户身体测量实体类，定义身体测量数据的数据库表结构
 * 功能: 
 *   - 定义身体测量数据的数据库表映射
 *   - 存储用户的身高、体重、BMI等身体指标
 *   - 支持健康状态追踪和趋势分析
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSupervision.Entities
{
    /// <summary>
    /// 用户身体测量实体类
    /// 对应数据库中的User_Body_Measurement表，记录用户的详细身体指标数据
    /// 用于健康状态追踪、趋势分析和健康建议生成
    /// </summary>
    [Table("User_Body_Measurement")] // 指定数据库表名，使用下划线命名法符合Oracle数据库规范
    public class UserBodyMeasurement
    {
        /// <summary>
        /// 测量记录唯一标识符（主键）
        /// 使用GUID字符串作为主键，确保全局唯一性
        /// 在对象创建时自动生成，避免ID冲突
        /// </summary>
        [Key]                           // 标识为主键
        [MaxLength(50)]                 // 限制最大长度为50个字符
        public string MeasurementID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 用户ID（外键）
        /// 关联到UserAccount表，建立测量记录与用户的从属关系
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
        /// 测量时间
        /// 自动设置为UTC时间，记录身体测量进行的确切时间
        /// 用于时间序列分析和历史数据追踪
        /// </summary>
        public DateTime MeasurementTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 用户体重
        /// 使用decimal类型确保精度，单位为公斤
        /// 用于体重变化趋势分析和健康状态评估
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 用户身高
        /// 使用decimal类型确保精度，单位为厘米
        /// 用于计算BMI和身体比例分析
        /// </summary>
        public decimal Height { get; set; }

        /// <summary>
        /// 身体质量指数(BMI)
        /// 使用decimal类型确保精度，计算公式：体重(kg) / 身高(m)²
        /// 用于评估用户的体重是否健康，是重要的健康指标
        /// </summary>
        public decimal BMI { get; set; }

        /// <summary>
        /// 体脂百分比
        /// 使用decimal类型确保精度，表示身体脂肪占总体重的百分比
        /// 用于评估身体成分和健康风险
        /// </summary>
        public decimal FatPercentage { get; set; }

        /// <summary>
        /// 肌肉百分比
        /// 使用decimal类型确保精度，表示肌肉组织占总体重的百分比
        /// 用于评估身体成分和运动效果
        /// </summary>
        public decimal MusclePercentage { get; set; }
    }
}