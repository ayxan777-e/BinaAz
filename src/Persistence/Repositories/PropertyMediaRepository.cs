using Application.Abstracts.Repositories;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories;

public class PropertyMediaRepository : GenericRepository<PropertyMedia, int>, IPropertyMediaRepository
{
    public PropertyMediaRepository(BinaLiteDbContext context) : base(context)
    {
    }
}
