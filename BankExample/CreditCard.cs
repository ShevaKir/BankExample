using BankExample.Enums;

namespace BankExample;

public class CreditCard : Card
{
    public int CreditLimit { get; }
    public int UsedCredit { get; private set; }

    public CreditCard(string name, CardType type, int creditLimit)
        : base(name, type)
    {
        CreditLimit = creditLimit;
        UsedCredit = 0;
    }

    public override void Deposit(int amount)
    {
        if (amount <= 0) return;

        if (UsedCredit > 0)
        {
            int pay = Math.Min(UsedCredit, amount);
            UsedCredit -= pay;
            amount -= pay;

            AddTransaction(new Transaction(pay, TransactionType.CreditPaid));
        }

        if (amount > 0)
        {
            base.Deposit(amount);
        }
    }

    public override bool Withdraw(int amount)
    {
        if (amount <= 0) return false;

        if (Balance >= amount)
        {
            return base.Withdraw(amount);
        }

        if (UsedCredit + amount <= CreditLimit)
        {
            UsedCredit += amount;
            AddTransaction(new Transaction(amount, TransactionType.CreditUsed));
            return true;
        }

        return false;
    }
}