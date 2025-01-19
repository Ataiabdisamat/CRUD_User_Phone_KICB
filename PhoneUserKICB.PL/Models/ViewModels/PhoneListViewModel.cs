namespace PhoneUserKICB.PL.Models.ViewModels
{
    
        public class PhoneListViewModel
        {
            public List<PhoneViewModel> Phones { get; set; } 
            public int CurrentPage { get; set; } 
            public int TotalPages { get; set; } 
        }
    
}
