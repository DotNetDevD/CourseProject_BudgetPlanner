using System;
using System.Collections.Generic;

namespace BudgetPlanner.DbModels;

public partial class Income
{
    public int Id { get; set; }

    public int? PersonId { get; set; }

    public DateTime? Date { get; set; }

    public string TypeOfIncomes { get; set; } = null!;

    public decimal? CountIncome { get; set; }

    public virtual Person? Person { get; set; }
}
