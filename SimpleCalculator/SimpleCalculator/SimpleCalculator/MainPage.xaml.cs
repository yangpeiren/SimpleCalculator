using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SimpleCalculator
{
    /// <summary>
    /// The main logic of the calculator comes from:
    /// http://resocoder.com/2017/09/15/make-a-calculator-app-with-xamarin-android-2-code/
    /// which used Xamarin.Andriod, here translated to Xamarin.Forms. Some obvious bugs are fixed.
    /// The Layout part comes mainly from the google original calculator:
    /// https://android.googlesource.com/platform/packages/apps/Calculator/
    /// </summary>
	public partial class MainPage : ContentPage
	{
        const int LENGTH = 12; // fractional size
        private string[] operands = new string[2];
        private string _operator;
        private Label calculatorText;

        public MainPage()
		{
			InitializeComponent();
            // assign label in the layout to class
            this.calculatorText = outText;
		}

        public void OnDisplayAbout(object sender, EventArgs e) => DisplayAlert("About", "Author: Peiren Yang" + Environment.NewLine + "Project Address:" + Environment.NewLine + "https://github.com/yangpeiren/SimpleCalculator.git", "OK");

        public void OnButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if ("0123456789.".Contains(button.Text))
                AddDigitOrDecimalPoint(button.Text);
            else if ("÷×+-".Contains(button.Text))
                AddOperator(button.Text);
            else if ("=" == button.Text)
                Calculate();
            else
                Reset();
        }

        private void Reset()
        {
            operands[0] = operands[1] = null;
            _operator = null;
            UpdateCalculatorText();
        }

        private void Calculate(string newOperator = null)
        {
            double? result = null;
            double? first = operands[0] == null ? null : (double?)double.Parse(operands[0]);
            double? second = operands[1] == null ? null : (double?)double.Parse(operands[1]);

            switch (_operator)
            {
                case "÷":
                    if(second != 0)
                        result = first / second;
                    break;
                case "×":
                    result = first * second;
                    break;
                case "+":
                    result = first + second;
                    break;
                case "-":
                    result = first - second;
                    break;
            }

            if (result != null)
            {
                // Set the fractional size of the result
                string temp = result.ToString();
                operands[0] = temp.Length <= LENGTH ? temp : temp.Substring(0, LENGTH); 
                _operator = newOperator;
                operands[1] = null;
                UpdateCalculatorText();
            }
        }

        private void AddOperator(string text)
        {
            if (operands[1] != null)
            {
                Calculate(text);
                return;
            }

            _operator = text;

            UpdateCalculatorText();
        }

        private void AddDigitOrDecimalPoint(string text)
        {
            int index = _operator == null ? 0 : 1;

            if (text == "." && operands[index].Contains("."))
                return;

            operands[index] += text;

            UpdateCalculatorText();
        }

        private void UpdateCalculatorText() => calculatorText.Text = $"{operands[0]} {_operator} {operands[1]}";
    }

}
