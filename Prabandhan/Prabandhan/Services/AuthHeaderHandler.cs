using Parbandhan.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Parbandhan.Services
{
    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly ITokenService _tokenService;
        public AuthHeaderHandler(ITokenService tokenService)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_tokenService.HasAccessToken())
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokenService.GetAccessToken());
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
