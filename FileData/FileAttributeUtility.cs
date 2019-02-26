using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ThirdPartyTools;

namespace FileData
{
    public class FileAttributeUtility
    {
        static string __versionCommands, __sizeCommands, __exitCommand;
        public FileAttributeUtility()
        {
            
        }
        /// <summary>
        /// Utility entry point which will take crae of the utility functionality
        /// </summary>
        public static void RunUtility()
        {
            bool exitFlag = false;//Flag true means utility will close
            string strInput, strCommmand, filePath = string.Empty;
          
            FileDetails objFileDetails = new FileDetails();//File details object to access version and size methods

            ShowUtilityIntroduction();//Utility introduction line
            while (!exitFlag)
            {
                try
                {
                    strInput = Console.ReadLine().Trim();
                    strCommmand = ValidateAndExtractCommand(strInput, out filePath);

                    var isFileValid = !string.IsNullOrEmpty(filePath) && (filePath.IndexOfAny(Path.GetInvalidFileNameChars()) < 0);

                    switch (strCommmand) //Based on verified command select case
                    {
                        case "Version":
                            if (isFileValid)//File name check
                            {
                                //Call version function
                                string strFileVersion = objFileDetails.Version(filePath);
                                Console.WriteLine("\nFile Version Information");
                                Console.WriteLine("File Version: " + strFileVersion);
                                ShowUtilityIntroduction();
                            }
                            else
                            {
                                Console.WriteLine("File name enter is invalid.");
                                ShowUtilityIntroduction();
                            }
                            break;
                        case "Size":
                            if (isFileValid)//File name check
                            {
                                //Call size function
                                int intFileSize = objFileDetails.Size(filePath);
                                if (int.TryParse(intFileSize.ToString(), out intFileSize))
                                {
                                    Console.WriteLine("\nFile Size Information");
                                    Console.WriteLine("File Size: " + intFileSize.ToString());
                                    ShowUtilityIntroduction();
                                }
                                else
                                {
                                    new Exception("\nInterger conversion failed for file size");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nFile name enter is invalid.");
                                ShowUtilityIntroduction();
                            }
                            break;
                        case "Exit":
                            exitFlag = true;
                            break;
                        case "Invalid":
                            Console.WriteLine("\nPlease enter valid command.");
                            ShowUtilityIntroduction();
                            break;
                    }
                }
                catch (Exception exce)
                {
                    Console.WriteLine("\nInternal error occured! ");
                    ShowUtilityIntroduction();
                }
            }
        }
        /// <summary>
        /// Method process input from the user and extract version/size command from the same. 
        /// If invalid command entered then method would return "Invalid"
        /// </summary>
        /// <param name="strInput">User Input</param>
        /// <param name="filepath">Output parameter to which will pass filepath</param>
        /// <returns></returns>
        public static string ValidateAndExtractCommand(string strInput, out string filepath)
        {
            __versionCommands = System.Configuration.ConfigurationManager.AppSettings.GetValues("Version").FirstOrDefault().ToString();
            __sizeCommands = System.Configuration.ConfigurationManager.AppSettings.GetValues("Size").FirstOrDefault().ToString();
            __exitCommand = System.Configuration.ConfigurationManager.AppSettings.GetValues("Exit").FirstOrDefault().ToString();
            string strCommmand = string.Empty;
            filepath = string.Empty;
            if (strInput == string.Empty)//No input provided
            {
                strCommmand = "Invalid";
            }
            else
            {
                string[] arguments = strInput.ToLower().Split(' ');
                if (arguments.Length != 2) //Proper command format is not provided
                {
                    if (arguments[0] == __exitCommand)//Check for exit command type
                        strCommmand = "Exit";
                    else
                        strCommmand = "Invalid";
                }
                else
                {

                    if (__versionCommands.Split(',').Contains(arguments[0])) //Check for version command type
                    {
                        strCommmand = "Version";
                        filepath = arguments[1];
                    }
                    else if (__sizeCommands.Split(',').Contains(arguments[0]))//Check for size command type
                    {
                        strCommmand = "Size";
                        filepath = arguments[1];
                    }
                    else
                    {
                        strCommmand = "Invalid";
                    }
                }
            }
            return strCommmand;
        }
        /// <summary>
        /// Common method to display introductory lines in the utility whenever required
        /// </summary>
        private static void ShowUtilityIntroduction()
        {
            Console.WriteLine("\nWelcome to File Utility Version/Size Check");
            Console.WriteLine("Enter following command(not case sensitive) along with FilePath");
            Console.WriteLine("\tFor getting version information type: -v, --v, /v, --version ");
            Console.WriteLine("\tFor getting file size type: -s, --s, /s, --size");
            Console.WriteLine("\tTo exit this utility type: exit");
        }
    }
}
