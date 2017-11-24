using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace SimpleCalculator.UITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        [Category("Default Test")]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        [Category("ButtonPressUITest")]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(0)]
        public void ButtonPressUITest(int buttonNumber)
        {
            //Arrange
            app.Tap("button" + buttonNumber.ToString());

            //Act
            app.Tap("buttonEqual");

            //Assert
            var appResult = app.Query("resultText").First(result => result.Text.Trim() == buttonNumber.ToString());
            Assert.IsTrue(appResult != null, "Result is not correct!");
        }

        [Test]
        [Category("AddTestUITest")]
        [TestCase(1, 2, null)]
        [TestCase(1, 2, 3)]
        [TestCase(7, 8, null)]
        [TestCase(1, 4, 9)]
        [TestCase(1, 0, 0)]
        public void AddTestUITest(int number1, int number2, int? number3)
        {
            //Arrange
            app.Tap("button" + number1.ToString());
            app.Tap("buttonAdd");
            app.Tap("button" + number2.ToString());
            if (number3 != null)
            {
                app.Tap("buttonAdd");
                app.Tap("button" + number3.ToString());
            }

            //Act
            app.Tap("buttonEqual");

            //Assert
            int value = number3 == null ? number1 + number2 : number1 + number2 + (int)number3;
            var appResult = app.Query("resultText").First(result => result.Text.Trim() == value.ToString());
            Assert.IsTrue(appResult != null, "Result is not correct!");
        }

        [Test]
        [Category("MinusTestUITest")]
        [TestCase(2, 1)]
        [TestCase(1, 2)]
        [TestCase(9, 4)]
        [TestCase(1, 0)]
        public void MinusTestUITest(int number1, int number2)
        {
            //Arrange
            app.Tap("button" + number1.ToString());
            app.Tap("buttonMinus");
            app.Tap("button" + number2.ToString());

            //Act
            app.Tap("buttonEqual");

            //Assert
            var appResult = app.Query("resultText").First(result => result.Text.Trim() == (number1 - number2).ToString());
            Assert.IsTrue(appResult != null, "Result is not correct!");
        }

        [Test]
        [Category("MultiplyTestUITest")]
        [TestCase(2, 1)]
        [TestCase(1, 2)]
        [TestCase(9, 4)]
        [TestCase(1, 0)]
        public void MultiplyTestUITest(int number1, int number2)
        {
            //Arrange
            app.Tap("button" + number1.ToString());
            app.Tap("buttonMultiply");
            app.Tap("button" + number2.ToString());

            //Act
            app.Tap("buttonEqual");

            //Assert
            var appResult = app.Query("resultText").First(result => result.Text.Trim() == (number1 * number2).ToString());
            Assert.IsTrue(appResult != null, "Result is not correct!");
        }

        [Test]
        [Category("DivideTestUITest")]
        [TestCase(2, 1)]
        [TestCase(1, 2)]
        [TestCase(9, 4)]
        [TestCase(1, 9)]
        public void DivideTestUITest(int number1, int number2)
        {
            //Arrange
            app.Tap("button" + number1.ToString());
            app.Tap("buttonDivide");
            app.Tap("button" + number2.ToString());

            //Act
            app.Tap("buttonEqual");

            //Assert
            string value = ((double)number1 / number2).ToString();
            if (value.Length > 12)
                value = value.Substring(0, 12);
            var appResult = app.Query("resultText").First(result => result.Text.Trim() == value);
            Assert.IsTrue(appResult != null, "Result is not correct!");
        }
    }
}

