using System;
using System.Collections.Generic;
using TenmoClient.Data;
using TenmoClient.Models;

namespace TenmoClient
{
    class Program
    {
        private static readonly ConsoleService consoleService = new ConsoleService();
        private static readonly AuthService authService = new AuthService();
        private static APIService apiService;
        private readonly static string API_Base_Url = "https://localhost:44315";



        static void Main(string[] args)
        {
            Run();
        }
        private static void Run()
        {
            while (true)
            {
                int loginRegister = -1;
                while (loginRegister != 1 && loginRegister != 2)
                {
                    Console.WriteLine("Welcome to TEnmo!");
                    Console.WriteLine("1: Login");
                    Console.WriteLine("2: Register");
                    Console.WriteLine("0: Exit");
                    Console.Write("Please choose an option: ");

                    if (!int.TryParse(Console.ReadLine(), out loginRegister))
                    {
                        Console.WriteLine("Invalid input. Please enter only a number.");
                    }
                    else if (loginRegister == 0)
                    {
                        Environment.Exit(0);
                    }
                    else if (loginRegister == 1)
                    {
                        while (!UserService.IsLoggedIn()) //will keep looping until user is logged in
                        {
                            LoginUser loginUser = consoleService.PromptForLogin();
                            API_User user = authService.Login(loginUser);

                            if (user != null)
                            {
                                UserService.SetLogin(user);
                                apiService = new APIService(API_Base_Url, user.Token);
                            }
                        }
                    }
                    else if (loginRegister == 2)
                    {
                        bool isRegistered = false;
                        while (!isRegistered) //will keep looping until user is registered
                        {
                            LoginUser registerUser = consoleService.PromptForLogin();
                            isRegistered = authService.Register(registerUser);
                            if (isRegistered)
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Registration successful. You can now log in.");
                                loginRegister = -1; //reset outer loop to allow choice for login
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection.");
                    }
                }

                MenuSelection();
            }
        }

        private static void MenuSelection()
        {
            int menuSelection = -1;
            while (menuSelection != 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Welcome to TEnmo! Please make a selection: ");
                Console.WriteLine("1: View your current balance");
                Console.WriteLine("2: View your past transfers");
                Console.WriteLine("3: Send TE bucks");
                Console.WriteLine("4: Log in as different user");
                Console.WriteLine("0: Exit");
                Console.WriteLine("---------");
                Console.Write("Please choose an option: ");

                if (!int.TryParse(Console.ReadLine(), out menuSelection))
                {
                    Console.WriteLine("Invalid input. Please enter only a number.");
                    menuSelection = -1;
                }
                else if (menuSelection == 1)
                {
                    //
                    decimal balance = apiService.GetAccountBalance();
                    Console.WriteLine($"Your current account balance is : {balance:C}");

                }
                else if (menuSelection == 2)
                {
                    List<Transfer> transfers = new List<Transfer>();
                    transfers = apiService.GetTransfers();
                    foreach (Transfer transfer in transfers)
                    {
                        Console.WriteLine(transfer);
                    }
                    int transferId = GetInteger("Please enter transfer ID to view details:  ");
                    Transfer transferDetails = apiService.GetTransferById(transferId);
                    Console.WriteLine(transferDetails);

                }
                else if (menuSelection == 3)
                {
                    List<Account> accounts = apiService.GetAccounts();
                    if (accounts.Count == 0 || accounts == null)
                    {
                        Console.WriteLine("No users found!");
                    }
                    foreach (Account account in accounts)
                    {
                        Console.WriteLine(account);
                    }
                    Transfer newTransfer = new Transfer();
                    newTransfer.Account_To = GetInteger("Enter ID of user you are sending to : ");
                    newTransfer.Amount = GetDecimal("Amount: ");
                    apiService.TransferMoney(newTransfer);
                    

                }
                else if (menuSelection == 4)
                {
                    // Log in as different user
                    Console.WriteLine("");
                    UserService.SetLogin(new API_User()); //wipe out previous login info
                    return; //return to entry point
                }
                else
                {
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                }

            }
        }

        static public int GetInteger(string message)
        {
            int resultValue = 0;
            while (true)
            {
                Console.Write(message + " ");
                string userInput = Console.ReadLine().Trim();
                if (int.TryParse(userInput, out resultValue))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("!!! Invalid input. Please enter a valid whole number.");
                }
            }
            return resultValue;
        }
        static public decimal GetDecimal(string message)
        {
            decimal resultValue = 0;
            while (true)
            {
                Console.Write(message + " ");
                string userInput = Console.ReadLine().Trim();
                if (decimal.TryParse(userInput, out resultValue))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("!!! Invalid input. Please enter a valid decimal number.");
                }
            }
            return resultValue;
        }
        static public string GetString(string message)
        {
            while (true)
            {
                Console.Write(message + " ");
                string userInput = Console.ReadLine().Trim();
                if (!String.IsNullOrEmpty(userInput))
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine("!!! Invalid input. Please enter a valid string.");
                }
            }
        }
        
    }
}
