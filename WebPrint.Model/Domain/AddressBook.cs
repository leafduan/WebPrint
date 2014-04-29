namespace WebPrint.Model
{
    public class AddressBook : EntityBase
    {
       //public virtual int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ShipBill Ship { get; set; }

        public virtual ShipBill Bill { get; set; }
    }
}
