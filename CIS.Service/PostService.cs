using CIS.Common;
using CIS.Data.Infrastructure;
using CIS.Data.Repositories;
using CIS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CIS.Service
{
    public interface IPostService
    {
        void Add(Post post);

        void Update(Post post);

        void Delete(int id);

        IEnumerable<Post> GetAll();       

        IEnumerable<Post> GetAll(int postCategoryId);

        IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow);

        Post GetById(int id);

        IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        IEnumerable<Post> GetTwoHotNews();

        IEnumerable<Post> GetThreeNews();

        IEnumerable<Post> GetListPostByCategoryIdPaging(int categoryId, int page, int pageSize, out int totalRow);

        void SaveChanges();
    }

    public class PostService : IPostService
    {
        private IPostRepository _postRepository;
        private ITagRepository _tagRepository;
        private IPostTagRepository _postTagRepository;
        private IUnitOfWork _unitOfWork;

        public PostService(IPostRepository postRepository, ITagRepository tagRepository, IPostTagRepository postTagRepository, IUnitOfWork unitOfWork)
        {
            this._postRepository = postRepository;
            this._postTagRepository = postTagRepository;
            this._tagRepository = tagRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Add(Post post)
        {
            _postRepository.Add(post);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(post.Tags))
            {
                string[] tags = post.Tags.Split(',');
                foreach (var tagItem in tags)
                {
                    Tag tag = new Tag();
                    var tagId = StringHelper.ToUnsignString(tagItem);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        tag.ID = tagId;
                        tag.Name = tagItem;
                        tag.Type = CommonConstant.PostTag;
                        _tagRepository.Add(tag);
                    }
                    PostTag postTag = new PostTag();
                    postTag.PostID = post.ID;
                    postTag.TagID = tagId;
                    _postTagRepository.Add(postTag);
                    _unitOfWork.Commit();
                }
            }
        }

        public void Delete(int id)
        {
            _postRepository.Delete(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll(new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAll(int categoryId)
        {
            return _postRepository.GetMulti(x => x.CategoryID == categoryId, new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(x => x.Status && x.CategoryID == categoryId, out totalRow, page, pageSize, new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            //TODO: Select all post by tag
            return _postRepository.GetAllByTag(tag, page, pageSize, out totalRow);
        }

        public IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public Post GetById(int id)
        {
            return _postRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }       

        public IEnumerable<Post> GetTwoHotNews()
        {
            return _postRepository.GetTwoHotNews();
        }

        public IEnumerable<Post> GetThreeNews()
        {
            return _postRepository.GetThreeNews();
        }

        public void Update(Post post)
        {
            _postRepository.Update(post);
            if (!string.IsNullOrEmpty(post.Tags))
            {
                string[] tags = post.Tags.Split(',');
                foreach (var tagItem in tags)
                {
                    Tag tag = new Tag();
                    var tagId = StringHelper.ToUnsignString(tagItem);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        tag.ID = tagId;
                        tag.Name = tagItem;
                        tag.Type = CommonConstant.PostTag;
                        _tagRepository.Add(tag);
                    }
                    if (_postTagRepository.Count(x => x.PostID == post.ID && x.TagID == tagId) == 0)
                    {
                        PostTag postTag = new PostTag();
                        postTag.PostID = post.ID;
                        postTag.TagID = tagId;
                        _postTagRepository.Add(postTag);
                        _unitOfWork.Commit();
                    }
                }
            }
            _unitOfWork.Commit();
        }

        public IEnumerable<Post> GetListPostByCategoryIdPaging(int categoryId, int page, int pageSize, out int totalRow)
        {
            var query = _postRepository.GetMulti(x => x.Status && x.CategoryID == categoryId).OrderBy(x => x.CreatedDate);

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}