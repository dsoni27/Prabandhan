using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parbandhan.Models
{
    public class StorageLocationModel
    {
        public int Slid { get; set; }
        public string? SlName { get; set; }

        // Device Type
        public int Dtid { get; set; }
        public string? DtName { get; set; }
        public string? DtType { get; set; }

        // Device
        public int DvcID { get; set; }
        public string? DvcName { get; set; }

        // Asset Type
        public int Atid { get; set; }
        public string? AtName { get; set; }

        // Storage Location Type
        public int Sltid { get; set; }
        public string? SltName { get; set; }

        // Path
        public string? Path { get; set; }

        // Optional navigation
        public object? AssetTypes { get; set; }
        public object? DeviceTypes { get; set; }
        public object? Devices { get; set; }
        public object? StorageLocationTypes { get; set; }
    }
}
