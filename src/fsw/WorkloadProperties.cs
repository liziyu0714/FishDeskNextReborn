using System;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace fsw
{
    public enum OSType
    {
        win,osx,linux
    }
    public class WorkloadProperties
    {
        //负载的友好名称
        [JsonProperty("name")]
        public required string WorkloadFriendlyName { get; set; }
        //负载的通用名称
        [JsonProperty("id")]
        public required string WorkloadGlobalName { get; set; }
        //负载的版本
        [JsonProperty("version")]
        public required string WorkloadVesrion { get; set; }
        //负载的作者
        [JsonProperty("author")]
        public required string WorkloadAuthor { get; set; }
        //负载源项目地址
        [JsonProperty("address")]
        public required string WorkloadProjectAddress { get; set; }
        //负载使用的程序集，可以是dll类库或是适用于当前平台的可执行程序
        [JsonProperty("assemble")]
        public required string WorkloadAssembly { get; set; }
        //负载适用的平台
        [JsonProperty("os")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OSType WorkloadOsPlatform { get; set; }
        //如果提供了类库，负载需要指明fsw需要读取的主类
        [JsonProperty("classname")]
        public required string WorkloadMainClassName { get; set; }
        //指示此负载是否需要被fsw持久化
        [JsonProperty("need_persistence")]
        public bool PersistenceNeeded { get; set; }

        //下面是fsw用于处理负载的属性，不需要被序列化
        //表示此负载是如何进入fsw作用域的
        [JsonIgnore]
        public bool IsBuiltIn;
        
    }
}
