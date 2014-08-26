using System.Linq;

namespace WebPrint.Data.Helper
{
    public interface IFetchRequest<out TQueried, TFetch> : IOrderedQueryable<TQueried>
    {
    }
}
