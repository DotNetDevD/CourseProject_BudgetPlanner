using BudgetPlanner.DAL;
using BudgetPlanner.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Clases
{
    internal class PersonRepository
    {
        // Add new user to DB
        public void Add(Person user)
        {
            using (MyBudgetPlannerContext db = new())
            {
                db.People.Add(user);
                db.SaveChanges();
            }
        }

        // Alter user in DB
        public void Alter(Person newUser, int id)
        {
            using (MyBudgetPlannerContext db = new())
            {
                if (db.People.Find(id) != null)
                {
                    foreach (var oldUser in db.People.Where(user => user.Id == id))
                    {
                        oldUser.Name = newUser.Name;
                        oldUser.Surname = newUser.Surname;
                        oldUser.Age = newUser.Age;
                    }
                    db.SaveChanges();
                }
                else
                    Console.WriteLine($"Not found user with ID = {id}");
            }
        }

        // Remove user from DB
        public void Remove(int id)
        {
            using (MyBudgetPlannerContext db = new())
            {
                // check if person have some expenses
                if (db.Expenses.Any(e => e.Person.Id == id))
                {
                    // remove all expenses for user by id
                    var listOfExpenses = db.Expenses.Where(e => e.Person.Id == id);
                    db.Expenses.RemoveRange(listOfExpenses);
                }
                // check if person have some income
                if (db.Incomes.Any(e => e.Person.Id == id))
                {
                    // remove all incomes for user by id
                    var listofIncomes = db.Incomes.Where(e => e.Person.Id == id);
                    db.Incomes.RemoveRange(listofIncomes);
                }
                if (db.People.Find(id) != null)
                {
                    db.People.Remove(db.People.Find(id));
                }
                else
                    Console.WriteLine($"Not found user with ID = {id}");
                db.SaveChanges();
            }
        }
    }
}
