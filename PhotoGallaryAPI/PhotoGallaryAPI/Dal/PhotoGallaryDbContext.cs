using Microsoft.EntityFrameworkCore;
using PhotoGallaryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallaryAPI.Dal
{
    public class PhotoGallaryDbContext:DbContext
    {
        public PhotoGallaryDbContext(DbContextOptions<PhotoGallaryDbContext> options): base(options){}

        public DbSet<PhotoGallaryUser> PhotoGallaryUser { get; set; }
    }
}
