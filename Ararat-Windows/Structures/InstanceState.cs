using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LXCLIENT.Structures.InstanceState
{
    public class AdditionalProp1
    {
        public int usage { get; set; }
        public List<Address> addresses { get; set; }
        public Counters counters { get; set; }
        public string host_name { get; set; }
        public string hwaddr { get; set; }
        public int mtu { get; set; }
        public string state { get; set; }
        public string type { get; set; }
    }

    public class AdditionalProp2
    {
        public int usage { get; set; }
        public List<Address> addresses { get; set; }
        public Counters counters { get; set; }
        public string host_name { get; set; }
        public string hwaddr { get; set; }
        public int mtu { get; set; }
        public string state { get; set; }
        public string type { get; set; }
    }

    public class AdditionalProp3
    {
        public int usage { get; set; }
        public List<Address> addresses { get; set; }
        public Counters counters { get; set; }
        public string host_name { get; set; }
        public string hwaddr { get; set; }
        public int mtu { get; set; }
        public string state { get; set; }
        public string type { get; set; }
    }

    public class Address
    {
        public string address { get; set; }
        public string family { get; set; }
        public string netmask { get; set; }
        public string scope { get; set; }
    }

    public class Counters
    {
        public int bytes_received { get; set; }
        public int bytes_sent { get; set; }
        public int errors_received { get; set; }
        public int errors_sent { get; set; }
        public int packets_dropped_inbound { get; set; }
        public int packets_dropped_outbound { get; set; }
        public int packets_received { get; set; }
        public int packets_sent { get; set; }
    }

    public class Cpu
    {
        public long usage { get; set; }
    }

    public class Disk
    {
        public AdditionalProp1 additionalProp1 { get; set; }
        public AdditionalProp2 additionalProp2 { get; set; }
        public AdditionalProp3 additionalProp3 { get; set; }
    }

    public class Memory
    {
        public int swap_usage { get; set; }
        public int swap_usage_peak { get; set; }
        public int usage { get; set; }
        public int usage_peak { get; set; }
    }

    public class Metadata
    {
        public Cpu cpu { get; set; }
        public Disk disk { get; set; }
        public Memory memory { get; set; }
        public Network network { get; set; }
        public int pid { get; set; }
        public int processes { get; set; }
        public string status { get; set; }
        public int status_code { get; set; }
    }

    public class Network
    {
        public AdditionalProp1 additionalProp1 { get; set; }
        public AdditionalProp2 additionalProp2 { get; set; }
        public AdditionalProp3 additionalProp3 { get; set; }
    }

    public class InstanceStateResponse
    {
        public Metadata metadata { get; set; }
        public string status { get; set; }
        public int status_code { get; set; }
        public string type { get; set; }
    }
}
