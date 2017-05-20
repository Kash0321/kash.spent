using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kash.spent.Model
{
    public class EntityData
    {
        public EntityData()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [CreatedAt]
        public DateTimeOffset CreatedAt { get; set; }

        [UpdatedAt]
        public DateTimeOffset UpdatedAt { get; set; }

        [Version]
        public string AzureVersion { get; set; }
    }

}
