using MacrixPracticalTask.Models;
using System.Collections.ObjectModel;

namespace MacrixPracticalTask.Services
{
    public interface IXmlFileService
    {
        /// <summary>
        /// Load all data from xml file
        /// </summary>
        /// <returns></returns>
        ObservableCollection<PersonModel>? LoadData();
        /// <summary>
        /// Save all data to xml file
        /// </summary>
        /// <param name="dataToSave">Data to save</param>
        void SaveData(ObservableCollection<PersonModel> dataToSave);
    }
}
