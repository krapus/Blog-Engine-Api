using System.Net.Http;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess.ExternalServices.Http.Interface
{
    public interface IHttpService
    {
        HttpRequestMessage CreateHttpRequestMessage(string requestUri, HttpMethod method, HttpContent content);
        Task<HttpResponseMessage> ExecuteRequest(HttpRequestMessage request);
    }
}
