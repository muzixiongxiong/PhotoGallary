using Microsoft.AspNetCore.Hosting;
using PhotoGallaryAPI.Dal.Repositories;
using PhotoGallaryAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallaryAPI.Dal
{
    public class UnitOfWork : IUnitOfWork
    {

        private PhotoGallaryDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private IPhotoGallaryRepo _photoGallaryRepo;

        public UnitOfWork(PhotoGallaryDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        IPhotoGallaryRepo IUnitOfWork._photoGallaryRepo
        {
            get
            {
                return _photoGallaryRepo = _photoGallaryRepo ?? new PhotoGallaryRepo(_context, _hostEnvironment);
            }

        }

        void IUnitOfWork.Save()
        {
            _context.SaveChanges();
        }
    }
}
