/*
 * 文件名: UserController.cs
 * 描述: 用户控制器，提供用户账户相关的HTTP API接口
 * 功能: 
 *   - 用户注册
 *   - 更新用户信息
 *   - 处理用户相关的HTTP请求
 */

using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HealthSupervision.DTOs;
using HealthSupervision.Services;
using HealthSupervision.Entities;

namespace HealthSupervision.Controllers
{
    /// <summary>
    /// 用户控制器
    /// 提供用户账户相关的REST API接口，包括注册、更新等功能
    /// 遵循RESTful设计原则，使用依赖注入获取服务层实例
    /// </summary>
    [ApiController]                    // 标识这是一个API控制器
    [Route("api/[controller]")]        // 路由模板，自动生成api/User路径
    public class UserController : ControllerBase
    {
        /// <summary>
        /// 用户服务实例，通过依赖注入获取
        /// 负责处理用户相关的业务逻辑，如注册、更新等
        /// </summary>
        private readonly UserService _userService;

        /// <summary>
        /// 构造函数，通过依赖注入获取用户服务
        /// </summary>
        /// <param name="userService">用户服务实例</param>
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 用户注册的HTTP POST接口
        /// 接收用户注册信息，创建新用户账户
        /// </summary>
        /// <param name="dto">用户注册数据传输对象，包含用户名、密码、身体信息等</param>
        /// <returns>注册结果的HTTP响应</returns>
        [HttpPost("register")]         // HTTP POST方法，路径为api/User/register
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO dto)
        {
            try
            {
                // 调用服务层进行用户注册
                // 服务层会处理用户名重复检查、密码加密等业务逻辑
                var user = await _userService.RegisterAsync(dto);
                
                // 注册成功，返回200 OK状态码和用户ID及成功消息
                // 使用匿名对象包装返回数据，提供清晰的响应结构
                return Ok(new { UserID = user.UserID, Message = "注册成功" });
            }
            catch (InvalidOperationException ex)
            {
                // 捕获业务逻辑异常，返回400 Bad Request状态码
                // 通常用于处理用户名已存在、数据验证失败等错误
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 更新用户信息的HTTP PUT接口
        /// 根据当前登录用户更新其基本信息
        /// </summary>
        /// <param name="dto">用户更新数据传输对象，包含要更新的字段</param>
        /// <returns>更新结果的HTTP响应</returns>
        [HttpPut("update")]            // HTTP PUT方法，路径为api/User/update
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO dto)
        {
            try
            {
                // 从JWT Token中获取当前登录用户的ID
                // User.Claims包含JWT Token中的所有声明信息
                // FirstOrDefault查找类型为"UserID"的声明，获取其值
                var userId = User.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;
                
                // 调用服务层更新用户信息
                // 服务层会验证用户存在性、数据有效性等
                await _userService.UpdateUserAsync(userId, dto);
                
                // 更新成功，返回204 No Content状态码
                // 表示请求成功但响应体不包含内容
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                // 捕获业务逻辑异常，返回400 Bad Request状态码
                // 通常用于处理用户不存在、数据验证失败等错误
                return BadRequest(ex.Message);
            }
        }
    }
}