
using ApiBanPlaz.models.Entities;
using Microsoft.EntityFrameworkCore;

public class NonceService
{
    private readonly BanPlazDbContext _context;

    public NonceService(BanPlazDbContext context)
    {
        _context = context;
    }

    public async Task<string> ObtNonce()
    {
        var result = await _context
       .Set<ContNonce>()
       .FromSqlRaw("CALL spGrdContNonce();")
       .AsNoTracking()
       .ToListAsync();

        return result.First().UltNonce;
    }
}
