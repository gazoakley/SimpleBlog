using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using NHibernate;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;

namespace SimpleBlog.Entities
{
    public class User : IUserIdentity
    {
        public virtual Guid Id { get; protected set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual IEnumerable<string> Claims { get; set; }
    }

    public class UserMappingOverride : IAutoMappingOverride<User>
    {
        public void Override(AutoMapping<User> mapping)
        {
            mapping.IgnoreProperty(p => p.Claims);
        }
    }

    public class UserMapper : IUserMapper
    {
        private ISessionFactory _sessionFactory;

        public UserMapper(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var user = session.Get<User>(identifier);
                return user;
            }
        }
    }
}
