using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using SimpleBlog.Entities;

namespace SimpleBlog
{
    public static class DbSessionFactory
    {
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = CreateSessionFactory();
                }
                return _sessionFactory;
            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            var configuration = Fluently
                .Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile("SimpleBlog.db"))
                .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<Post>(t => t.Name.Equals("Post"))))
                .BuildConfiguration();

            new SchemaUpdate(configuration).Execute(true, true);
            return configuration.BuildSessionFactory();
        }
    }
}
