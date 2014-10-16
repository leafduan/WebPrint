
namespace WebPrint.Model
{
    // can set Id as TId, generic
    public class EntityBase
    {
        public virtual int Id { get; set; }

        // Handling versioning and concurrency
        // protected virtual int Version { get; set; }
    }
}
