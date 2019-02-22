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
            string strUserInput = "NOCOMMAND";
            string strFilePath = string.Empty;
            string strExpectedCommandResult = "Invalid";
            string strActualCommandResult = FileData.FileAttributeUtility.ValidateAndExtractCommand(strUserInput, out strFilePath);
            Assert.AreEqual(strExpectedCommandResult, strActualCommandResult, "No command test failed.");
        }
        [TestMethod]
        public void InvalidCommandTest()
        {
            string strUserInput = "-a";
            string strFilePath = string.Empty;
            string strExpectedCommandResult = "Invalid";
            string strActualCommandResult = FileData.FileAttributeUtility.ValidateAndExtractCommand(strUserInput, out strFilePath);
            Assert.AreEqual(strExpectedCommandResult, strActualCommandResult, "Invalid command test failed.");
        }
        [TestMethod]
        public void PossibleInvalidCommandTest()
        {
            string strUserInput = "--Ext ope.txt";
            string strFilePath = string.Empty;
            string strExpectedCommandResult = "Invalid";
            string strActualCommandResult = FileData.FileAttributeUtility.ValidateAndExtractCommand(strUserInput, out strFilePath);
            Assert.AreEqual(strExpectedCommandResult, strActualCommandResult, "Possible invalid command test failed.");
        }
        [TestMethod]
        public void VersionCommandWithNoFileTest()
        {
            string strUserInput = "--version";
            string strFilePath = string.Empty;
            string strExpectedCommandResult = "Invalid";
            string strActualCommandResult = FileData.FileAttributeUtility.ValidateAndExtractCommand(strUserInput, out strFilePath);
            Assert.AreEqual(strExpectedCommandResult, strActualCommandResult, "Version command with no file test failed.");
        }
        [TestMethod]
        public void VersionCommandWithFileTest()
        {
            string strUserInput = "--version open.txt";
            string strFilePath = string.Empty;
            string strExpectedCommandResult = "Version";
            string strActualCommandResult = FileData.FileAttributeUtility.ValidateAndExtractCommand(strUserInput, out strFilePath);
            Assert.AreEqual(strExpectedCommandResult, strActualCommandResult, "Size command with file test failed.");
            Assert.AreEqual("open.txt", strFilePath, "Version command returned incorrect filename.");
        }
        [TestMethod]
        public void SizeCommandWithNoFileTest()
        {
            string strUserInput = "--size";
            string strFilePath = string.Empty;
            string strExpectedCommandResult = "Invalid";
            string strActualCommandResult = FileData.FileAttributeUtility.ValidateAndExtractCommand(strUserInput, out strFilePath);
            Assert.AreEqual(strExpectedCommandResult, strActualCommandResult, "Invalid command test failed.");
        }
        [TestMethod]
        public void SizeCommandWithFileTest()
        {
            string strUserInput = "--size open.txt";
            string strFilePath = string.Empty;
            string strExpectedCommandResult = "Size";
            string strActualCommandResult = FileData.FileAttributeUtility.ValidateAndExtractCommand(strUserInput, out strFilePath);
            Assert.AreEqual(strExpectedCommandResult, strActualCommandResult, "Size command with file test failed.");
            Assert.AreEqual("open.txt", strFilePath, "Size command returned incorrect filename.");
        }
    }
}
