using GarageApp3p0.Application.Model;
using GarageApp3p0.Model.Vehicles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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

        private static Regex regNumberRegex = new Regex(@"^[A-Z]{3}\d{3}$");
        public static bool IsValidRegistrationNumber(string regNumber)
        {
            regNumber = regNumber.ToUpper();
            return regNumberRegex.IsMatch(regNumber);
        }

        public void AddVehicle(T vehicle)
        {
            if (_vehicles.Count == Capacity)
                throw new InvalidOperationException("Garaget är fullpackat.");
            _vehicles.Add(vehicle);
        }

        public void RemoveVehicle(T vehicle)
        {
            _vehicles.Remove(vehicle);
        }

        public void Print()
        {
            foreach (var vehicle in _vehicles)
            {
                Console.WriteLine(vehicle.ToString());
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _vehicles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool DoesThisRegNumberAlreadyExist(string regNumber)
        {
            return _vehicles.Any(v => v.RegNumber == regNumber);
        }

        public bool IsFull()
        {
            return _vehicles.Count >= Capacity;
        }

        public bool IsEmpty()
        {
            return _vehicles.Count == 0;
        }
    }
}
