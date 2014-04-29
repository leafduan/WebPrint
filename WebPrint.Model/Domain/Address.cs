using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebPrint.Model
{
    public class Address
    {
        public virtual string Company { get; set; }

        public virtual string Attention { get; set; }

        public virtual string Address1 { get; set; }

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
    }
}
