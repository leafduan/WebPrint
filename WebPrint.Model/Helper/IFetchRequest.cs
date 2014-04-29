using System.Linq;

namespace WebPrint.Model.Helper
{
    public interface IFetchRequest<out TQueried, TFetch> : IOrderedQueryable<TQueried>
    {
    }
}
