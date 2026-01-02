using Parbandhan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parbandhan.Interfaces
{
    public interface IAssetTypeService
    {
        Task<ApiResponse<List<AssetTypeModel>>> GetAssetTypesAsync();
    }
}
