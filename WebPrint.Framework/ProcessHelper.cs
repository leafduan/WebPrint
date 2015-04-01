using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace WebPrint.Framework
{
    public class ProcessHelper
    {
        //ASP.NET 无法执行CMD命令时 可能是应用程序池的账号权限问题 用系统本地账号一般没问题
        // 参数空格解决 "空格 问题.mp4"
        // 中文乱码解决 UTF8 encoding
        // http://stackoverflow.com/questions/139593/processstartinfo-hanging-on-waitforexit-why
        /// <summary>
        /// timeout in millisecond
        /// </summary>
        /*
        public static void Process(string startFile, string args, int timeout, out string standardOutput,
            out string standardError)
        {
            using (var outputWaitHandle = new AutoResetEvent(false))
            using (var errorWaitHandle = new AutoResetEvent(false))
            {
                using (var process = new Process())
                {
                    //设置进程启动信息属性StartInfo，这是ProcessStartInfo类
                    process.StartInfo.FileName = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(startFile));
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

                    if (process.WaitForExit(timeout) &&
                        outputWaitHandle.WaitOne(timeout) &&
                        errorWaitHandle.WaitOne(timeout))
                    {
                        // Process completed. Check process.ExitCode here.
                    }
                    else
                    {
                        // 如果超时没完成，则强制kill掉
                        Console.WriteLine("timeout");
                        process.Kill(); // 引发 outputWaitHandle 等 disposed 调用问题
                    }

                    standardOutput = output.ToString();
                    standardError = error.ToString();
                }
            }
        }
         * */

        // https://github.com/borismod/OsTestFramework/blob/master/OsTestFramework/ProcessExecutor.cs
        // perfect method
        /// <summary>
        /// timeout in millisecond 
        /// </summary>
        public static void Process(string startFile, string args, int timeout, out string standardOutput,
            out string standardError)
        {
            using (var process = new ProcessExecutor(startFile, args, timeout))
            {
                process.Execute(out standardOutput, out standardError);
            }
        }
    }

    internal class ProcessExecutor : IDisposable
    {
        private readonly StringBuilder error;
        private readonly AutoResetEvent errorWaitHandle;
        private readonly StringBuilder output;
        private readonly int timeout;
        private AutoResetEvent outputWaitHandle;
        private Process process;

        public ProcessExecutor(string startFile, string args, int timeout = 0)
        {
            process = new Process();
            //设置进程启动信息属性StartInfo，这是ProcessStartInfo类
            process.StartInfo.FileName = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(startFile));
            process.StartInfo.Arguments = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(args));

            process.StartInfo.UseShellExecute = false;
            //提供的标准输出流只有2k,超过大小会卡住;如果有大量输出,就读出来
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;

            output = new StringBuilder();
            error = new StringBuilder();

            outputWaitHandle = new AutoResetEvent(false);
            errorWaitHandle = new AutoResetEvent(false);

            this.timeout = timeout;

            RegisterToEvents();
        }

        public void Dispose()
        {
            UnregisterFromEvents();

            if (process != null)
            {
                process.Dispose();
                process = null;
            }
            if (errorWaitHandle != null)
            {
                errorWaitHandle.Close();
                outputWaitHandle = null;
            }
            if (outputWaitHandle != null)
            {
                outputWaitHandle.Close();
                outputWaitHandle = null;
            }
        }

        public void Execute(out string standardOutput, out string standardError)
        {
            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            if (process.WaitForExit(timeout) &&
                outputWaitHandle.WaitOne(timeout) &&
                errorWaitHandle.WaitOne(timeout))
            {

            }
            else
            {
                // if timeout then kill the procee
                output.AppendLine("timeout in limited time: '{0}'".Formatting(timeout));
                process.Kill();
            }

            standardOutput = output.ToString();
            standardError = error.ToString();
        }

        private void RegisterToEvents()
        {
            process.OutputDataReceived += process_OutputDataReceived;
            process.ErrorDataReceived += process_ErrorDataReceived;
        }

        private void UnregisterFromEvents()
        {
            process.OutputDataReceived -= process_OutputDataReceived;
            process.ErrorDataReceived -= process_ErrorDataReceived;
        }

        private void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
            {
                errorWaitHandle.Set();
            }
            else
            {
                error.AppendLine(e.Data);
            }
        }

        private void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
            {
                outputWaitHandle.Set();
            }
            else
            {
                output.AppendLine(e.Data);
            }
        }
    }
}