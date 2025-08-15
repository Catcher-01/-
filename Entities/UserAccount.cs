/*
 * 文件名: UserAccount.cs
 * 描述: 用户账户实体类，定义用户账户的数据库表结构和业务属性
 * 功能: 
 *   - 定义用户账户的数据库表映射
 *   - 包含用户的基本信息、健康数据和账户安全信息
 *   - 作为用户相关业务操作的核心实体
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthSupervision.Entities
{
    /// <summary>
    /// 用户账户实体类
    /// 对应数据库中的User_Account表，包含用户的基本信息、健康数据和账户安全信息
    /// 使用数据注解特性定义表结构、字段约束和关系映射
    /// </summary>
    [Table("User_Account")] // 指定数据库表名，使用下划线命名法符合Oracle数据库规范
    public class UserAccount
    {
        /// <summary>
        /// 用户唯一标识符（主键）
        /// 使用GUID字符串作为主键，确保全局唯一性
        /// 在对象创建时自动生成，避免ID冲突
        /// </summary>
        [Key]                           // 标识为主键
        [MaxLength(50)]                 // 限制最大长度为50个字符
        public string UserID { get; set; } = Guid.NewGuid().ToString(); // 自动生成GUID字符串

        /// <summary>
        /// 用户名
        /// 必填字段，用于用户登录和身份识别
        /// 在系统中必须唯一，不能重复
        /// </summary>
        [Required(ErrorMessage = "用户名是必填项")]
        [MaxLength(50, ErrorMessage = "用户名长度不能超过50个字符")]
        public string Username { get; set; }

        /// <summary>
        /// 用户密码
        /// 必填字段，存储加密后的密码哈希值
        /// 使用BCrypt算法加密，确保密码安全性
        /// </summary>
        [Required(ErrorMessage = "密码是必填项")]
        [MaxLength(100, ErrorMessage = "密码长度不能超过100个字符")]
        public string Password { get; set; }

        /// <summary>
        /// 用户头像
        /// 可选字段，存储用户头像图片的路径或URL
        /// 用于个性化界面显示
        /// </summary>
        [MaxLength(200, ErrorMessage = "头像路径长度不能超过200个字符")]
        public string Avatar { get; set; }

        /// <summary>
        /// 账户创建时间
        /// 自动设置为UTC时间，记录用户注册的确切时间
        /// 用于账户管理和审计追踪
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// 手机号码
        /// 必填字段，支持国际格式，如+8613800138000
        /// 用于账户验证和密码重置等功能
        /// </summary>
        [Required(ErrorMessage = "手机号是必填项")]
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
        /// 可选字段，使用可空类型DateTime?
        /// 用于计算年龄和健康风险评估
        /// </summary>
        public DateTime? Birthdate { get; set; }
    }
}