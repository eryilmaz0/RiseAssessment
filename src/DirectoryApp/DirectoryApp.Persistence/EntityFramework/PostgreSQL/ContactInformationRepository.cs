using DirectoryApp.Application.Repository;
using DirectoryApp.Domain.Entity;
using DirectoryApp.Persistence.Context;

namespace DirectoryApp.Persistence.EntityFramework.PostgreSQL;

public class ContactInformationRepository : BaseRepository<ContactInformation, Guid>, IContactInformationRepository
{
    private readonly ApplicationContext _context;

    public ContactInformationRepository(ApplicationContext context) : base(context)
    {
        _context = context;
    }
}