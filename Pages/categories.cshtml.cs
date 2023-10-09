using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using WebApplicationKinoAPI0510;

namespace WebApplicationKino0410.Pages
{
    public class categoriesModel : PageModel
    {
        public Genre Genre { get; set; } = new Genre { Id = 0, GenreName = "All"};
        public List<Title> TitleList { get; set; }

        public int Sort { get; set; } = 0;

        public async Task<IActionResult> OnGet(int id, string? query)
        {
            CommonOperations commonOperations = new CommonOperations();
            if(id != 0)
            {
                Genre = await commonOperations.GetByIdAsync<Genre>(id);
            }
            var titleList = await commonOperations.GetAllAsync<Title>();
            if (query != null)
            {
                titleList = await commonOperations.GetAllByFieldAsync<Title>(t => t.TitleName, query);
                if(titleList.IsNullOrEmpty())
                {
                    titleList = await commonOperations.GetAllAsync<Title>();
                }
            }
            TitleList = titleList.ToList();
            if (Genre.Id != 0)
            {
                titleList = titleList.Where(t => t.Genres.Contains(Genre) != false);
                TitleList = titleList.ToList();
            }
            else
            {
                TitleList = titleList.ToList();
            }

            switch (Sort)
            {
                case 0:
                    break;
                case 1:
                    TitleList.OrderBy(t => t.TitleName);
                    break;
                case 2:
                    TitleList.OrderBy(t => t.Date);
                    break;
            }
            return Page();

        }

        public async Task<IActionResult> OnPostDoSortAsync(int id, int sort)
        {
            Sort = sort;

            CommonOperations commonOperations = new CommonOperations();
            if (id != 0)
            {
                Genre = await commonOperations.GetByIdAsync<Genre>(id);
            }


            var titleList = await commonOperations.GetAllAsync<Title>();
            TitleList = titleList.ToList();
            if (Genre.Id != 0)
            {
                titleList = titleList.Where(t => t.Genres.Contains(Genre) != false);
                TitleList = titleList.ToList();
            }
            else
            {
                TitleList = titleList.ToList();
            }
            switch (Sort)
            {
                case 0:
                    break;
                case 1:
                    TitleList= TitleList.OrderBy(t => t.TitleName).ToList();
                    break;
                case 2:
                    TitleList=TitleList.OrderByDescending(t => t.Date).ToList();
                    break;
            }
            return Page();

            
          


        }


        public async Task<IActionResult> OnPostSearchAsync(string query)
        {


            CommonOperations commonOperations = new CommonOperations();
            var titleList = await commonOperations.GetAllByFieldContainsAsync<Title>(t => t.TitleName, query);
            TitleList = titleList.ToList();
                TitleList = titleList.ToList();
            switch (Sort)
            {
                case 0:
                    break;
                case 1:
                    TitleList= TitleList.OrderBy(t => t.TitleName).ToList();
                    break;
                case 2:
                    TitleList=TitleList.OrderByDescending(t => t.Date).ToList();
                    break;
            }
            return Page();





        }



    }
}
