using BudgetPlanner.Clases;
using BudgetPlanner.DbModels;
using BudgetPlanner.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Validation
{
    internal class PersonValidation : Validation, IShowData
    {
        public override string? DataBaseName { get; set; } = "User";

        public override void Add()
        {
            using (MyBudgetPlannerContext db = new())
            {
                Person user = new Person();

                Console.Write("Please enter your Name: ");
                user.Name = CorrectString();
                Console.Write("Еnter your Surname: ");
                user.Surname = CorrectString();
                Console.Write("Еnter your age: ");
                user.Age = ValidAge();

                // add new user to DB
                db.People.Add(user);
                db.SaveChanges();
            }
        }

        public override void Alter()
        {
            if (IsUser())
            {
                using (MyBudgetPlannerContext db = new())
                {
                    new PersonValidation().Show();
                    Console.WriteLine("Choose Id User for change");
                    int id = (int)CorrectNumber();
                    if (db.People.Find(id) != null)
                    {
                        foreach (var changeUser in db.People.Where(user => user.Id == id))
                        {
                            Console.Write("Please change Name: ");
                            changeUser.Name = CorrectString();
                            Console.Write("Change Surname: ");
                            changeUser.Surname = CorrectString();
                            Console.Write("Change age: ");
                            changeUser.Age = ValidAge();
                        }
                    }
                    else
                        Console.WriteLine($"Not found user with ID = {id}");
                    db.SaveChanges();
                }
            }
            else
                Console.WriteLine("No users available for change.");
        }
        public override void Remove()
        {
            if (IsUser())
            {
                using (MyBudgetPlannerContext db = new())
                {
                    IShowData iShow = new PersonValidation();
                    iShow.Show();
                    Console.WriteLine("Choose Id User for remove");
                    int id = (int)CorrectNumber();
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
            else
                Console.WriteLine("No users available for delete.");
        }
        public override void Show()
        {
            using (MyBudgetPlannerContext db = new())
            {
                var users = db.People.ToList();
                if (IsUser())
                    foreach (var user in users)
                        Console.WriteLine($"{user.Id}. {user.Name} {user.Surname} {user.Age}");
                else
                    Console.WriteLine("No users available at this moment.");
            }
        }

        public static bool IsUser()
        {
            bool isUsersInDB;
            using (MyBudgetPlannerContext db = new())
            {
                var users = db.People.ToList();
                isUsersInDB = users.Count > 0 ? true : false;
            }
            return isUsersInDB;
        }
        public int ValidAge()
        {
            int age = (int)CorrectNumber();

            bool isValid = (age > 0 || age < 110) ? true : false;
            if (!isValid)
            {
                Console.Write("Incorrect input, age cannot be less 0 and more 110\n" +
                    "Еnter your age one more time: ");
                ValidAge();
            }
            return age;
        }
    }
}


