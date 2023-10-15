using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationKinoAPI0510;
using WebApplicationKinoAPI0510.Additional;
using WebApplicationKinoAPI0510.Models;

namespace WebApplicationKino0410.Pages
{
    public class anime_detailsModel : PageModel
    {
        public Title Title { get; set; }
        public int id { get; set; }
        public int VoteCount { get; set; } = 0;
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int Id)
        {
            CommonOperations commonOperations1 = new CommonOperations();
            Title = await commonOperations1.GetByIdAsync<Title>(Id);

            VoteCount = await GetVoteCountAsync(Id);
            User = Current.CUser;


            return Page();
        }

        public async Task<IActionResult> OnPostMakeVoteAsync(int count, int id)
        {


            CommonOperations commonOperations2 = new CommonOperations();
            Title t = await commonOperations2.GetByIdAsync<Title>(id);


            Vote vote = new Vote() {Title = t, UserId = commonOperations2.GetByIdAsync<User>(Current.CUser.Id).Result.Id, Rating = count };
            try
            {
                var v = await commonOperations2.AddEntityAsync<Vote>(vote);
                if (v == null)
                {
                    v = await commonOperations2.UpdateEntityAsync<Vote>(vote);
                }
            }
            catch{ }
            VoteCount = await GetVoteCountAsync(id);


            return await OnGetAsync(id);
        }

        public async Task<IActionResult> OnPostToFavesAsync(int id)
        {
            CommonOperations commonOperations2 = new CommonOperations();
            Title t = await commonOperations2.GetByIdAsync<Title>(id);
            VoteCount = await GetVoteCountAsync(id);
            if(Current.CUser.Id != null)
            {
                var fL = t.FaveLists.FirstOrDefault(l => l.UserId == Current.CUser.Id);
                if (fL != null)
                {
                    await commonOperations2.RemoveEntityAsync(fL);
                }
                else
                {
                    await commonOperations2.AddEntityAsync(new FaveList() { TitleId = id, UserId = Current.CUser.Id });
                }
            }


            return await OnGetAsync(id);
        }

        public async Task<IActionResult> OnPostAddCommentAsync(int id, string commentContent)
        {
            CommonOperations commonOperations1 = new CommonOperations();
            Title = await commonOperations1.GetByIdAsync<Title>(id);

            try
            {
                
                CommonOperations commonOperations2 = new CommonOperations();
                Title t = await commonOperations2.GetByIdAsync<Title>(id);
                if(Current.CUser != null)
                {
                    Comment newComment = new Comment() { TitleId = Title.Id, Date = DateTime.Now, UserId= Current.CUser.Id, TextContent = commentContent };
                var addedComment = await commonOperations2.AddEntityAsync<Comment>(newComment);
                VoteCount = await GetVoteCountAsync(id);

                    // Вернуть успешный результат или другой ответ клиенту
                    //return await OnGetAsync(id);
                    return await OnGetAsync(id);
                }
                else
                {
                    return await OnGetAsync(id);
                }
                
            }
            catch (Exception ex)
            {
                return await OnGetAsync(id);
            }

        }

        public async Task<int> GetVoteCountAsync(int Id)
        {
            CommonOperations commonOperations1 = new CommonOperations();
            Title title = await commonOperations1.GetByIdAsync<Title>(Id);
            int? c = 0;
            if(title != null)
            {
            foreach(var v in  title.Votes) {
                c += v.Rating;
            }

            if (title.Votes.Count() != null)
            {
            if ((title.Votes.Count() != 0))
            {
                c = c/title.Votes.Count();
                if (c != null)
                {
                    return c.Value;
                }
                else { return 0; }
            }
                    else
                    {
                        return 0;
                    }
                

            }
            else
            {
                return 0;
            }

            }
            else { return 0; }

        }


    }
}
