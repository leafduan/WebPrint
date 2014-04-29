using System;
using WebPrint.Framework;
using Seagull.BarTender.PrintServer.Tasks;
using System.IO;

namespace WebPrint.BarTender
{
    public sealed class BarTenderClientPrint : BarTenderPrint
    {
        public delegate void TaskClientPrintSucceeded(TaskClientPrintEventArgs args);

        public event TaskClientPrintSucceeded ClientPrintSucceeded;

        public BarTenderClientPrint(/*TaskManager taskManger, */Settings settings) : base(/*taskManger, */settings) { }

        public void Print(string license, string printerName, int inputCopies, out string message)
        {
            Formatter(license, printerName, inputCopies);

            /* 因不想暴露EventHandler<TaskEventArgs>及PrintLabelFormatTask给使用者，故在此触发自定义事件（目前没有找到其他实现方式） */
            if (this.ClientPrintSucceeded != null)
            {
                base.Succeeded += new EventHandler<TaskEventArgs>(
                    (sender, obj) =>
                    {
                        var task = sender as PrintLabelFormatTask;
                        var args = new TaskClientPrintEventArgs();
                        if (task != null)
                        {
                            args.PrintCode = task.PrintCode; /* 大数据复制 */
                            args.Message = task.Messages.Join(Environment.NewLine, msg => msg.Text.Replace("\n", Environment.NewLine)); /* 消息被多处 */
                        }

                        ClientPrintSucceeded(args); /* 捕捉的外部变量 */
                    });
            }

            Print(out message);
        }

        /// <summary>
        /// 目前只提供测试打印一张标签（正常打印不应该返回PrintCode, 因数据量大, String会造成复制浪费内存? 最好在回调方法中使用）
        /// (且不会执行实例绑定的事件)
        /// </summary>
        /// <param name="license"></param>
        /// <param name="printerName"></param>
        /// <param name="msg"></param>
        /// <returns>PrintCode</returns>
        public string TestPrint(string license, string printerName, out string msg)
        {
            string printCode = string.Empty;

            Formatter(license, printerName, 1);

            /* 这种方式能保证获得PrintCode, 因为客户端打印, 因此必须等task完成打印才执行return代码 */
            base.Succeeded += new EventHandler<TaskEventArgs>(
                (sender, args) =>
                {
                    var task = sender as PrintLabelFormatTask;
                    printCode = task == null ? string.Empty : task.PrintCode;
                });
            Print(out msg);

            return printCode;
        }

        private void Formatter(string license,string printerName, int inputCopies)
        {
            // 将打印数据写到文件中
            LabelFormat.PrintSetup.PrintToFile = true;
            // 服务器端生成打印数据的打印机名称 
            LabelFormat.PrintSetup.PrinterName = printerName;
            // 客户端生成的打印 license
            LabelFormat.PrintSetup.PrintToFileLicense = license;
            // 要写入的文件的绝对路径名称
            LabelFormat.PrintSetup.PrintToFileName = Path.Combine(Settings.PrintFileFullPath, Guid.NewGuid().ToString() + ".prn");

            // 每一个打印重复数量
            LabelFormat.PrintSetup.IdenticalCopiesOfLabel = 1;
            // 要打印的标签数量
            LabelFormat.PrintSetup.NumberOfSerializedLabels = inputCopies;

            // 是否使用btw设置的数据库
            LabelFormat.PrintSetup.UseDatabase = false;
        }
    }

    public class TaskClientPrintEventArgs : EventArgs
    {
        public string PrintCode { get; set; }
        public string Message { get; set; }
    }
}
