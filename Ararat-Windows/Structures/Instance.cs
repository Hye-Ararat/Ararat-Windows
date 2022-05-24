using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXCLIENT.Structures.Instance
{
    public class Config
    {
        [JsonProperty("security.nesting")]
        public string SecurityNesting { get; set; }
    }

    public class Devices
    {
        public Root2 root { get; set; }
    }

    public class ExpandedConfig
    {
        [JsonProperty("security.nesting")]
        public string SecurityNesting { get; set; }
    }

    public class ExpandedDevices
    {
        public Root2 root { get; set; }
    }

    public class Metadata
    {
        public string architecture { get; set; }
        public Config config { get; set; }
        public DateTime created_at { get; set; }
        public string description { get; set; }
        public Devices devices { get; set; }
        public bool ephemeral { get; set; }
        public ExpandedConfig expanded_config { get; set; }
        public ExpandedDevices expanded_devices { get; set; }
        public DateTime last_used_at { get; set; }
        public string location { get; set; }
        public string name { get; set; }
        public List<string> profiles { get; set; }
        public string project { get; set; }
        public string restore { get; set; }
        public bool stateful { get; set; }
        public string status { get; set; }
        public int status_code { get; set; }
        public string type { get; set; }
    }

    public class InstancesResponse
    {
        public List<Metadata> metadata { get; set; }
        public string status { get; set; }
        public int status_code { get; set; }
        public string type { get; set; }
    }
    public class InstanceResponse
    {
        public Metadata metadata { get; set; }
        public string status { get; set; }
        public int status_code { get; set; }
        public string type { get; set; }
    }
    public class Root2
    {
        public string path { get; set; }
        public string pool { get; set; }
        public string type { get; set; }
    }

}
