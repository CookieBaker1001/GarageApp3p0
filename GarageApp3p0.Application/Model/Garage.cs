using GarageApp3p0.Model.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Model
{
    internal class Garage<T> where T : Vehicle , IEnumerable<T>
    {
        private List<Vehicle> _vehicles;

        private int _capacity;
        public int Capacity
        {
            get => _capacity;
            private set;
        }

        public Garage(int capacity)
        {
            _vehicles = new List<Vehicle>();
            Capacity = capacity;
        }

        public IEnumerator<Vehicle> GetEnumerator()
        {
            return _vehicles.GetEnumerator();
        }
    }
}
