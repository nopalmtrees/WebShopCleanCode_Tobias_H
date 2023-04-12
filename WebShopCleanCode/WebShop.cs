using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WebShopCleanCode
{
    public class WebShop
    {
        bool running = true;
        Database database = new Database();
        List<ProductProxy> products = new List<ProductProxy>();
        List<Customer> customers = new List<Customer>();

        string currentMenu = "main menu";
        int currentChoice = 1;
        int amountOfOptions = 3;
        string option1 = "See Wares";
        string option2 = "Customer Info";
        string option3 = "Login";
        string option4 = "";
        string info = "What would you like to do?";

        string username = null;
        string password = null;
        Customer currentCustomer;

        public WebShop()
        {
            products = database.GetProducts();
            customers = database.GetCustomers();
        }

        public void Run()
        {
            Console.WriteLine("Welcome to the WebShop!");
            while (running)
            {
                StartMenu();

                string choice = Console.ReadLine().ToLower();
                switch (choice)
                {
                    case "left":
                    case "l":
                        StearLeft();
                        break;
                    case "right":
                    case "r":
                        StearRight();
                        break;
                    case "ok":
                    case "k":
                    case "o":
                        if (currentMenu.Equals("main menu"))
                        {
                            switch (currentChoice)
                            {
                                case 1:
                                    WaresMenu();
                                    break;
                                case 2:
                                    CustomerMenu();
                                    break;
                                case 3:
                                    LogInMenu();
                                    break;
                                default:
                                    NotAOption();
                                    break;
                            }
                        }
                        else if (currentMenu.Equals("customer menu"))
                        {
                            AddFunds();
                        }
                        else if (currentMenu.Equals("sort menu"))
                        {
                            bool back = true;
                            back = WaresSorted(back);
                            if (back)
                            {
                                WaresMenu();
                            }
                        }
                        else if (currentMenu.Equals("wares menu"))
                        {
                            switch (currentChoice)
                            {
                                case 1:
                                    PrintProductInfo();
                                    break;
                                case 2:
                                    if (currentCustomer != null)
                                    {
                                        PurchaseMenu();
                                    }
                                    else
                                    {
                                        Console.WriteLine(" n/ You must be logged in to purchase wares. n/");
                                        currentChoice = 1;
                                    }
                                    break;
                                case 3:
                                    SortProducts();
                                    break;
                                case 4:
                                    LogInMenu();
                                    break;
                                case 5:
                                    break;
                                default:
                                    NotAOption();
                                    break;
                            }
                        }
                        else if (currentMenu.Equals("login menu"))
                        {
                            switch (currentChoice)
                            {
                                case 1:
                                    EnterUserName();
                                    break;
                                case 2:
                                    EnterPassword();
                                    break;
                                case 3:
                                    if (username == null || password == null)
                                    {
                                        Console.WriteLine(" n/ Incomplete data. n/");
                                    }
                                    else
                                    {
                                        bool found = false;
                                        foreach (Customer customer in customers)
                                        {
                                            if (username.Equals(customer.Username) && customer.CheckPassword(password))
                                            {
                                                found = LoggedInSuccessful(customer);
                                                break;
                                            }
                                        }
                                        if (found == false)
                                        {
                                            Console.WriteLine(" n/ Invalid credentials. n/");
                                        }
                                    }
                                    break;
                                    
                                case 4:
                                    string newUsername = ChecUserName();

                                    //Testar en Builder här.

                                    CustomerBuilder customerBuilder = new CustomerBuilder();

                                    customerBuilder.SetUsername(newUsername);

                                    string newPassword = RegisterForm("password");
                                    if (!newPassword.Equals(""))
                                    {
                                        customerBuilder.SetPassword(newPassword);

                                    }

                                    string firstName = RegisterForm("firstname");
                                    if (!firstName.Equals(""))
                                    {
                                        customerBuilder.SetFirstName(firstName);
                                    }

                                    string lastName = RegisterForm("lastname");
                                    if (!lastName.Equals(""))
                                    {
                                        customerBuilder.SetLastName(lastName);
                                    }

                                    string email = RegisterForm("email");
                                    if (!email.Equals(""))
                                    {
                                        customerBuilder.SetEmail(email);
                                    }

                                    string address = RegisterForm("address");
                                    if (!address.Equals(""))
                                    {
                                        customerBuilder.SetAddress(address);
                                    }

                                    string phoneNumber = RegisterForm("phone number");
                                    if (!phoneNumber.Equals(""))
                                    {
                                        customerBuilder.SetPhone(phoneNumber);
                                    }

                                    int age = RegisterFormAge();
                                    if (age != 0)
                                    {
                                        customerBuilder.SetAge(age);
                                    }

                                    Customer newCustomer = customerBuilder.Build();
                                    customers.Add(newCustomer);
                                    currentCustomer = newCustomer;
                                    Console.WriteLine();
                                    Console.WriteLine(newCustomer.Username + " successfully added and is now logged in.");
                                    Console.WriteLine();
                                    Back();
                                    break;
                                default:
                                    NotAOption();
                                    break;
                            }
                        }
                        else if (currentMenu.Equals("purchase menu"))
                        {
                            ChechIfPurchaseIsAnOption();
                        }
                        break;
                    case "back":
                    case "b":
                        BackToMain();
                        break;
                    case "quit":
                    case "q":
                        Console.WriteLine("The console powers down. You are free to leave.");
                        return;
                    default:
                        Console.WriteLine("That is not an applicable option.");
                        break;
                }
            }
        }

        private void PrintProductInfo()
        {
            Console.WriteLine();
            foreach (ProductProxy product in products)
            {
                product.PrintInfo();
            }
            Console.WriteLine();
        }

        private void ChechIfPurchaseIsAnOption()
        {
            int index = currentChoice - 1;
            ProductProxy product = products[index];
            if (product.IsInStock())
            {
                if (currentCustomer.CanAfford(product.GetPrice()))
                {
                    currentCustomer.Funds -= product.GetPrice();
                    product.RemoveFromStock();
                    currentCustomer.Orders.Add(new Order(product.GetName(), product.GetPrice(), DateTime.Now));
                    Console.WriteLine();
                    Console.WriteLine("Successfully bought " + product.GetName);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("n/ You cannot afford. n/");
                }
            }
            else
            {
                Console.WriteLine("n/ Not in stock. n/");
            }
        }

        private void BackToMain()
        {
            if (currentMenu.Equals("main menu"))
            {
                Console.WriteLine("You're already on the main menu.");
            }
            else if (currentMenu.Equals("purchase menu"))
            {
                WaresMenu();
            }
            else
            {
                Back();
            }
        }

        private void StearRight()
        {
            if (currentChoice < amountOfOptions)
            {
                currentChoice++;
            }
        }

        private void StearLeft()
        {
            if (currentChoice > 1)
            {
                currentChoice--;
            }
        }

        private string ChecUserName()
        {
            Console.WriteLine("Please write your username.");
            string newUsername = Console.ReadLine();
            foreach (Customer customer in customers)
            {
                if (customer.Username.Equals(username))
                {
                    Console.WriteLine(" n/ Username already exists. n/ ");
                    break;
                }
            }

            return newUsername;
        }

        private void PurchaseMenu()
        {
            currentMenu = "purchase menu";
            info = "What would you like to purchase?";
            currentChoice = 1;
            amountOfOptions = products.Count;
        }

        private void Back()
        {
            option1 = "See Wares";
            option2 = "Customer Info";
            if (currentCustomer == null)
            {
                option3 = "Login";
            }
            else
            {
                option3 = "Logout";
            }
            info = "What would you like to do?";
            currentMenu = "main menu";
            currentChoice = 1;
            amountOfOptions = 3;
        }

        private bool LoggedInSuccessful(Customer customer)
        {
            bool found;
            Console.WriteLine();
            Console.WriteLine(customer.Username + " logged in.");
            Console.WriteLine();
            currentCustomer = customer;
            found = true;
            Back();
            return found;
        }

        private void EnterPassword()
        {
            Console.WriteLine("A keyboard appears.");
            Console.WriteLine("Please input your password.");
            password = Console.ReadLine();
            Console.WriteLine();
        }

        private void EnterUserName()
        {
            Console.WriteLine("A keyboard appears.");
            Console.WriteLine("Please input your username.");
            username = Console.ReadLine();
            Console.WriteLine();
        }

        private void SortProducts()
        {
            option1 = "Sort by name, descending";
            option2 = "Sort by name, ascending";
            option3 = "Sort by price, descending";
            option4 = "Sort by price, ascending";
            info = "How would you like to sort them?";
            currentMenu = "sort menu";
            currentChoice = 1;
            amountOfOptions = 4;
        }

        private bool WaresSorted(bool back)
        {
            switch (currentChoice)
            {
                case 1:
                    bubbleSort("name", false);
                    WaresSortedText();
                    break;
                case 2:
                    bubbleSort("name", true);
                    WaresSortedText();
                    break;
                case 3:
                    bubbleSort("price", false);
                    WaresSortedText();
                    break;
                case 4:
                    bubbleSort("price", true);
                    WaresSortedText();
                    break;
                default:
                    back = false;
                    NotAOption();
                    break;
            }

            return back;
        }

        private static void WaresSortedText()
        {
            Console.WriteLine("n/ Wares sorted. n/");
        }

        private int RegisterFormAge()
        {
            string choice;
            while (true)
            {
                int age = 0;
                Console.WriteLine("Do you want an age? y/n");
                choice = Console.ReadLine();
                if (choice.Equals("y"))
                {
                    while (true)
                    {
                        Console.WriteLine("Please write your age.");
                        string ageString = Console.ReadLine();
                        try
                        {
                            age = int.Parse(ageString);
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine("n/ Please write a number. n/");
                            continue;
                        }
                        return age;
                        break;
                    }
                }
                if (choice.Equals("n"))
                {
                    return age;
                    break;
                }
                Console.WriteLine(" n/ y or n, please. n/");
            }

            //return age;
        }

        private string RegisterForm(string Text)
        {

            while (true)
            {
                Console.WriteLine("Do you want a " + Text + " y/n");
                string choice = Console.ReadLine();
                if (choice.Equals("y"))
                {
                    while (true)
                    {
                        Console.WriteLine("Please write your " + Text);
                        string userInput = Console.ReadLine();
                        if (userInput.Equals(""))
                        {
                            Console.WriteLine("n/ Please actually write something. n/");
                            continue;
                        }
                        else
                        {
                            return userInput;
                        }
                    }
                }
                if (choice.Equals("n"))
                {
                    return "";
                }
                Console.WriteLine("n/ y or n, please. n/");
            }
        }
        private void AddFunds()
        {
            switch (currentChoice)
            {
                case 1:
                    currentCustomer.PrintOrders();
                    break;
                case 2:
                    currentCustomer.PrintInfo();
                    break;
                case 3:
                    Console.WriteLine("How many funds would you like to add?");
                    string amountString = Console.ReadLine();
                    try
                    {
                        int amount = int.Parse(amountString);
                        if (amount < 0)
                        {
                            Console.WriteLine(" n/ Don't add negative amounts. n/ ");
                        }
                        else
                        {
                            currentCustomer.Funds += amount;
                            Console.WriteLine(amount + " added to your profile.");
                        }
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Please write a number next time.");
                    }
                    break;
                default:
                    NotAOption();
                    break;
            }
        }

        private static void NotAOption()
        {
            Console.WriteLine("Not an option.");
        }

        private void LogInMenu()
        {
            if (currentCustomer == null)
            {
                option1 = "Set Username";
                option2 = "Set Password";
                option3 = "Login";
                option4 = "Register";
                amountOfOptions = 4;
                currentChoice = 1;
                info = "Please submit username and password.";
                username = null;
                password = null;
                currentMenu = "login menu";
            }
            else
            {
                option3 = "Login";
                Console.WriteLine();
                Console.WriteLine(currentCustomer.Username + " logged out.");
                Console.WriteLine();
                currentChoice = 1;
                currentCustomer = null;
            }
        }

        private void CustomerMenu()
        {
            if (currentCustomer != null)
            {
                option1 = "See your orders";
                option2 = "Set your info";
                option3 = "Add funds";
                option4 = "";
                amountOfOptions = 3;
                currentChoice = 1;
                info = "What would you like to do?";
                currentMenu = "customer menu";
            }
            else
            {
                Console.WriteLine(" n/ Nobody is logged in. n/");
            }
        }

        private void WaresMenu()
        {
            option1 = "See all wares";
            option2 = "Purchase a ware";
            option3 = "Sort wares";
            if (currentCustomer == null)
            {
                option4 = "Login";
            }
            else
            {
                option4 = "Logout";
            }
            amountOfOptions = 4;
            currentChoice = 1;
            currentMenu = "wares menu";
            info = "What would you like to do?";
        }

        private void StartMenu()
        {
            Console.WriteLine(info);

            if (currentMenu.Equals("purchase menu"))
            {
                for (int i = 0; i < amountOfOptions; i++)
                {
                    Console.WriteLine(i + 1 + ": " + products[i].GetName + ", " + products[i].GetPrice + "kr");
                }
                Console.WriteLine("Your funds: " + currentCustomer.Funds);
            }
            else
            {
                Console.WriteLine("1: " + option1);
                Console.WriteLine("2: " + option2);
                if (amountOfOptions > 2)
                {
                    Console.WriteLine("3: " + option3);
                }
                if (amountOfOptions > 3)
                {
                    Console.WriteLine("4: " + option4);
                }
            }

            for (int i = 0; i < amountOfOptions; i++)
            {
                Console.Write(i + 1 + "\t");
            }
            Console.WriteLine();
            for (int i = 1; i < currentChoice; i++)
            {
                Console.Write("\t");
            }
            Console.WriteLine("|");

            Console.WriteLine("Your buttons are Left, Right, OK, Back and Quit.");
            if (currentCustomer != null)
            {
                Console.WriteLine("Current user: " + currentCustomer.Username);
            }
            else
            {
                Console.WriteLine("Nobody logged in.");
            }
        }

        private void bubbleSort(string variable, bool ascending)
        {
            if (variable.Equals("name")) {
                int length = products.Count;
                for(int i = 0; i < length - 1; i++)
                {
                    bool sorted = true;
                    int length2 = length - i;
                    for (int j = 0; j < length2 - 1; j++)
                    {
                        if (ascending)
                        {
                            if (products[j].GetName().CompareTo(products[j + 1].GetName()) < 0)
                            {
                                ProductProxy temp = products[j];
                                products[j] = products[j + 1];
                                products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                        else
                        {
                            if (products[j].GetName().CompareTo(products[j + 1].GetName()) > 0)
                            {
                                ProductProxy temp = products[j];
                                products[j] = products[j + 1];
                                products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                    }
                    if (sorted == true)
                    {
                        break;
                    }
                }
            }
            else if (variable.Equals("price"))
            {
                int length = products.Count;
                for (int i = 0; i < length - 1; i++)
                {
                    bool sorted = true;
                    int length2 = length - i;
                    for (int j = 0; j < length2 - 1; j++)
                    {
                        if (ascending)
                        {
                            if (products[j].GetPrice() > products[j + 1].GetPrice())
                            {
                                ProductProxy temp = products[j];
                                products[j] = products[j + 1];
                                products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                        else
                        {
                            if (products[j].GetPrice() < products[j + 1].GetPrice())
                            {
                                ProductProxy temp = products[j];
                                products[j] = products[j + 1];
                                products[j + 1] = temp;
                                sorted = false;
                            }
                        }
                    }
                    if (sorted == true)
                    {
                        break;
                    }
                }
            }
        }
    }
}
