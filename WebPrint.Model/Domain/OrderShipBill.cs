using System;

namespace WebPrint.Model
{
    public class OrderShipBill : EntityBase
    {
        /*
        public virtual string ShipCompany { get; set; }

        public virtual string ShipAttention { get; set; }
        
        public virtual string ShipAddress { get; set; }
        
        public virtual string ShipAddress2 { get; set; }
        
        public virtual string ShipAddress3 { get; set; }
        
        public virtual string ShipCityTown { get; set; }
        
        public virtual string ShipState { get; set; }
        
        public virtual string ShipZipCode { get; set; }
        
        public virtual string ShipCountry { get; set; }
        
        public virtual string ShipPhone { get; set; }
        
        public virtual string ShipFax { get; set; }
        
        public virtual string ShipEmail { get; set; }
        
        public virtual string ShipRemark { get; set; }
        
        public virtual string BillCompany { get; set; }
        
        public virtual string BillAttention { get; set; }
        
        public virtual string BillAddress { get; set; }

        public virtual string BillAddress2 { get; set; }
 
        public virtual string BillAddress3 { get; set; }

        public virtual string BillCityTown { get; set; }

        public virtual string BillState { get; set; }

        public virtual string BillZipCode { get; set; }

        public virtual string BillCountry { get; set; }

        public virtual string BillPhone { get; set; }

        public virtual string BillFax { get; set; }

        public virtual string BillEmail { get; set; }

        public virtual string BillRemark { get; set; }
         * */

        public virtual Address ShipAddress { get; set; }

        public virtual Address BillAddress { get; set; }

        public virtual DateTime CreatedTime { get; set; }

        public virtual Order Order { get; set; }
    }
}
