using Parbandhan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parbandhan.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginResponseModel>> LoginAsync(LoginRequestModel loginRequestModel);
        Task<string> RefreshAccessTokenAsync();
        bool IsTokenExpired();
    }
}
