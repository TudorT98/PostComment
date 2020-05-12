using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using AspNetCoreWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceReferencePostComment;
namespace AspNetCoreWebApp.Pages.Posts
{
    public class DeleteModel : PageModel
    {
        PostCommentClient pcc = new PostCommentClient();
        [BindProperty]
        public Post PostDTO { get; set; }
        public DeleteModel()
        {
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();
            var post = await pcc.GetPostByIdAsync(id.Value);
            if (post != null)
            {
                PostDTO = new Post();
                PostDTO.Id = post.Id;
                PostDTO.Domain = post.Domain;
                PostDTO.Description = post.Description;
                PostDTO.Date = post.Date;
                return Page();
            }
            else
                return NotFound();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int result = 0;
            // Nu pot fi sterse inregistrari parinte daca exista descendenti (cheie FKactiva)
 // "Prind" exceptia si afisez o pagina cu eroare. Nu e finisat aici...
 try
            {
                result = await pcc.DeletePostAsync(id.Value);
                // result ar trebui valorificat mai departe in cod...
            }
            catch (Exception ex)
            {
                return RedirectToPage("/Error");
            }

            return RedirectToPage("./Index");
        }
    }
}