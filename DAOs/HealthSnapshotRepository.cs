/*
 * 文件名: HealthSnapshotRepository.cs
 * 描述: 健康快照数据访问对象，负责处理健康快照相关的数据库操作
 * 功能: 
 *   - 生成用户的健康快照数据
 *   - 整合用户的身体测量、活动记录等信息
 *   - 计算BMI等健康指标
 */

using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HealthSupervision.Entities;
using HealthSupervision.Data;

namespace HealthSupervision.DAOs
{
    /// <summary>
    /// 健康快照仓储类
    /// 实现健康快照的数据访问逻辑，包括生成快照和保存到数据库
    /// </summary>
    public class HealthSnapshotRepository : IHealthSnapshotRepository
    {
        /// <summary>
        /// 数据库上下文，用于访问数据库
        /// </summary>
        private readonly AppDbContext _dbContext;

        /// <summary>
        /// 构造函数，注入数据库上下文依赖
        /// </summary>
        /// <param name="dbContext">数据库上下文实例</param>
        public HealthSnapshotRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 异步生成用户的健康快照
        /// 整合用户的身体测量数据、活动记录等信息，生成完整的健康快照
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>生成的健康快照对象</returns>
        /// <exception cref="InvalidOperationException">当用户不存在时抛出异常</exception>
        public async Task<HealthSnapshot> GenerateSnapshotAsync(string userId)
        {
            // 获取用户基本信息
            var user = await _dbContext.UserAccounts.FindAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("用户不存在");
            }

            // 获取用户最新的身体测量数据
            // 使用OrderByDescending按测量时间倒序排列，取第一条记录
            var latestMeasurement = await _dbContext.UserBodyMeasurements
                .Where(m => m.UserID == userId)
                .OrderByDescending(m => m.MeasurementTime)
                .FirstOrDefaultAsync();

            // 获取用户今日的活动记录
            // 使用DateTime.Today获取今天的日期，精确匹配记录日期
            var todayActivity = await _dbContext.UserActivityRecords
                .Where(a => a.UserID == userId && a.RecordDate == DateTime.Today)
                .FirstOrDefaultAsync();

            // 创建健康快照对象，整合所有相关信息
            var snapshot = new HealthSnapshot
            {
                UserID = userId,
                MeasurementID = latestMeasurement?.MeasurementID,  // 关联最新的身体测量记录
                ActivityID = todayActivity?.ActivityID,            // 关联今日活动记录
                Weight = user.Weight,                             // 用户当前体重
                // 计算BMI指数：体重(kg) / 身高(m)²
                // 注意：全部使用decimal类型避免double/decimal混合运算的精度问题
                // 身高从厘米转换为米：除以100
                BMI = (user.Height > 0)
                    ? user.Weight / ((user.Height / 100m) * (user.Height / 100m))
                    : 0,
                Steps = todayActivity?.Steps ?? 0  // 今日步数，如果没有活动记录则默认为0
            };

            // 将快照添加到数据库上下文
            _dbContext.HealthSnapshots.Add(snapshot);
            // 保存更改到数据库
            await _dbContext.SaveChangesAsync();

            return snapshot;
        }
    }
}