using Acme.Data.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Data.Repositories.Utils
{
    public static class Utility
    {
        public static AcmeErrorLog GetErrorLogObject(Exception ex, string methodName = "")
        {
            var exceptionLog = new AcmeErrorLog()
            {
                ClassName = "UnitOfWork",
                MethodName = methodName,
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace,
                ExceptionDate = DateTime.Now
            };

            return exceptionLog;
        }

        public static void LogErrorInFile(Exception ex)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"********************************* Exception - {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} *********************************");
            builder.AppendLine();
            builder.AppendLine($"Exception: { ex.Message}");
            builder.AppendLine($"Stacktrace: { ex.StackTrace }");
            builder.AppendLine();
            builder.AppendLine($"******************************************************************************************************************************");

            string fileName = $"AcmeErrorLog_{DateTime.Now.ToString("yyyyMMdd")}.txt";

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + fileName,
                  builder.ToString());

            throw new Exception($"The exception has been logged into the file:  {fileName}");
        }
    }
}
