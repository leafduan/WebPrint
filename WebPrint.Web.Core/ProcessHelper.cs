using System;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace WebPrint.Web.Core
{
    internal class ProcessHelper
    {
        //ASP.NET 无法执行CMD命令时 可能是应用程序池的账号权限问题 用系统本地账号一般没问题
        // 参数空格解决 "空格 问题.mp4"
        // 中文乱码解决 UTF8 encoding
        // http://stackoverflow.com/questions/139593/processstartinfo-hanging-on-waitforexit-why
        internal static void Render(string startFile, string args, out string standardOutput, out string standardError)
        {
            // 5 分钟
            var timeout = 5*60*1000;

            using (var outputWaitHandle = new AutoResetEvent(false))
            using (var errorWaitHandle = new AutoResetEvent(false))
            {
                using (var process = new Process())
                {
                    try
                    {
                        //设置进程启动信息属性StartInfo，这是ProcessStartInfo类
                        process.StartInfo.FileName = "\"" + startFile + "\"";
                        process.StartInfo.Arguments = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(args));

                        process.StartInfo.UseShellExecute = false;
                        //提供的标准输出流只有2k,超过大小会卡住;如果有大量输出,就读出来
                        process.StartInfo.RedirectStandardOutput = true;
                        process.StartInfo.RedirectStandardError = true;
                        process.StartInfo.CreateNoWindow = true;

                        #region old method

                        //process.Start();
                        //process.PriorityClass = ProcessPriorityClass.High;

                        //string output;
                        //var sb = new StringBuilder();
                        //do
                        //{
                        //    output = process.StandardOutput.ReadLine();
                        //    sb.AppendLine(output);
                        //}
                        //while (!string.IsNullOrEmpty(output));
                        //standardOutput = sb.ToString();

                        //sb = new StringBuilder();
                        //do
                        //{
                        //    output = process.StandardError.ReadLine();
                        //    sb.AppendLine(output);
                        //}
                        //while (!string.IsNullOrEmpty(output));
                        //standardError = sb.ToString();

                        #endregion

                        var output = new StringBuilder();
                        var error = new StringBuilder();


                        process.OutputDataReceived += (sender, e) =>
                        {
                            if (e.Data == null)
                            {
                                outputWaitHandle.Set();
                            }
                            else
                            {
                                output.AppendLine(e.Data);
                            }
                        };
                        process.ErrorDataReceived += (sender, e) =>
                        {
                            if (e.Data == null)
                            {
                                errorWaitHandle.Set();
                            }
                            else
                            {
                                error.AppendLine(e.Data);
                            }
                        };

                        process.Start();

                        process.BeginOutputReadLine();
                        process.BeginErrorReadLine();

                        if (process.WaitForExit(timeout))
                        {
                            // do something
                        }

                        standardOutput = output.ToString();
                        standardError = error.ToString();
                    }
                    catch (Exception e)
                    {
                        standardOutput = string.Empty;
                        standardError = e.Message;
                    }
                    finally
                    {
                        outputWaitHandle.WaitOne(timeout);
                        errorWaitHandle.WaitOne(timeout);
                    }
                }
            }
        }
    }
}
