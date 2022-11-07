using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Xpf;
using DevExpress.Xpf.Grid;
using MacrixPracticalTask.Models;
using MacrixPracticalTask.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

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
            dg_Main.ItemsSource = _collection;
        }

        #region GridControl Events
        private void TableView_AddingNewRow(object sender, System.ComponentModel.AddingNewEventArgs e)
        {
            e.NewObject = new PersonModel()
            {
                DateOfBirth = new DateTime(2010, 1, 1)
            };
        }

        [Command]
        public void ValidateRowDeletion(ValidateRowDeletionArgs args)
        {
            var item = (PersonModel)args.Items.Single();
            _collection.Remove(item);
        }

        private void OnValidateRow(object sender, GridRowValidationEventArgs e)
        {
            var personModel = (PersonModel)e.Row;
            if (string.IsNullOrEmpty(personModel.FirstName))
            {
                e.SetError("Please, enter first name");
                return;
            }
            if (string.IsNullOrEmpty(personModel.LastName))
            {
                e.SetError("Please, enter last name");
                return;
            }
            if (string.IsNullOrEmpty(personModel.StreetName))
            {
                e.SetError("Please, enter street name");
                return;
            }
            if (string.IsNullOrEmpty(personModel.HouseNumber))
            {
                e.SetError("Please, enter house number");
                return;
            }
            if (string.IsNullOrEmpty(personModel.PostalCode))
            {
                e.SetError("Please, enter postal code");
                return;
            }
            if (string.IsNullOrEmpty(personModel.Town))
            {
                e.SetError("Please, enter town name");
                return;
            }
            if (string.IsNullOrEmpty(personModel.PhoneNumber))
            {
                e.SetError("Please, enter phone number");
                return;
            }
            if (personModel.DateOfBirth.Date > DateTime.UtcNow.Date)
            {
                e.SetError("Date of birth cannot be in the future");
                return;
            }
        }

        void OnInvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.Xpf.Grid.ExceptionMode.NoAction;
        }

        private void TableView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == nameof(PersonModel.DateOfBirth))
            {
                int value = DateTime.UtcNow.Year - ((DateTime)e.Value).Year;
                dg_Main.SetCellValue(e.RowHandle, nameof(PersonModel.Age), value);
            }
        }
        #endregion

        #region Button Events
        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            _xmlFileService.SaveData(_collection);
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Load data from file without save
            _collection = _xmlFileService.LoadData() ?? new ObservableCollection<PersonModel>();
            dg_Main.ItemsSource = _collection;
        }
        #endregion
    }
}