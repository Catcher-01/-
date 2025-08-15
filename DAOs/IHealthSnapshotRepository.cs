/*
 * 文件名: IHealthSnapshotRepository.cs
 * 描述: 健康快照仓储接口，定义健康快照相关的数据访问操作契约
 * 功能: 定义生成健康快照的方法接口
 */

using System.Threading.Tasks;
using HealthSupervision.Entities;
using HealthSupervision.DTOs;

namespace HealthSupervision.DAOs
{
    /// <summary>
    /// 健康快照仓储接口
    /// 定义了健康快照相关的数据访问操作，遵循仓储模式设计原则
    /// </summary>
    public interface IHealthSnapshotRepository
    {
        /// <summary>
        /// 异步生成用户的健康快照
        /// 根据用户ID生成包含身体测量、活动记录等信息的健康快照
        /// </summary>
        /// <param name="userId">用户唯一标识符</param>
        /// <returns>包含用户健康信息的快照对象</returns>
        Task<HealthSnapshot> GenerateSnapshotAsync(string userId);
    }
}