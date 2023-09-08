using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
	public sealed partial class Currency_Calculator_Page : Page
	{
		// Define the conversion rates
		private Dictionary<string, Dictionary<string, double>> conversionRates = new Dictionary<string, Dictionary<string, double>>()
		{
			{"US Dollar", new Dictionary<string, double>(){
				{"Euro", 0.85189982},
				{"British Pound", 0.72872436},
				{"Indian Rupee", 74.257327}
			}},
			{"Euro", new Dictionary<string, double>(){
				{"US Dollar", 1.1739732},
				{"British Pound", 0.8556672},
				{"Indian Rupee", 87.00755}
			}},
			{"British Pound", new Dictionary<string, double>(){
				{"US Dollar", 1.371907},
				{"Euro", 1.1686692},
				{"Indian Rupee", 101.68635}
			}},
			{"Indian Rupee", new Dictionary<string, double>(){
				{"US Dollar", 0.011492628},
				{"Euro", 0.013492774},
				{"British Pound", 0.0098339397}
			}},
		};

		public Currency_Calculator_Page()
		{
			this.InitializeComponent();
		}

		private void calcButton_Click(object sender, RoutedEventArgs e)
		{
			// Get selected currencies and amount
			string fromCurrency = fromValueComboBox.SelectedItem.ToString();
			string toCurrency = toValueComboBox.SelectedItem.ToString();
			double amount;

			if (double.TryParse(amountValueTextBox.Text, out amount))
			{
				// Perform the conversion
				double convertedAmount = ConvertCurrency(fromCurrency, toCurrency, amount);

				// Display the results
				entryAmount.Text = $"From {amount} {fromCurrency}";
				currencyConversion.Text = $"{convertedAmount} {toCurrency}";
				fromConversionDetails.Text = $"{fromCurrency} > {toCurrency} Conversion";
				toConversionDetails.Text = $"{toCurrency} > {fromCurrency} Conversion";
			}
			else
			{
				// Handle invalid input
				entryAmount.Text = "Invalid amount entered.";
				currencyConversion.Text = "";
				fromConversionDetails.Text = "";
				toConversionDetails.Text = "";
			}
		}

		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			// Return to MainMenu for additional selections
			this.Frame.Navigate(typeof(MainMenu));
		}

		private double ConvertCurrency(string fromCurrency, string toCurrency, double amount)
		{
			if (conversionRates.ContainsKey(fromCurrency) && conversionRates[fromCurrency].ContainsKey(toCurrency))
			{
				double rate = conversionRates[fromCurrency][toCurrency];
				return amount * rate;
			}
			else
			{
				// Handle unsupported currency conversion
				return 0.0;
			}
		}
	}
}
