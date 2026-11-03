using System;
using System.Collections.Generic;
using System.Text;

namespace GarageApp3p0.Application.HandlerClasses
{
    public static class PromptHandler
    {
        private const string MainMenu =
            "-=---=- Huvudmeny -=---=-\n" +
            "1. Lägg till fordon\n" +
            "2. Ta bort fordon\n" +
            "3. Visa alla fordon (Hög detalj)\n" +
            "4. Visa alla fordon (Låg detalj)\n" +
            "5. Sök efter fordon\n" +
            "6. Avsluta";

        private const string AdvancedSearchMenu =
            "Avancerad sökning: Välj alternativ för " +
            "att filtrera bland garagets fordon:\n\n" +
            "-=---=- Avancerad sökmeny -=---=-\n" +
            "1. Filtrera efter märke\n" +
            "2. Filtrera efter modell\n" +
            "3. Sök efter registreringsnummer\n" +
            "4. Filtrera efter årtal\n" +
            "5. Filtrera efter färg\n" +
            "6. Snegla på listan\n" +
            "7. Återgå till huvudmenyn";

        public static void AskUser(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.Write(">");
        }

        public static void PromptUserMainMenu()
        {
            AskUser(MainMenu, ConsoleColor.Yellow);
        }

        public static void PromptUserAdvancedSearchMenu()
        {
            AskUser(AdvancedSearchMenu, ConsoleColor.Yellow);
        }
    }
}
