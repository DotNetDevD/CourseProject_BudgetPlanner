using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Clases
{
    internal class BudgetStatistic
    {
        private decimal? _totalIncome = 0;
        private decimal? _totalExpense = 0;
        private decimal? _revenue = 0;
        public void GetPersonIncome(int id)
        {
            using (MyBudgetPlannerContext db = new())
            {
                // check if person have some income
                if (db.Incomes.Any(e => e.Person.Id == id))
                {
                    // remove all incomes for user by id
                    var listofIncomes = db.Incomes.Where(e => e.Person.Id == id);
                    foreach (var income in listofIncomes)
                    {
                        _totalIncome += income.CountIncome;
                    }
                }
                Console.WriteLine($"Total user incomes with ID:{id} = {_totalIncome}.");
            }
        }
        public void GetPersonExpense(int id)
        {
            using (MyBudgetPlannerContext db = new())
            {
                // check if person have some expenses
                if (db.Expenses.Any(e => e.Person.Id == id))
                {
                    // remove all expenses for user by id
                    var listOfExpenses = db.Expenses.Where(e => e.Person.Id == id);
                    foreach (var income in listOfExpenses)
                    {
                        _totalExpense += income.CountExpenses;
                    }
                }
                Console.WriteLine($"Total user expenses with ID:{id} = {_totalExpense}.");
            }
        }
        public void GetPersonRevenue(int id)
        {
            _revenue = _totalIncome - _totalExpense;
            if (_revenue < 0)
                Console.WriteLine($"User with ID:{id} over budget at {_revenue}. Try to save move and spend less.");
            else if (_revenue == 0)
                Console.WriteLine($"User with ID:{id} under budget {_revenue}. Try to save something");
            else
                Console.WriteLine($"User with ID:{id} saved more then {_revenue}. Great job! Keep it up!");
        }

    }
}
