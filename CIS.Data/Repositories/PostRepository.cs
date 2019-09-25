using CIS.Common;
using CIS.Data.Infrastructure;
using CIS.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace CIS.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow);

        IEnumerable<Post> GetTwoHotNews();

        IEnumerable<Post> GetThreeNews();

    }

    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Post> GetAllByTag(string tag, int pageIndex, int pageSize, out int totalRow)
        {
            var query = from p in DbContext.Posts
                        join pt in DbContext.PostTags
                        on p.ID equals pt.PostID
                        where pt.TagID == tag && p.Status
                        orderby p.CreatedDate descending
                        select p;

            totalRow = query.Count();

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return query;
        }

        public IEnumerable<Post> GetTwoHotNews()
        {
            return this.DbContext.Posts.Where(x => x.HotFlag && x.HomeFlag && x.Status && !string.IsNullOrEmpty(x.ReferenceUrl)).OrderByDescending(x => x.CreatedDate).ToList().Take(2);            
        }

        public IEnumerable<Post> GetThreeNews()
        {
            return this.DbContext.Posts.Where(x => x.HomeFlag && x.Status && string.IsNullOrEmpty(x.ReferenceUrl) && x.CategoryID == CommonConstant.NewsPostCategoryID).OrderByDescending(x => x.CreatedDate).ToList().Take(3);
        }
    }
}