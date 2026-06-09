using System.Linq;
// Create dictionary to hold all of the shop items
Dictionary<string, double> STORE_ITEMS = new Dictionary<string, double>();
STORE_ITEMS.Add("Mouse", 25.00);
STORE_ITEMS.Add("Keyboard", 35.75);
STORE_ITEMS.Add("Headphones", 39.99);
STORE_ITEMS.Add("Webcam", 60.00);
STORE_ITEMS.Add("Speakers", 45.30);
STORE_ITEMS.Add("USB Drive", 15.00);
STORE_ITEMS.Add("SSD Drive", 94.99);
STORE_ITEMS.Add("HDMI Cable", 12.30);
STORE_ITEMS.Add("Ethernet Cable", 10.00);
STORE_ITEMS.Add("Mouse Pad", 10.00);
STORE_ITEMS.Add("Laptop Sleeve", 25.00);
STORE_ITEMS.Add("Cooling Pad", 30.00);
STORE_ITEMS.Add("USB Hub", 20.00);
STORE_ITEMS.Add("Power Bank", 49.98);
STORE_ITEMS.Add("SD Card", 20.00);
STORE_ITEMS.Add("Card Reader", 18.50);
STORE_ITEMS.Add("Desk Fan", 22.00);
STORE_ITEMS.Add("Screen Cleaner", 15.00);
STORE_ITEMS.Add("Power Board", 28.00);
STORE_ITEMS.Add("USB Adapter", 14.99);
// Define budget variable
double budget = 0;
// Method that will get the users input and test if it has no errors
static double GetUserInput()
{
    while (true)
    {
        // Get input and trim any spaces
        string input = Console.ReadLine()?.Trim() ?? String.Empty;
        // Tell user if string is empty and try again
        if (input == String.Empty)
        {
            Console.WriteLine("Input can not be empty");
        }
        // Tell user if string is cannot be converted to an double
        else if (!double.TryParse(input, out double inputDouble))
        {
            Console.WriteLine("Input must be a valid number (float).");
        }
        // Return if it passes the tests
        else
        {
            return inputDouble;
        }
    }
}
static void ListStoreItems(Dictionary<string, double> storeItems, double? budget)
{
    if (budget.HasValue)
    {
        List<string> itemsInBudget = new List<string>{};
        foreach (var i in storeItems)
        {
            if (i.Value <= budget)
            {
                itemsInBudget.Add($"{i.Key}: ${i.Value}");
            }
        }
        if (itemsInBudget.Count > 0)
        {
            foreach (string i in itemsInBudget)
            {
                Console.WriteLine(i);
            }
        }
        else
        {
            Console.WriteLine($"There are no items in the store within your budget of ${budget}");
        }
    }
    else
    {
        Console.WriteLine("These are all the items in the store:");
        foreach (var i in storeItems)
        {
            Console.WriteLine($"{i.Key}: ${i.Value}");
        }
    }
}
// Function to display the menu
void DisplayMenu()
{
    // Clear console then display a menu of difficulties and get user key input
    Console.WriteLine("Store");
    Console.WriteLine("""
    [1] List all items in store
    [2] List all items in store within budget
    [3] View Budget
    [4] Purchase Item
    [5] Print all purchased items
    [0] Exit
    """);
    ConsoleKeyInfo selection = Console.ReadKey(true);
    // Exit if user presses 0
    if (selection.Key == ConsoleKey.D0)
    {
    }
    else if (selection.Key == ConsoleKey.D1)
    {
        ListStoreItems(STORE_ITEMS, null);
    }
    else if (selection.Key == ConsoleKey.D2)
    {
        ListStoreItems(STORE_ITEMS, budget);
    }
    else if (selection.Key == ConsoleKey.D3)
    {
        Console.WriteLine($"Your budget is ${budget}");
    }
    else if (selection.Key == ConsoleKey.D4)
    {

    }
    else if (selection.Key == ConsoleKey.D5)
    {

    }
    // Clear console if user types anything else to avoid spamming console
    else
    {
        Console.Clear();
    }
}
// Get budget than display menu
Console.WriteLine("What is your budget? (NZD)");
budget = GetUserInput();
DisplayMenu();