using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SimpleCalculator
{
	public partial class MainPage : ContentPage
	{
        private string[] operands = new string[2];
        private string _operator;
        private Label calculatorText;

        public MainPage()
		{
			InitializeComponent();
            this.calculatorText = outText;
		}

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
                operands[0] = result.ToString();
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
