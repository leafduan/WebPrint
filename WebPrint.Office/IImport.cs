using System.Data;

namespace WebPrint.Office
{
    public interface IImport
    {
        DataTable Import(string filepath);
        DataTable Import(string filepath, string[] headerNames); 
    }
}
