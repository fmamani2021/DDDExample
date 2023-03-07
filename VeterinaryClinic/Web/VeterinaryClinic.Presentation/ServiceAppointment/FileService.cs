using VeterinaryClinic.AppointmentModule.Shared.DTOs;

namespace VeterinaryClinic.Presentation.ServiceAppointment
{
    public class FileService
    {
        private readonly IHttpServiceAppointment _httpService;

        public FileService(IHttpServiceAppointment httpService)
        {
            _httpService = httpService;
        }

        public async Task<string> ReadPicture(string pictureName)
        {
            if (string.IsNullOrEmpty(pictureName))
            {
                return null;
            }
            var fileItem = await _httpService.HttpGetAsync<FileItem>($"api/files/{pictureName}");

            return fileItem == null ? null : fileItem.DataBase64;
        }
    }
}
