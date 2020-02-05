using Send4.Geral;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Send4.Geral
{
    public class ExtentReport : BaseTest
    {
        #region Properties
        public static string image;
        public static ExtentTest test;
        public static ExtentReports extent;
        private static Screenshot screenshot;
        private static string pathProjetc = AppDomain.CurrentDomain.BaseDirectory.Replace(ConfigurationManager.AppSettings["PATHPROJECT"], @"\TestResults");
        #endregion

        public static void StartTest(string nameTest, string descriptionText)
        {
            test = extent.StartTest(nameTest, descriptionText);
        }

        public static void EndTest(ExtentTest test)
        {
            extent.EndTest(test);
        }

        public static void CreateFileLog()
        {
            ValidationPathFolders(Path.Combine(pathProjetc, "Evidências", dateTime));
            string extentFileName = Path.Combine(pathProjetc, "Evidências", dateTime, "extent.html");
            extent = new ExtentReports(extentFileName, true);
        }

        public static void GenerateScreenshot()
        {
            screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            string pathImage = Path.Combine(ValidationPathFolders(Path.Combine(pathProjetc, "Evidências", dateTime)), "Imagem_" + DateTime.Now.ToString("ddMMyyyyThhmmss") + ".png");
            screenshot.SaveAsFile(pathImage, ScreenshotImageFormat.Png);

            image = test.AddScreenCapture(pathImage);
        }

        public static void SaveLogPassWithScreenshot(string text)
        {
            GenerateScreenshot();
            test.Log(LogStatus.Pass, text + image);
        }

        public static void SaveLogFailWithScreenshot(string text)
        {
            GenerateScreenshot();
            test.Log(LogStatus.Fail, text + image);
        }

        public static void SaveLogInfoWithScreenshot(string text)
        {
            GenerateScreenshot();
            test.Log(LogStatus.Info, text + image);
        }

        public static void SaveLogPass(string text)
        {
            test.Log(LogStatus.Pass, text);
        }

        public static void SaveLogFail(string text)
        {
            test.Log(LogStatus.Fail, text);
        }

        public static void SaveLogInfo(string text)
        {
            test.Log(LogStatus.Info, text);
        }
    }
}
