using GarageApp3p0.Application.Model;
using GarageApp3p0.Model.Vehicles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace GarageApp3p0.Model
{
    public class Garage<T> : IEnumerable<T> where T : Vehicle
    {
        private List<T>? _vehicles;
        private int _currentCapacity = 0;
        public int CurrentCapacity
        {
            get => _currentCapacity;
            private set { _currentCapacity = value; }
        }
        private int _maxCapacity = 0;
        public int MaxCapacity
        {
            get => _maxCapacity;
            private set
            {
                if (value <= 0) throw new ArgumentException("Capacity cannot be zero or negative.");
                _maxCapacity = value;
            }
        }

        public Garage()
        {
            _vehicles = new List<T>();
        }
        public Garage(int capacity)
        {
            _vehicles = new List<T>();
            MaxCapacity = capacity;
        }

        public bool DoesThisVehicleFit(T vehicle) { 
            return (CurrentCapacity + vehicle.ParkingSize) <= MaxCapacity;
        }

        public void AddVehicle(T vehicle)
        {
            if (_currentCapacity == MaxCapacity)
                throw new InvalidOperationException("Garaget är fullpackat.");

            if (!DoesThisVehicleFit(vehicle))
                throw new InvalidOperationException("Det finns inte tillräckligt med utrymme i garaget för detta fordon.");

            vehicle.RegNumber = vehicle.RegNumber.ToUpper();
            CurrentCapacity += vehicle.ParkingSize;
            _vehicles.Add(vehicle);
        }

        public void RemoveVehicle(int index)
        {
            if (index < 0 || index >= _vehicles.Count)
                throw new ArgumentOutOfRangeException("Index is out of bounds.");

            CurrentCapacity -= _vehicles[index].ParkingSize;
            _vehicles.RemoveAt(index);
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
            return _vehicles.Count >= MaxCapacity;
        }

        public bool IsEmpty()
        {
            return _vehicles.Count == 0;
        }

        public void ImportVehicles(List<T> vehicles)
        {
            _vehicles = new List<T>(vehicles.Count);
            foreach (var vehicle in vehicles)
            {
                _vehicles.Add(vehicle);
                CurrentCapacity += vehicle.ParkingSize;
            }
        }

        public List<T> CopyContent()
        {
            List<T> copy = new List<T>();
            if (_vehicles is not null) copy.AddRange(_vehicles);
            return copy;
        }
    }
}
