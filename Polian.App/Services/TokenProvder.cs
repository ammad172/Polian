using NuGet.Common;
using Polian.App.Services.IServices;
using Polian.App.Utility;

namespace Polian.App.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SetToken(string token)
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Append(Utils.Tokencookie, token);
        }

        public string? GetToken()
        {
            string? token = null;

            bool? hasvalue = _httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(Utils.Tokencookie, out token);

            return hasvalue is true ? token : null;
        }

        public void ClearToken()
        {
            _httpContextAccessor.HttpContext?.Response.Cookies.Delete(Utils.Tokencookie);
        }
    }
}
