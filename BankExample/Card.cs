using BankExample.Enums;

namespace BankExample;

public class Card
{
    private readonly Random _random = new Random();
    private int _balance;
    private readonly List<Transaction> _transactions;

    public Card(string cardName, CardType cardType)
    {
        CardName = cardName;
        CardType = cardType;

        Number = _random.Next(1000, 9999);
        Cvv = _random.Next(100, 999);
        ExpiryDate = DateTime.Now.AddYears(1);

        _balance = 1000;
        _transactions = [];
    }

    public int Number { get; }
    public string CardName { get; }
    public CardType CardType { get; set; }
    public DateTime ExpiryDate { get; }
    public int Cvv { get; }
    public int Balance => _balance;

    public IReadOnlyList<Transaction> Transactions => _transactions;

    protected void AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
    }

    public virtual void Deposit(int amount)
    {
        if (amount <= 0) return;

        _balance += amount;
        AddTransaction(new Transaction(amount, TransactionType.Deposit));
    }

    public virtual bool Withdraw(int amount)
    {
        if (amount <= 0) return false;
        if (amount > _balance) return false;

        _balance -= amount;
        AddTransaction(new Transaction(amount, TransactionType.Withdraw));
        return true;
    }

    public int TotalSpend()
    {
        int total = 0;

        for (int i = 0; i < _transactions.Count; i++)
        {
            if (_transactions[i].Type == TransactionType.Withdraw)
            {
                total += _transactions[i].Amount;
            }
        }

        return total;
    }
}