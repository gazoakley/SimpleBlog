using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace SimpleBlog.Entities
{
    public class Post
    {
        public virtual int Id { get; protected set; }
        public virtual string Subject { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual string Body { get; set; }
    }

    public interface IPostRepository
    {
        IEnumerable<Post> GetAll();
        Post Get(int id);
        void Store(Post post);
        void Update(Post post);
        void Destroy(Post post);
    }

    public class PostRepository : IPostRepository
    {
        private ISessionFactory _sessionFactory;

        public PostRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public IEnumerable<Post> GetAll()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.QueryOver<Post>().OrderBy(post => post.CreatedDate).Desc.List();
            }
        }

        public Post Get(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Get<Post>(id);
            }
        }

        public void Store(Post post)
        {
            using (var session = _sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                post.CreatedDate = DateTime.UtcNow;
                session.Save(post);
                transaction.Commit();
            }
        }

        public void Update(Post post)
        {
            using (var session = _sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Update(post);
                transaction.Commit();
            }
        }

        public void Destroy(Post post)
        {
            using (var session = _sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Delete(post);
                transaction.Commit();
            }
        }
    }
}
