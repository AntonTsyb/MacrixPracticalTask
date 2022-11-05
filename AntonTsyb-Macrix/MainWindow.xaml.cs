using MacrixPracticalTask.Models;
using MacrixPracticalTask.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MacrixPracticalTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<PersonModel> _collection;
        private XmlFileService _xmlFileService;
        public MainWindow()
        {
            InitializeComponent();
            _xmlFileService = new XmlFileService();
            _collection = _xmlFileService.LoadData() ?? new ObservableCollection<PersonModel>();
            mainGrid.ItemsSource = _collection;
        }

        #region Events
        private void mainGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
        }

        private void mainGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
        }
        #endregion
    }
}
