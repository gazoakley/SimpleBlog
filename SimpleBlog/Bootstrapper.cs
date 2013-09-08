using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Nancy;
using Nancy.Authentication.Forms;
using SimpleBlog.Entities;

namespace SimpleBlog
{
    class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            container.Register<ISessionFactory>((c, p) => CreateSessionFactory());

            var formsAuthConfiguration =
                new FormsAuthenticationConfiguration()
                {
                    RedirectUrl = "~/login",
                    UserMapper = container.Resolve<IUserMapper>(),
                };

            FormsAuthentication.Enable(pipelines, formsAuthConfiguration);

            base.ApplicationStartup(container, pipelines);
        }

        private ISessionFactory CreateSessionFactory()
        {
            var configuration = Fluently
                .Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile("SimpleBlog.db"))
                //.Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<Post>(t => t.Namespace == "SimpleBlog.Entities").UseOverridesFromAssemblyOf<Post>()))
                .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<Post>(t => t.Name == "Post" || t.Name == "User").UseOverridesFromAssemblyOf<Post>()))
                .BuildConfiguration();

            new SchemaUpdate(configuration).Execute(true, true);
            return configuration.BuildSessionFactory();
        }
    }
}
