using Ardalis.Specification;
using VeterinaryClinic.SharedKernel.Interfaces;

namespace VeterinaryClinic.AppointmentModule.Infrastructure.Data
{
    public class EfReadRepository<T> : IReadRepository<T> where T : class, IAggregateRoot
    {
        private readonly EfRepository<T> _sourceRepository;        
        public EfReadRepository(EfRepository<T> sourceRepository)
        {
            _sourceRepository = sourceRepository;
        }

        public async Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            return await _sourceRepository.AnyAsync(specification, cancellationToken);
        }

        public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            return await _sourceRepository.AnyAsync(cancellationToken);
        }

        public async Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            return await _sourceRepository.CountAsync(specification, cancellationToken);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await _sourceRepository.CountAsync(cancellationToken);    
        }

        public async Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            return await _sourceRepository.FirstOrDefaultAsync(specification, cancellationToken);
        }

        public async Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        {
            return await _sourceRepository.FirstOrDefaultAsync<TResult>(specification, cancellationToken);
        }

        public async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        {
            return await _sourceRepository.GetByIdAsync<TId>(id, cancellationToken);
        }

        public async Task<T?> GetBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            return await _sourceRepository.GetBySpecAsync(specification, cancellationToken);
        }

        public async Task<TResult?> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        {
            return await _sourceRepository.GetBySpecAsync<TResult>(specification, cancellationToken);
        }

        public async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await _sourceRepository.ListAsync(cancellationToken);
        }

        public async Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            return await _sourceRepository.ListAsync(specification, cancellationToken);
        }

        public async Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        {
            return await _sourceRepository.ListAsync<TResult>(specification, cancellationToken);
        }

        public async Task<T?> SingleOrDefaultAsync(ISingleResultSpecification<T> specification, CancellationToken cancellationToken = default)
        {
            return await _sourceRepository.SingleOrDefaultAsync(specification, cancellationToken);
        }

        public async Task<TResult?> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        {
            return await _sourceRepository.SingleOrDefaultAsync<TResult>(specification, cancellationToken);
        }
    }
}
