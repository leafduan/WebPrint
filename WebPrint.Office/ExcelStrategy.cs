namespace WebPrint.Office
{
    public class ExcelStrategy:IExcel
    {
        static ExcelStrategy _instance;
        static object lock_instance = new object();

        public IExport Excel_Exporter;
        public IImport Excel_Importer;
        private ExcelStrategy()
        {
            Excel_Exporter = ExcelForExport.CreateInstance();
            Excel_Importer = ExcelForImport.CreateInstance();
        }

        #region IExport 成员

        public void Export(string filepath, System.Data.DataTable dt)
        {
            this.Excel_Exporter.Export(filepath, dt);
        }

        public void Export(string filepath, System.Data.DataTable dt, string[] header)
        {
            this.Excel_Exporter.Export(filepath, dt, header);
        }

        #endregion

        #region IImport 成员

        public System.Data.DataTable Import(string filepath)
        {
            return this.Excel_Importer.Import(filepath);
        }

        public System.Data.DataTable Import(string filepath, string[] headerNames)
        {
            return this.Excel_Importer.Import(filepath, headerNames);
        }

        #endregion

        public static ExcelStrategy CreateInstance()
        {
            lock (lock_instance)
            {
                 
                    if (_instance == null)
                    {
                        _instance = new ExcelStrategy();
                    }
                
            }

            return _instance;
        }


    }
}
