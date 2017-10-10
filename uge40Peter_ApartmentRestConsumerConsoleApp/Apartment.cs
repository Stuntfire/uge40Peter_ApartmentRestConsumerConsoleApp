using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace uge40Peter_ApartmentRestConsumerConsoleApp
{
    public class Apartment
    {
        private int _id;
        [DataMember]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _price;
        [DataMember]
        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private string _location;
        [DataMember]
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        private int _postalcode;
        [DataMember]
        public int Postalcode
        {
            get { return _postalcode; }
            set { _postalcode = value; }
        }

        private int _size;
        [DataMember]
        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        private int _noRoom;
        [DataMember]
        public int NoRoom
        {
            get { return _noRoom; }
            set { _noRoom = value; }
        }

        private bool _washingMachine;
        [DataMember]
        public bool WashingMachine
        {
            get { return _washingMachine; }
            set { _washingMachine = value; }
        }

        private bool _dishwasher;
        [DataMember]
        public bool Dishwasher
        {
            get { return _dishwasher; }
            set { _dishwasher = value; }
        }

        public Apartment(int id, int price, string location, int postalcode, int size, int noRoom, bool washingMachine, bool dishwasher)
        {
            Id = id;
            Price = price;
            Location = location;
            Postalcode = postalcode;
            Size = size;
            NoRoom = noRoom;
            WashingMachine = washingMachine;
            Dishwasher = dishwasher;
        }

        public Apartment()
        {

        }

        public override string ToString() => Id + "  " + Price + "  " + Location + "  " + Postalcode + "  " + Size + "  " + NoRoom + "  " + WashingMachine + "  " + Dishwasher;
    }
}