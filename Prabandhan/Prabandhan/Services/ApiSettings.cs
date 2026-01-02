using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parbandhan.Services
{
    public class ApiSettings
    {
        public string BaseUrl { get; set; }
        public AuthEndpoints Auth { get; set; }
        public AssetTypeEndpoints AssetType { get; set; }
        public StorageLocationEndPoints StorageLocation { get; set; }
    }

    public class AuthEndpoints
    {
        public string Login { get; set; }
        public string RefreshToken { get; set; }
    }

    public class AssetTypeEndpoints
    {
        public string GetAllAssetType { get; set; }
        public string GetAssetType { get; set; }
    }

    public class StorageLocationEndPoints
    {
        public string GetAllStorageLocation { get; set; }
        public string GetStorageLocationFilter { get; set; }
    }
}
