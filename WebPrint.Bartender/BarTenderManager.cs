using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using WebPrint.Framework;

using Seagull.BarTender.Print;
using Seagull.BarTender.PrintServer;
using Seagull.BarTender.PrintServer.Tasks;

namespace WebPrint.BarTender
{
    public class BarTenderManager
    {
        private TaskManager TaskManager { get; set; }

        public event EventHandler<TaskEventArgs> TaskPrintSucceeded;
        public Settings Settings { get; private set; }

        public PrintersManager Printers { get; private set; }

        internal BarTenderManager( /*TaskManager taskManager*/) : this( /*taskManager, */new Settings())
        {
        }

        internal BarTenderManager( /*TaskManager taskManger, */ Settings settings)
        {
            //TaskManager = taskManger;
            TaskManager = BarTenderBootStrapper.TaskManager;
            Settings = settings;
            Printers = new PrintersManager();
        }

        /// <summary>
        /// GetLabelFormatTask 获得LabelFormat 而不是 new LabelFormat(path)
        /// </summary>
        /// <param name="labelFormatFullPath">BTW文件路径</param>
        /// <returns></returns>
        public LabelFormat GetLabelFormatFromFile(string labelFormatFullPath)
        {
            return GetLabelFormatFromFile(labelFormatFullPath, delegate { });
        }

        /// <summary>
        /// GetLabelFormatTask 获得LabelFormat 而不是 new LabelFormat(path)
        /// </summary>
        /// <param name="labelFormatFullPath">BTW文件路径</param>
        /// <param name="formatter">设置LabelFormat</param>
        /// <returns></returns>
        public LabelFormat GetLabelFormatFromFile(string labelFormatFullPath, Action<LabelFormat> formatter)
        {
            LabelFormat labelFormat = null;

            if (IsTaskManagerRunning())
            {
                var getFormatTask = new GetLabelFormatTask(labelFormatFullPath);
                TaskManager.TaskQueue.QueueTaskAndWait(getFormatTask, this.Settings.TaskWaitForCompletionTimeout);
                labelFormat = getFormatTask.LabelFormat;
                if (formatter != null) formatter(labelFormat);
            }
            return labelFormat;
        }

        /// <summary>
        /// 设置LabelFormat中与Dictionary相匹配的命名子字符串的值，不匹配则忽略
        /// </summary>
        /// <param name="labelFormat"></param>
        /// <param name="subStringsNameValuePair"></param>
        public void SetSubStrings(LabelFormat labelFormat, Dictionary<string, string> subStringsNameValuePair)
        {
            var matchedSubStrings = from sub in subStringsNameValuePair
                                    join str in labelFormat.SubStrings
                                        on sub.Key equals str.Name
                                    select new {Name = sub.Key, sub.Value};

            foreach (var sub in matchedSubStrings)
            {
                labelFormat.SubStrings[sub.Name].Value = sub.Value;
            }
        }

        /// <summary>
        /// 打印 LabelFormat
        /// </summary>
        /// <param name="labelFormat"></param>
        /// <param name="message"></param>
        public void Print(LabelFormat labelFormat, out string message)
        {
            try
            {
                if (!IsTaskManagerRunning())
                {
                    message = TaskManagerNotRuningMsg;
                    return;
                }

                var taskPrint = new PrintLabelFormatTask(labelFormat);
                taskPrint.PrintTimeout = this.Settings.TaskPrintTimeout;

                if (TaskPrintSucceeded != null) taskPrint.Succeeded += TaskPrintSucceeded;

                var status = TaskManager.TaskQueue.QueueTaskAndWait(taskPrint,
                                                                    this.Settings.TaskWaitForCompletionTimeout);
                message = taskPrint.Messages.Join(Environment.NewLine,
                                                  msg => msg.Text.Replace("\n", Environment.NewLine));

                if (status == TaskStatus.Timeout) message += PrintTimeoutMsg;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="labelFormat"></param>
        /// <param name="imgDirectory">full path</param>
        /// <param name="fileName">*.%PageNumber%.png 其中 %PageNumber% 表示数量，如果只有一个就 Replace("%PageNumber%","1");</param>
        /// <param name="showMessage"></param>
        public void GeneratePreview(LabelFormat labelFormat, string imgDirectory, string fileName,
                                    out string showMessage)
        {
            try
            {
                if (!IsTaskManagerRunning())
                {
                    showMessage = TaskManagerNotRuningMsg;
                    return;
                }

                var taskPrint = new ExportPrintPreviewToFileTask(labelFormat,
                                                                 imgDirectory,
                                                                 fileName,
                                                                 ImageType.PNG,
                                                                 new Resolution(550, 550),
                                                                 ColorDepth.ColorDepth24bit,
                                                                 Color.White,
                                                                 true,
                                                                 true,
                                                                 OverwriteOptions.Overwrite);

                taskPrint.LabelFormat.PrintSetup.EnablePrompting = false;
                var status = TaskManager.TaskQueue.QueueTaskAndWait(taskPrint,
                                                                    this.Settings.TaskWaitForCompletionTimeout);
                showMessage = taskPrint.Messages.Join(Environment.NewLine,
                                                      msg => msg.Text.Replace("\n", Environment.NewLine));
            }
            catch (Exception ex)
            {
                showMessage = ex.Message;
            }
        }

        /// <summary>
        /// bartender是否开启并有可用打印引擎否
        /// </summary>
        /// <returns></returns>
        private bool IsTaskManagerRunning()
        {
            return (TaskManager != null) && (TaskManager.TaskEngines.AliveCount != 0);
        }

        private const string TaskManagerNotRuningMsg =
            "Unable to view the label print preview. Please make sure you have BarTender installed, activated as Enterprise Print Server edition, and that print engines are running.";

        private const string PrintTimeoutMsg = "Timeout";
    }

    public sealed class BarTenderBootStrapper
    {
        public static TaskManager TaskManager { get; private set; }

        public static void Start()
        {
            if (TaskManager != null) return;

            TaskManager = new TaskManager();
            TaskManager.Start(1);
        }

        public static void Stop()
        {
            if (TaskManager == null) return;

            TaskManager.Stop(3000, true);
            TaskManager.Dispose();
            TaskManager = null;
        }
    }

    public class Settings
    {
        public Settings()
        {
            TaskPrintTimeout = 6000;
            TaskWaitForCompletionTimeout = 99000;
        }

        public int TaskWaitForCompletionTimeout { get; set; }
        public int TaskPrintTimeout { get; set; }

        public string PreviewFileFullPath { get; set; }
        public string PrintFileFullPath { get; set; }
        public string LabelFormatFullPath { get; set; }
    }
}
