using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MacrixPracticalTask.Models
{
    public class PersonModel : IEditableObject, INotifyPropertyChanged, ICloneable
    {
        private string _firstName;
        private string _lastName;
        private string _phoneNumber;
        private DateTime _dateOfBirth;
        private string _town;
        private string _postalCode;
        private string _streetName;
        private string _houseNumber;
        private string _apartmentNumber;

        public PersonModel()
        {
        }

        public PersonModel(
            string firstName,
            string lastName,
            string phoneNumber,
            DateTime dateOfBirth,
            string town,
            string postalCode,
            string streetName,
            string houseNumber,
            string? apartmentNumber)
        {
            _firstName = firstName;
            _lastName = lastName;
            _phoneNumber = phoneNumber;
            _dateOfBirth = dateOfBirth;
            _town = town;
            _postalCode = postalCode;
            _streetName = streetName;
            _houseNumber = houseNumber;
            _apartmentNumber = apartmentNumber;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The First name cannot be empty. Please correct.")]
        [MaxLength(64)]
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName == value) return;
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Last name cannot be empty. Please correct.")]
        [MaxLength(64)]
        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName == value) return;
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Phone number cannot be empty. Please correct.")]
        [MaxLength(11)]
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (_phoneNumber == value) return;
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Date of birth cannot be empty. Please correct.")]
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (_dateOfBirth == value) return;
                _dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }
        public int Age => GetAge(DateTime.UtcNow, DateOfBirth);

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Town name cannot be empty. Please correct.")]
        [MaxLength(32)]
        public string Town
        {
            get => _town;
            set
            {
                if (_town == value) return;
                _town = value;
                OnPropertyChanged(nameof(Town));
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Postal code cannot be empty. Please correct.")]
        [MaxLength(10)]
        public string PostalCode
        {
            get => _postalCode;
            set
            {
                if (_postalCode == value) return;
                _postalCode = value;
                OnPropertyChanged(nameof(PostalCode));
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Street name cannot be empty. Please correct.")]
        [MaxLength(32)]
        public string StreetName
        {
            get => _streetName;
            set
            {
                if (_streetName == value) return;
                _streetName = value;
                OnPropertyChanged(nameof(StreetName));
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The House number cannot be empty. Please correct.")]
        [MaxLength(8)]
        public string HouseNumber
        {
            get => _houseNumber;
            set
            {
                if (_houseNumber == value) return;
                _houseNumber = value;
                OnPropertyChanged(nameof(HouseNumber));
            }
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The Apartment number cannot be empty. Please correct.")]
        [MaxLength(8)]
        public string ApartmentNumber
        {
            get => _apartmentNumber;
            set
            {
                if (_apartmentNumber == value) return;
                _apartmentNumber = value;
                OnPropertyChanged(nameof(ApartmentNumber));
            }
        }

        public static int GetAge(DateTime reference, DateTime birthday)
        {
            int age = reference.Year - birthday.Year;
            if (reference < birthday.AddYears(age))
                age--;

            return age;
        }

        #region IEditableObject
        private PersonModel? _backupCopy;
        private bool _inEdit;

        public void BeginEdit()
        {
            if (_inEdit) return;
            _inEdit = true;
            _backupCopy = MemberwiseClone() as PersonModel;
        }

        public void CancelEdit()
        {
            if (!_inEdit || _backupCopy == null) return;
            _inEdit = false;
            FirstName = _backupCopy.FirstName;
            LastName = _backupCopy.LastName;
            PhoneNumber = _backupCopy.PhoneNumber;
            DateOfBirth = _backupCopy.DateOfBirth;
            Town = _backupCopy.Town;
            PostalCode = _backupCopy.PostalCode;
            StreetName = _backupCopy.StreetName;
            HouseNumber = _backupCopy.HouseNumber;
            ApartmentNumber = _backupCopy.ApartmentNumber;
        }

        public void EndEdit()
        {
            if (!_inEdit) return;
            _inEdit = false;
            _backupCopy = null;
        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region ICloneable
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        #endregion
    }
}