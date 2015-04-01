
namespace WebPrint.Model
{
    // can set Id as TId, generic
    public class EntityBase
    {
        public virtual int Id { get; protected set; }

        // Handling versioning and concurrency
        public virtual int NhVersion { get; protected set; }
    }
}
