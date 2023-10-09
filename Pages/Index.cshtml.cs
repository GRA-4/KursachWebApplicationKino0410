using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using WebApplicationKinoAPI0510;
using static WebApplicationKino0410.Pages.IndexModel;

namespace WebApplicationKino0410.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public class TitlesViewModel
        {
            public List<Title> Titles { get; set; } = new List<Title>();
            public int Page { get; set; } = 1;
            public int TotalCount { get; set; } = 0;
            public int ItemsPerPage { get; set; } = 6;
        }


        public TitlesViewModel _TitlesViewModel;
        public List<Title> AllTitles;
        public List<Title> TitlesCurrent;
        

        public async void OnGet()
        {
            _TitlesViewModel = new TitlesViewModel() { Page = 1};


            var commonOperationsTitle1 = new CommonOperations();
            
            var l = commonOperationsTitle1.GetAllAsync<Title>();
            _TitlesViewModel.Titles = l.Result.ToList();
            _TitlesViewModel.TotalCount =  _TitlesViewModel.Titles.Count();


            TitlesCurrent = _TitlesViewModel.Titles
        .Skip((_TitlesViewModel.Page - 1) * _TitlesViewModel.ItemsPerPage)
        .Take(_TitlesViewModel.ItemsPerPage)
        .ToList();

        }
    }
}