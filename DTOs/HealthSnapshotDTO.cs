/*
 * 文件名: HealthSnapshotDTO.cs
 * 描述: 健康快照数据传输对象，用于向前端返回健康快照数据
 * 功能: 
 *   - 定义健康快照的数据结构
 *   - 作为服务层和控制器层之间的数据传输载体
 *   - 向前端提供用户健康状态的快照信息
 */

using System;

namespace HealthSupervision.DTOs
{
    /// <summary>
    /// 健康快照数据传输对象
    /// 用于向前端返回用户的健康快照数据，包含体重、BMI、步数等关键健康指标
    /// 与HealthSnapshot实体类不同，此DTO专注于数据传输，不包含复杂的关联关系
    /// </summary>
    public class HealthSnapshotDTO
    {
        /// <summary>
        /// 快照唯一标识符
        /// 用于前端识别和引用特定的健康快照记录
        /// </summary>
        public string SnapshotID { get; set; }

        /// <summary>
        /// 快照生成时间
        /// 记录健康快照创建的具体时间，用于时间序列分析和历史追踪
        /// </summary>
        public DateTime GenerateTime { get; set; }

        /// <summary>
        /// 用户当前体重
        /// 使用decimal类型确保精度，单位为公斤
        /// 用于体重变化趋势分析和健康状态评估
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 身体质量指数(BMI)
        /// 使用decimal类型确保精度，计算公式：体重(kg) / 身高(m)²
        /// 用于评估用户的体重是否健康，是重要的健康指标
        /// </summary>
        public decimal BMI { get; set; }

        /// <summary>
        /// 今日步数
        /// 使用decimal类型，记录用户当天的活动量
        /// 用于评估用户的日常活动水平和健康生活方式
        /// </summary>
        public decimal Steps { get; set; }
    }
}
