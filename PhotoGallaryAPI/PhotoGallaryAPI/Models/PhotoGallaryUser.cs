using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallaryAPI.Models
{
    public class PhotoGallaryUser
    {
        [Key]
        public int UserID { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string ImageDescription { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ImageName { get; set; }
        
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        public string ImageSrc { get; set; }

        //Pascal(EmployeeName) -> Camel EmployeeID ->employeeID
        //Camel(employeeName) -> Pascal
    }
}
