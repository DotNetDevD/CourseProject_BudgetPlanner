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
    internal class IncomeManager : DbManager
    {
        public override string? DataBaseName { get; set; } = "Income";

        public override void Add()
        {
            Console.WriteLine("Choose ID user for add income");
            if (IsCorrectUser(out int id))
            {
                using (MyBudgetPlannerContext db = new())
                {
                    Income income = new Income();
                    Console.Write("Enter type of your income: ");
                    string nameOfIncome = CorrectString();
                    income.TypeOfIncomes = nameOfIncome;
                    Console.Write("Enter how much your income was: ");
                    income.CountIncome = CorrectNumber();
                    income.PersonId = id;
                    income.Person = db.People.FirstOrDefault(x => x.Id == id);
                    income.Date = DateTime.Today;
                    db.Incomes.Add(income);
                    db.SaveChanges();
                }
            }
        }
        public override void Alter()
        {
            if (IsIncome())
            {
                using (MyBudgetPlannerContext db = new())
                {
                    new IncomeManager().Show();
                    Console.WriteLine("Choose ID income for change");
                    int id = (int)CorrectNumber();
                    if (db.Incomes.Find(id) != null)
                    {
                        foreach (var changeIncome in db.Incomes.Where(income => income.Id == id))
                        {
                            Console.Write("Enter name of your income: ");
                            string nameOfIncome = CorrectString();
                            changeIncome.TypeOfIncomes = nameOfIncome;
                            Console.Write("Enter how much your expenses was: ");
                            changeIncome.CountIncome = CorrectNumber();
                        }
                    }
                    else
                        Console.WriteLine($"Not found income with ID = {id}");
                    db.SaveChanges();
                }
            }
            else
                Console.WriteLine("No income available for change.");
        }
        public override void Remove()
        {
            if (IsIncome())
            {
                using (MyBudgetPlannerContext db = new())
                {
                    new IncomeManager().Show();
                    Console.WriteLine("Choose ID for remove income.");
                    int id = (int)CorrectNumber();
                    if (db.Incomes.Find(id) != null)
                    {
                        db.Incomes.Remove(db.Incomes.Find(id));
                    }
                    else
                        Console.WriteLine($"Not found income with ID = {id}.");
                    db.SaveChanges();
                }
            }
            else
                Console.WriteLine("No expenses available for delete.");
        }
        public override void Show()
        {
            using (MyBudgetPlannerContext db = new())
            {
                if (IsIncome())
                {
                    var incomes = db.Incomes.ToList();
                    foreach (var income in incomes)
                    {
                        //Find person who has income
                        Person person = db.People?.FirstOrDefault(p => p.Id == income.PersonId);
                        Console.WriteLine($"{income.Id}. {person.Name} {person.Surname} has {income.TypeOfIncomes} income = {income.CountIncome}.");
                    }
                }
                else
                    Console.WriteLine("No income available at this moment.");
            }
        }
        public static bool IsIncome()
        {
            bool isIncomeInDB;
            using (MyBudgetPlannerContext db = new())
            {
                isIncomeInDB = db.Incomes.Any();
            }
            return isIncomeInDB;
        }
    }
}
