﻿namespace VeterinaryClinic.SharedKernel.ValueObjects
{
    [Serializable]
    public abstract class SimpleValueObject<T> : ValueObject
    {
        public T Value { get; }

        protected SimpleValueObject(T value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value?.ToString();
        }

        public static implicit operator T(SimpleValueObject<T> valueObject)
        {
            return valueObject == null ? default : valueObject.Value;
        }
    }
}
