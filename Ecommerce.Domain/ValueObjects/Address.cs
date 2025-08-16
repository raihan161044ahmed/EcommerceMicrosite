using Ecommerce.Domain.Abstractions;

namespace Ecommerce.Domain.ValueObjects
{
    public sealed class Address : IEquatable<Address>
    {
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string ZipCode { get; }

        public Address(string street, string city, string state, string zipCode)
        {
            if (string.IsNullOrWhiteSpace(street)) throw new DomainException("Street is required.");
            if (string.IsNullOrWhiteSpace(city)) throw new DomainException("City is required.");
            if (string.IsNullOrWhiteSpace(state)) throw new DomainException("State is required.");
            if (string.IsNullOrWhiteSpace(zipCode)) throw new DomainException("ZipCode is required.");

            Street = street.Trim();
            City = city.Trim();
            State = state.Trim();
            ZipCode = zipCode.Trim();
        }

        public bool Equals(Address? other) =>
            other is not null &&
            Street == other.Street &&
            City == other.City &&
            State == other.State &&
            ZipCode == other.ZipCode;

        public override bool Equals(object? obj) => Equals(obj as Address);
        public override int GetHashCode() => HashCode.Combine(Street, City, State, ZipCode);
        public override string ToString() => $"{Street}, {City}, {State} {ZipCode}";
    }
}
