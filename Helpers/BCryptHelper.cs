/*
 * 文件名: BCryptHelper.cs
 * 描述: BCrypt密码加密辅助类，提供密码哈希和验证功能
 * 功能: 
 *   - 使用BCrypt算法对用户密码进行安全哈希
 *   - 提供密码验证功能，确保用户登录安全
 *   - 实现密码安全存储的最佳实践
 */

using BCrypt.Net;

namespace HealthSupervision.Helpers
{
    /// <summary>
    /// BCrypt密码加密辅助类
    /// 提供静态方法进行密码哈希和验证，使用BCrypt算法确保密码安全
    /// BCrypt是一种自适应哈希算法，具有内置的盐值和可配置的工作因子
    /// </summary>
    public static class BCryptHelper
    {
        /// <summary>
        /// 对用户密码进行哈希加密
        /// 使用BCrypt算法生成安全的密码哈希值，包含随机盐值
        /// 每次调用都会生成不同的哈希值，即使密码相同
        /// </summary>
        /// <param name="password">用户输入的明文密码</param>
        /// <returns>加密后的密码哈希字符串，可直接存储到数据库</returns>
        /// <remarks>
        /// BCrypt算法特点：
        /// - 自动生成随机盐值，防止彩虹表攻击
        /// - 可配置工作因子，平衡安全性和性能
        /// - 生成的哈希值包含算法版本、工作因子、盐值和哈希值
        /// </remarks>
        public static string HashPassword(string password)
        {
            // 调用BCrypt.Net库的HashPassword方法
            // 该方法会自动生成随机盐值并应用默认工作因子
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// 验证用户输入的密码是否与存储的哈希值匹配
        /// 使用BCrypt算法验证明文密码与哈希值的匹配性
        /// 这是用户登录时的核心安全验证方法
        /// </summary>
        /// <param name="password">用户输入的明文密码</param>
        /// <param name="hash">数据库中存储的密码哈希值</param>
        /// <returns>如果密码匹配返回true，否则返回false</returns>
        /// <remarks>
        /// 验证过程：
        /// - 从哈希值中提取盐值和工作因子
        /// - 使用相同的盐值对输入密码进行哈希
        /// - 比较生成的哈希值与存储的哈希值
        /// - 即使哈希值不同，也能正确验证（因为盐值不同）
        /// </remarks>
        public static bool VerifyPassword(string password, string hash)
        {
            // 调用BCrypt.Net库的Verify方法
            // 该方法会自动从哈希值中提取盐值进行验证
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
