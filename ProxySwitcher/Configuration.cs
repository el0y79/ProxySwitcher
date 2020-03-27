using System.Collections.Generic;
using System.Linq;

namespace ProxySwitcher
{
    public class Configuration
    {
        public List<ProxyConfig> Proxies { get; } = new List<ProxyConfig>();
        public bool AutoUpdate { get; set; } = true;
        public bool CyclicCheck { get; set; } = false;
        public int RetryTimeSec { get; set; } = 10;
        public bool ConsiderWinHTTP { get; set; } = true;

        public ProxyConfig GetByName(string proxyConfig)
        {
            return Proxies.FirstOrDefault(x => x.Name.Equals(proxyConfig));
        }

    }
}