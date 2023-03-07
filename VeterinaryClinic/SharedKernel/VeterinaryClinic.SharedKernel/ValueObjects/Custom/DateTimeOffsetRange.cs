using Ardalis.GuardClauses;

namespace VeterinaryClinic.SharedKernel.ValueObjects.Custom
{
    public class DateTimeOffsetRange : ValueObject
    {
        public DateTimeOffset Start { get; private set; }
        public DateTimeOffset End { get; private set; }

        private DateTimeOffsetRange(DateTimeOffset start, DateTimeOffset end)
        {
            Guard.Against.Null(start, nameof(start));
            Guard.Against.Null(end, nameof(end));
            Guard.Against.OutOfRange(start, nameof(start), start, end);

            Start = start;
            End = end;
        }

        public static DateTimeOffsetRange Create(DateTimeOffset start, DateTimeOffset end) {
            return new DateTimeOffsetRange(start, end);            
        }

        public static DateTimeOffsetRange CreateWithDuration(DateTimeOffset start, TimeSpan duration)
        {
            return new DateTimeOffsetRange(start, start.Add(duration));            
        }

        public int DurationInMinutes()
        {
            return (int)Math.Round((End - Start).TotalMinutes, 0);
        }

        public DateTimeOffsetRange NewEnd(DateTimeOffset newEnd)
        {
            return new DateTimeOffsetRange(Start, newEnd);
        }

        public bool Overlaps(DateTimeOffsetRange dateTimeRange)
        {
            return Start < dateTimeRange.End &&
                End > dateTimeRange.Start;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Start;
            yield return End;
        }
    }
}
