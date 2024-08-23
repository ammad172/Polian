using System.Net;
using System.Text;
using Newtonsoft.Json;
using Polian.App.Models;
using Polian.App.Services.IServices;
using static Polian.App.Utility.Utils;

namespace Polian.App.Services
{
    public class BaseServices : IBaseServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;
        public BaseServices(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }


        public async Task<ResponseDTO?> SendAsync(RequestDTO requetDTO, bool? IsbearerToken = true)
        {

            HttpClient httpClient = _httpClientFactory.CreateClient("PolinaApi");
            HttpRequestMessage message = new();
            if (requetDTO.ContentType == Contenttype.Json)
            {
                message.Headers.Add("Accept", "application/json");
            }
            else
            {

                message.Headers.Add("Accept", "*/*");
            }
            //token

            if ((bool)IsbearerToken)
            {
                var token = _tokenProvider.GetToken();
                message.Headers.Add("Authorization", $"Bearer {token}");
            }

            message.RequestUri = new Uri(requetDTO.Url);


            if (requetDTO.ContentType == Contenttype.MultiPartFormData)
            {
                var content = new MultipartFormDataContent();
                foreach (var item in requetDTO.Data.GetType().GetProperties())
                {
                    var value = item.GetValue(requetDTO.Data);

                    if (value is FormFile)
                    {
                        var file = (FormFile)value;

                        if (file != null)
                        {
                            content.Add(new StreamContent(file.OpenReadStream()), item.Name, file.FileName);
                        }
                    }
                    else
                    {
                        content.Add(new StringContent(value == null ? "" : value.ToString()), item.Name);
                    }
                }

                message.Content = content;
            }
            else
            {
                if (requetDTO.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requetDTO.Data), Encoding.UTF8, "application/json");
                }
            }


            HttpResponseMessage? reponse = null;

            switch (requetDTO.ApiType)
            {
                case AppiType.Post:
                    message.Method = HttpMethod.Post;
                    break;
                case AppiType.Get:
                    message.Method = HttpMethod.Get;
                    break;
                case AppiType.Put:
                    message.Method = HttpMethod.Put;
                    break;
                case AppiType.Delete:
                    message.Method = HttpMethod.Delete;
                    break;
            }

            reponse = await httpClient.SendAsync(message);

            try
            {
                switch (reponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };

                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };

                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };

                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    case HttpStatusCode.BadRequest:
                        return new() { IsSuccess = false, Message = "Bad Request" };
                    default:
                        
                        var apiconetent = await reponse.Content.ReadAsStringAsync();
                        var apiresponse = JsonConvert.DeserializeObject<ResponseDTO>(apiconetent);
                        return apiresponse;
                }
            }
            catch (Exception ex)
            {

                var dto = new ResponseDTO { IsSuccess = false, Message = ex.Message };
                return dto;
            }
        }
    }
}
