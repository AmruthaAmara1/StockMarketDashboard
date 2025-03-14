using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace StockMarketDashboard
{
    public partial class MainWindow : Window
    {
        private readonly StockDataService _stockDataService;
        public ObservableCollection<StockData> Stocks { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _stockDataService = new StockDataService();
            Stocks = new ObservableCollection<StockData>();
            DataContext = this;
            LoadTop8StockDataAsync();
        }

        // Mapped to the Refresh button click
        private async void RefreshDataButton_Click(object sender, RoutedEventArgs e)
        {
            Stocks.Clear();
            await LoadTop8StockDataAsync();
        }

        // Method to load top 8 stock data asynchronously
        private async Task LoadTop8StockDataAsync()
        {
            try
            {
                var stockDataList = await _stockDataService.GetTop8StockDataAsync();
                foreach (var stockData in stockDataList)
                {
                    Stocks.Add(stockData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading stock data: {ex.Message}");
            }
        }
    }
}