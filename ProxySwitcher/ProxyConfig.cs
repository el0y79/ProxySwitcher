namespace ProxySwitcher
{
    public class ProxyConfig
    {
        public string Name { get; set; }
        public string OwnIP { get; set; }
        public string Proxy { get; set; }
        public bool BypassLocal { get; set; }
        public string AdditionalExceptions { get; set; }
    }
}