using System;
using System.Collections.Generic;
using System.Linq;
using Seagull.BarTender.Print;
using WebPrint.Framework;

namespace WebPrint.BarTender
{
    public class PrintersManager
    {
        private readonly Lazy<Printers> lazyPrinters = new Lazy<Printers>(() => new Printers());

        public List<string> PrinterNames
        {
            get { return lazyPrinters.Value.Select(p => p.PrinterName).ToList(); }
        }

        public List<string> PrinterModels
        {
            get { return lazyPrinters.Value.Select(p => p.PrinterModel).ToList(); }
        }

        public string DefaultPrinterName
        {
            get { return lazyPrinters.Value.Default == null ? string.Empty : lazyPrinters.Value.Default.PrinterName; }
        }
    }
}
