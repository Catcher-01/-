/*
 * 文件名: HealthSnapshotController.cs
 * 描述: 健康快照控制器，提供健康快照相关的HTTP API接口
 * 功能: 
 *   - 生成用户的健康快照数据
 *   - 处理健康快照相关的HTTP请求
 */

using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HealthSupervision.Services;
using HealthSupervision.DTOs;

namespace HealthSupervision.Controllers
{
    /// <summary>
    /// 健康快照控制器
    /// 提供健康快照相关的REST API接口，遵循RESTful设计原则
    /// 使用依赖注入获取服务层实例，实现业务逻辑与控制器分离
    /// </summary>
    [ApiController]                    // 标识这是一个API控制器
    [Route("api/[controller]")]        // 路由模板，自动生成api/HealthSnapshot路径
    public class HealthSnapshotController : ControllerBase
    {
        /// <summary>
        /// 健康快照服务实例，通过依赖注入获取
        /// 负责处理健康快照相关的业务逻辑
        /// </summary>
        private readonly HealthSnapshotService _snapshotService;

        /// <summary>
        /// 构造函数，通过依赖注入获取健康快照服务
        /// </summary>
        /// <param name="snapshotService">健康快照服务实例</param>
        public HealthSnapshotController(HealthSnapshotService snapshotService)
        {
            _snapshotService = snapshotService;
        }

        /// <summary>
        /// 生成健康快照的HTTP POST接口
        /// 根据当前登录用户生成包含身体测量、活动记录等信息的健康快照
        /// </summary>
        /// <returns>包含健康快照数据的HTTP响应</returns>
        [HttpPost("generate")]         // HTTP POST方法，路径为api/HealthSnapshot/generate
        public async Task<IActionResult> GenerateSnapshot()
        {
            try
            {
                // 从JWT Token中获取当前登录用户的ID
                // User.Claims包含JWT Token中的所有声明信息
                // FirstOrDefault查找类型为"UserID"的声明，获取其值
                var userId = User.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
                
                // 调用服务层生成健康快照
                var snapshotDTO = await _snapshotService.GenerateSnapshotAsync(userId);
                
                // 返回200 OK状态码和快照数据
                return Ok(snapshotDTO);
            }
            catch (InvalidOperationException ex)
            {
                // 捕获业务逻辑异常，返回400 Bad Request状态码
                // 通常用于处理用户不存在、数据无效等业务错误
                return BadRequest(ex.Message);
            }
        }
    }
}