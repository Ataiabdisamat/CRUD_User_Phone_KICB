﻿
namespace PhoneUserKICB.DAL.Entities
{
    /// <summary>
    /// User entity
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Phone> Phones { get; set; }
    }
}
