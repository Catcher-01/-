/*
 * 文件名: UserUpdateDTO.cs
 * 描述: 用户信息更新数据传输对象，用于接收前端用户信息更新请求的数据
 * 功能: 
 *   - 定义用户信息更新时需要的数据结构
 *   - 提供数据验证特性，确保数据完整性
 *   - 支持部分字段更新，提高API的灵活性
 */

using System;
using System.ComponentModel.DataAnnotations;

namespace HealthSupervision.DTOs
{
    /// <summary>
    /// 用户信息更新数据传输对象
    /// 用于接收前端发送的用户信息更新请求，支持部分字段更新
    /// 与UserRegisterDTO不同，大部分字段为可选，提供更大的灵活性
    /// </summary>
    public class UserUpdateDTO
    {
        /// <summary>
        /// 用户名
        /// 可选字段，最大长度50个字符
        /// 如果不提供则保持原值不变
        /// </summary>
        [MaxLength(50, ErrorMessage = "用户名长度不能超过50个字符")]
        public string Username { get; set; }

        /// <summary>
        /// 手机号码
        /// 可选字段，使用Phone数据类型验证手机号格式
        /// 支持国际格式，如+8613800138000
        /// 如果不提供则保持原值不变
        /// </summary>
        [MaxLength(20, ErrorMessage = "手机号长度不能超过20个字符")]
        [Phone(ErrorMessage = "请输入有效的手机号码")]
        public string Phone { get; set; }

        /// <summary>
        /// 用户身高
        /// 使用decimal类型确保精度，单位为厘米
        /// 用于计算BMI等健康指标
        /// 注意：此字段为必填，因为身高变化会影响BMI计算
        /// </summary>
        public decimal Height { get; set; }

        /// <summary>
        /// 用户体重
        /// 使用decimal类型确保精度，单位为公斤
        /// 用于计算BMI等健康指标
        /// 注意：此字段为必填，因为体重变化会影响BMI计算
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 用户出生日期
        /// 可选字段，使用Date数据类型，只包含日期部分
        /// 使用可空类型DateTime?，如果不提供则保持原值不变
        /// 用于计算年龄和健康风险评估
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }
    }
}
