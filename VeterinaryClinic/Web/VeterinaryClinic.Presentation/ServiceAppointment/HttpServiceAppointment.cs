namespace VeterinaryClinic.Presentation.ServiceAppointment
{
    public class HttpServiceAppointment : BaseHttpFactory, IHttpServiceAppointment
    {        
        public HttpServiceAppointment(IHttpClientFactory httpClientFactory) : base(httpClientFactory, "HttpClientAppointment")
        {            
        }        
    }
}
