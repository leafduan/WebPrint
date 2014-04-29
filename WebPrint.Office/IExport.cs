using System.Data;

namespace WebPrint.Office
{
    public interface IExport
    {
        void Export(string filepath, DataTable dt);
        void Export(string filepath, DataTable dt, string[] header);
        
    }
}
