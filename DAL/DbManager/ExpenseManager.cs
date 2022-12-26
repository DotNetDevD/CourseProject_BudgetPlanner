using BudgetPlanner.DAL;
using BudgetPlanner.DbModels;
using BudgetPlanner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Validation
{
    internal class ExpenseManager : DbManager, IShowData
    {
        public override string? DataBaseName { get; set; } = "Expense";
        public override void Add()
        {
            Console.WriteLine("Choose ID user for add expenses");
            if (IsCorrectUser(out int id))
            {
                using (MyBudgetPlannerContext db = new())
                {
                    Expense expense = new Expense();
                    Console.Write("Enter type of your expense: ");
                    string nameOfExpense = CorrectString();
                    expense.TypeOfExpenses = nameOfExpense;
                    Console.Write("Enter how much your expense was: ");
                    expense.CountExpenses = CorrectNumber();
                    expense.PersonId = id;
                    expense.Person = db.People.FirstOrDefault(x => x.Id == id);
                    expense.Date = DateTime.Today;
                    db.Expenses.Add(expense);
                    db.SaveChanges();
                }
            }
        }
        public override void Remove()
        {
            if (IsExpense())
            {
                using (MyBudgetPlannerContext db = new())
                {
                    new ExpenseManager().Show();
                    Console.WriteLine("Choose ID for remove expense.");
                    int id = (int)CorrectNumber();
                    if (db.Expenses.Find(id) != null)
                    {
                        db.Expenses.Remove(db.Expenses.Find(id));
                    }
                    else
                        Console.WriteLine($"Not found expense with ID = {id}.");
                    db.SaveChanges();
                }
            }
            else
                Console.WriteLine("No expenses available for delete.");
        }

        public override void Alter()
        {
            if (IsExpense())
            {
                using (MyBudgetPlannerContext db = new())
                {
                    new ExpenseManager().Show();
                    Console.WriteLine("Choose ID expense for change");
                    int id = (int)CorrectNumber();
                    if (db.Expenses.Find(id) != null)
                    {
                        foreach (var changeExpense in db.Expenses.Where(expense => expense.Id == id))
                        {
                            Console.Write("Enter name of your expense: ");
                            string nameOfExpense = CorrectString();
                            changeExpense.TypeOfExpenses = nameOfExpense;
                            Console.Write("Enter how much your expenses was: ");
                            changeExpense.CountExpenses = CorrectNumber();
                        }
                    }
                    else
                        Console.WriteLine($"Not found expense with ID = {id}");
                    db.SaveChanges();
                }
            }
            else
                Console.WriteLine("No expense available for change.");
        }
        public override void Show()
        {
            using (MyBudgetPlannerContext db = new())
            {
                var expenses = db.Expenses.ToList();
                if (IsExpense())
                {
                    foreach (var expense in expenses)
                    {
                        //Find person who has expense
                        Person person = db.People?.FirstOrDefault(p => p.Id == expense.PersonId);
                        Console.WriteLine($"{expense.Id}. {person.Name} {person.Surname} has {expense.TypeOfExpenses} expense = {expense.CountExpenses}.");
                    }
                }
                else
                    Console.WriteLine("No expenses available at this moment.");
            }
        }
        public static bool IsExpense()
        {
            bool isExpenseInDB;
            using (MyBudgetPlannerContext db = new())
            {
                isExpenseInDB = db.Expenses.Any();
            }
            return isExpenseInDB;
        }
    }
}
