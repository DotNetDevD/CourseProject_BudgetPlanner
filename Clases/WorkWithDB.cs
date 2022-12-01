using BudgetPlanner.Enum;
using BudgetPlanner.Validation;
using Data = BudgetPlanner.Validation;

namespace BudgetPlanner.Clases
{
    internal class WorkWithDB
    {
        private int _choose;
        private string[] _operationList = { "Perss 1 to ADD ",
                                            "Perss 2 to ALTER ",
                                            "Perss 3 to REMOVE ",
                                            "Press 4 to SHOW "};
        private string[] _dbList = { "Perss 1 to add Person",
                                     "Perss 2 to add Expense",
                                     "Perss 3 to add Income"};
        public void ShowOperation(Data.Validation validData)
        {
            foreach (var operation in _operationList)
            {
                Console.WriteLine(operation + validData.DataBaseName);
            }
        }
        public void ShowDB()
        {
            foreach (var db in _dbList)
            {
                Console.WriteLine(db);
            }
        }
        public Data.Validation? WorkWithDb()
        {
            Data.Validation setDb = null;
            Console.WriteLine("Choose database for work");
            ShowDB();
            int.TryParse(Console.ReadLine(), out _choose);
            switch (_choose)
            {
                case (int)DbSet.Person:
                    setDb = new PersonValidation();
                    break;
                case (int)DbSet.Expense:
                    setDb = new ExpenseValidation();
                    break;
                case (int)DbSet.Income:
                    setDb = new IncomeValidation();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Incorrect input");
                    WorkWithDb();
                    break;
            }
            return setDb;
        }
        public void WorkWithEntity(Data.Validation validData)
        {
            Console.WriteLine("Choose action for work");
            ShowOperation(validData);
            int.TryParse(Console.ReadLine(), out _choose);
            switch (_choose)
            {
                case (int)DbOperation.Add:
                    validData.Add();
                    break;
                case (int)DbOperation.Alter:
                    validData.Alter();
                    break;
                case (int)DbOperation.Remove:
                    validData.Remove();
                    break;
                case (int)DbOperation.Show:
                    validData.Show();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Incorrect input");
                    WorkWithEntity(validData);
                    break;
            }
        }
    }

}
