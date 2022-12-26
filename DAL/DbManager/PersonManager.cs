using BudgetPlanner.Clases;
using BudgetPlanner.DAL;
using BudgetPlanner.DbModels;
using BudgetPlanner.Interfaces;
using BudgetPlanner.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Validation
{
    internal class PersonManager : DbManager, IShowData
    {
        public override string? DataBaseName { get; set; } = "User";

        PersonRepository _repository = new();
        PersonValidator _validator = new();
        public override void Add()
        {
            Person user = new Person();

            Console.Write(Resources.InputName);
            user.Name = Console.ReadLine();
            Console.Write(Resources.InputSurname);
            user.Surname = Console.ReadLine();
            Console.Write(Resources.InputAge);
            user.Age = int.Parse(Console.ReadLine());

            if (ValidatePerson(user))
                _repository.Add(user);
        }

        public override void Alter()
        {
            if (IsAnyUser())
            {
                new PersonManager().Show();
                Console.WriteLine(Resources.UserIdForChange);
                int id = (int)CorrectNumber();

                Person changeUser = new();
                Console.Write(Resources.ChangeName);
                changeUser.Name = Console.ReadLine();
                Console.Write(Resources.ChangeSurname);
                changeUser.Surname = Console.ReadLine();
                Console.Write(Resources.ChangeAge);
                changeUser.Age = int.Parse(Console.ReadLine());

                if (ValidatePerson(changeUser))
                    _repository.Alter(changeUser, id);
            }
            else
                Console.WriteLine(Resources.NotAvailableUser);
        }
        public override void Remove()
        {
            if (IsAnyUser())
            {
                new PersonManager().Show();
                Console.WriteLine(Resources.UserIdForRemove);
                int id = (int)CorrectNumber();
                _repository.Remove(id);
            }
            else
                Console.WriteLine(Resources.NotAvailableUser);
        }
        public override void Show()
        {
            using (MyBudgetPlannerContext db = new())
            {
                var users = db.People.ToList();
                if (IsAnyUser())
                    foreach (var user in users)
                        Console.WriteLine($"{user.Id}. {user.Name} {user.Surname} {user.Age}");
                else
                    Console.WriteLine("No users available at this moment.");
            }
        }
        private bool ValidatePerson(Person person)
        {
            var result = _validator.Validate(person);
            if (result.IsValid)
            {
                return true;
            }

            foreach (var error in result.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return false;
        }

        public static bool IsAnyUser()
        {
            bool isUsersInDB;
            using (MyBudgetPlannerContext db = new())
            {
                isUsersInDB = db.People.Any();
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


