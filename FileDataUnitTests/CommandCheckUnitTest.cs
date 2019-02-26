using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileData;
namespace FileDataUnitTests
{
    [TestClass]
    public class CommandCheckUnitTest
    {
        [TestMethod]
        public void NocommandTest()
        {
            string userInput = "NOCOMMAND";
            string filePath = string.Empty;
            string expectedCommandResult = "Invalid";
            string actualCommandResult = FileData.FileAttributeUtility.ValidateAndExtractCommand(userInput, out filePath);
            Assert.AreEqual(expectedCommandResult, actualCommandResult, "No command test failed.");
        }
        [TestMethod]
        public void InvalidCommandTest()
        {
            string userInput = "-a";
            string filePath = string.Empty;
            string expectedCommandResult = "Invalid";
            string actualCommandResult = FileData.FileAttributeUtility.ValidateAndExtractCommand(userInput, out filePath);
            Assert.AreEqual(expectedCommandResult, actualCommandResult, "Invalid command test failed.");
        }
        [TestMethod]
        public void PossibleInvalidCommandTest()
        {
            string userInput = "--Ext ope.txt";
            string filePath = string.Empty;
            string expectedCommandResult = "Invalid";
            string actualCommandResult = FileData.FileAttributeUtility.ValidateAndExtractCommand(userInput, out filePath);
            Assert.AreEqual(expectedCommandResult, actualCommandResult, "Possible invalid command test failed.");
        }
        [TestMethod]
        public void VersionCommandWithNoFileTest()
        {
            string userInput = "--version";
            string filePath = string.Empty;
            string expectedCommandResult = "Invalid";
            string actualCommandResult = FileData.FileAttributeUtility.ValidateAndExtractCommand(userInput, out filePath);
            Assert.AreEqual(expectedCommandResult, actualCommandResult, "Version command with no file test failed.");
        }
        [TestMethod]
        public void VersionCommandWithFileTest()
        {
            string userInput = "--version open.txt";
            string filePath = string.Empty;
            string expectedCommandResult = "Version";
            string actualCommandResult = FileData.FileAttributeUtility.ValidateAndExtractCommand(userInput, out filePath);
            Assert.AreEqual(expectedCommandResult, actualCommandResult, "Size command with file test failed.");
            Assert.AreEqual("open.txt", filePath, "Version command returned incorrect filename.");
        }
        [TestMethod]
        public void SizeCommandWithNoFileTest()
        {
            string userInput = "--size";
            string filePath = string.Empty;
            string expectedCommandResult = "Invalid";
            string actualCommandResult = FileData.FileAttributeUtility.ValidateAndExtractCommand(userInput, out filePath);
            Assert.AreEqual(expectedCommandResult, actualCommandResult, "Invalid command test failed.");
        }
        [TestMethod]
        public void SizeCommandWithFileTest()
        {
            string userInput = "--size open.txt";
            string filePath = string.Empty;
            string expectedCommandResult = "Size";
            string actualCommandResult = FileData.FileAttributeUtility.ValidateAndExtractCommand(userInput, out filePath);
            Assert.AreEqual(expectedCommandResult, actualCommandResult, "Size command with file test failed.");
            Assert.AreEqual("open.txt", filePath, "Size command returned incorrect filename.");
        }
    }
}
