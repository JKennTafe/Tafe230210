using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class Mortgage_Calculator_Page : Page
	{
		public Mortgage_Calculator_Page()
		{
			this.InitializeComponent();
		}

		private async void calculateButton_Click(object sender, RoutedEventArgs e)
		{
			//			M = P [ i(1 + i)^n ] / [ (1 + i)^n – 1]
			//			P = principal loan amount
			//			i = monthly interest rate
			//			n = number of months required to repay the loan
			//			For example, if the annual interest rate is 4 %, the monthly interest rate would be 0.33 % (0.04 / 12 = 0.0033).

			//			A 30 - year mortgage would require 360 monthly payments, while a 15 - year mortgage would require exactly half that number of monthly payments, or 180.
			//			Hints: use C# Math.Pow() function for calculating exponential.

			double monthlyInterestRate, n, m;
			int principal;
			double loanYears = double.Parse(yearsTextBox.Text);
			double loanMonths = double.Parse(andMonthsTextBox.Text);
			double yearInterestRate = double.Parse(yearlyInterestRateTextBox.Text);



			try
			{
				principal = int.Parse(principalBorrowTextBox.Text);
			}
			catch
			{
				var errPrincipalBorrow = new MessageDialog("Error, please enter a correct amount in Principal Borrow $");
				await errPrincipalBorrow.ShowAsync();
				principalBorrowTextBox.SelectAll();
				principalBorrowTextBox.Focus(FocusState.Programmatic);
				return;
			}

			monthlyInterestRate = yearInterestRate /100 / 12;

			n = (loanYears * 12) + loanMonths;

			monthlyInterestRateTextBox.Text = monthlyInterestRate.ToString("P");



			double monthlyRepayment = principal * (monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, n)) / (Math.Pow(1 + monthlyInterestRate, n) - 1);
			monthlyRepayment = Math.Round(monthlyRepayment, 2);
			mothlyRepaymentAmountTextBox.Text = monthlyRepayment.ToString();
		}

		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(MainMenu));
		}
	}
}
