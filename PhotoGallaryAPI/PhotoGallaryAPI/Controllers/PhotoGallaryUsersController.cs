using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotoGallaryAPI.Dal;
using PhotoGallaryAPI.Interfaces;
using PhotoGallaryAPI.Models;

namespace PhotoGallaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoGallaryUsersController : ControllerBase
    {
        //private readonly IPhotoGallaryRepo _iPhotoGallaryRepo;
        private readonly IUnitOfWork _unitOfWork;

        public PhotoGallaryUsersController(IUnitOfWork iUnitOfWork)
        {
            _unitOfWork = iUnitOfWork;
        }

        //public PhotoGallaryUsersController(IPhotoGallaryRepo iPhotoGallaryRepo)
        //{
        //    _iPhotoGallaryRepo = iPhotoGallaryRepo;
        //}

        // GET: api/PhotoGallaryUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoGallaryUser>>> GetPhotoGallaryUser()
        {
            return await _unitOfWork._photoGallaryRepo.GetAllPhotoGallaryUserAsync(Request.Scheme, Request.Host, Request.PathBase);
        }

        // GET: api/PhotoGallaryUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoGallaryUser>> GetPhotoGallaryUser(int id)
        {
            var photoGallaryUser = await _unitOfWork._photoGallaryRepo.GetPhotoGallaryUserByIdAsync(id);
            if (photoGallaryUser == null)
            {
                return NotFound();
            }
            return photoGallaryUser;
        }


        // PUT: api/PhotoGallaryUsers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhotoGallaryUser([FromForm] PhotoGallaryUser photoGallaryUser)
        {
            await _unitOfWork._photoGallaryRepo.UpdatePhotoGallaryUserAsync(photoGallaryUser);

            return NoContent();
        }

        // POST: api/PhotoGallaryUsers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PhotoGallaryUser>> PostPhotoGallaryUser([FromForm] PhotoGallaryUser photoGallaryUser)
        {
            await _unitOfWork._photoGallaryRepo.CreatePhotoGallaryUserAsync(photoGallaryUser);
            return StatusCode(201);
        }


        // DELETE: api/PhotoGallaryUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PhotoGallaryUser>> DeletePhotoGallaryUser(int id)
        {
            var photoGallaryUser = await _unitOfWork._photoGallaryRepo.DeletePhotoGallaryUserByIdAsync(id);
            if (photoGallaryUser == null)
            {
                return NotFound();
            }
            return photoGallaryUser;
        }
    }
}
