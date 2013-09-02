using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.ModelBinding;
using SimpleBlog.Entities;

namespace SimpleBlog
{
    public class PostModule : NancyModule
    {
        public PostModule()
        {
            Get["/"] = _ => Index();
            Get["/create"] = _ => Create();
            Post["/"] = _ => Store();
            Get["/{id}"] = _ => Show(_.id);
            Get["/{id}/edit"] = _ => Edit(_.id);
            Put["/{id}"] = _ => Update(_.id);
            Delete["/{id}"] = _ => Destroy(_.id);
        }

        public IEnumerable<Post> Index()
        {
            using (var session = DbSessionFactory.SessionFactory.OpenSession())
            {
                var posts = session.QueryOver<Post>().List();
                return posts;
            }
        }

        public string Create()
        {
            return "create";
        }

        public string Store()
        {
            using (var session = DbSessionFactory.SessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var post = this.Bind<Post>(p => p.Id);
                session.Save(post);
                transaction.Commit();
            }

            // Redirect to show or display error
            return "store";
        }

        public Post Show(int id)
        {
            using (var session = DbSessionFactory.SessionFactory.OpenSession())
            {
                var post = session.Get<Post>(id);
                return post;
            }
        }

        public string Edit(int id)
        {
            Post post;
            using (var session = DbSessionFactory.SessionFactory.OpenSession())
            {
                post = session.Get<Post>(id);
            }

            return "edit";
        }

        public string Update(int id)
        {
            using (var session = DbSessionFactory.SessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var post = session.Get<Post>(id);
                post = this.BindTo(post);
                session.Update(post);
                transaction.Commit();
            }

            // Redirect to show or display error
            return "update";
        }

        public string Destroy(int id)
        {
            using (var session = DbSessionFactory.SessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var post = session.Get<Post>(id);
                session.Delete(post);
                transaction.Commit();
            }

            return "destroy";
        }
    }
}
