using BudgetPlanner.DbModels;
using Microsoft.EntityFrameworkCore;

namespace BudgetPlanner.Clases;

public partial class MyBudgetPlannerContext : DbContext
{
    public MyBudgetPlannerContext()
    {
    }

    public MyBudgetPlannerContext(DbContextOptions<MyBudgetPlannerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Income> Incomes { get; set; }

    public virtual DbSet<Person> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database = MyBudgetPlanner; Trusted_Connection = true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Expense__3214EC076DA6F468");

            entity.ToTable("Expense");

            entity.Property(e => e.CountExpenses)
                .HasDefaultValueSql("((0))")
                .HasColumnType("money");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.TypeOfExpenses).HasMaxLength(50);

            entity.HasOne(d => d.Person).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__Expense__PersonI__4E88ABD4");
        });

        modelBuilder.Entity<Income>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Income__3214EC0736EBADCD");

            entity.ToTable("Income");

            entity.Property(e => e.CountIncome)
                .HasDefaultValueSql("((0))")
                .HasColumnType("money");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.TypeOfIncomes).HasMaxLength(50);

            entity.HasOne(d => d.Person).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__Income__PersonId__49C3F6B7");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Person__3214EC07E2BC5A00");
            entity.ToTable("Person");
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Surname).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
