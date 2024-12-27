using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirSense.Domain
{
    public class StorageCondition
    {
        public float Co { get; set; }
        public float No { get; set; }
        public float No2 { get; set; }
        public float O3 { get; set; }
        public float So2 { get; set; }
        public float Pm2_5 { get; set; }
        public float Pm10 { get; set; }
        public float Nh3 { get; set; }
        public string MeasuredAt { get; set; }
        public string DeviceId { get; set; }
        public bool IsSynced { get; set; }
    }

}
