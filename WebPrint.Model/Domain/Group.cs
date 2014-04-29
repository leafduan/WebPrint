using System;
using System.Collections.Generic;

namespace WebPrint.Model
{
    public class Group : EntityBase
    {
        public Group()
        {
            //Users = new List<User>();
            Permissions = new List<Permission>();
        }

        public virtual string Name { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        // 用户较多 因此可以去掉这样一个连接
        //public virtual IList<User> Users { get; protected set; }

        public virtual IList<Permission> Permissions { get; protected set; }
    }
}
