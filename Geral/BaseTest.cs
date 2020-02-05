using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Events;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using NUnit.Framework.Interfaces;
using System.Configuration;


namespace Send4.Geral
{
    public abstract class BaseTest
    {
        #region Properties
        public IWebDriver webDriver;
        public static EventFiringWebDriver driver;
        public static Actions action;
        private TimeSpan defaultTimeOut = new TimeSpan(0, 0, 360);
        public static string Url = ConfigurationManager.AppSettings["URL"];
        private static string pathDriverBrowser = AppDomain.CurrentDomain.BaseDirectory.Replace(ConfigurationManager.AppSettings["PATHDRIVERBROWSER"], @"\Drivers");
        public static String dateTime = DateTime.Now.ToString("ddMMyyyyThhmmss");
        public static string nameScenario, idLog;
        public static LogStatus statuScenario;
        private static int contadorExtentResports = 0;
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;
        #endregion

        #region Attributes
        [SetUp]
        public void Setup()
        {
            statuScenario = new LogStatus();

            if (contadorExtentResports == 0)
            {
                ExtentReport.CreateFileLog();
                contadorExtentResports += 1;
            }
            else
            {
                contadorExtentResports += 1;
            }

            ExecuteCMD("taskkill /im chromedriver.exe /f /t");
            ExecuteCMD("taskkill /im chrome.exe /f /t");
            webDriver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory);

            driver = new EventFiringWebDriver(webDriver);
            driver.ElementClicking += DriverElementClicking;
            driver.ElementClicked += DriverElementClicked;
            driver.ElementValueChanging += DriverElementValueChanging;
            driver.ElementValueChanged += DriverElementValueChanged;

            action = new Actions(driver);

            driver.Navigate().GoToUrl(Url);

            driver.Manage().Window.Maximize();

            action = new Actions(driver);
        }

        [TearDown]
        public void Cleanup()
        {
            if (TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Success))
            {
                LogSucessoEAddPrint(true, statuScenario);
            }
            else
            {
                LogSucessoEAddPrint(false, statuScenario);
            }

            webDriver.Quit();
            driver.Quit();
            ExtentReport.extent.Flush();

            ExecuteCMD("taskkill /im chromedriver.exe /f /t");
            ExecuteCMD("taskkill /im chrome.exe /f /t");

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void LogSucessoEAddPrint(bool aux, LogStatus logStatus)
        {
            ExtentReport.EndTest(ExtentReport.test);

            if (aux == true)
            {
                if (logStatus.Equals(LogStatus.Pass))
                {
                    if (ExtentReport.image != null)
                    {
                        ExtentReport.test.Log(LogStatus.Pass, "Teste executado com sucesso!" + ExtentReport.image);
                    }
                    else
                    {
                        ExtentReport.test.Log(LogStatus.Pass, "Teste executado com sucesso!");
                    }
                }
                else
                {
                    if (ExtentReport.image != null)
                    {
                        ExtentReport.test.Log(LogStatus.Fail, "Falha na execução do cenário!" + ExtentReport.image);
                    }
                    else
                    {
                        ExtentReport.test.Log(LogStatus.Fail, "Falha na execução do cenário!");
                    }
                }
            }
            else
            {
                if (ExtentReport.image != null)
                {
                    ExtentReport.test.Log(LogStatus.Fail, "Teste com falha, favor verificar a evidência do teste." + ExtentReport.image);
                }
                else
                {
                    ExtentReport.test.Log(LogStatus.Fail, "Teste com falha, favor verificar a evidência do teste.");
                }
            }
        }
        #endregion

        #region Methods
        public List<KeyValuePair<string, string>> ExecuteCMD(string command)
        {
            Process process = new Process();
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/C " + command;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            process.WaitForExit();

            list.Add(new KeyValuePair<string, string>("Output", process.StandardOutput.ReadToEnd()));
            list.Add(new KeyValuePair<string, string>("Error", process.StandardError.ReadToEnd()));

            return list;
        }

        public static void SelecionarBrowser(int index)
        {
            driver.SwitchTo().Window(driver.WindowHandles[index]);
        }

        public string GetErrorMessage()
        {
            const BindingFlags privateGetterFlags = System.Reflection.BindingFlags.GetField |
                                                System.Reflection.BindingFlags.GetProperty |
                                                System.Reflection.BindingFlags.NonPublic |
                                                System.Reflection.BindingFlags.Instance |
                                                System.Reflection.BindingFlags.FlattenHierarchy;

            var m_message = string.Empty; // Returns empty if TestOutcome is not failed
                                          //if (TestContext.CurrentContext.Result.Outcome == )
                                          //{
                                          // Get hold of TestContext.m_currentResult.m_errorInfo.m_stackTrace (contains the stack trace details from log)
            var field = TestContext.GetType().GetField("m_currentResult", privateGetterFlags);
            var m_currentResult = field.GetValue(TestContext);
            field = m_currentResult.GetType().GetField("m_errorInfo", privateGetterFlags);
            var m_errorInfo = field.GetValue(m_currentResult);
            field = m_errorInfo.GetType().GetField("m_stackTrace", privateGetterFlags);
            m_message = field.GetValue(m_errorInfo) as string;
            //}

            return m_message;
        }

        private static string GetDescription(WebElementEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(e.Element.GetAttribute("id")))
                {
                    return e.Element.TagName + " " + e.Element.GetAttribute("type") + " [Id=" + e.Element.GetAttribute("id") + ']';
                }

                if (!string.IsNullOrEmpty(e.Element.GetAttribute("class")))
                {
                    return e.Element.TagName + " " + e.Element.GetAttribute("type") + " [Class=" + e.Element.GetAttribute("class") + ']';
                }

                if (!string.IsNullOrEmpty(e.Element.GetAttribute("name")))
                {
                    return e.Element.TagName + " " + e.Element.GetAttribute("type") + " [Name=" + e.Element.GetAttribute("name") + ']';
                }

                if (!string.IsNullOrEmpty(e.Element.GetAttribute("innertext")))
                {
                    return e.Element.TagName + " " + e.Element.GetAttribute("type") + " [InnerText=" + e.Element.GetAttribute("innertext") + ']';
                }

                if (!string.IsNullOrEmpty(e.Element.GetAttribute("xpath")))
                {
                    return e.Element.TagName + " " + e.Element.GetAttribute("type") + " [xpath=" + e.Element.GetAttribute("xpath") + ']';
                }
            }
            catch (Exception) { }

            return "";
        }

        public static void Wait(int milliSeconds)
        {
            Thread.Sleep(milliSeconds);
        }

        public static string CurrentDate(int typeDate)
        {
            string date = "";

            switch (typeDate)
            {
                case 0:
                    date = DateTime.Now.ToString("dd/MM/yyyy");
                    break;

                case 1:
                    date = DateTime.Now.ToString("dd-MM-yyyy");
                    break;

                default:
                    date = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
                    break;
            }

            return date;
        }

        public static bool WaitElement(By by, int timeoutSeconds = 10)
        {
            int count = 0;
            bool displayed = false;

            while (driver.FindElements(by).Count.Equals(0) || !displayed)
            {
                if (driver.FindElements(by).Any(e => e.Displayed))
                {
                    displayed = true;
                }

                Wait(1000);

                count++;

                if (count > timeoutSeconds)
                {
                    break;
                }
            }

            return displayed;
        }

        public static string ValidationPathFolders(string path)
        {
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        public static void Clicks(By by)
        {
            if (WaitElement(by) != false)
            {
                driver.FindElement(by).Click();
            }
        }

        public static void ClicksCheckBoxAngular(By by)
        {
            driver.ExecuteScript("arguments[0].click()", driver.FindElement(by));
        }

        public static void Sendkeys(By by, string text)
        {
            if (WaitElement(by) != false)
            {
                driver.FindElement(by).SendKeys(text);
            }
        }

        public static void SendKeysJavaScript(By by, string text)
        {
            if (WaitElement(by) != false)
            {
                driver.FindElement(by).Clear();
                driver.ExecuteScript("arguments[0].setAttribute('value', '" + text +"')", driver.FindElement(by));
                driver.ExecuteScript("arguments[0].value='" + text +"';", driver.FindElement(by));
            }
        }

        public static string Text(By by)
        {
            string text = "";

            if (WaitElement(by) != false)
            {
                text = driver.FindElement(by).Text.Trim();
            }

            return text;
        }

        public static string GetAttribute(By by)
        {
            string text = "";

            if (WaitElement(by) != false)
            {
                text = driver.FindElement(by).GetAttribute("value");
            }

            return text;
        }

        public static void Clear(By by)
        {
            if (WaitElement(by) != false)
            {
                driver.FindElement(by).Clear();
            }
        }

        public static IReadOnlyList<IWebElement> ListElement(By by)
        {
            IReadOnlyList<IWebElement> listElement = new List<IWebElement>();

            if (WaitElement(by) != false)
            {
                listElement = driver.FindElements(by);
            }

            return listElement;
        }

        public static void ScroolElement(By by)
        {
            driver.ExecuteScript("arguments[0].scrollIntoView()", driver.FindElement(by));
        }

        public static string RepleceString(string strUpdate, string searchString, string updateString)
        {
            strUpdate = strUpdate.Replace(searchString, updateString);

            return strUpdate;
        }

        public static int QuantidadeDiasMes()
        {
            int qtdDiasMes = 0, ano = 0;

            if ((DateTime.Now.Month == 1) || (DateTime.Now.Month == 3) || (DateTime.Now.Month == 5) ||
              (DateTime.Now.Month == 7) || (DateTime.Now.Month == 8) || (DateTime.Now.Month == 10) ||
              (DateTime.Now.Month == 12))
            {
                qtdDiasMes = 31;
            }
            else if ((DateTime.Now.Month == 4) || (DateTime.Now.Month == 6) || (DateTime.Now.Month == 9) ||
                    (DateTime.Now.Month == 11))
            {
                qtdDiasMes = 30;
            }
            else if (DateTime.Now.Month == 2)
            {
                switch (Convert.ToString(Convert.ToInt32(ano) / 400))
                {
                    case "4.05":
                        qtdDiasMes = 29;
                        break;

                    case "4.06":
                        qtdDiasMes = 29;
                        break;

                    case "4.07":
                        qtdDiasMes = 29;
                        break;

                    case "4.08":
                        qtdDiasMes = 29;
                        break;

                    case "4.09":
                        qtdDiasMes = 29;
                        break;

                    case "4.10":
                        qtdDiasMes = 29;
                        break;

                    default:
                        qtdDiasMes = 28;
                        break;
                }
            }

            return qtdDiasMes;
        }
        #endregion

        #region Events
        private static void DriverElementValueChanged(object sender, WebElementEventArgs e)
        {
            ExtentReport.GenerateScreenshot();
            ExtentReport.test.Log(LogStatus.Info, "Antes da inserção do valor: " + GetDescription(e) + ExtentReport.image);
        }

        private static void DriverElementValueChanging(object sender, WebElementEventArgs e)
        {
            ExtentReport.GenerateScreenshot();
            ExtentReport.test.Log(LogStatus.Pass, "Após a inserção do valor: " + GetDescription(e) + ExtentReport.image);
        }

        private static void DriverElementClicking(object sender, WebElementEventArgs e)
        {
            ExtentReport.GenerateScreenshot();
            ExtentReport.test.Log(LogStatus.Info, "Antes da ação: " + GetDescription(e) + ExtentReport.image);
        }

        private static void DriverElementClicked(object sender, WebElementEventArgs e)
        {
            ExtentReport.GenerateScreenshot();
            ExtentReport.test.Log(LogStatus.Pass, "Após a ação: " + GetDescription(e) + ExtentReport.image);
        }
        #endregion
    }
}
