using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Domain.ValueObjects
{
    public sealed class Money : IEquatable<Money>
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public static Money Zero(string currency = "USD") => new(0m, currency);

        public Money(decimal amount, string currency = "USD")
        {
            if (amount < 0) throw new DomainException("Money cannot be negative.");
            if (string.IsNullOrWhiteSpace(currency)) throw new DomainException("Currency is required.");
            Amount = decimal.Round(amount, 2);
            Currency = currency.ToUpperInvariant();
        }

        public Money Add(Money other)
        {
            EnsureSameCurrency(other);
            return new Money(Amount + other.Amount, Currency);
        }

        public Money Multiply(int quantity)
        {
            if (quantity < 0) throw new DomainException("Quantity cannot be negative.");
            return new Money(Amount * quantity, Currency);
        }

        private void EnsureSameCurrency(Money other)
        {
            if (Currency != other.Currency) throw new DomainException("Currency mismatch.");
        }

        public bool Equals(Money? other) =>
            other is not null && Amount == other.Amount && Currency == other.Currency;

        public override bool Equals(object? obj) => Equals(obj as Money);
        public override int GetHashCode() => HashCode.Combine(Amount, Currency);
        public override string ToString() => $"{Amount:0.00} {Currency}";
    }
}
