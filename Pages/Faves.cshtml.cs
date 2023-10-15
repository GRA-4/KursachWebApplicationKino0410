using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationKinoAPI0510;
using WebApplicationKinoAPI0510.Additional;

namespace WebApplicationKino0410.Pages
{
    public class FavesModel : PageModel
    {
        public User fUser = new User();
        public List<Title> FaveTitles = new List<Title>();
        public async Task<IActionResult> OnGet()
        {

            CommonOperations commonOperations = new CommonOperations();
            var faveList = await commonOperations.GetAllAsync<FaveList>();
            if (1 != null)
            {
                 fUser = await commonOperations.GetByIdAsync<User>(1);
            }
            else
            {
                 fUser = await commonOperations.GetByIdAsync<User>(Current.CUser.Id);

            }
            foreach (var fave in faveList.Where(f => f.UserId == fUser.Id))
            {
                FaveTitles.Add(fave.Title);
            }
            return Page();

            
        }
    }

}
