namespace VeterinaryClinic.Presentation.ServiceManagement
{
    public class HttpServiceManagement : BaseHttpFactory, IHttpServiceManagement
    {        
        public HttpServiceManagement(IHttpClientFactory httpClientFactory) : base(httpClientFactory, "HttpClientManagement")
        {            
        }        
    }
}
