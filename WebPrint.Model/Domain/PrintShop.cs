using System.Collections.Generic;

namespace WebPrint.Model
{
    public class PrintShop : EntityBase
    {
        public PrintShop ()
        {
            //Users = new List<User>();
            //Orders = new List<Order>();
        }

        public virtual string Code { get; set; }
  
        public virtual string DisplayName { get; set; }

        public virtual string EmailList { get; set; }

        // 数据量很大，特殊查询实现，因此去掉此关联

        //public virtual IList<User> Users { get; protected set; }

        //public virtual IList<Order> Orders { get; protected set; }
    }
}
