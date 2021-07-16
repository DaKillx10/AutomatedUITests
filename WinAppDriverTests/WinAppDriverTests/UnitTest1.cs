using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace WinAppDriverTests
{
    [TestClass]
    public class UnitTest1 : NoteSession
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            SetUp(context);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            EditBox.SendKeys(Keys.Control + "a" + Keys.Control);
            EditBox.SendKeys(Keys.Delete);
            Assert.AreEqual(string.Empty, EditBox.Text);
        }

        [TestMethod]
        public void ShowHelp()
        {
            var helpItem = NotePadSession.FindElementByName("?");
            helpItem.Click();

            var infoItem = helpItem.FindElementByAccessibilityId("47000");
            infoItem.Click();

            var dialog = NotePadSession.FindElementByClassName("#32770");
            dialog.Click();

            var okButton = dialog.FindElementByAccessibilityId("1");
            okButton.Click();

        }
        [TestMethod]
        public void EditNote()
        {
            EditBox.SendKeys("abcdeABCDE 12345" + Keys.Shift + "7890");
            Assert.AreEqual(@"abcdeABCDE 12345/()=", EditBox.Text);

            EditBox.SendKeys(Keys.Control + "a");
            EditBox.SendKeys(Keys.Backspace);
            EditBox.Click();

            EditBox.SendKeys("Test");
            EditBox.SendKeys(Keys.Control + "a" + Keys.Control);
            EditBox.SendKeys(Keys.Control + "c" + Keys.Control);
            EditBox.SendKeys(Keys.Control + "vvv" + Keys.Control);
            Assert.AreEqual("TestTestTest", EditBox.Text);

            EditBox.SendKeys(Keys.Control + "a");
            EditBox.SendKeys(Keys.Backspace);

            EditBox.SendKeys("012345678910" + Keys.Shift + "0134");
            Assert.AreEqual("012345678910=!§$", EditBox.Text);

            EditBox.SendKeys(Keys.Control + "a");
            EditBox.SendKeys(Keys.Backspace);


            EditBox.SendKeys(Keys.Control + "a");
            EditBox.SendKeys(Keys.Backspace);

            EditBox.SendKeys("Dies ist ein Testdokument, welches mit WinAppDriver Tests erstellt wurde!");
            Assert.AreEqual("Dies ist ein Testdokument, welches mit WinAppDriver Tests erstellt wurde!", EditBox.Text);

            EditBox.SendKeys(Keys.Control + "a" + Keys.Control);
            EditBox.SendKeys(Keys.Delete);
            Assert.AreEqual(string.Empty, EditBox.Text);


            EditBox.SendKeys(Keys.Control + Keys.Shift + "w");

        }
    }
}
