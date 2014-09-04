using System;
using Microsoft.SPOT;

namespace appTest.Debugging
{
    public class Log
    {
        public static void Info(params object[] args)
        {
            Debug.Print(GetMessage(args));
        }

        public static void Warning(params object[] args)
        {
            Debug.Print(GetMessage(args));
        }

        public static void Error(params object[] args)
        {
            Debug.Print(GetMessage(args));
        }

        private static string GetMessage(params object[] args)
        {
            string retVal = "";
            foreach (object o in args)
            {
                retVal += o.ToString();
            }
            return retVal;
        }
    }
}
