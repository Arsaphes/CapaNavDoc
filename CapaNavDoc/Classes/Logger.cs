using System;
using CapaNavDoc.Interfaces;
using log4net;

namespace CapaNavDoc.Classes
{
    public class Logger : ILogger
    {
        private static readonly ILog Log = LogManager.GetLogger("ErrorLogger");


        public void Error(Exception ex)
        {
            Log.Error(ex.Message);
        }

        public void Info(object msg)
        {
            Log.Info(msg);
        }

        public void Debug(string msg)
        {
            Log.Debug(msg);
        }

        public void Error(string msg, Exception ex)
        {
            Log.Error(msg, ex);
        }
    }
}