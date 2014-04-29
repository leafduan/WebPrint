using System;
using System.Text;
using System.Diagnostics;

namespace WebPrint.Web.Core
{
    internal class ProcessHelper
    {
        //ASP.NET 无法执行CMD命令时 可能是应用程序池的账号权限问题 用系统本地账号一般没问题
        internal static void Render(string startFile, string args, out string standardOutput, out string standardError)
        {
            try
            {
                using (Process process = new Process())
                {
                    //设置进程启动信息属性StartInfo，这是ProcessStartInfo类
                    process.StartInfo.FileName = startFile;
                    process.StartInfo.Arguments = args;

                    process.StartInfo.UseShellExecute = false;
                    //提供的标准输出流只有2k,超过大小会卡住;如果有大量输出,就读出来
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    process.PriorityClass = ProcessPriorityClass.High;

                    string output = string.Empty;
                    StringBuilder sb = new StringBuilder();
                    do
                    {
                        output = process.StandardOutput.ReadLine();
                        sb.AppendLine(output);
                    }
                    while (!string.IsNullOrEmpty(output));
                    standardOutput = sb.ToString();

                    sb = new StringBuilder();
                    do
                    {
                        output = process.StandardError.ReadLine();
                        sb.AppendLine(output);
                    }
                    while (!string.IsNullOrEmpty(output));
                    standardError = sb.ToString();

                    process.WaitForExit();
                }
            }
            catch (Exception e)
            {
                standardOutput = string.Empty;
                standardError = e.Message;
            }
        }
    }
}
