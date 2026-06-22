/* Store Assessment — This program is designed to let a user to set a budget, 
   print items from a store, purchase them, see what they have purchased and exit */

// Create dictionary to hold all of the shop items 
// Using decimals thoughout this program to ensure no errors due to floats
Dictionary<string, decimal> storeItems = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
storeItems.Add("Mouse", 25.00m);
storeItems.Add("Keyboard", 35.75m);
storeItems.Add("Headphones", 39.99m); 
storeItems.Add("Webcam", 60.00m);
storeItems.Add("Speakers", 45.30m);
storeItems.Add("USB Drive", 15.00m);
storeItems.Add("SSD Drive", 94.99m);
storeItems.Add("HDMI Cable", 12.30m);
storeItems.Add("Ethernet Cable", 10.00m);
storeItems.Add("Mouse Pad", 10.00m);
storeItems.Add("Laptop Sleeve", 25.00m);
storeItems.Add("Cooling Pad", 30.00m);
storeItems.Add("USB Hub", 20.00m);
storeItems.Add("Power Bank", 49.98m);
storeItems.Add("SD Card", 20.00m);
storeItems.Add("Card Reader", 18.50m);
storeItems.Add("Desk Fan", 22.00m);
storeItems.Add("Screen Cleaner", 15.00m);
storeItems.Add("Power Board", 28.00m);
storeItems.Add("USB Adapter", 14.99m);

// Define dictionary to hold the purchased items 
Dictionary<string, decimal> purchasedItems = new Dictionary<string, decimal>();

// Define budget variables
decimal budget = 0;
decimal initialBudget = 0;
// Minimum budget (cannot be below this number)
const decimal MIN_BUDGET = 1;

// Method that prompts the user to press any key to continue
static void PressAnyKeyPrompt()
{
    Console.WriteLine("\nPress any key to continue.");
    Console.ReadKey(true);
    Console.Clear();
}

// Method that will get the users input and test if it has no errors
static decimal GetUserInput()
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
        // Tell user if string is cannot be converted to an decimal
        else if (!decimal.TryParse(input, out decimal inputDecimal))
        {
            Console.WriteLine("Input must be a valid number (float).");
        }
        // Return if it passes the tests
        else
        {
            return inputDecimal;
        }
    }
}

// Method to show the budget to the user
static void GetBudget(decimal budget)
{
    Console.Clear();
    Console.WriteLine($"Your current budget is ${budget:F2}");
}

// Method to list all the items in the store
static void ListStoreItems(Dictionary<string, decimal> storeItems, decimal? budget)
{
    Console.Clear();
    // If there is no items in store, tell user
    if (storeItems.Count <= 0)
    {
        Console.WriteLine("There are no items in the store.");
    }
    // If there is a budget, show only items within that budget
    else if (budget.HasValue)
    {
        // Make a list containing all the items within the budget 
        List<string> itemsInBudget = new List<string>{};
        foreach (var i in storeItems)
        {
            if (i.Value <= budget)
            {
                itemsInBudget.Add($"{i.Key}: ${i.Value:F2}");
            }
        }
        // If there are items within budget, print all of them
        if (itemsInBudget.Count > 0)
        {
            Console.WriteLine($"These are all the items in the store within your budget of ${budget:F2}:");
            foreach (string i in itemsInBudget)
            {
                Console.WriteLine(i);
            }
        }
        // If there are no items within budget, tell user
        else
        {
            Console.WriteLine($"There are no items in the store within your budget of ${budget:F2}");
        }
    }
    // If there isn't a budget, list all items in store
    else
    {
        Console.WriteLine("These are all the items in the store:");
        foreach (var i in storeItems)
        {
            Console.WriteLine($"{i.Key}: ${i.Value:F2}");
        }
    }
}

// Method to list all purchased items
static void ListPurchasedItems(Dictionary<string, decimal> itemsList, decimal budget, decimal initialBudget)
{
    Console.Clear();
    // List all purchased items if user has bought any
    if (itemsList.Count > 0)
    {
        Console.WriteLine("These are all of the items you purchased:\n");
        foreach (var i in itemsList)
        {
            Console.WriteLine($"You purchased {i.Key} for ${i.Value:F2}");
        }
        Console.WriteLine($"\nYour budget after purchasing is ${budget:F2}");
        Console.WriteLine($"You spent ${(initialBudget - budget):F2} in total.");
    }
    // If user has not purchased anything, tell user
    else
    {
        Console.WriteLine("You did not purchase anything.");
    }
}

// Method to confirm a purchase
void PurchaseConfirmation(Dictionary<string, decimal> currentPurchasedItems, KeyValuePair<string, decimal> item)
{
    Console.WriteLine($"Are you sure you would like to purchase {item.Key} for ${item.Value:F2}? (Y/N)");
    while (true) 
    {
        ConsoleKeyInfo selection = Console.ReadKey(true);
        // If user says y, remove purchased item from the store, negate money and add to the purchased items dictionaries
        if (selection.Key == ConsoleKey.Y)
        {
            budget = (budget - item.Value);
            currentPurchasedItems.Add(item.Key, item.Value);
            purchasedItems.Add(item.Key, item.Value);
            Console.WriteLine($"Purchased {item.Key} for ${item.Value:F2}");
            Console.WriteLine($"Your budget is now ${budget:F2}");
            storeItems.Remove(item.Key);
            break;
        }
        // If user says no, cancel purchase
        else if (selection.Key == ConsoleKey.N) 
        {
            Console.WriteLine("Purchase cancelled.");
            break;
        }
        // If the user types anything else
        else
        {
            Console.WriteLine("Please press Y for yes or N for no.");
        }
    }
}

// Method to purchase item
void PurchaseItem()
{
    Console.Clear();
    // Make temp variables to hold the current purchased items and the initial budget
    Dictionary<string, decimal> currentPurchasedItems = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
    decimal initialBudget = budget;
    // Loop until the user has picked an item or exited
    while (true)
    {
        // Get input, trim it and if it's null make it string.empty
        Console.WriteLine("What item would you like to purchase? (type 'exit' to exit):");
        string userItem = Console.ReadLine()?.Trim() ?? String.Empty;
        // If input empty tell user
        if (userItem == String.Empty)
        {
            Console.WriteLine("Item cannot be empty");
        }
        // If input is in the store, ask user if they are sure before letting them purchase
        else if (storeItems.ContainsKey(userItem))
        {
            /* Loop though dictionary to find the users item AND retain the capitalisation from the dictionary, 
            rather then use the userItem variable, which might have different capitalisation */
            foreach (var item in storeItems)
            {
                // If the key is the item the user entered, proceed
                if (item.Key.Equals(userItem, StringComparison.OrdinalIgnoreCase))
                {
                    // Check if user has enough budget to buy item
                    if (item.Value > budget)
                    {
                        Console.WriteLine($"You do not have enough budget (${budget:F2}) to purchase {item.Key} (${item.Value:F2}).");
                    }
                    // If user has enough budget, proceed
                    else
                    {
                        // Ask for user confirmation upon purchasing item
                        PurchaseConfirmation(currentPurchasedItems, item);
                    }
                }
            }
        }
        // If user entered 0, exit
        else if (userItem.Equals("exit", StringComparison.OrdinalIgnoreCase))
        {
            ListPurchasedItems(currentPurchasedItems, budget, initialBudget);
            break;
        }
        // If anything else is typed, tell user
        else
        {
            Console.WriteLine($"{userItem} is not in the store.");
        }
    }
}

// Method to display the menu
void DisplayMenu()
{
    while (true)
    {
        // Clear console than display the store menu
        Console.Clear();
        Console.WriteLine("""
        ----------------- Store -----------------
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
            break;
        }
        // Show all items in store if user presses 1
        else if (selection.Key == ConsoleKey.D1)
        {
            ListStoreItems(storeItems, null);
            PressAnyKeyPrompt();
        }
        // Show all items in store within users budget if user presses 2
        else if (selection.Key == ConsoleKey.D2)
        {
            ListStoreItems(storeItems, budget);
            PressAnyKeyPrompt();
        }
        // Show users budget if user presses 3
        else if (selection.Key == ConsoleKey.D3)
        {
            GetBudget(budget);
            PressAnyKeyPrompt();
        }
        // Let user purchase items if user presses 4
        else if (selection.Key == ConsoleKey.D4)
        {
            PurchaseItem();
            PressAnyKeyPrompt();
        }
        // List all purchased items if user presses 5
        else if (selection.Key == ConsoleKey.D5)
        {
            ListPurchasedItems(purchasedItems, budget, initialBudget);
            PressAnyKeyPrompt();
        }
    }

}

// Ask for users budget
Console.WriteLine("What is your budget? (NZD):");
// Loop until user enters a valid budget
while (true)
{
    budget = GetUserInput();
    if (budget >= MIN_BUDGET)
    {
        break;
    }
    else
    {
        Console.WriteLine($"Budget must be at least ${MIN_BUDGET:F2}. Try again:");
    }
}
// Set initial budget to the budget than display menu
initialBudget = budget;
DisplayMenu();