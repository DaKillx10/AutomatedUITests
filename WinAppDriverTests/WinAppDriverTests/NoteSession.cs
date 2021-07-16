using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace WinAppDriverTests
{
    public class NoteSession
    {
        protected static WindowsDriver<WindowsElement> NotePadSession;
        protected static WindowsElement EditBox;
        private static Process _winAppProcess;
        private static readonly string _installPath =
            @"C:\Program Files\Notepad++\notepad++.exe";
        private static readonly string _windowsAppDriverUrl = "http://127.0.0.1:4723";

        private static void StartDriverProcess()
        {
            if (_winAppProcess == null)
            {
                _winAppProcess = Process.Start(@"C:\Program Files (x86)\Windows Application Driver\WinAppDriver.exe");
            }
            else
            {
                KillDriverProcess();
            }
        }

        private static void KillDriverProcess()
        {
            _winAppProcess?.Kill();
        }

        public static void SetUp(TestContext context)
        {
            StartDriverProcess();

            if (NotePadSession == null)
            {
                AppiumOptions appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", _installPath);
                appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");

                NotePadSession = new WindowsDriver<WindowsElement>(new Uri(_windowsAppDriverUrl), appiumOptions);

                Assert.IsNotNull(NotePadSession);

                EditBox = NotePadSession.FindElementByClassName("Scintilla");
                Assert.IsNotNull(EditBox);
            }
        }

        public static void TearDown()
        {
            if (NotePadSession != null)
            {
                NotePadSession.Quit();
                NotePadSession = null;
                KillDriverProcess();
            }
        }
    }
}