using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationKinoAPI0510;

namespace WebApplicationKino0410.Pages
{
    public class registrationModel : PageModel
    {
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostRegAsync(string userNameInput, string passwordInput, string emailInput, string repeatPasswordInput)
        {
            CommonOperations commonOperations = new CommonOperations();
            var uList = await commonOperations.GetAllAsync<User>();

            try
            {
                User regUser = new User { UserName = userNameInput, Password = passwordInput, Email = emailInput, RoleId =1 };
                var foundUser = uList.FirstOrDefault(u => u.UserName.Trim().ToLower() == regUser.UserName.Trim().ToLower());
                if ((foundUser == null)&&(uList.Where(u => u.Email != null).FirstOrDefault(u => u.Email.Trim().ToLower() == regUser.Email.Trim().ToLower())== null))
                {
                    if (regUser.Password == repeatPasswordInput)
                    {
                        var newUser = await commonOperations.AddEntityAsync<User>(regUser);
                        Current.CUser = newUser;
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
