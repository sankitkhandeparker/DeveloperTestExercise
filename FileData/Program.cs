using System;
using System.Collections.Generic;
using System.Linq;
using ThirdPartyTools;

namespace FileData
{
    public static class Program
    {
        /// <summary>
        /// Console utilities main method from where utility will start
        /// </summary>
        /// <param name="args">Arguments</param>
        public static void Main(string[] args)
        {
            FileAttributeUtility.RunUtility();//Call file attribute check utility
        }
    }
}
