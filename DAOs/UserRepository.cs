/*
 * 文件名: UserRepository.cs
 * 描述: 用户仓储实现类，负责处理用户账户相关的数据库操作
 * 功能: 
 *   - 实现用户注册逻辑，包括用户名重复检查和密码加密
 *   - 根据ID查询用户信息
 *   - 更新用户基本信息
 */

using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HealthSupervision.Entities;
using HealthSupervision.DTOs;
using HealthSupervision.Data;
using HealthSupervision.Helpers;

namespace HealthSupervision.DAOs
{
    /// <summary>
    /// 用户仓储实现类
    /// 实现IUserRepository接口，提供用户账户相关的数据访问功能
    /// 使用Entity Framework Core进行数据库操作，遵循仓储模式设计原则
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// 数据库上下文，用于访问用户账户相关的数据库表
        /// </summary>
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// 构造函数，通过依赖注入获取数据库上下文
        /// </summary>
        /// <param name="dbContext">数据库上下文实例</param>
        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 异步注册新用户
        /// 检查用户名唯一性，加密密码，创建用户账户并保存到数据库
        /// </summary>
        /// <param name="dto">用户注册信息传输对象</param>
        /// <returns>注册成功后的用户账户实体</returns>
        /// <exception cref="InvalidOperationException">当用户名已存在时抛出异常</exception>
        public async Task<UserAccount> RegisterAsync(UserRegisterDTO dto)
        {
            // 检查用户名是否已存在于数据库中
            // 使用AnyAsync方法进行异步查询，提高性能
            if (await _dbContext.UserAccounts.AnyAsync(u => u.Username == dto.Username))
            {
                throw new InvalidOperationException("用户名已存在");
            }

            // 创建新的用户账户实体对象
            // 将DTO中的数据传输到实体对象，并进行必要的转换
            var user = new UserAccount
            {
                Username = dto.Username,                                    // 用户名
                Phone = dto.Phone,                                         // 手机号
                Password = BCryptHelper.HashPassword(dto.Password),        // 使用BCrypt算法加密密码，提高安全性
                Height = dto.Height,                                       // 身高（厘米）
                Weight = dto.Weight,                                       // 体重（公斤）
                Birthdate = dto.Birthdate                                 // 出生日期
            };

            // 将新用户实体添加到数据库上下文的跟踪集合中
            _dbContext.UserAccounts.Add(user);
            // 异步保存所有更改到数据库
            await _dbContext.SaveChangesAsync();

            return user;
        }

        /// <summary>
        /// 根据用户ID异步查询用户信息
        /// 使用Entity Framework的FindAsync方法进行高效的主键查询
        /// </summary>
        /// <param name="userId">用户的唯一标识符</param>
        /// <returns>用户账户实体，如果不存在则返回null</returns>
        public async Task<UserAccount> GetByIdAsync(string userId)
        {
            return await _dbContext.UserAccounts.FindAsync(userId);
        }

        /// <summary>
        /// 异步更新用户信息
        /// 查找指定用户并更新其信息，支持部分字段更新
        /// </summary>
        /// <param name="userId">要更新的用户ID</param>
        /// <param name="dto">包含更新信息的传输对象</param>
        /// <exception cref="InvalidOperationException">当用户不存在时抛出异常</exception>
        public async Task UpdateAsync(string userId, UserUpdateDTO dto)
        {
            // 根据用户ID查找用户实体
            var user = await _dbContext.UserAccounts.FindAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("用户不存在");
            }

            // 更新用户字段，使用空合并运算符(??)实现条件更新
            // 只有当DTO中的值不为null时才更新对应字段
            user.Username = dto.Username ?? user.Username;    // 用户名（如果提供则更新，否则保持原值）
            user.Phone = dto.Phone ?? user.Phone;            // 手机号（如果提供则更新，否则保持原值）
            user.Height = dto.Height;                        // 身高（直接更新，因为Height是值类型）
            user.Weight = dto.Weight;                        // 体重（直接更新，因为Weight是值类型）
            user.Birthdate = dto.Birthdate ?? user.Birthdate; // 出生日期（如果提供则更新，否则保持原值）

            // 保存更改到数据库
            await _dbContext.SaveChangesAsync();
        }
    }
}