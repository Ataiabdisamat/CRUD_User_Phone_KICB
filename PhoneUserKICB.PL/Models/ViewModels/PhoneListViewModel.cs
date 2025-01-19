namespace PhoneUserKICB.PL.Models.ViewModels
{
    
    /// <summary>
    /// View model for phone index
    /// </summary>
        public class PhoneListViewModel
        {
            public List<PhoneViewModel> Phones { get; set; } 
            public int CurrentPage { get; set; } 
            public int TotalPages { get; set; } 
        }
    
}
