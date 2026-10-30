using GarageApp3p0.Model;
using GarageApp3p0.Model.Vehicles;
using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Application
{
    public static class Utilities
    {   
        public static Garage<Vehicle> WelcomeSequennce()
        {
            string message = 
                "      ::::::::      :::     :::::::::      :::      ::::::::  :::::::::: :::::::::::          ::::::::       ::::::: \r\n" +
                "    :+:    :+:   :+: :+:   :+:    :+:   :+: :+:   :+:    :+: :+:            :+:             :+:    :+:     :+:   :+: \r\n" +
                "   +:+         +:+   +:+  +:+    +:+  +:+   +:+  +:+        +:+            +:+                    +:+     +:+   +:+  \r\n" +
                "  :#:        +#++:++#++: +#++:++#:  +#++:++#++: :#:        +#++:++#       +#+                 +#++:      +#+   +:+   \r\n" +
                " +#+   +#+# +#+     +#+ +#+    +#+ +#+     +#+ +#+   +#+# +#+            +#+                    +#+     +#+   +#+    \r\n" +
                "#+#    #+# #+#     #+# #+#    #+# #+#     #+# #+#    #+# #+#            #+#             #+#    #+# #+# #+#   #+#     \r\n" +
                "########  ###     ### ###    ### ###     ###  ########  ##########     ###              ########  ###  #######       ";
            Console.WriteLine(message);

            Console.WriteLine("Välkommen till Garage Appen 3.0!\n" +
                "Här kan du hantera dina fordon i garaget.\n" +
                "Hur stort ska garaget vara?");
            int garageSize;
            while (!int.TryParse(Console.ReadLine(), out garageSize))
            {
                Console.WriteLine("Ogiltigt värde. Försök igen.");
            }
            Garage<Vehicle> garage = new Garage<Vehicle>(garageSize);
            return garage;
        }

        public static void PrintMainMenu() {
            Console.WriteLine(" -=- Huvudmeny -=-");
            Console.WriteLine("1. Lägg till fordon");
            Console.WriteLine("2. Ta bort fordon");
            Console.WriteLine("3. Visa alla fordon");
            Console.WriteLine("4. Sök efter fordon");
            Console.WriteLine("5. Avsluta");
        }
    }
}
