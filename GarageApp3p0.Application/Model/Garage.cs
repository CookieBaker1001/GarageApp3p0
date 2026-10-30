using GarageApp3p0.Application.Model;
using GarageApp3p0.Model.Vehicles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Model
{
    public class Garage<T> : IEnumerable<T> where T : Vehicle
    {
        private List<T> _vehicles;

        private int _capacity;
        public int Capacity
        {
            get => _capacity;
            private set
            {
                if (value <= 0) throw new ArgumentException("Capacity cannot be zero or negative.");
                _capacity = value;
            }
        }

        public Garage(int capacity)
        {
            _vehicles = new List<T>();
            Capacity = capacity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _vehicles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
