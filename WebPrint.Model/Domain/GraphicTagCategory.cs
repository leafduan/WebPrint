using System;
using System.Collections.Generic;

namespace WebPrint.Model
{
    public class GraphicTagCategory : EntityBase
    {
        public virtual string Name { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual short Active { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public virtual IList<GraphicTagItem> Items { get; set; }
    }
}
