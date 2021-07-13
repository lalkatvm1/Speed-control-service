using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace ServiceLibrary
{
    public static class Data
    {
        public static List<Registration> currentRegistrationsList;
        public static List<Registration> tempRegistrationsList;

        private static DateTime currentDate = DateTime.Now;
        private static string currentFileName;
        public static bool firstLoad = true;

        /// <summary>
        /// Changes currentFileName to the new value and clears currentRegistrationsList if not empty
        /// </summary>
        /// <param name="reg"></param>
        public static void FileNameUpdate(Registration reg)
        {
            if (currentDate.ToShortDateString() != reg.RegistrationTime.ToShortDateString())
            {
                currentDate = reg.RegistrationTime.Date;
                currentFileName = currentDate.ToShortDateString().Replace('.', '_') + ".xml";
                if (currentRegistrationsList != null)
                {
                    if (currentRegistrationsList.Count != 0)
                    {
                        currentRegistrationsList.Clear();
                    }
                }
            }
        }

        /// <summary>
        /// Method initiates list object in Data class and initiates currentFileName.
        /// </summary>
        public static void StartInit()
        {
            currentRegistrationsList = new List<Registration>();
            currentFileName = currentDate.ToShortDateString().Replace('.', '_') + ".xml";
        }

        /// <summary>
        /// Adds new registration to currentRegistrationsList
        /// </summary>
        /// <param name="reg"></param>
        public static void Add(Registration reg)
        {
            currentRegistrationsList.Add(reg);
        }

        /// <summary>
        /// Clears currentRegistrationsList
        /// </summary>
        public static void Clear()
        {
            currentRegistrationsList.Clear();
        }

        /// <summary>
        /// Serializes currentRegistrationList in XML-file
        /// </summary>
        /// <param name="reg"></param>
        public static void SaveData(Registration reg)
        {
            FileStream writer = new FileStream(currentFileName, FileMode.OpenOrCreate);
            DataContractSerializer serializer = new DataContractSerializer(typeof(List<Registration>));
            serializer.WriteObject(writer, currentRegistrationsList);
            writer.Close();
        }

        /// <summary>
        /// Loads data from a specific XML file with a serialized <Registration> list
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<Registration> LoadDataByDate(DateTime date)
        {
            FileStream fs = new FileStream(date.ToShortDateString().Replace('.', '_') + ".xml", FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);
            DataContractSerializer ser = new DataContractSerializer(typeof(List<Registration>));
            var list = (List<Registration>)ser.ReadObject(reader);
            fs.Close();
            return list;
        }

        /// <summary>
        /// Loads data from XML file with a serialized List<Registration>
        /// </summary>
        /// <returns></returns>
        public static List<Registration> LoadData()
        {
            FileStream fs = new FileStream(currentFileName, FileMode.OpenOrCreate);
            XmlReader reader = XmlReader.Create(fs);
            DataContractSerializer ser = new DataContractSerializer(typeof(List<Registration>));
            var list = (List<Registration>)ser.ReadObject(reader);
            fs.Close();
            return list;
        }


    }
}
