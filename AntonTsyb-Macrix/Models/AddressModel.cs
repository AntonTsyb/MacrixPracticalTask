namespace MacrixPracticalTask.Models
{
    public class AddressModel
    {
        public AddressModel(string town,
            string postalCode,
            string streetName,
            string houseNumber,
            string? apartmentNumber)
        {
            Town = town;
            PostalCode = postalCode;
            StreetName = streetName;
            HouseNumber = houseNumber;
            ApartmentNumber = apartmentNumber;
        }

        public string Town { get; set; }
        public string PostalCode { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string? ApartmentNumber { get; set; }
    }
}