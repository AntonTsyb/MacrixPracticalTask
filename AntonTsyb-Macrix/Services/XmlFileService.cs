using MacrixPracticalTask.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace MacrixPracticalTask.Services
{
    public class XmlFileService : IXmlFileService
    {
        private readonly XmlSerializer _xmlSerializer;

        private const string XmlFileName = "DataToLoad.xml";

        public XmlFileService()
        {
            _xmlSerializer = new XmlSerializer(typeof(ObservableCollection<PersonModel>));
        }

        public ObservableCollection<PersonModel>? LoadData()
        {
            var data = new ObservableCollection<PersonModel>();
            // Load data from xml file
            using FileStream fs = new(XmlFileName, FileMode.OpenOrCreate, FileAccess.Read);
            if (fs != null && fs.Length > 0)
                data = (ObservableCollection<PersonModel>?)_xmlSerializer.Deserialize(fs);

            return data;
        }

        public void SaveData(ObservableCollection<PersonModel> dataToSave)
        {
            // Re-write all data in file to new from DataGrid
            using FileStream fs = new(XmlFileName, FileMode.Truncate, FileAccess.Write);
            _xmlSerializer.Serialize(fs, dataToSave);
        }
    }
}
