using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryApp.Application.ConfigurationModel
{
    public class RedisConfiguration : BaseConfiguration
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public int DatabaseIndex { get; set; }
        public string Password { get; set; }
    }
}
