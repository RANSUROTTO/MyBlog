# My Blog - 我的博客 &nbsp;&nbsp;&nbsp;&nbsp; (´；ω；`)  23333
***
用于分享从现在开始,成长所积累的知识、经验和见解的小屋
> 引用苏莉安的一个短句： "人在互联网上总要有个自己的家。微博也好，推特也好，都是露天剧场的一张座椅，图书馆的一个位子，要守无数规矩，来自上面的、左面右面的、下面的。而自己的网站就像独栋小别墅，想翻建、想挖个地洞、想在客厅摆两张沙发不许不喜欢的人来坐，都没问题。尽管下面的土地是别人的，尽管破门而入的可能性也是有的，但，总比步步是雷的地方强多了啊。"

## 介绍
  基于Asp.Net Mvc5 + EntityFramework + Autofac的一个分布式(嘛...怎么配是自己决定的嘛)博客系统... <br/>
  参考开源项目 Nopcommerce 编写(搬了一大堆东西,框架上就改了改验证出了一下分布式的东西...我只是个很菜的业务码农罢了)

## 环境配置
* 1.搞一台服务器,管你是本地和是云的.依据喜好挑选Windows Server系统[建议2008+](线上部署的是2012)
* 2.装一个mysql数据库. 5.5或者5.6吧...据说5.7变化有点大(没测试过环境,不敢动不敢动)
* 3.装一个Asp.net的运行环境(IIS什么的)
* 4.装个Redis和memcached...(业务层直接使用了,懒得关了...可以自己去改DependencyRegistrar依赖注册的内容)
* 5.把项目代码克隆到本地(git Clone)
* 6.先听我吹牛逼到这里吧...毕竟项目没完工...嘻嘻嘻

## 项目结构 (2017/9/7)
(不怕吓到你们，都是按文件夹名称翻译过来的...英语好应该能见名识意了)
```
MyBlog 
├── Blog.Libraries.Core         核心层
|   ├── Caching                     缓存设施层
|   |   ├── RedisCaching            Redis缓存设施
|   |   ├── MemCaching              Memcached缓存设施
|   |   ├── CouchbaseCaching        CouchbaseCaching缓存设施
|   |   └── Null                    没有更多啦~
|   ├── Common                  常用设施层
|   ├── ComponentModel          组件层
|   ├── Configuration           配置层
|   ├── Data                    持久化层
|   ├── Domain                  实体层
|   ├── Extensions              扩展层
|   ├── Fakes                   测试模拟层
|   ├── Helper                  常用帮助层
|   ├── Html                    常用Html帮助层
|   └── Infrastructure          基础设施层
|
├── Blog.Libraries.Data         持久化数据层
|   ├── Context                 上下文目录
|   ├── Domain                  实体目录
|   ├── intializers             初始化目录
|   ├── Mapping                 实体映射目录
|   ├── Provider                持久化驱动提供者目录
|   └── Repository              持久化仓储目录
|
├── Blog.Libraries.Services     业务层
|   ├── Services                业务代码目录
|   └── Null                    没有更多啦
|
├── Blog.Libraries.Gateway      第三方服务层
|   └── Null                    没有更多啦
|
├── Blog.Presentation.Framework 视图框架层
├── Blog.Presentation.Web       前台MVC项目
├── Blog.Presentation.Framework 后台MVC项目
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
|
```
