using System;
using System.Collections.Generic;

namespace WebPrint.Model
{
    public class User : EntityBase
    {
        public User()
        {
            Groups = new List<Group>();
            //CreatedOrders = new List<Order>();
            //ModifiedOrders = new List<Order>();
            AddressBooks = new List<AddressBook>();
        }

        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual string Email { get; set; }

        public virtual string VendorCode { get; set; }

        public virtual short Active { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public virtual DateTime ModifiedTime { get; set; }

        public virtual IList<Group> Groups { get; protected set; }

        // 关联数据较大，分页查询实现，因此去掉关联

        //public virtual IList<Order> CreatedOrders { get; protected set; }

        //public virtual IList<Order> ModifiedOrders { get; protected set; }

        public virtual IList<AddressBook> AddressBooks { get; protected set; }

        public virtual PrintShop PrintShop { get; set; }
    }
}
