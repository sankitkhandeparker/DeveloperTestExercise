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
        public FileAttributeUtility()
        {
            
        }
        /// <summary>
        /// Utility entry point which will take care of the utility functionality
        /// </summary>
        public static void RunUtility()
        {
            bool exitFlag = false;
            string userInput, commmand, filePath = string.Empty;
          
            FileDetails fileDetails = new FileDetails();

            ShowUtilityIntroduction();
            while (!exitFlag)
            {
                try
                {
                    userInput = Console.ReadLine().Trim();
                    commmand = ValidateAndExtractCommand(userInput, out filePath);

                    var isFileValid = !string.IsNullOrEmpty(filePath) && (filePath.IndexOfAny(Path.GetInvalidFileNameChars()) < 0);

                    switch (commmand) 
                    {
                        case "Version":
                            if (isFileValid)
                            {
                                                               string strFileVersion = fileDetails.Version(filePath);
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
                            if (isFileValid)
                            {
                                                               int intFileSize = fileDetails.Size(filePath);
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
        /// <param name="userInput">User Input</param>
        /// <param name="filepath">Output parameter to which will pass filepath</param>
        /// <returns></returns>
        public static string ValidateAndExtractCommand(string userInput, out string filepath)
        {
            string versionCommands, sizeCommands, exitCommand;
            versionCommands = System.Configuration.ConfigurationManager.AppSettings.GetValues("Version").FirstOrDefault().ToString();
            sizeCommands = System.Configuration.ConfigurationManager.AppSettings.GetValues("Size").FirstOrDefault().ToString();
            exitCommand = System.Configuration.ConfigurationManager.AppSettings.GetValues("Exit").FirstOrDefault().ToString();
            string commmand = string.Empty;
            filepath = string.Empty;
            if (userInput == string.Empty)
            {
                commmand = "Invalid";
            }
            else
            {
                string[] arguments = userInput.ToLower().Split(' ');
                if (arguments.Length != 2) 
                {
                    if (arguments[0] == exitCommand)
                        commmand = "Exit";
                    else
                        commmand = "Invalid";
                }
                else
                {

                    if (versionCommands.Split(',').Contains(arguments[0]))
                    {
                        commmand = "Version";
                        filepath = arguments[1];
                    }
                    else if (sizeCommands.Split(',').Contains(arguments[0]))
                    {
                        commmand = "Size";
                        filepath = arguments[1];
                    }
                    else
                    {
                        commmand = "Invalid";
                    }
                }
            }
            return commmand;
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
