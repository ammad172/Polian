using Polian.App.Models;

namespace Polian.App.Services.IServices
{
    public interface IBaseServices
    {
        Task<ResponseDTO?> SendAsync(RequestDTO requetDTO, bool? IsbearerToken); 
    }
}
