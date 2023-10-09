using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationKinoAPI0510;

namespace WebApplicationKino0410.Pages
{
    public class profileModel : PageModel
    {
        public User ProfileUser { get; set; }
        public async Task<IActionResult> OnGet(int? id)
        {
            if (id == null)
            {
                if(Current.CUser != null)
                {
                    CommonOperations commonOperations = new CommonOperations();
                    User profileUser = await commonOperations.GetByIdAsync<User>(Current.CUser.Id);
                    ProfileUser = profileUser;
                }
                else
                {
                    return RedirectToPage("/login");
                }
            }
            else
            {
                CommonOperations commonOperations = new CommonOperations();
                User profileUser = await commonOperations.GetByIdAsync<User>(id);
                ProfileUser = profileUser;
            }
            

            return Page();
        }

        public async Task<IActionResult> OnPostChangeImageUrlAsync(string imageUrl)
        {
            if ((Current.CUser != null)&&(Current.CUser != null))
            {
                CommonOperations commonOperations = new CommonOperations();
                User u = await commonOperations.GetByIdAsync<User>(Current.CUser.Id);
                u.ImageUrl = imageUrl;
                var newUser = await commonOperations.UpdateEntityAsync<User>(u);
                Current.CUser = newUser;
            }
            return await OnGet(Current.CUser.Id);
        }

        public async Task<IActionResult> OnPostLogOutAsync(int id)
        {
            if (Current.CUser != null)
            {
                Current.CUser = null;
                return RedirectToPage("/login");
            }
            return await OnGet(Current.CUser.Id);
        }

        public async Task<IActionResult> OnPostToFavesAsync(int id)
        {
            CommonOperations commonOperations = new CommonOperations();
            User profileUser = await commonOperations.GetByIdAsync<User>(id);
            ProfileUser = profileUser;

            return  RedirectToPage("/fave-titles", new { id = id });
        }
    }
}
