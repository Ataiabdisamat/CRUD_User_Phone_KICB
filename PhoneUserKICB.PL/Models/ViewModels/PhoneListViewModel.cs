namespace PhoneUserKICB.PL.Models.ViewModels
{
    
        public class PhoneListViewModel
        {
            public List<PhoneViewModel> Phones { get; set; } // Список телефонов
            public int CurrentPage { get; set; } // Текущая страница
            public int TotalPages { get; set; } // Общее количество страниц
        }
    
}
