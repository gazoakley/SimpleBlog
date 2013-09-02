using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleBlog.Entities
{
    public class Post
    {
        public virtual int Id { get; protected set; }
        public virtual string Subject { get; set; }
        public virtual string Body { get; set; }
    }
}
