using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace serialization
{
    class Program
    {
        static void Main()
        {
            BankAccount account = FileHandler.LoadData();

            if (account == null)
            {
                Console.WriteLine("Create New Account");

                Console.Write("Enter Account ID: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Enter Name: ");
                string name = Console.ReadLine();

                Console.Write("Enter Bank Name: ");
                string bank = Console.ReadLine();

                Console.Write("Enter Initial Amount: ");
                double amount = double.Parse(Console.ReadLine());

                if (amount < 500)
                {
                    Console.WriteLine("Initial amount must be at least ₹500");
                    return;
                }

                account = new BankAccount(id, name, bank, amount);
                FileHandler.SaveData(account);
            }
            else
            {
                Console.WriteLine($"Welcome back, {account.HolderName}!");
            }

            while (true)
            {
                Console.WriteLine("\n==== MENU ====");
                Console.WriteLine("1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Balance");
                Console.WriteLine("4. Transaction History");
                Console.WriteLine("5. Exit");

                Console.Write("Choose option: ");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.Write("Enter amount: ");
                        double dep = double.Parse(Console.ReadLine());
                        account.AddMoney(dep);
                        break;

                    case 2:
                        Console.Write("Enter amount: ");
                        double wd = double.Parse(Console.ReadLine());
                        account.DeductMoney(wd);
                        break;

                    case 3:
                        account.ShowBalance();
                        break;

                    case 4:
                        account.ShowTransactions();
                        break;

                    case 5:
                        FileHandler.SaveData(account);
                        Console.WriteLine("Data saved successfully. Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }
            }
        }
    }
}