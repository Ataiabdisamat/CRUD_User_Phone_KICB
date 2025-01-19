

namespace PhoneUserKICB.DAL.Entities
{
    /// <summary>
    /// Phone entity
    /// </summary>
    public class Phone
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }  

    }
}
