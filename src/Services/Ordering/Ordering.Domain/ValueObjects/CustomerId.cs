namespace Ordering.Domain.ValueObjects
{
    public record CustomerId
    {
        public Guid Id { get; }

        private CustomerId(Guid id) => Id = id;

        public static CustomerId Of(Guid id) 
        { 
            ArgumentNullException.ThrowIfNull(id);
            if (id == Guid.Empty) {
                throw new DomainException("Customer Id cannot be Empty");
            }
            return new CustomerId(id);
        }

    }
}
