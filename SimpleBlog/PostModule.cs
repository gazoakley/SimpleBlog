using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nancy;

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

        public string Index()
        {
            return "index";
        }

        public string Create()
        {
            return "create";
        }

        public string Store()
        {
            return "store";
        }

        public string Show(int id)
        {
            return "show";
        }

        public string Edit(int id)
        {
            return "edit";
        }

        public string Update(int id)
        {
            return "update";
        }

        public string Destroy(int id)
        {
            return "destroy";
        }
    }
}
