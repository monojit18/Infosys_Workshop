using System;
using System.Threading;
using System.Threading.Tasks;


namespace TestFormsApp.Services
{
    public interface ICapturePhotoService
    {

        Task<byte[]> CapturePhoto();


    }
}
