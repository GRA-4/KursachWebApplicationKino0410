using WebApplicationKinoAPI0510;

namespace WebApplicationKino0410
{
    public class Current
    {
        public static List<Genre> CGenre { get; set; } = getHeaderGenre().Result;
        public static User CUser { get; set; }/* = getTestUser().Result;*/
        private static async Task<User> getTestUser()
        {
            CommonOperations commonOperations = new CommonOperations();
            User u = await commonOperations.GetByIdAsync<User>(1);
            return u;
        }
        private static async Task<List<Genre>> getHeaderGenre()
        {
            CommonOperations commonOperations = new CommonOperations();
            var g = await commonOperations.GetAllAsync<Genre>();
            return g.ToList();
        }
    }
}
