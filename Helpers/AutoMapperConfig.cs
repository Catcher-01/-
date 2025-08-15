/*
 * 文件名: AutoMapperConfig.cs
 * 描述: AutoMapper配置类，定义实体类与DTO之间的对象映射规则
 * 功能: 
 *   - 配置AutoMapper的映射规则
 *   - 简化实体类与DTO之间的数据转换
 *   - 提高代码的可维护性和可读性
 */

using AutoMapper;
using HealthSupervision.Entities;
using HealthSupervision.DTOs;

namespace HealthSupervision.Helpers
{
    /// <summary>
    /// AutoMapper配置类
    /// 继承自Profile类，用于定义对象映射规则
    /// AutoMapper是一个流行的.NET对象映射库，简化对象间的数据转换
    /// </summary>
    public class AutoMapperConfig : Profile
    {
        /// <summary>
        /// 构造函数，配置各种映射规则
        /// 在应用程序启动时，AutoMapper会自动扫描并应用这些配置
        /// </summary>
        public AutoMapperConfig()
        {
            // 配置实体类与DTO之间的双向映射规则
            // 使用CreateMap方法定义源类型到目标类型的映射关系
            
            // 用户账户实体与注册DTO的映射
            // 支持从UserAccount实体映射到UserRegisterDTO
            CreateMap<UserAccount, UserRegisterDTO>();
            
            // 用户更新DTO与用户账户实体的映射
            // 支持从UserUpdateDTO映射到UserAccount实体
            // 通常用于更新操作，将DTO数据映射到现有实体
            CreateMap<UserUpdateDTO, UserAccount>();
            
            // 健康快照实体与DTO的映射
            // 支持从HealthSnapshot实体映射到HealthSnapshotDTO
            // 用于向前端返回健康快照数据
            CreateMap<HealthSnapshot, HealthSnapshotDTO>();
            
            // 注意：AutoMapper会自动处理属性名称相同的字段映射
            // 如果属性名称不同，可以使用ForMember方法进行自定义配置
            // 例如：CreateMap<Source, Target>().ForMember(dest => dest.DestProperty, opt => opt.MapFrom(src => src.SourceProperty));
        }
    }
} 
