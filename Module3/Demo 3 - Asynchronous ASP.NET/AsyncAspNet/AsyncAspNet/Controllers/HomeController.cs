using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AsyncAspNet.Controllers
{
    public class HomeController : Controller
    {
        //Index here is asyncronous, aftera async operation within this method is done, it's going to return ActionResult
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                //in ASP.Net ConfigureAwait(false) is a loot quickier, picks one of the thread in ThreadPool and after completion
                //picks one thread which is available to process result after completion,
                var httpMessage = await client.GetAsync("http://www.filipekberg.se/rss/").ConfigureAwait(false);

                var content = await httpMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                return Content(content);
            }
        }
    }
}