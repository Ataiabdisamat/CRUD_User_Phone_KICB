using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneUserKICB.DAL.Entities;

namespace PhoneUserKICB.BLL.DTO
{
    /// <summary>
    /// Dto for phone 
    /// </summary>
    public class PhoneDto
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public int UserId { get; set; }
    }
}
