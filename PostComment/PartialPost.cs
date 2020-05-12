using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostComment
{
    public partial class Post
    {
        public bool AddPost()
        {
            using (PostCommentContainer ctx = new PostCommentContainer())
            {
                bool bResult = false;
                if (this.Id == 0)
                {
                    var it = ctx.Entry<Post>(this).State = EntityState.Added;
                    ctx.SaveChanges();
                    bResult = true;
                }
                return bResult;
            }
        }
        public Post UpdatePost(Post newPost)
        {
            using (PostCommentContainer ctx = new PostCommentContainer())
            {
                Post oldPost = ctx.PostSet.Find(newPost.Id);
                if (oldPost == null) 
                {
                    return null;
                }
                oldPost.Description = newPost.Description;
                oldPost.Domain = newPost.Domain;
                oldPost.Date = newPost.Date;
                ctx.SaveChanges();
                return oldPost;
            }
        }
        public int DeletePost(int id)
        {
            using (PostCommentContainer ctx = new PostCommentContainer())
            {
                return ctx.Database.ExecuteSqlCommand("Delete From PostSet where id = @p0", id);
            }
        }
        public Post GetPostById(int id)
        {
            using (PostCommentContainer ctx = new PostCommentContainer())
            {
                var items = from p in ctx.PostSet where (p.Id == id) select p;
                if (items != null)
                    return items.Include(c => c.Comment).SingleOrDefault();
                return null; 
            }
        }
        public List<Post> GetAllPosts()
        {
            using (PostCommentContainer ctx = new PostCommentContainer())
            {
                return ctx.PostSet.Include("Comments").ToList<Post>();

            }
        }
    }

}

