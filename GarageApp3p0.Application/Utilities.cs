using GarageApp.Model;
using GarageApp3p0.Application.Data;
using GarageApp3p0.Application.HandlerClasses;
using GarageApp3p0.Application.HelperClasses;
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

            char loadFromFile = 'j';
            if (GarageRepository.SaveFileExists()) {
                PromptHandler.AskUser("Hittade en sparad garagelista.\n" +
                    "Vill du ladda in den? (j/n)", ConsoleColor.Yellow);
                
                while (!char.TryParse(Console.ReadLine(), out loadFromFile) || (loadFromFile != 'j' && loadFromFile != 'n'))
                {
                    Console.WriteLine("Ogiltigt värde. Försök igen.");
                }
                if (char.ToLower(loadFromFile) == 'j')
                {
                    return GarageRepository.LoadGarage();
                }
            }

            PromptHandler.AskUser("Hur stort ska garaget vara?", ConsoleColor.Yellow);
            int garageSize;
            while (!int.TryParse(Console.ReadLine(), out garageSize) && garageSize <= 0)
            {
                Console.WriteLine("Ogiltigt värde. Försök igen.");
            }

            Garage<Vehicle> garage = new Garage<Vehicle>(garageSize);

            if (loadFromFile == 'n') { 
                PromptHandler.AskUser("Vill du fylla garaget med en slumpmässig lista av fordon? (j/n)", ConsoleColor.Yellow);
                char fillGarage;
                while (!char.TryParse(Console.ReadLine(), out fillGarage) || (fillGarage != 'j' && fillGarage != 'n'))
                {
                    Console.WriteLine("Ogiltigt värde. Försök igen.");
                }
                if (fillGarage == 'j') {
                    List<Vehicle> randomList = StartList.GetRandomList(garage.MaxCapacity);
                    garage.ImportVehicles(randomList);
                }
            }
            
            return garage;
        }

        public static void RemoveVehicle(Garage<Vehicle> garage) { 
            PrintGarage(garage, true, true);
            PromptHandler.AskUser("Vilket fordon vill du ta bort? (1-indexering)", 
                ConsoleColor.Yellow);
            int index;

            if (!int.TryParse(Console.ReadLine(), out index))
                throw new ArgumentException("Ogiltigt index.");

            index--; // Konvertera till 0-indexering
            garage.RemoveVehicle(index);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Fordonet har tagits bort.");
            Console.ResetColor();
        }

        public static void PrintGarageContent(IEnumerator<Vehicle> enumerator, bool showLowDetails, bool showIndex)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("----------");
            int i = 1;
            while (enumerator.MoveNext())
            {
                Vehicle currentVehicle = enumerator.Current;
                if (showIndex)
                {
                    Console.Write($"{i}. ");
                    i++;
                }
                if (!showLowDetails) Console.WriteLine(currentVehicle.ToString()+"\n");
                else Console.WriteLine(currentVehicle.GetLowDetails());
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
            PrintGarageContent(garage.GetEnumerator(), showLowDetails, showIndex);
        }

        public static Vehicle CreateVehicle(Garage<Vehicle> garage) {
            PromptHandler.AskUser(
                "Olika fordon tar olika mycket plats i garaget.\n" +
                $"Nuvarande kapacitet: {garage.CurrentCapacity}/{garage.MaxCapacity}\n" +
                "Ange fordonstyp. (Utrymme anges i parentes)\n" +
                "1. Bil(3), 2. Motorcykel(1), 3. Flygplan(5), 4. Båt(4), 5. Buss(8)",
                ConsoleColor.Yellow);
            int type;
            if (!int.TryParse(Console.ReadLine(), out type))
                throw new ArgumentException("Ogiltig fordonstyp.");
            if (type < 1 || type > 5)
                throw new ArgumentOutOfRangeException("Ogiltig fordonstyp.");

            return PromptForVehicleDetails(type, garage);
        }

        private static Vehicle PromptForVehicleDetails(int type, Garage<Vehicle> garage) {

            PromptHandler.AskUser("Ange märke.", ConsoleColor.Yellow);
            string? make = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(make))
                throw new ArgumentException("Ogiltigt märke.");

            PromptHandler.AskUser("Ange modell.", ConsoleColor.Yellow);
            string? model = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Ogiltig modell.");

            PromptHandler.AskUser("Ange registreringsnummer.", ConsoleColor.Yellow);
            string? regNumber = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(regNumber))
                throw new ArgumentException("Ogiltigt registreringsnummer.");
            if (!RegexHandler.IsValidRegistrationNumber(regNumber))
                throw new ArgumentException("Ogiltigt registreringsnummer.");
            if (garage.DoesThisRegNumberAlreadyExist(regNumber))
                throw new ArgumentException($"Registreringsnumret {regNumber} finns redan i garaget.");

            PromptHandler.AskUser("Ange tillverkningsår.", ConsoleColor.Yellow);
            int year;
            if (!int.TryParse(Console.ReadLine(), out year))
                throw new ArgumentException("Inmatningsfel.");

            PromptHandler.AskUser("Ange antal sätten.", ConsoleColor.Yellow);
            int seatCount;
            if (!int.TryParse(Console.ReadLine(), out seatCount))
                throw new ArgumentException("Inmatningsfel.");

            PromptHandler.AskUser($"Ange färg.\n{ColorHandler.GetValidColorsString(6)}", ConsoleColor.Yellow);
            string? color = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(color))
                throw new ArgumentException("Ogiltig färg.");
            if (!ColorHandler.IsValidColor(color))
                throw new ArgumentException("Ogiltig färg.");

            Vehicle vehicle;
            if (type == 1) {
                bool hasSunRoof;
                PromptForCarDetails(out hasSunRoof);
                vehicle = new Car(make, model, regNumber, year, seatCount, color, hasSunRoof);
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

        private static void PromptForCarDetails(out bool hasSunRoof)
        {
            PromptHandler.AskUser("Har bilen soltak? (j/n)", ConsoleColor.Yellow);
            char input;
            if (!char.TryParse(Console.ReadLine(), out input))
                throw new ArgumentException("Inmatningsfel.");

            if (input.ToString().ToLower() == "j")
                hasSunRoof = true;
            else if (input.ToString().ToLower() == "n")
                hasSunRoof = false;
            else
                throw new ArgumentException("Ogiltigt värde.");
        }

        private static void PromptForMotorcycleDetails(out bool hasSidecar)
        {
            PromptHandler.AskUser("Har motorcykeln sidovagn? (j/n)", ConsoleColor.Yellow);
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
            PromptHandler.AskUser("Ange planets maxhöjd.", ConsoleColor.Yellow);
            int seatCount;
            if (!int.TryParse(Console.ReadLine(), out seatCount))
                throw new ArgumentException("Inmatningsfel.");
            maxHeight = seatCount;

            PromptHandler.AskUser("Ange antal motorer.", ConsoleColor.Yellow);
            if (!int.TryParse(Console.ReadLine(), out seatCount))
                throw new ArgumentException("Inmatningsfel.");
            engineCount = seatCount;
        }

        private static void PromptForBoatDetails(out PropulsionType propulsionType)
        {
            PromptHandler.AskUser("Ange drivmedelstyp (1 = Rodd, 2 = Segel, 3 = Motor). (1-indexering))", ConsoleColor.Yellow);
            int propulsionTypeInt;
            if (!int.TryParse(Console.ReadLine(), out propulsionTypeInt))
                throw new ArgumentException("Ogiltig drivmedelstyp.");
            propulsionTypeInt--; // Konvertera till 0-indexering
            if (propulsionTypeInt < 0 || propulsionTypeInt > 2)
                throw new ArgumentOutOfRangeException("Ogiltig drivmedelstyp.");
            propulsionType = (PropulsionType)propulsionTypeInt;
        }

        private static void PromptForBusDetails(out EngineType engineType)
        {
            PromptHandler.AskUser("Ange motortyp (1 = Bensin, 2 = Diesel, 3 = Elektrisk). (1-indexering)", ConsoleColor.Yellow);
            int engineTypeInt;
            if (!int.TryParse(Console.ReadLine(), out engineTypeInt))
                throw new ArgumentException("Ogiltig motortyp.");
            engineTypeInt--; // Konvertera till 0-indexering
            if (engineTypeInt < 0 || engineTypeInt > 2)
                throw new ArgumentOutOfRangeException("Ogiltig motortyp.");
            engineType = (EngineType)engineTypeInt;
        }

        public static void SaveGarage(Garage<Vehicle> garage)
        {
            PromptHandler.AskUser("Vill du spara garaget innan programmet avslutas? (j/n)", ConsoleColor.Yellow);
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

        
    }
}
