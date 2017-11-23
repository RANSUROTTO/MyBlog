# My Blog - 我的博客 &nbsp;&nbsp;&nbsp;&nbsp; (´；ω；`)  23333
***
用于分享从现在开始,成长所积累的知识、经验和见解的小屋
> 引用苏莉安的一个短句： "人在互联网上总要有个自己的家。微博也好，推特也好，都是露天剧场的一张座椅，图书馆的一个位子，要守无数规矩，来自上面的、左面右面的、下面的。而自己的网站就像独栋小别墅，想翻建、想挖个地洞、想在客厅摆两张沙发不许不喜欢的人来坐，都没问题。尽管下面的土地是别人的，尽管破门而入的可能性也是有的，但，总比步步是雷的地方强多了啊。"

## 介绍
  基于Asp.Net Mvc5 + EntityFramework + Autofac的一个(可以是分布式的)博客系统... <br/>
  参考开源项目 Nopcommerce 编写(搬了一大堆东西,给这东西定位花的时间太多了...以后再考虑造轮子换模块A.A )
  
  <h2>2017/9/8 我发现工作量太多大了.决定先搞个能写东西的上线,每天再进行更新迭代...顺便锻炼一下数据迁移能力</h2>

## 环境配置
* 1.搞一台服务器,管你是本地和是云的.依据喜好挑选Windows Server系统(建议2008+,线上部署的是2012)
* 2.装一个mysql数据库. 5.5或者5.6吧...据说5.7变化有点大(没测试过环境,不敢动不敢动)
* 3.装一个Asp.net的运行环境(IIS什么的)
* 4.装个Redis和memcached...(业务层直接使用了,懒得关了...可以自己去改DependencyRegistrar依赖注册的内容)
* 5.把项目代码克隆到本地(git Clone)
* 6.先听我吹牛逼到这里吧...毕竟项目没完工...嘻嘻嘻

## 项目结构 (2017/9/7)
(不怕吓到你们，都是按文件夹名称翻译过来的...英语好应该能见名识意了)
```
MyBlog 
├── Blog.Libraries.Core             核心层
|   ├── Caching                     缓存设施层
|   |   ├── RedisCaching            Redis缓存设施
|   |   ├── MemCaching              Memcached缓存设施
|   |   ├── CouchbaseCaching        CouchbaseCaching缓存设施
|   |   └── Null                    没有更多啦~
|   ├── Common                      常用设施层
|   ├── ComponentModel              组件层
|   ├── Configuration               配置层
|   ├── Data                        持久化层
|   ├── Domain                      实体层
|   ├── Extensions                  扩展层
|   ├── Fakes                       测试模拟层
|   ├── Helper                      常用帮助层
|   ├── Html                        常用Html帮助层
|   └── Infrastructure              基础设施层
|    
├── Blog.Libraries.Data             持久化层
|   ├── Context                     上下文目录
|   ├── Domain                      实体目录
|   ├── intializers                 初始化目录
|   ├── Mapping                     实体映射目录
|   ├── Provider                    持久化驱动提供者目录
|   └── Repository                  持久化仓储目录
|    
├── Blog.Libraries.Services         业务层
|   ├── Services                    业务代码目录
|   └── Null                        没有更多啦
|    
├── Blog.Libraries.Gateway          第三方服务层
|   └── Null                        没有更多啦
|    
├── Blog.Presentation.Framework     视图框架层
|
├── Blog.Presentation.Web           前台MVC项目
|
├── Blog.Presentation.Framework     后台MVC项目
|
├── Blog.Tests                      单元测试核心
|
├── Blog.Libraries.Core.Tests       针对核心层的单元测试
|
├── Blog.Libraries.Data.Tests       针对持久层的单元测试
|
└── Blog.Libraries.Services.Tests   针对业务层的单元测试
```

## Initialization Configuration

### Center Config
 `Blog.Libraries.Core.Configuration` 命名空间下,存在配置类 `WebConfig.cs` 这是一个用于控制所有其它配置的配置类<br/>
 通过启动项目的配置文件(`App.config`,`Web.config`)进行初始化<br/>
 您需要在启动项目的配置文件中这样进行配置：<br/>
 ```xml
  <configSections>
    <!-- WebConfig.cs 配置文件 type 指向 WebConfig.cs 程序集地址 -->
    <section name="WebConfig" type="Blog.Libraries.Core.Configuration.WebConfig" requirePermission="false" />
  </configSections>

  <!-- WebConfig.cs 配置节点 -->
  <WebConfig>
    <Startup IgnoreStartupTasks="false" />
    <Cluster OpenClusterPattern="true"/>
    <!-- 其它属性配置 。。。。。。 -->
  </WebConfig>
 ```
 [ 当需要进行集群设置时，这也是主要的配置类。你需要保证它们的同步！]
 
### Redis Caching
  1_博客线上版本因为仅仅只有一个服务器的原因，而且使用了 `Windows Server 2012` 系统，所以仅考虑使用了 `Redis-Windows-3.2.100` 作为服务端，使用    `StackExchange.Redis` 作为应用程序操作redis的类库。<br/>
  2_配置 `Redis` 缓存功能：你需要配置 `Web.config` 内 `<WebConfig>` 节点的 `<RedisCaching>` 节点.将其 `Enable` 属性设置为 `true`，并且配置 `ConfigString`。
 ```xml
  <!-- Web.config -->
  <WebConfig>
    <!-- Redis缓存配置 -->
    <RedisCaching Enable="true" ConfigString="127.0.0.1:6379"/>
  </WebConfig>
 ```
  3_使用了 `RedisCache` 后建议弃用 `MemoryCache`
  
### Memcached Chaching
 1_开启 `memcached` 缓存功能：你需要配置 `Web.config` 内 `<WebConfig>` 节点内的 `<MemCaching>` 节点,将其 `Enable` 属性设置为 `true`。我们使用的 `memchached` 的操作类库为 `Enyim.Caching`，你需要在 `Web.config` 中另行配置 `Enyim.Caching`。
  ```xml
  <configSections>
    <sectionGroup name="enyim.com">
      <section name="memcached" type="Enyim.Caching.Configuration.MemcachedClientSection, Enyim.Caching"/>
    </sectionGroup>
  </configSections>
 
  <enyim.com>
    <memcached>
      <servers>
        <add address="127.0.0.1" port="11211" />
      </servers>
    </memcached>
  </enyim.com>
 
  <WebConfig>
    <!-- Memcached缓存配置 -->
    <MemCaching Enable="true" />
  </WebConfig>
  
  ```

### Application Settings
 1_该设置主要针对业务功能的配置。目前所有该类设置都放置在 `Blog.Libraries.Data.Settings` 命名空间下。该设置实际存储在数据库中,正常使用将会添加至缓存中。<br/>
 2_创建一个新的业务配置：新建一个以 `Settings` 结尾的类,实现 `ISettings` 接口即可.`TypeFinder` 和 `Autofac` 将会帮你完成剩下的包括依赖注册等操作。
 ```csharp
  //create
  public class TestSettings : ISetting{
    public bool RunStartTask { get; set; }
  }
  //use
  public static void Main(string[] args){
    TestSettings settings = EngineContext.Current.Resolve<TestSettings>();
  }
 ```
 
 ### Database Connection Settings 
  1_该项目数据持久化默认提供 `Entity Framework` ORM框架，项目线上使用 `mysql` 数据库.<br/>
  2_项目提供 `mysql` 和 `sqlserver` 及 `sqlite` 持久化操作的实现（ `sqlserver` 和 `sqlite` 仅经过单元测试,未在线上版本使用）<br/>
  3_项目内所有数据库实体均继承于 `BaseEntity` 类（便于管理）。项目实质上使用 EF6 DataMigration 对数据库进行实体变更管理（该功能未提交，请自行启用）。<br/>
  4_设置你的数据库连接信息（包括数据库提供商和数据库连接字符串），在启动项目 `AppData\` 目录下找到 `db.config` 文件（该文件不包含于解决方案视图目录下），如在文件资源管理器中仍然未找到该文件，您需要启动项目进行一次数据初始化。 <br/>
 ```
   
 ```
 
 


