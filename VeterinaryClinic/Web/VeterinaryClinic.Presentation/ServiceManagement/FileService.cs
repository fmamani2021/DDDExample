using VeterinaryClinic.ManagementModule.Shared.DTOs;

namespace VeterinaryClinic.Presentation.ServiceManagement
{
    public class FileService
    {
        private readonly IHttpServiceManagement _httpService;

        public FileService(IHttpServiceManagement httpService)
        {
            _httpService = httpService;
        }

        public async Task<string> ReadPicture(string pictureName)
        {
            if (string.IsNullOrEmpty(pictureName))
            {
                return null;
            }
            var fileItem = await _httpService.HttpGetAsync<FileItem>($"files/{pictureName}");

            return fileItem == null ? null : fileItem.DataBase64;
        }
    }
}
