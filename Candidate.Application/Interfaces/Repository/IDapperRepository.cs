﻿using Candidate.Application.Common.Service;
using System.Data;

namespace Candidate.Application.Interfaces.Repository
{
    public interface IDapperRepository : ITransientService
    {
        Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
            where T : class;

        /// <summary>
        /// Get a <typeparamref name="T"/> using raw sql string with parameters.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="sql">The sql string.</param>
        /// <param name="param">The paramters in the sql string.</param>
        /// <param name="transaction">The transaction to be performed.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task"/> of <typeparamref name="T"/>.</returns>
        Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
             where T : class;

        /// <summary>
        /// Get a <typeparamref name="T"/> using raw sql string with parameters.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="sql">The sql string.</param>
        /// <param name="param">The paramters in the sql string.</param>
        /// <param name="transaction">The transaction to be performed.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task"/> of <typeparamref name="T"/>.</returns>
        Task<T> QuerySingleAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, CancellationToken cancellationToken = default)
            where T : class;
    }
}
