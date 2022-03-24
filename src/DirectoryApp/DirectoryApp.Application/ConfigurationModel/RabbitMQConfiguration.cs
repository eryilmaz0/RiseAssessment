using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryApp.Application.ConfigurationModel
{
    public class RabbitMQConfiguration : BaseConfiguration
    {
        public string Host { get; set; }
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public string QueueBindKey { get; set; }
    }
}
