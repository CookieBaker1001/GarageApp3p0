using GarageApp3p0.Application.Model;
using GarageApp3p0.Model;
using GarageApp3p0.Model.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Application.Data
{
    internal class GarageRepository
    {
        private static string _savefile = "garage.txt";
        // Detta är en sökväg som pekar på en mapp som ligger på
        // projektets rot-nivå
        private static string _saveFolder = Path.Combine(
            Directory.GetParent(AppContext.BaseDirectory)!
            .Parent!.Parent!.Parent!.FullName, "repository");
        private static string _savePath = Path.Combine(_saveFolder, _savefile);
        
        public static bool SaveFileExists()
        {
            return File.Exists(_savePath);
        }

        private static void CreateSaveFile()
        {
            if (!Directory.Exists(_saveFolder))
            {
                Directory.CreateDirectory(_saveFolder);
            }
            if (!File.Exists(_savePath))
            {
                File.Create(_savePath).Dispose();
            }
        }

        public static string SaveGarage(Garage<Vehicle> garage)
        {
            Console.WriteLine($"Sparar filen i: {Path.GetFullPath(_saveFolder)}");

            CreateSaveFile();

            string[] initialLines = FromGarageToText(garage);
            File.WriteAllText(_savePath, string.Join("\n", initialLines));
            // await File.WriteAllLinesAsync(_savePath, initialLines);
            return _savePath;
        }

        public static Garage<Vehicle> LoadGarage()
        {
            string[] data = File.ReadAllLines(_savePath);
            int maxCapacity;
            List<Vehicle> vehicles = FromTextToGarage(data, out maxCapacity);

            Garage<Vehicle> garage = new Garage<Vehicle>(maxCapacity);
            garage.ImportVehicles(vehicles);
            return garage;
        }

        private static string[] FromGarageToText(Garage<Vehicle> garage)
        {
            List<string> lines = new List<string>();
            lines.Add(garage.MaxCapacity.ToString());

            IEnumerator<Vehicle> vehicles = garage.GetEnumerator();
            while (vehicles.MoveNext())
            {
                Vehicle v = vehicles.Current;
                StringBuilder sb = new StringBuilder();
                sb.Append(v.GetType().Name).Append(";");
                sb.Append(v.Make).Append(";");
                sb.Append(v.Model).Append(";");
                sb.Append(v.RegNumber).Append(";");
                sb.Append(v.Year.ToString()).Append(";");
                sb.Append(v.SeatCount.ToString()).Append(";");
                sb.Append(v.Color).Append(";");

                if (v is Car car)
                    sb.Append(car.HasSunRoof ? "t" : "f");
                else if (v is Motorcycle motorcycle)
                    sb.Append(motorcycle.HasSidecar ? "t" : "f");
                else if (v is Airplane airplane)
                    sb.Append(airplane.MaxHeight.ToString()).Append(";").Append(airplane.EngineCount.ToString());
                else if (v is Boat boat)
                    sb.Append((int)boat.PropulsionType);
                else if (v is Bus bus)
                    sb.Append((int)bus.EngineType);

                lines.Add(sb.ToString());
            }
            return lines.ToArray();
        }

        private static List<Vehicle> FromTextToGarage(string[] data, out int maxCapacity) {
            List<Vehicle> vehicles = new List<Vehicle>();
            maxCapacity = int.Parse(data[0]);
            for (int i = 1; i < data.Length; i++)
            {
                string[] parts = data[i].Split(';');
                string type = parts[0];
                switch (type)
                {
                    case "Car":
                        vehicles.Add(new Car(parts[1], parts[2], parts[3],
                            int.Parse(parts[4]), int.Parse(parts[5]),
                            parts[6], parts[7] == "t"));
                        break;
                    case "Motorcycle":
                        vehicles.Add(new Motorcycle(parts[1], parts[2], parts[3],
                            int.Parse(parts[4]), int.Parse(parts[5]),
                            parts[6], parts[7] == "t"));
                        break;
                    case "Airplane":
                        vehicles.Add(new Airplane(parts[1], parts[2], parts[3],
                            int.Parse(parts[4]), int.Parse(parts[5]),
                            parts[6], int.Parse(parts[7]),
                            int.Parse(parts[8])));
                        break;
                    case "Boat":
                        vehicles.Add(new Boat(parts[1], parts[2], parts[3],
                            int.Parse(parts[4]), int.Parse(parts[5]),
                            parts[6], (PropulsionType)int.Parse(parts[7])));
                        break;
                    case "Bus":
                        vehicles.Add(new Bus(parts[1], parts[2], parts[3],
                            int.Parse(parts[4]), int.Parse(parts[5]),
                            parts[6], (EngineType)int.Parse(parts[7])));
                        break;
                    default:
                        break;
                }
            }
            return vehicles;
        }
    }
}
