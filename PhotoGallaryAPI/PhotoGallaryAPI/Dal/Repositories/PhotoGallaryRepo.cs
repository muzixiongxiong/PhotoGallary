using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoGallaryAPI.Interfaces;
using PhotoGallaryAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallaryAPI.Dal.Repositories
{
    public class PhotoGallaryRepo: IPhotoGallaryRepo
    {
        private  PhotoGallaryDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PhotoGallaryRepo(PhotoGallaryDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<PhotoGallaryUser> CreatePhotoGallaryUserAsync([FromForm] PhotoGallaryUser photoGallaryUser)
        {
            photoGallaryUser.ImageName = await SaveImage(photoGallaryUser.ImageFile);
            _context.PhotoGallaryUser.Add(photoGallaryUser);
            await _context.SaveChangesAsync();
            return photoGallaryUser;
        }

        public async Task<PhotoGallaryUser> DeletePhotoGallaryUserByIdAsync(int id)
        {
            var photoGallaryUser = await _context.PhotoGallaryUser.FindAsync(id);
            if (photoGallaryUser == null)
            {
                return null;
            }
            DeleteImage(photoGallaryUser.ImageName);
            _context.PhotoGallaryUser.Remove(photoGallaryUser);
            await _context.SaveChangesAsync();

            return photoGallaryUser;
        }

        public async Task<List<PhotoGallaryUser>> GetAllPhotoGallaryUserAsync(string requestScheme, HostString requestHost, PathString requestPathBase)
        {
            return await _context.PhotoGallaryUser
                .Select(x => new PhotoGallaryUser()
                {
                    UserID = x.UserID,
                    ImageDescription = x.ImageDescription,
                    ImageName = x.ImageName,
                    ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", requestScheme, requestHost, requestPathBase, x.ImageName)
                })
                .ToListAsync();
        }

        public async Task<PhotoGallaryUser> GetPhotoGallaryUserByIdAsync(int id)
        {
            var photoGallaryUser = await _context.PhotoGallaryUser.FindAsync(id);

            if (photoGallaryUser == null)
            {
                return null;
            }

            return photoGallaryUser;
        }

        public async Task<PhotoGallaryUser> UpdatePhotoGallaryUserAsync([FromForm] PhotoGallaryUser photoGallaryUser)
        {
            if (photoGallaryUser.ImageFile != null)
            {
                DeleteImage(photoGallaryUser.ImageName);
                photoGallaryUser.ImageName = await SaveImage(photoGallaryUser.ImageFile);
            }

            _context.Entry(photoGallaryUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoGallaryUserExists(photoGallaryUser.UserID))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return photoGallaryUser;
        }


        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }

        private bool PhotoGallaryUserExists(int id)
        {
            return _context.PhotoGallaryUser.Any(e => e.UserID == id);
        }
    }
}
