using DirectoryApp.Application.Repository;
using DirectoryApp.Domain.Entity;
using DirectoryApp.Persistence.Context;

namespace DirectoryApp.Persistence.EntityFramework.PostgreSQL;

public class EnrolleeRepository : BaseRepository<Enrollee, Guid>, IEnrolleeRepository
{
    private readonly ApplicationContext _context;

    public EnrolleeRepository(ApplicationContext context) : base(context)
    {
        _context = context;
    }
}