/*
 * 文件名: Program.cs
 * 描述: 应用程序入口点，配置和启动ASP.NET Core Web应用程序
 * 功能: 
 *   - 配置应用程序服务和中间件
 *   - 设置HTTP请求管道
 *   - 启动Web服务器
 * 作者: HealthSupervision团队
 * 创建时间: 2024年
 */

var builder = WebApplication.CreateBuilder(args);

// 添加服务到容器中
// 了解更多关于配置OpenAPI的信息，请访问 https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// 配置HTTP请求管道
if (app.Environment.IsDevelopment())
{
    // 在开发环境中启用OpenAPI文档
    // 提供API的交互式文档和测试界面
    app.MapOpenApi();
}

// 启用HTTPS重定向中间件
// 将HTTP请求自动重定向到HTTPS，提高安全性
app.UseHttpsRedirection();

// 示例数据：天气摘要数组
// 这些是示例数据，用于演示API功能
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// 定义天气预测API端点
// 这是一个示例API，展示如何创建GET请求端点
app.MapGet("/weatherforecast", () =>
{
    // 生成未来5天的天气预测数据
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),  // 计算未来日期
            Random.Shared.Next(-20, 55),                        // 生成-20到55度之间的随机温度
            summaries[Random.Shared.Next(summaries.Length)]     // 随机选择天气摘要
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");  // 为端点指定名称，用于生成OpenAPI文档

// 启动应用程序
app.Run();

/// <summary>
/// 天气预测记录类
/// 这是一个示例数据模型，用于演示API的数据结构
/// 包含日期、温度（摄氏度）和天气摘要信息
/// </summary>
/// <param name="Date">预测日期</param>
/// <param name="TemperatureC">温度（摄氏度）</param>
/// <param name="Summary">天气摘要描述</param>
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    /// <summary>
    /// 温度（华氏度）
    /// 根据摄氏度自动计算华氏度
    /// 转换公式：F = 32 + (C / 0.5556)
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
