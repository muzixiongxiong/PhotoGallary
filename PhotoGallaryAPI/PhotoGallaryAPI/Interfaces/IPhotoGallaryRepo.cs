using Microsoft.AspNetCore.Http;
using PhotoGallaryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PhotoGallaryAPI.Interfaces
{
    public interface IPhotoGallaryRepo
    {
        Task<List<PhotoGallaryUser>> GetAllPhotoGallaryUserAsync(string requestScheme, HostString requestHost, PathString requestPathBase);

        Task<PhotoGallaryUser> GetPhotoGallaryUserByIdAsync(int id);

        Task<PhotoGallaryUser> CreatePhotoGallaryUserAsync(PhotoGallaryUser photoGallaryUser);

        Task<PhotoGallaryUser> UpdatePhotoGallaryUserAsync(PhotoGallaryUser photoGallaryUser);

        Task<PhotoGallaryUser> DeletePhotoGallaryUserByIdAsync(int id);



    }
}
