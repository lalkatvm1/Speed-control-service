using System;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace ServiceLibrary
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void Start();

        [OperationContract]
        string GetByDateWithSpeedLimit(string registrationDate, string speedLimit);

        [OperationContract]
        string GetByDate(string registrationDate);

        [OperationContract]
        void AddRegistration(string registrationDate, string vehicleNumber, string vehicleSpeed);


    }


    [DataContract]
    [DataContractFormat]
    public class Registration
    {
        [DataMember]
        private DateTime registrationTime;
        [DataMember]
        private string vehicleNumber;
        [DataMember]
        private float vehicleSpeedValue;

       [DataMember]
        public DateTime RegistrationTime
        {
            get { return registrationTime; }
            set { registrationTime = value; }
        }



        [DataMember]
        public string VehicleNumber
        {
            get { return vehicleNumber; }
            set {
                if (value.Length != 9)
                {
                    throw new ArgumentException("Wrong vehicle number format");
                }

                for (int i = 0; i < 4; i++)
                {
                    if (value[i] < 48 || value[i] > 57)
                    {
                        throw new ArgumentException("Wrong vehicle number format");
                    }
                }

                if (value[4] != ' ')
                {
                    throw new ArgumentException("Wrong vehicle number format");
                }

                if (((byte)value[5]) < 65 || ((byte)value[5]) > 90 || ((byte)value[6]) < 65 || ((byte)value[6]) > 90)
                {
                    throw new ArgumentException("Wrong vehicle number format");
                }

                if (((byte)value[5]) < 65 || ((byte)value[5]) > 90 || ((byte)value[6]) < 65 || ((byte)value[6]) > 90)
                {
                    throw new ArgumentException("Wrong vehicle number format");
                }

                if (value[7] != '-')
                {
                    throw new ArgumentException("Wrong vehicle number format");
                }

                if (((byte)value[5]) < 65 || ((byte)value[5]) > 90 || ((byte)value[6]) < 65 || ((byte)value[6]) > 90)
                {
                    throw new ArgumentException("Wrong vehicle number format");
                }


                vehicleNumber = value;
            }
        }
        
        [DataMember]
        public float VehicleSpeedValue
        {
            get { return vehicleSpeedValue; }
            set {

                vehicleSpeedValue = value;
            }
        }

        public Registration(string registrationDate, string vehicleNumber, string vehicleSpeed) 
        {
            var time = new DateTime(Int32.Parse(registrationDate.Substring(6, 4)), Int32.Parse(registrationDate.Substring(3, 2)), Int32.Parse(registrationDate.Substring(0, 2)), Int32.Parse(registrationDate.Substring(11, 2)), Int32.Parse(registrationDate.Substring(14, 2)), Int32.Parse(registrationDate.Substring(17, 2)));
            RegistrationTime = time;
            this.vehicleNumber = vehicleNumber;
            vehicleSpeedValue = float.Parse(vehicleSpeed);
        }
    }
}
