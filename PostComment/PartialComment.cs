using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostComment
{
 
        public partial class Comment
        {
            public bool AddComment()
            {
                using (PostCommentContainer ctx = new PostCommentContainer())
                {
                    bool bResult = false;
                    if (this == null || this.PostId == 0)
                        return bResult;
                    if (this.Id == 0)
                    {
                        ctx.Entry<Comment>(this).State = EntityState.Added;
                        Post p = ctx.PostSet.Find(this.PostId);
                        ctx.Entry<Post>(p).State = EntityState.Unchanged;
                        ctx.SaveChanges();
                        bResult = true;
                    }
                    return bResult;
                }
            }
            public Comment UpdateComment(Comment newComment)
            {
                using (PostCommentContainer ctx = new PostCommentContainer())
                {
                    Comment oldComment = ctx.CommentSet.Find(newComment.Id);
                    if (newComment.Text != null)
                        oldComment.Text = newComment.Text;
                    if ((oldComment.PostId != newComment.PostId)
                   && (newComment.PostId != 0))
                    {
                        oldComment.PostId = newComment.PostId;
                    }
                    ctx.SaveChanges();
                    return oldComment;
                }
            }
            public Comment GetCommentById(int id)
            {
                using (PostCommentContainer ctx = new PostCommentContainer())
                {
                    var items = from c in ctx.CommentSet where (c.Id == id) select c;
                    return items.Include(p => p.Post).SingleOrDefault();
                }
            }
        }
    }
