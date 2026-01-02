using Parbandhan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parbandhan.Interfaces
{
    public interface IStorageLocationService
    {
        Task<ApiResponse<List<StorageLocationModel>>> GetAllAsync();

        Task<ApiResponse<List<StorageLocationModel>>> GetByAssetTypesAsync(IEnumerable<int> assetTypeIds);
    }
}
