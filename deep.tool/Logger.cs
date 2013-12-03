using log4net;
using System;
using System.IO;
using System.Text;

public class Logger
{
    private static readonly string LOG_DIR = (AppDomain.CurrentDomain.BaseDirectory + "logs");
    private static readonly string FILE_PATH = (LOG_DIR + @"\" + DateTime.Now.ToString("yy-M-d") + ".log");
    private static ILog lg = LogManager.GetLogger("AppInfo");   
    private static StringBuilder sb = new StringBuilder();

    public static void Info(object message)
    {
        lg.Info(message);
    }

    public static void Info(object message, Exception exception)
    {
        lg.Info(message, exception);
    }

    public static void log(object obj)
    {
        try
        {
            if (File.Exists(FILE_PATH))
            {
                sb.Length = 0;
                sb.AppendLine("==============================================================================");
                sb.AppendFormat("Date:\t{0}     Time:\t{1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
                sb.AppendLine();
               
                sb.AppendLine(obj.ToString());
                StreamWriter writer = File.AppendText(FILE_PATH);
                writer.WriteLine(sb.ToString());
                writer.Flush();
                writer.Close();
            }
            else
            {
                if (!Directory.Exists(LOG_DIR))
                {
                    Directory.CreateDirectory(LOG_DIR);
                }
                File.Create(FILE_PATH).Close();
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.ToString());
        }
    }
}

