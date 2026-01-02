using Parbandhan.Services;
using Parbandhan.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parbandhan.Interfaces
{
    public interface ITokenService : INotifyPropertyChanged
    {
        string AccessToken { get; }
        string RefreshToken { get; }
        UserClaims UserClaims { get; }

        void SetToken(string accessToken, string refreshToken, UserClaims claims);
        void RefreshAccessToken(string newAccessToken);
        string GetAccessToken();
        string GetRefreshToken();
        bool HasAccessToken();
        bool HasRefreshToken();
        void Clear();
    }
}
