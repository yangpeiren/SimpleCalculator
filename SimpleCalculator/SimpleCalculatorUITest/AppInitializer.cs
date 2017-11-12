using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace SimpleCalculatorUITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android.ApkFile(@"D:\My projects\SimpleCalculator\SimpleCalculator\SimpleCalculator\SimpleCalculator.Android\bin\Release\com.companyname.SimpleCalculator.apk")
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                .StartApp();
        }
    }
}

