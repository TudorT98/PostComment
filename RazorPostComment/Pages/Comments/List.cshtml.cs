using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using AspNetCoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceReferencePostComment;
namespace AspNetCoreWebApp.Pages.Comments
{
    public class ListModel : PageModel
    {
        PostCommentClient pcc = new PostCommentClient();
        public List<Comment> Comments { get; set; }
        public ListModel()
        {
            Comments = new List<Comment>();
        }
        public async Task OnGetAsync(int? id)
        {
            if (!id.HasValue)
                return;
            var item = await pcc.GetPostByIdAsync(id.Value);
            ViewData["Post"] = item.Id.ToString() + " : " + item.Description.Trim();
            foreach (var cc in item.Comment)
            {
                Comment cdto = new Comment();
                cdto.PostId = cc.PostId;
                cdto.Text = cc.Text;
                cdto.Id = cc.Id;
                Comments.Add(cdto);
            }
        }
    }
}
