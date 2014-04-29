using System;

namespace WebPrint.Model
{
    public enum TicketType
    {
        RFID = 0,
        NonRFID
    }

    public class Format : EntityBase
    {
        public virtual int CategoryId { get; set; }

        public virtual string Name { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual string Path { get; set; }

        public virtual string PreviewImage { get; set; }

        public virtual string Code { get; set; }

        public virtual TicketType Type { get; set; }

        public virtual int CreatedId { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public virtual int ModifiedId { get; set; }

        public virtual DateTime ModifiedTime { get; set; }
    }
}
