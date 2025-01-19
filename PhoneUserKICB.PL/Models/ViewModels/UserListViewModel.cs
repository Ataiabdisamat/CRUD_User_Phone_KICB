namespace PhoneUserKICB.PL.Models.ViewModels
{
    public class UserListViewModel
    {
        /// <summary>
        /// View model for user index
        /// </summary>
        public List<UserViewModel> Users { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string SearchTerm { get; set; }
    }

}
