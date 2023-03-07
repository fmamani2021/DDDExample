namespace VeterinaryClinic.AppointmentModule.Domain.Interfaces
{
    public interface IFileSystem
    {
        Task<bool> SavePicture(string pictureName, string pictureBase64);
    }
}
