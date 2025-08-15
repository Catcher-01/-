/*
 * 文件名: UserRegisterDTO.cs
 * 描述: 用户注册数据传输对象，用于接收前端用户注册请求的数据
 * 功能: 
 *   - 定义用户注册时需要的数据结构
 *   - 提供数据验证特性，确保数据完整性
 *   - 作为控制器和服务层之间的数据传输载体
 */

using System;
using System.ComponentModel.DataAnnotations;

namespace HealthSupervision.DTOs
{
    /// <summary>
    /// 用户注册数据传输对象
    /// 用于接收前端发送的用户注册信息，包含用户名、密码、身体信息等
    /// 使用数据注解特性进行输入验证，确保数据质量和安全性
    /// </summary>
    public class UserRegisterDTO
    {
        /// <summary>
        /// 用户名
        /// 必填字段，最大长度50个字符
        /// 用于用户登录和身份识别
        /// </summary>
        [Required(ErrorMessage = "用户名是必填项")]
        [MaxLength(50, ErrorMessage = "用户名长度不能超过50个字符")]
        public string Username { get; set; }

        /// <summary>
        /// 用户密码
        /// 必填字段，使用Password数据类型进行前端输入验证
        /// 后端会使用BCrypt算法进行加密存储
        /// </summary>
        [Required(ErrorMessage = "密码是必填项")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// 手机号码
        /// 必填字段，使用Phone数据类型验证手机号格式
        /// 支持国际格式，如+8613800138000
        /// </summary>
        [Required(ErrorMessage = "手机号是必填项")]
        [Phone(ErrorMessage = "请输入有效的手机号码")]
        [MaxLength(20, ErrorMessage = "手机号长度不能超过20个字符")]
        public string Phone { get; set; }

        /// <summary>
        /// 用户身高
        /// 使用decimal类型确保精度，单位为厘米
        /// 用于计算BMI等健康指标
        /// </summary>
        public decimal Height { get; set; }

        /// <summary>
        /// 用户体重
        /// 使用decimal类型确保精度，单位为公斤
        /// 用于计算BMI等健康指标
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 用户出生日期
        /// 使用Date数据类型，只包含日期部分，不包含时间
        /// 用于计算年龄和健康风险评估
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
    }
}