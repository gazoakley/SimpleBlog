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
        private IPostRepository _postRepository;

        public PostModule(IPostRepository postRepository)
        {
            _postRepository = postRepository;

            Get["/"] = _ => View["posts/index", Index()];
            Get["/create"] = _ => View["posts/create"];
            Post["/"] = _ => Store();
            Get["/{id}"] = _ => View["posts/show", Show(_.id)];
            Get["/{id}/edit"] = _ => View["posts/edit", Show(_.id)];
            Put["/{id}"] = _ => Update(_.id);
            Delete["/{id}"] = _ => Destroy(_.id);
        }

        public dynamic Index()
        {
            dynamic model = new ExpandoObject();
            model.Posts = _postRepository.GetAll();

            return model;
        }

        public dynamic Store()
        {
            var post = this.Bind<Post>();
            _postRepository.Store(post);

            return Response.AsRedirect(string.Format("/{0}", post.Id));
        }

        public dynamic Show(int id)
        {
            var post = _postRepository.Get(id);
            dynamic model = new ExpandoObject();
            model.Post = post;

            return model;
        }

        public dynamic Update(int id)
        {
            var post = _postRepository.Get(id);
            post = this.BindTo(post);
            _postRepository.Update(post);

            return Response.AsRedirect(string.Format("/{0}", post.Id));
        }

        public dynamic Destroy(int id)
        {
            var post = _postRepository.Get(id);
            _postRepository.Destroy(post);

            return Response.AsRedirect("/");
        }
    }
}
