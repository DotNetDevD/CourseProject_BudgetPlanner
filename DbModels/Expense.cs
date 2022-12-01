using System;
using System.Collections.Generic;

namespace BudgetPlanner.DbModels;

public partial class Expense
{
    public int Id { get; set; }

    public int? PersonId { get; set; }

    public DateTime? Date { get; set; }

    public string TypeOfExpenses { get; set; } = null!;

    public decimal? CountExpenses { get; set; }

    public virtual Person? Person { get; set; }
}
