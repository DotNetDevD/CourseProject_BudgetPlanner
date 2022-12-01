using BudgetPlanner.Clases;
using BudgetPlanner.Validation;
using DataBase = BudgetPlanner.Validation.Validation;

Console.WriteLine("Welcome to Budget planner APP!");
WorkWithDB start = new();

do
{
    DataBase? dataBase = start.WorkWithDb();
    start.WorkWithEntity(dataBase!);
}
while (DataBase.IsAction());



//TODO: Correct my equation of income and expense

/*
 public override string ToString()
//        {
//            decimal balance = _income.Total - _expense.Total;
//            if (balance < 0)
//                return $"Your over budget at {balance}. Try to save move and expense less";
//            else if (balance == 0)
//                return $"Your under budget {balance}. Try to save something";
//            else
//                return $"Your over budget at {balance}. Great job! Keep it up!";
//        }
 
public override string ToString()
//        {
//            decimal balance = _income.Total - _expense.Total;
//            if (balance < 0)
//                return $"Your over budget at {balance}. Try to save move and expense less";
//            else if (balance == 0)
//                return $"Your under budget {balance}. Try to save something";
//            else
//                return $"Your over budget at {balance}. Great job! Keep it up!";
//        }
 
//public decimal Total
//        {
//            get
//            {
//                decimal sum = 0;
//                if (Amount is not null)
//                    foreach (var amount in Amount)
//                    {
//                        sum += amount;
//                    }
//                return sum;
//            }
//        }
//public decimal DailyExpenses
//        {
//            get
//            {
//                int dayInCurrentMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
//                return Math.Round((Total / dayInCurrentMonth), 4);
//            }
//        }
*/