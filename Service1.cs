using System;
using System.Linq;
using System.Text;

namespace ServiceLibrary
{
    public class Service1 : IService1
    {
        private static Registration registration;

        /// <summary>
        /// Method initiates list object in Data class and initiates currentFileName.
        /// </summary>
        public void Start()
        {
            Data.StartInit();
        }

        /// <summary>
        /// Method adds new registration object to registrationg collection.
        /// </summary>
        /// <param name="registrationDate"></param>
        /// <param name="vehicleNumber"></param>
        /// <param name="vehicleSpeed"></param>
        public void AddRegistration(string registrationDate, string vehicleNumber, string vehicleSpeed)
        {
            if (registrationDate == null)
            {
                throw new ArgumentNullException("Date value is empty");
            }

            if (vehicleNumber == null)
            {
                throw new ArgumentNullException("Vahicle number is empty");
            }

            if (vehicleSpeed == null)
            {
                throw new ArgumentNullException("Vahicle speed value is empty");
            }

            if (Data.firstLoad == false)
            {
                Data.LoadData();
            }
            else 
            {
                Data.firstLoad = false;
            }
            registration = new Registration(registrationDate, vehicleNumber, vehicleSpeed);

            Data.FileNameUpdate(registration);
            Data.Add(registration);
            Data.SaveData(registration);
        }

        /// <summary>
        /// Returns all registrations where speed if higher than speetLimit argument.
        /// </summary>
        /// <param name="registrationDate"></param>
        /// <param name="speedLimit"></param>
        /// <returns></returns>
        public string GetByDateWithSpeedLimit(string registrationDate, string speedLimit)
        {
            var date = new DateTime(Int32.Parse(registrationDate.Substring(6, 4)), Int32.Parse(registrationDate.Substring(3, 2)), Int32.Parse(registrationDate.Substring(0, 2)));
            var list = Data.LoadDataByDate(date);

            StringBuilder builder = new StringBuilder();
            var temp = list.Where(reg => reg.VehicleSpeedValue > float.Parse(speedLimit));
            Data.tempRegistrationsList = temp.ToList();
            Data.tempRegistrationsList = Data.tempRegistrationsList.OrderBy(reg => reg.RegistrationTime).ToList();

            foreach (var reg in Data.tempRegistrationsList)
            {
                builder.Append($"{reg.RegistrationTime.ToShortDateString()} {reg.RegistrationTime.ToLongTimeString()} {reg.VehicleNumber} {reg.VehicleSpeedValue}\n");
            }
            return builder.ToString();
        }

        /// <summary>
        /// Returns two registrations with max and min speed. 
        /// </summary>
        /// <param name="registrationDate"></param>
        /// <returns></returns>
        public string GetByDate(string registrationDate)
        {
            var date = new DateTime(Int32.Parse(registrationDate.Substring(6, 4)), Int32.Parse(registrationDate.Substring(3, 2)), Int32.Parse(registrationDate.Substring(0, 2)));
            var list = Data.LoadDataByDate(date);

            StringBuilder builder = new StringBuilder();
            var temp = list.OrderBy(reg => reg.VehicleSpeedValue).Take(1);
            var temp1 = list.OrderByDescending(reg => reg.VehicleSpeedValue).Take(1);
            Data.tempRegistrationsList = temp.ToList();
            Data.tempRegistrationsList.Add(temp1.First());
            Data.tempRegistrationsList = Data.tempRegistrationsList.OrderBy(reg => reg.RegistrationTime).ToList();

            foreach (var reg in Data.tempRegistrationsList)
            {
                builder.Append($"{reg.RegistrationTime.ToShortDateString()} {reg.RegistrationTime.ToLongTimeString()} {reg.VehicleNumber} {reg.VehicleSpeedValue}\n");
            }
            return builder.ToString();
        }
    }
}
