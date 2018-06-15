using System;
using System.Collections.Generic;
using SHY.Data.Infrastructure;
using SHY.Data.Repositories;
using SHY.Model.Models;
using System.Linq;

namespace SHY.Service
{
    public interface IPostService
    {
        void Add(Post post);

        void Update(Post post);

        Post Delete(int id);

        IEnumerable<Post> GetAll();

        IEnumerable<Post> GetAll(string keyword);

        IEnumerable<Post> GetAllPaging(int page, int pageSize, out int totalRow);

        IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow);

        Post GetById(int id);

        IEnumerable<Post> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);
        IEnumerable<Post> GetListPost(string keyword);
        IEnumerable<Post> Search(string keyword, int page, int pageSize, string sort, out int totalRow);
        IEnumerable<Post> GetNewPost(int top);

        void SaveChanges();
        void Save();
    }

    public class PostService : IPostService
    {
        IPostRepository _postRepository;
        IUnitOfWork _unitOfWork;

        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            this._postRepository = postRepository;
            this._unitOfWork = unitOfWork;
        }

        public void Add(Post post)
        {
            _postRepository.Add(post);
        }


        public Post Delete(int id)
        {
            return _postRepository.Delete(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll(new string[] { "PostCategory" });
        }

        public IEnumerable<Post> GetNewPost(int top)
        {
            return _postRepository.GetMulti(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top);

        }

        public IEnumerable<Post> Search(string keyword, int page, int pageSize, string sort, out int totalRow)
        {
            var query = !String.IsNullOrEmpty(keyword) ? _postRepository.GetMulti(x => x.Status && x.Name.Contains(keyword)) : _postRepository.GetMulti(x => x.Status);

            switch (sort)
            {
                case "popular":
                    query = query.OrderByDescending(x => x.ViewCount);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Post> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _postRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            else
                return _postRepository.GetAll();
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

        public void Update(Post post)
        {
            _postRepository.Update(post);
        }

        public IEnumerable<Post> GetListPost(string keyword)
        {
            IEnumerable<Post> query;
            if (!string.IsNullOrEmpty(keyword))
                query = _postRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                query = _postRepository.GetAll();
            return query;
        }
    }
}