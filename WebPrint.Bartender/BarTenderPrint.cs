using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Seagull.BarTender.Print;
using Seagull.BarTender.PrintServer.Tasks;

namespace WebPrint.BarTender
{
    public abstract class BarTenderPrint
    {
        protected BarTenderManager BarTender { get; set; }

        /* 是否需要防止同一个事件被绑定多次 */
        protected event EventHandler<TaskEventArgs> Succeeded
        {
            add {  BarTender.TaskPrintSucceeded += value; }
            remove { BarTender.TaskPrintSucceeded -= value; }
        }
        public Settings Settings { get { return BarTender.Settings; } }

        public PrintersManager Printers { get { return BarTender.Printers; } }

        private LabelFormat labelFormat;
        protected LabelFormat LabelFormat
        {
            get { return labelFormat ?? (labelFormat = BarTender.GetLabelFormatFromFile(Settings.LabelFormatFullPath)); }
        }

        protected BarTenderPrint(/*TaskManager taskManager, */Settings settings)
        {
            this.BarTender = new BarTenderManager(/*taskManager, */settings);
        }

        protected virtual void Print(out string message)
        {
            BarTender.Print(this.LabelFormat,out message);
        }

        public void SetSubStrings(Dictionary<string, string> subStringsNameValuePair)
        {
            BarTender.SetSubStrings(this.LabelFormat, subStringsNameValuePair);
        }

        public Dictionary<string, string> GetSubStrings()
        {
            return LabelFormat
                .SubStrings
                .ToDictionary(sub => sub.Name, sub => sub.Value);
        }

        /// <summary>
        /// 生成多张预览图,并返回第一张图片的绝对路径
        /// </summary>
        /// <param name="fileName">输入leaf.png,输出为 leaf.1.png leaf.2.png leaf.*.png (图片格式一定为PNG)</param>
        /// <param name="inputCopies">要生成图片的标签数</param>
        /// <param name="message"></param>
        public string GeneratePreview(string fileName, int inputCopies, out string message)
        {
            fileName = string.Format("{0}.%PageNumber%.png", fileName.Replace(".png", ""));

            this.LabelFormat.PrintSetup.IdenticalCopiesOfLabel = 1;
            this.LabelFormat.PrintSetup.NumberOfSerializedLabels = inputCopies;

            BarTender.GeneratePreview(this.LabelFormat, Settings.PreviewFileFullPath, fileName, out message);
            return Path.Combine(Settings.PreviewFileFullPath, fileName).Replace("%PageNumber%", "1");
        }

        /// <summary>
        /// 设置LabelFormat 只生成一张预览图
        /// </summary>
        /// <param name="fileName">输入leaf.png,输出为 leaf.1.png(图片格式一定为PNG)</param>
        /// <param name="message"></param>
        /// <returns>返回图片的绝对路径</returns>
        public string GeneratePreview(string fileName, out string message)
        {
            return GeneratePreview(fileName, 1, out message);
        }

        /// <summary>
        /// 生成一张适合网络路径的图片
        /// </summary>
        /// <param name="physicalApplicationPath">应用程序物理路径</param>
        /// <param name="fileName">输入leaf.png,输出为leaf.1.png(图片格式一定为PNG)</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string GeneratePreview(string physicalApplicationPath, string fileName, out string message)
        {
            return GeneratePreview(fileName, out message)
                .Replace(physicalApplicationPath, "/")
                .Replace(@"\", "/");
        }
    }
}
