using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using BarTender;
using WebPrint.Framework;

namespace WebPrint.BarTender
{
    public enum ColorType
    {
        TextBackgroundColor = 0,
        LineColor,
        FillColor
    }

    /// <summary>
    /// 直接调用COM组件 Automating BarTender(With ActiveX)
    /// </summary>
    public class BarTenderApplication
    {
        private Application BtApplication
        {
            get { return BtApplicationBootStrapper.BtApplication; }
        }

        private Lazy<Format> lazyBtFormat;

        private Format BtFormat
        {
            get { return lazyBtFormat.Value; }
        }

        public BtSettings Settings { get; private set; }

        public BarTenderApplication(BtSettings settings)
        {
            this.Settings = settings;
            lazyBtFormat = new Lazy<Format>(
                () => BtApplication.Formats.Open(Settings.LabelFormatFullPath, true, "")
                );
        }

        /// <summary>
        /// 设置Format中与Dictionary相匹配的命名子字符串的值，不匹配则忽略
        /// </summary>
        /// <param name="subStringsNameValuePair"></param>
        public void SetSubStrings(Dictionary<string, string> subStringsNameValuePair)
        {
            var subs = BtFormat
                .NamedSubStrings
                .GetAll(",", "|")
                .Split('|')
                .Where(s => !s.IsNullOrEmpty())
                .Select(sub => sub.Split(',').First());

            var matchedSubs = from args in subStringsNameValuePair
                              join sub in subs
                                  on args.Key equals sub
                              select new {Name = args.Key, args.Value};

            matchedSubs.ForEach(sub => BtFormat.SetNamedSubStringValue(sub.Name, sub.Value));
        }

        /// <summary>
        /// Set text backgroud color.
        /// <para>In the future, making it available to set the bar or other object's color</para>
        /// </summary>
        /// <param name="designObjectsColor"></param>
        public void SetDesignObjectsColor(IEnumerable<KeyValuePair<string, Color>> designObjectsColor,
                                          ColorType colorType = ColorType.TextBackgroundColor)
        {
            designObjectsColor.ForEach(item => SetDesignObjectColor(item.Key, item.Value, colorType));
        }

        private void SetDesignObjectColor(string name, Color color, ColorType colorType)
        {
            //var c = Color.FromArgb(0, 0, 0);
            var obj = BtFormat.Objects.Find(name);
            if (obj == null) return;

            if (colorType == ColorType.TextBackgroundColor)
            {
                obj.TextBackgroundColor = (uint) color.ToArgb();
            }

            if (colorType == ColorType.LineColor)
            {
                obj.LineColor = (uint) color.ToArgb();
            }

            if (colorType == ColorType.FillColor)
            {
                obj.FillColor = (uint) color.ToArgb();
            }
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

            BtFormat.PrintSetup.IdenticalCopiesOfLabel = 1;
            BtFormat.PrintSetup.NumberSerializedLabels = inputCopies;
            message = GeneratePreview(fileName);
            return Path.Combine(Settings.PreviewFileFullPath, fileName).Replace("%PageNumber%", "1");
        }

        /// <summary>
        /// 只生成一张预览图
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

        /// <summary>
        /// 生成网络图片
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GeneratePreview(string fileName)
        {
            string msg;
            try
            {
                // Color.White.ToArgb()  // is not white color, but like gray
                var whiteColorArgb = Color.FromArgb(0, Color.White).ToArgb();
                Messages msgs;
                BtFormat.ExportPrintPreviewToImage(Settings.PreviewFileFullPath, fileName, "png", BtColors.btColors24Bit,
                                                   200, whiteColorArgb, BtSaveOptions.btDoNotSaveChanges, true,
                                                   true, out msgs);
                msg = msgs.Cast<Message>().Join(Environment.NewLine,
                                                m => m.Message.Replace("\n", Environment.NewLine));
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }
    }

    public static class BtApplicationBootStrapper
    {
        public static Application BtApplication { get; private set; }

        public static void Start()
        {
            if (BtApplication != null) return;

            /* 如果释放了再次调用 是否禁止不再生成 */
            /* Retrieve an existing instance of BarTender, fisrtly the TaskManager should be initialised */
            BtApplication = new Application {Visible = false};//Marshal.GetActiveObject("BarTender.Application") as Application; //?? new Application(); /* new an interface? */
            /*
            if (BtApplication == null)
                throw new NullReferenceException(
                    "When Marshal.GetActiveObject(\"BarTender.Application\"),there has no bartender is initialised");
             * */
        }

        public static void Stop()
        {
            if (BtApplication == null) return;

            BtApplication.Quit(BtSaveOptions.btDoNotSaveChanges); /* 调用此方法应该就行了 */
            Marshal.FinalReleaseComObject(BtApplication);
            BtApplication = null;
        }
    }

    public class BtSettings
    {
        public string PreviewFileFullPath { get; set; }
        public string LabelFormatFullPath { get; set; }
    }
}
