using GarageApp3p0.Application.Data;
using GarageApp3p0.Application.Model;
using GarageApp3p0.Model;
using GarageApp3p0.Model.Vehicles;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace GarageApp3p0.Application
{
    public static class Utilities
    {   
        public static Garage<Vehicle> StartupSequence()
        {
            string message = 
                "      ::::::::      :::     :::::::::      :::      ::::::::  :::::::::: :::::::::::          ::::::::       ::::::: \r\n" +
                "    :+:    :+:   :+: :+:   :+:    :+:   :+: :+:   :+:    :+: :+:            :+:             :+:    :+:     :+:   :+: \r\n" +
                "   +:+         +:+   +:+  +:+    +:+  +:+   +:+  +:+        +:+            +:+                    +:+     +:+   +:+  \r\n" +
                "  :#:        +#++:++#++: +#++:++#:  +#++:++#++: :#:        +#++:++#       +#+                 +#++:      +#+   +:+   \r\n" +
                " +#+   +#+# +#+     +#+ +#+    +#+ +#+     +#+ +#+   +#+# +#+            +#+                    +#+     +#+   +#+    \r\n" +
                "#+#    #+# #+#     #+# #+#    #+# #+#     #+# #+#    #+# #+#            #+#             #+#    #+# #+# #+#   #+#     \r\n" +
                "########  ###     ### ###    ### ###     ###  ########  ##########     ###              ########  ###  #######       ";
            Console.WriteLine(message+"\n");

            Console.WriteLine("Välkommen till Garage Appen 3.0!\n" +
                "Här kan du hantera dina fordon i garaget.");

            if (GarageRepository.SaveFileExists()) { 
                Console.WriteLine("Hittade en sparad garagelista.");
                Console.WriteLine("Vill du ladda in den? (j/n)");
                char input;
                while (!char.TryParse(Console.ReadLine(), out input))
                {
                    Console.WriteLine("Ogiltigt värde. Försök igen.");
                }
                if (char.ToLower(input) == 'j') {
                    return GarageRepository.LoadGarage();
                }
            }

            Console.WriteLine("Hur stort ska garaget vara?");
            Console.Write(">");
            int garageSize;
            while (!int.TryParse(Console.ReadLine(), out garageSize) && garageSize <= 0)
            {
                Console.WriteLine("Ogiltigt värde. Försök igen.");
            }
            Garage<Vehicle> garage = new Garage<Vehicle>(garageSize);
            return garage;
        }

        public static void PrintMainMenu() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" -=- Huvudmeny -=-");
            Console.WriteLine("1. Lägg till fordon");
            Console.WriteLine("2. Ta bort fordon");
            Console.WriteLine("3. Visa alla fordon");
            Console.WriteLine("4. Sök efter fordon");
            Console.WriteLine("5. Avsluta");
            Console.ResetColor();
            Console.Write(">");
        }

        public static void RemoveVehicle(Garage<Vehicle> garage) { 
            PrintGarage(garage, true, true);
            Console.WriteLine("Vilket fordon vill du ta bort? (1-indexering)");
            Console.Write(">");
            int index;

            if (!int.TryParse(Console.ReadLine(), out index))
                throw new ArgumentException("Ogiltigt index.");

            index--; // Konvertera till 0-indexering
            garage.RemoveVehicle(index);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Fordonet har tagits bort.");
            Console.ResetColor();
        }

        private static void PrintList(IEnumerator<Vehicle> enumerator, bool showLowDetails, bool showIndex)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("----------\n");
            int i = 1;
            while (enumerator.MoveNext())
            {
                Vehicle currentVehicle = enumerator.Current;
                if (showIndex)
                {
                    Console.Write($"{i}. ");
                    i++;
                }
                if (!showLowDetails) Console.WriteLine(currentVehicle.ToString());
                else Console.WriteLine(currentVehicle.GetLowDetails());
                Console.WriteLine();
            }
            Console.WriteLine("----------");
            Console.ResetColor();
        }

        public static void PrintGarage(Garage<Vehicle> garage, bool showLowDetails, bool showIndex)
        {
            if (garage.IsEmpty())
                throw new InvalidOperationException("Garaget är tomt. Inget att visa.");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Fordon i garaget:");
            Console.WriteLine($"Kapacitet: {garage.CurrentCapacity}/{garage.MaxCapacity}");
            PrintList(garage.GetEnumerator(), showLowDetails, showIndex);
        }

        public static Vehicle CreateVehicle(Garage<Vehicle> garage) {
            Console.WriteLine("Olika fordon tar olika mycket plats i garaget.");
            Console.WriteLine($"Nuvarande kapacitet: {garage.CurrentCapacity}/{garage.MaxCapacity}");
            Console.WriteLine("Ange fordonstyp. (Utrymme anges i parentes)");
            Console.WriteLine("1. Bil(3), 2. Motorcykel(1), 3. Flygplan(5), 4. Båt(4), 5. Buss(8)");
            Console.Write(">");
            int type;
            if (!int.TryParse(Console.ReadLine(), out type))
                throw new ArgumentException("Ogiltig fordonstyp.");
            if (type < 1 || type > 5)
                throw new ArgumentOutOfRangeException("Ogiltig fordonstyp.");

            return PromptForVehicleDetails(type, garage);
        }

        private static Vehicle PromptForVehicleDetails(int type, Garage<Vehicle> garage) {

            Console.WriteLine("Ange märke.");
            Console.Write(">");
            string? make = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(make))
                throw new ArgumentException("Ogiltigt märke.");

            Console.WriteLine("Ange modell.");
            Console.Write(">");
            string? model = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Ogiltig modell.");

            Console.WriteLine("Ange registreringsnummer.");
            Console.Write(">");
            string? regNumber = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(regNumber))
                throw new ArgumentException("Ogiltigt registreringsnummer.");
            if (!Garage<Vehicle>.IsValidRegistrationNumber(regNumber))
                throw new ArgumentException("Ogiltigt registreringsnummer.");
            if (garage.DoesThisRegNumberAlreadyExist(regNumber))
                throw new ArgumentException($"Registreringsnumret {regNumber} finns redan i garaget.");

            Console.WriteLine("Ange tillverkningsår.");
            Console.Write(">");
            int year;
            if (!int.TryParse(Console.ReadLine(), out year))
                throw new ArgumentException("Inmatningsfel.");

            Console.WriteLine("Ange antal sätten.");
            Console.Write(">");
            int seatCount;
            if (!int.TryParse(Console.ReadLine(), out seatCount))
                throw new ArgumentException("Inmatningsfel.");

            Console.WriteLine("Ange färg.");
            int count = 0;
            foreach (string c in validColors)
            {
                Console.Write($"{c} ");
                count++;
                if (count == 5) {
                    Console.WriteLine();
                    count = 0;
                }
            }
            Console.WriteLine();
            Console.Write(">");
            string? color = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(color))
                throw new ArgumentException("Ogiltig färg.");
            if (!IsValidColor(color))
                throw new ArgumentException("Ogiltig färg.");

            Vehicle vehicle;
            if (type == 1) { 
                vehicle = new Car(make, model, regNumber, year, seatCount, color);
            } else if (type == 2) {
                bool hasSidecar;
                PromptForMotorcycleDetails(out hasSidecar);
                vehicle = new Motorcycle(make, model, regNumber, year, seatCount, color, hasSidecar);
            } else if (type == 3) {
                int maxHeight;
                int engineCount;
                PromptForAirplaneDetails(out maxHeight, out engineCount);
                vehicle = new Airplane(make, model, regNumber, year, seatCount, color, maxHeight, engineCount);
            } else if (type == 4) {
                PropulsionType propulsionType;
                PromptForBoatDetails(out propulsionType);
                vehicle = new Boat(make, model, regNumber, year, seatCount, color, propulsionType);
            } else {
                EngineType engineType;
                PromptForBusDetails(out engineType);
                vehicle = new Bus(make, model, regNumber, year, seatCount, color, engineType);
            }
            return vehicle;
        }

        private static void PromptForMotorcycleDetails(out bool hasSidecar)
        {
            Console.WriteLine("Har motorcykeln sidovagn? (j/n)");
            Console.Write(">");
            char input;
            if (!char.TryParse(Console.ReadLine(), out input))
                throw new ArgumentException("Inmatningsfel.");
            
            if (input.ToString().ToLower() == "j")
                hasSidecar = true;
            else if (input.ToString().ToLower() == "n")
                hasSidecar = false;
            else
                throw new ArgumentException("Ogiltigt värde.");
        }

        private static void PromptForAirplaneDetails(out int maxHeight, out int engineCount)
        {
            Console.WriteLine("Ange planets maxhöjd.");
            Console.Write(">");
            int seatCount;
            if (!int.TryParse(Console.ReadLine(), out seatCount))
                throw new ArgumentException("Inmatningsfel.");
            maxHeight = seatCount;

            Console.WriteLine("Ange antal motorer.");
            Console.Write(">");
            if (!int.TryParse(Console.ReadLine(), out seatCount))
                throw new ArgumentException("Inmatningsfel.");
            engineCount = seatCount;
        }

        private static void PromptForBoatDetails(out PropulsionType propulsionType)
        {
            Console.WriteLine("Ange drivmedelstyp (0 = Rodd, 1 = Segel, 2 = Motor).");
            Console.Write(">");
            int propulsionTypeInt;
            if (!int.TryParse(Console.ReadLine(), out propulsionTypeInt))
                throw new ArgumentException("Ogiltig drivmedelstyp.");
            if (propulsionTypeInt < 0 || propulsionTypeInt > 2)
                throw new ArgumentOutOfRangeException("Ogiltig drivmedelstyp.");
            propulsionType = (PropulsionType)propulsionTypeInt;
        }

        private static void PromptForBusDetails(out EngineType engineType)
        {
            Console.WriteLine("Ange motortyp (0 = Elektrisk, 1 = Bensin, 2 = Diesel).");
            Console.Write(">");
            int engineTypeInt;
            if (!int.TryParse(Console.ReadLine(), out engineTypeInt))
                throw new ArgumentException("Ogiltig motortyp.");
            if (engineTypeInt < 0 || engineTypeInt > 2)
                throw new ArgumentOutOfRangeException("Ogiltig motortyp.");
            engineType = (EngineType)engineTypeInt;
        }

        private static HashSet<string> validColors = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "röd", "blå", "grön", "gul", "orange",
                "svart", "vit", "grå", "ljusgrå", "mörkgrå",
                "lila", "rosa", "brun", "turkos", "beige",
                "silver", "guld",
            };
        private static bool IsValidColor(string color)
        {
            return validColors.Contains(color.ToLower());
        }

        public static void SaveGarage(Garage<Vehicle> garage)
        {
            Console.WriteLine("Vill du spara garaget innan programmet avslutas? (j/n)");
            Console.Write(">");
            char input;
            if (!char.TryParse(Console.ReadLine(), out input))
                throw new ArgumentException("Inmatningsfel.");

            if (input.ToString().ToLower() == "n")
                return;

            try
            {
                string path = GarageRepository.SaveGarage(garage);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Garaget har sparats.");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Fil: " + path);
                Console.ResetColor();
            }
            catch (IOException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"IO-fel: {ex.Message}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Generellt fel: {ex.Message}");
                Console.ResetColor();
            }
        }

        public static void PerformAdvancedSearch(Garage<Vehicle> garage) {
            bool exit = false;
            string? input;
            List<Vehicle> filteredVehicles = garage.CopyContent();
            while (!exit) {
                PrintAdvancedSearchMenu();
                Console.Write(">");
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) {
                    Console.WriteLine("Inmatningsfel. Försök igen.");
                    continue;
                }

                switch (input.ToLower()) {
                    case "1":
                        // Ej implementerat ännu.
                        //FilterHelperClass.FilterIndex(vehicles);
                        break;
                    case "2":
                        // Sort by make
                        filteredVehicles = FilterHelperClass.FilterMake(filteredVehicles);
                        break;
                    case "3":
                        // Sort by model
                        filteredVehicles = FilterHelperClass.FilterModel(filteredVehicles);
                        break;
                    case "4":
                        // Sort by regnumber
                        break;
                    case "5":
                        // Sort by year
                        break;
                    case "6":
                        // Sort by color
                        break;
                    case "7":
                        // View the list
                        PrintList(filteredVehicles.GetEnumerator(), true, true);
                        break;
                    case "8":
                        // Done with advanced search, return to main menu
                        exit = true;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        Console.ResetColor();
                        break;
                }
            }
            PrintList(filteredVehicles.GetEnumerator(), true, true);
        }

        private static void PrintAdvancedSearchMenu() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Avancerad sökning. Välj alternativ för " +
                "att filtrera bland garagets fordon:");
            Console.WriteLine("1. Filtrera efter index");
            Console.WriteLine("2. Sortera efter märke");
            Console.WriteLine("3. Sortera efter modell");
            Console.WriteLine("4. Filtrera efter registreringsnummer");
            Console.WriteLine("5. Sortera efter årtal");
            Console.WriteLine("6. Sortera efter färg");
            Console.WriteLine("7. Snegla på listan");
            Console.WriteLine("8. Återgå till huvudmenyn");
            Console.ResetColor();
        }
    }
}
