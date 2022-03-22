using DirectoryApp.Application.Repository;
using DirectoryApp.Domain.Entity;

namespace DirectoryApp.Persistence.EntityFramework.PostgreSQL;

public class EnrolleeRepository : BaseRepository<Enrollee, Guid>, IEnrolleeRepository
{
    
}