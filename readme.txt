HealthSupervision/
├── Controllers/        # 接口层：接收请求，调用Service
│   ├── UserController.cs   # 用户相关接口
│   └── HealthSnapshotController.cs # 健康快照接口
├── Services/           # 业务逻辑层：处理核心逻辑
│   ├── UserService.cs      # 用户注册、更新
│   └── HealthSnapshotService.cs # 快照生成、存储
├── DAOs/               # 数据访问层：操作数据库
│   ├── IUserRepository.cs  # 用户仓储接口
│   ├── UserRepository.cs    # 用户仓储实现
│   └── HealthSnapshotRepository.cs # 快照仓储
├── Entities/           # 数据库实体：映射表结构
│   ├── UserAccount.cs    # 用户账号实体（对应User_Account表）
│   ├── UserBodyMeasurement.cs # 身体测量实体
│   └── HealthSnapshot.cs   # 健康快照实体
├── DTOs/               # 数据传输对象：前后端交互格式
│   ├── UserRegisterDTO.cs  # 注册输入
│   ├── UserUpdateDTO.cs    # 信息更新输入
│   └── HealthSnapshotDTO.cs # 快照输入/输出
├── Data/               # 数据库上下文
│   └── AppDbContext.cs     # EF Core DbContext
├── Helpers/            # 工具类：加密、映射等
│   ├── BCryptHelper.cs     # 密码加密
│   └── AutoMapperConfig.cs # 自动映射配置
└── appsettings.json    # 配置文件（含Oracle连接字符串）填充这些程序内容按照报告里面的内容