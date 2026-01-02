using Parbandhan.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parbandhan.Services
{
    public class TokenService : ITokenService
    {
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }
        public UserClaims UserClaims { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void NotifyAll()
        {
            Notify(nameof(AccessToken));
            Notify(nameof(RefreshToken));
            Notify(nameof(UserClaims));
        }

        public void SetToken(string accessToken, string refreshToken, UserClaims claims)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            UserClaims = claims;

            NotifyAll();
        }

        public void RefreshAccessToken(string newAccessToken)
        {
            AccessToken = newAccessToken;
            Notify(nameof(AccessToken));
        }

        public string GetAccessToken() => AccessToken;

        public string GetRefreshToken() => RefreshToken;

        public bool HasAccessToken() => !string.IsNullOrWhiteSpace(AccessToken);
        public bool HasRefreshToken() => !string.IsNullOrWhiteSpace(RefreshToken);

        public void Clear()
        {
            AccessToken = null;
            RefreshToken = null;
            UserClaims = null;

            NotifyAll();
        }
    }
}
