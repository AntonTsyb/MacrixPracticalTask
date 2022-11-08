using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Grid;
using MacrixPracticalTask.Models;
using MacrixPracticalTask.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace MacrixPracticalTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<PersonModel> _collection;
        private ObservableCollection<PersonModel> _backUp;
        private XmlFileService _xmlFileService;
        private bool _isEdited;

        public MainWindow()
        {
            InitializeComponent();
            _xmlFileService = new XmlFileService();
            _collection = _xmlFileService.LoadData() ?? new ObservableCollection<PersonModel>();
            _backUp = CopyCollection(_collection, _backUp);
            dg_Main.ItemsSource = _collection;

            CheckButtonAvailability(false);
        }

        private ObservableCollection<PersonModel> CopyCollection(
            ObservableCollection<PersonModel> source,
            ObservableCollection<PersonModel>? target)
        {
            target = new ObservableCollection<PersonModel>();
            source
                .ForEach(x => target.Add((PersonModel)x.Clone()));
            return target;
        }

        #region GridControl Events
        private void TableView_AddingNewRow(object sender, System.ComponentModel.AddingNewEventArgs e)
        {
            e.NewObject = new PersonModel()
            {
                DateOfBirth = new DateTime(2010, 1, 1)
            };
        }

        private void TableView_ValidateRowDeletion(object sender, GridValidateRowDeletionEventArgs e)
        {
            CheckButtonAvailability(true);
        }

        private void OnValidateRow(object sender, GridRowValidationEventArgs e)
        {
            var personModel = (PersonModel)e.Row;
            if (personModel == null) return;
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
            e.ExceptionMode = ExceptionMode.NoAction;
        }

        private void TableView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == nameof(PersonModel.DateOfBirth))
            {
                int value = PersonModel.GetAge(DateTime.UtcNow, (DateTime)e.Value);
                if (value < 0) value = 0;
                dg_Main.SetCellValue(e.RowHandle, nameof(PersonModel.Age), value);
            }

            CheckButtonAvailability(true);
        }
        #endregion

        #region Button Events
        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (!_isEdited) return;
            if (dg_Main.View.HasErrors || dg_Main.View.HasValidationError)
            {
                MessageBox.Show("Unable to save data with errors. Correct the errors," +
                    " or cancel all changes.", "Macrix Practical Task", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _xmlFileService.SaveData(_collection);
            // rewrite backup
            _backUp = CopyCollection(_collection, _backUp);
            dg_Main.SelectedItem = 0;

            CheckButtonAvailability(false);
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (!_isEdited) return;
            // Load data from backup
            _collection = CopyCollection(_backUp, _collection);

            dg_Main.ItemsSource = _collection;
            dg_Main.SelectedItem = 0;

            CheckButtonAvailability(false);
        }

        private void CheckButtonAvailability(bool isEdited)
        {
            _isEdited = isEdited;
            btn_Save.IsEnabled = _isEdited;
            btn_Cancel.IsEnabled = _isEdited;
        }
        #endregion
    }
}