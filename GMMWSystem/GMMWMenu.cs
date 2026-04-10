namespace GMMWSystem;

public class GMMWMenu
{
    public static void Menu()
    {
        Console.WriteLine("\n---Welcome to Greater Manchester Motor Workshop---");
        Console.WriteLine("\n---Enter the number for the option---");
        Console.WriteLine("1. Booking Management");
        Console.WriteLine("2. Repair Management");
        Console.WriteLine("3. Motorist Management");
        Console.WriteLine("4. Vehicle Management");
        Console.WriteLine("5. Parts Management");
        Console.WriteLine("6. Training Class Management");
        Console.WriteLine("7. Staff Management");
        Console.WriteLine("8. Debug");
        Console.WriteLine("0. Exit");
    }

    public static int? userPrompt()
    {
        Console.Write("Choice: ");
        var valid = int.TryParse(Console.ReadLine(), out int choice);
        Console.WriteLine();
        return valid ? choice : null;
    }

    public static void Main(string[] args)
    {
        bool exit = false;
        int? choice;
        do
        {
            Menu();
            choice = userPrompt();
            switch (choice)
            {
                case 1:
                    MotoristMenu();
                    break;
                case 2:
                    VehicleMenu();
                    break;
                case 3:
                    BookingMenu();
                    break;
                case 4:
                    RepairsMenu();
                    break;
                case 5:
                    PartsMenu();
                    break;
                case 6:
                    TCMenu();
                    break;
                case 7:
                    UserMenu();
                    break;
                case 8:
                    DebugMenu();
                    break;
                case 0:
                    exit = true;
                    Console.WriteLine("Exiting program...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        } while (!exit);
    }

    private static void MotoristMenu()
    {
    }

    private static void VehicleMenu()
    {
    }

    private static void BookingMenu()
    {
    }

    private static void RepairsMenu()
    {
    }

    private static void PartsMenu()
    {
    }

    private static void TCMenu()
    {
    }
    private static void UserMenu()
    {
    }
    
    private static void DebugMenu()
    {
    }
    
}