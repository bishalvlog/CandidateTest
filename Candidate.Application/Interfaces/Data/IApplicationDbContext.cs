using Candidate.Application.Common.Service;
using Candidate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Candidate.Application.Interfaces.Data
{
    public interface IApplicationDbContext : IScopedService
    {
        #region Modules
         DbSet<Candidates> Candidates { get; set; }
        #endregion
        IDbConnection Connection { get;}
    }
}
