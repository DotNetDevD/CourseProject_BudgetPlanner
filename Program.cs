using BudgetPlanner.Clases;
using BudgetPlanner.Validation;
using DataBase = BudgetPlanner.Validation.DbManager;

Console.WriteLine("Welcome to Budget planner APP!");
WorkWithDB start = new();

do
{
    DataBase? dataBase = start.WorkWithDb();
    start.WorkWithEntity(dataBase!);
}
while (DataBase.IsAction());
