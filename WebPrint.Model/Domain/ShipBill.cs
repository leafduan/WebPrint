using System;

namespace WebPrint.Model
{
    public class ShipBill : EntityBase
    {
        /*
        public virtual string Company { get; set; }

        public virtual string Attention { get; set; }

        public virtual string Address { get; set; }

        public virtual string Address2 { get; set; }

        public virtual string Address3 { get; set; }

        public virtual string CityTown { get; set; }

        public virtual string State { get; set; }

        public virtual string ZipCode { get; set; }

        public virtual string Country { get; set; }

        public virtual string Phone { get; set; }

        public virtual string Fax { get; set; }

        public virtual string Email { get; set; }

        public virtual string Remark { get; set; }
         * */

        public virtual Address Address { get; set; }

        public virtual string Type { get; set; }

        public virtual int Active { get; set; }

        public virtual int CreatedId { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public virtual int ModifiedId { get; set; }

        public virtual DateTime ModifiedTime { get; set; }
    }
}
