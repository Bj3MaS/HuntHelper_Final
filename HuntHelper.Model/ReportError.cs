using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntHelper.Model
{
    /// <summary>
    /// Write all the execption to a spesific file.
    /// </summary>
    public static class ReportError
    {
        /// <summary>
        /// Errors the asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        public static async void ErrorAsync(string message)
        {
            
            string tempPath = Path.GetFullPath(Path.Combine(System.IO.Path.GetTempPath(), @"Error.txt"));

            try
            {
                await Task.Run(() =>
                {
                    if (File.Exists(tempPath))
                    {
                        try
                        {
                            File.AppendAllText(tempPath, message + Environment.NewLine + Environment.NewLine);
                        }

                        catch
                        {

                        }
                        
                       

                    }

                    else
                    {
                        try
                        {
                            var file = File.Create(tempPath);
                            File.AppendAllText(tempPath, message + Environment.NewLine + Environment.NewLine);
                            file.Dispose();
                        }
                        catch{

                        }
                      
                    }
                });
            }

            catch
            {

            }
            
        }
    }
}
