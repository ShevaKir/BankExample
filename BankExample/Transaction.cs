using BankExample.Enums;

namespace BankExample;

public class Transaction
{
    public int Amount { get; }
    public TransactionType Type { get; }
    public DateTime Date { get; }

    public Transaction(int amount, TransactionType type)
    {
        Amount = amount;
        Type = type;
        Date = DateTime.Now;
    }
}