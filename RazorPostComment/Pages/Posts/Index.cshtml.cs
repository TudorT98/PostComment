using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
//using AspNetCoreWebApp.Models;
using ServiceReferencePostComment;
namespace AspNetCoreWebApp.Pages.Posts
{
    public class IndexModel : PageModel
    {
        PostCommentClient pcc = new PostCommentClient();
        public List<Post> Posts { get; set; }

        public IndexModel()
        {
            Posts = new List<Post>();
        }
        public async Task OnGetAsync()
        {
            var posts = await pcc.GetPostsAsync();
            foreach (var item in posts)
            {
                // Trebuia folosit AutoMapper. Transform Post in PostDTO
                Post pd = new Post();
                pd.Description = item.Description;
                pd.Id = item.Id;
                pd.Domain = item.Domain;
                pd.Date = item.Date;
                foreach (var cc in item.Comment)
                {
                    Comment cdto = new Comment();
                    cdto.PostId = cc.PostId;
                    cdto.Text = cc.Text;
                    pd.Comment.Add(cdto);
                }
                Posts.Add(pd);
            }
        }
    }
}