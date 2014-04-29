using System;

namespace WebPrint.Model
{
    public class Permission : EntityBase
    {
        public virtual string Name { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual DateTime CreatedTime { get; set; }
    }
}
