using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace serialization
{
    [Serializable]
    public class BankAccount
    {
        public int Id { get; set; }
        public string HolderName { get; set; }
        public string Bank { get; set; }
        public double Amount { get; set; }

        public List<string> Transactions { get; set; } = new List<string>();

        private const double MIN_LIMIT = 500;

        public BankAccount() { }

        public BankAccount(int id, string name, string bank, double amount)
        {
            Id = id;
            HolderName = name;
            Bank = bank;
            Amount = amount;

            Transactions.Add($"Account created with ₹{amount}");
        }

        public void AddMoney(double money)
        {
            if (money <= 0)
            {
                Console.WriteLine("Invalid deposit amount!");
                return;
            }

            Amount += money;
            Transactions.Add($"Deposited ₹{money}");
            Console.WriteLine("Deposit Successful");
        }

        public void DeductMoney(double money)
        {
            if (money <= 0)
            {
                Console.WriteLine("Invalid withdrawal amount!");
                return;
            }

            if (Amount - money >= MIN_LIMIT)
            {
                Amount -= money;
                Transactions.Add($"Withdrew ₹{money}");
                Console.WriteLine("Withdrawal Successful");
            }
            else
            {
                Console.WriteLine("Minimum ₹500 balance must be maintained!");
            }
        }

        public void ShowBalance()
        {
            Console.WriteLine($"Current Balance: ₹{Amount}");
        }

        public void ShowTransactions()
        {
            Console.WriteLine("\n--- Transaction History ---");
            foreach (var t in Transactions)
            {
                Console.WriteLine(t);
            }
        }
    }

    public class FileHandler
    {
        private static readonly string path = "bankdata.json";

        public static void SaveData(BankAccount account)
        {
            string data = JsonSerializer.Serialize(account, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(path, data);
        }

        public static BankAccount LoadData()
        {
            if (!File.Exists(path))
                return null;

            string data = File.ReadAllText(path);
            return JsonSerializer.Deserialize<BankAccount>(data);
        }
    }
}
