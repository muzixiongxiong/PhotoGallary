using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallaryAPI.Interfaces
{
    public interface IUnitOfWork
    {
        IPhotoGallaryRepo _photoGallaryRepo { get; }

        void Save();
    }
}
