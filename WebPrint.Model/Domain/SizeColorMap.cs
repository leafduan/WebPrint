using System;

namespace WebPrint.Model
{
    public class SizeColorMap : EntityBase
    {
        public virtual string Size { get; set; }

        public virtual string PmsColor { get; set; }

        public virtual string Version { get; set; }

        public virtual short Red { get; set; }

        public virtual short Green { get; set; }

        public virtual short Blue { get; set; }

        public virtual string SizeType { get; set; }

        public virtual int SortNo { get; set; }

        public virtual DateTime CreatedTime { get; set; }
    }
}
