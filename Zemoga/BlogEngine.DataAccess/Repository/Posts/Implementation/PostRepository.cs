using BlogEngine.DataAccess.Models;
using BlogEngine.DataAccess.Repository.Posts.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.DataAccess.Repository.Posts.Implementation
{
    public class PostRepository : IPostRepository
    {
        private ZemogaBlogEngineContext _zemogaBlogEngineContext;
        public PostRepository(ZemogaBlogEngineContext zemogaBlogEngineContext)
        {
            _zemogaBlogEngineContext = zemogaBlogEngineContext;
        }

        public async Task<int> CreatePost(Post post)
        {
            await _zemogaBlogEngineContext.Post.AddAsync(post);
            return await _zemogaBlogEngineContext.SaveChangesAsync();
        }

        public async Task<Post> GetPost(long id)
        {
            return await _zemogaBlogEngineContext.Post.Include(c => c.Comment)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _zemogaBlogEngineContext.Post
                .Include(c => c.Comment).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByStatusId(long statusId)
        {
            return await _zemogaBlogEngineContext.Post
                .Include(c => c.Comment).Where(p => p.IdStatus == statusId).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPostsByUserId(long userId)
        {
            return await _zemogaBlogEngineContext.Post
                .Include(c => c.Comment).Where(p => p.IdUser == userId).ToListAsync();
        }

        public async Task<int> UpdatePost(Post post, long idPost)
        {
            var entity = await _zemogaBlogEngineContext.Post.FindAsync(idPost);
            entity.Content = post.Content;
            entity.Title = post.Title;
            return await _zemogaBlogEngineContext.SaveChangesAsync();
        }

        public async Task<int> UpdatePostStatus(long idPost, long statusId)
        {
            var entity = await _zemogaBlogEngineContext.Post.FindAsync(idPost);
            entity.IdStatus = statusId;
            return await _zemogaBlogEngineContext.SaveChangesAsync();
        }
        public async Task<int> UpdatePostStatusAndComment(long idPost, long statusId, string statusComment)
        {
            var entity = await _zemogaBlogEngineContext.Post.FindAsync(idPost);
            entity.IdStatus = statusId;
            entity.StatusComment = statusComment;
            return await _zemogaBlogEngineContext.SaveChangesAsync();
        }
    }
}
