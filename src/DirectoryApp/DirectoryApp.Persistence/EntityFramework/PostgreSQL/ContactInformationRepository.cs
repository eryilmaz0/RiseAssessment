using DirectoryApp.Application.Repository;
using DirectoryApp.Domain.Entity;

namespace DirectoryApp.Persistence.EntityFramework.PostgreSQL;

public class ContactInformationRepository : BaseRepository<ContactInformation, Guid>, IContactInformationRepository
{
    
}