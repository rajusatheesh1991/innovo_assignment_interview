using System.Net.Http;

namespace InnovoAssignment.Web.Services
{
    public partial interface IClient
    {
        public HttpClient HttpClient { get; }

    }
}
