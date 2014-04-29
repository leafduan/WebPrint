using System;
using System.Collections.Generic;

namespace WebPrint.Model
{
    public class GraphicTagItem : EntityBase
    {
        public virtual string Name { get; set; }

        public virtual string Code { get; set; }

        public virtual string Size { get; set; }

        public virtual string PreviewImage { get; set; }

        public virtual string Desc { get; set; }

        /// <summary>
        /// below moq surcharge
        /// </summary>
        public virtual string Surcharge { get; set; }

        public virtual string LeadTime { get; set; }

        public virtual int SortNo { get; set; }

        public virtual short Active { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public virtual GraphicTagCategory Category { get; set; }

        public virtual IList<GraphicTagItemDetail> Details { get; set; }
    }
}
