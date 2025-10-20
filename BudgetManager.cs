using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perosnal_Budget_Tracker
{
    public class BudgetManager // Class to manage budget transactions
    {
        private List<Transaction> transactions = new(); // List to store transactions

        public void AddTransaction(Transaction tx) // Method to add a new transaction
        {
            transactions.Add(tx); // Lägg till transaktionen i listan
        }

        public void ShowAll() // Method to display all transactions
        {
            foreach (var tx in transactions) tx.ShowInfo(); // Visa alla transaktioner med färgkodning
            
            if (transactions.Count == 0) 
            {
                Console.WriteLine("Inga transaktioner registrerade.");
                return;
            }

            Console.WriteLine("\n--- Alla transaktioner ---"); // Rubrik
            
            int count = 1;
            
            foreach (var tx in transactions) // Loop för att visa varje transaktion
            {
                Console.Write($"{count}. ");
                tx.ShowInfo();   // Här används färginställningarna!
                count++;
            }
        }

        public decimal CalculateBalance() // Method to calculate total balance
        {
            return transactions.Sum(t => t.Amount); // Summera alla transaktionsbelopp
        }

        public void DeleteTransaction(int index) // Method to delete a transaction by index
        {
            if (index >= 0 && index < transactions.Count) // Kontrollera giltigt index
                transactions.RemoveAt(index); // Ta bort transaktionen från listan
        }

        public void ShowByCategory() // Method to display transactions grouped by category
        {
            var grouped = transactions.GroupBy(t => t.Category); // Gruppera transaktioner efter kategori
            
            foreach (var group in grouped) // Loop för varje kategori
            {
                Console.WriteLine($"\nKategori: {group.Key}"); // Visa kategorinamn
                
                foreach (var tx in group) // Loop för varje transaktion i kategorin
                {
                    tx.ShowInfo(); // Visa
                }
            }
        }
        public void DeleteByCategory() // Method to delete transactions by selected category
        {
            var categories = transactions.Select(t => t.Category).Distinct().ToList(); // Hämta unika kategorier
            Console.WriteLine("Tillgängliga kategorier:");
            
            for (int i = 0; i < categories.Count; i++) // Loop för att visa kategorier med nummer
            {
                Console.WriteLine($"{i + 1}. {categories[i]}");
            }
           
            Console.Write("Ange numret på kategorin");
            int choice; // Variabel för användarens val
            
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > categories.Count) // Validera inmatning
            {
                Console.Write("Ogiltigt val. Försök igen: ");
            }
            
            string selectedCategory = categories[choice - 1]; // Hämta vald kategori baserat på användarens val
            
            transactions.RemoveAll(t => t.Category == selectedCategory); // Ta bort alla transaktioner i den valda kategorin
            Console.WriteLine($"Alla transaktioner i kategorin '{selectedCategory}' har tagits bort.");

        }
        public void DeleteByCategory(string category) // Overloaded method to delete transactions by specified category
        {
            transactions.RemoveAll(t => t.Category.Equals(category, StringComparison.OrdinalIgnoreCase)); // Ta bort alla transaktioner i den angivna kategorin
            Console.WriteLine($"Alla transaktioner i kategorin '{category}' har tagits bort.");
        }

        public void FilterByCategory(string category) // Method to filter and display transactions by category
        {
            var filtered = transactions.Where(t => t.Category.Equals(category, StringComparison.OrdinalIgnoreCase)); // Filtrera transaktioner baserat på kategori
            Console.WriteLine($"\nTransaktioner i kategori '{category}':");
           
            foreach (var tx in filtered) // Loop för att visa varje filtrerad transaktion
                tx.ShowInfo();   // Färg på raden beroende på belopp!
            
            if (!filtered.Any()) Console.WriteLine("Inga transaktioner hittades."); // Meddela om inga transaktioner hittades
            
            else Console.WriteLine($"Totalt {filtered.Count()} transaktion(er) i kategorin '{category}'."); // Visa antal transaktioner i kategorin
        }

        public void SortByDate() // Method to sort and display transactions by date
        {
            var sorted = transactions.OrderBy(t => t.Date); // Sortera transaktioner efter datum
            Console.WriteLine("\nTransaktioner sorterade efter datum:");
            
            foreach (var tx in sorted) // Loop för att visa varje sorterad transaktion
                tx.ShowInfo();
        }

        public void ShowStatistics() // Method to display statistics about transactions
        {
            int count = transactions.Count; // Räkna antal transaktioner
            decimal totalIncome = transactions.Where(t => t.Amount >= 0).Sum(t => t.Amount); // Summera alla inkomster
            decimal totalExpense = transactions.Where(t => t.Amount < 0).Sum(t => t.Amount); // Summera alla utgifter

            Console.WriteLine($"\nSTATISTIK:");
            Console.WriteLine($"Antal transaktioner: {count}");
            Console.WriteLine($"Total inkomst: {totalIncome} kr");
            Console.WriteLine($"Total utgift: {totalExpense} kr");
        }
    }
}
