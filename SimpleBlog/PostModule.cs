using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using SimpleBlog.Entities;

namespace SimpleBlog
{
    public class PostModule : NancyModule
    {
        public PostModule()
        {
            Get["/"] = _ => View["posts/index", Index()];
            Get["/create"] = _ => View["posts/create"];
            Post["/"] = _ => Store();
            Get["/{id}"] = _ => View["posts/show", Show(_.id)];
            Get["/{id}/edit"] = _ => View["posts/edit", Edit(_.id)];
            Put["/{id}"] = _ => Update(_.id);
            Delete["/{id}"] = _ => Destroy(_.id);
        }

        public dynamic Index()
        {
            using (var session = DbSessionFactory.SessionFactory.OpenSession())
            {
                var posts = session.QueryOver<Post>().OrderBy(post => post.CreatedDate).Desc.List();
                
                dynamic model = new ExpandoObject();
                model.Posts = posts;

                return model;
            }
        }

        public dynamic Store()
        {
            using (var session = DbSessionFactory.SessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var post = this.Bind<Post>();
                post.CreatedDate = DateTime.UtcNow;
                session.Save(post);
                transaction.Commit();

                return Response.AsRedirect(string.Format("/{0}", post.Id));
            }
        }

        public dynamic Show(int id)
        {
            using (var session = DbSessionFactory.SessionFactory.OpenSession())
            {
                var post = session.Get<Post>(id);
                dynamic model = new ExpandoObject();
                model.Post = post;

                return model;
            }
        }

        public dynamic Edit(int id)
        {
            using (var session = DbSessionFactory.SessionFactory.OpenSession())
            {
                var post = session.Get<Post>(id);
                dynamic model = new ExpandoObject();
                model.Post = post;

                return model;
            }
        }

        public dynamic Update(int id)
        {
            using (var session = DbSessionFactory.SessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var post = session.Get<Post>(id);
                post = this.BindTo(post);
                session.Update(post);
                transaction.Commit();

                return Response.AsRedirect(string.Format("/{0}", post.Id));
            }
        }

        public dynamic Destroy(int id)
        {
            using (var session = DbSessionFactory.SessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var post = session.Get<Post>(id);
                session.Delete(post);
                transaction.Commit();

                return Response.AsRedirect("/");
            }
        }
    }
}
