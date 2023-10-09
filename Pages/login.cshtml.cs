using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationKinoAPI0510;

namespace WebApplicationKino0410.Pages
{
    public class loginModel : PageModel
    {
        public User User { get; set; }
        public async Task<IActionResult> OnGet()
        {
            if (Current.CUser != null)
            {
               return RedirectToPage("/profile");
            }
            else
            {
                return Page();
            }
        }
        public async Task<IActionResult> OnPostLogInAsync(string userNameInput, string passwordInput)
        {
            CommonOperations commonOperations = new CommonOperations();
            var uList = await commonOperations.GetAllAsync<User>();

            try
            {
                User logUser = new User { UserName = userNameInput, Password = passwordInput };
                var foundUser= uList.FirstOrDefault(u => u.UserName.Trim().ToLower() == logUser.UserName.Trim().ToLower());
                if(foundUser != null)
                {
                    if (foundUser.Password == passwordInput)
                    {
                        Current.CUser = foundUser;
                        return RedirectToPage("/Index");
                    }
                }

                return Page();
            }
            catch (Exception ex)
            {
                return Page();
            }
        }
        

        }



    }

