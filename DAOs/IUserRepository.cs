/*
 * 文件名: IUserRepository.cs
 * 描述: 用户仓储接口，定义用户账户相关的数据访问操作契约
 * 功能: 
 *   - 用户注册
 *   - 根据ID查询用户
 *   - 更新用户信息
 */

using System.Threading.Tasks;
using HealthSupervision.Entities;
using HealthSupervision.DTOs;

namespace HealthSupervision.DAOs
{
    /// <summary>
    /// 用户仓储接口
    /// 定义了用户账户相关的数据访问操作，包括注册、查询和更新功能
    /// 遵循仓储模式设计原则，提供数据访问的抽象层
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// 异步注册新用户
        /// 将用户注册信息转换为用户账户实体并保存到数据库
        /// </summary>
        /// <param name="dto">用户注册数据传输对象，包含用户名、密码、身体信息等</param>
        /// <returns>注册成功后的用户账户实体</returns>
        Task<UserAccount> RegisterAsync(UserRegisterDTO dto);

        /// <summary>
        /// 根据用户ID异步查询用户信息
        /// 从数据库中检索指定ID的用户账户信息
        /// </summary>
        /// <param name="userId">用户的唯一标识符</param>
        /// <returns>用户账户实体，如果不存在则返回null</returns>
        Task<UserAccount> GetByIdAsync(string userId);

        /// <summary>
        /// 异步更新用户信息
        /// 根据用户ID查找用户并更新其信息
        /// </summary>
        /// <param name="userId">要更新的用户ID</param>
        /// <param name="dto">包含更新信息的传输对象</param>
        Task UpdateAsync(string userId, UserUpdateDTO dto);
    }
}