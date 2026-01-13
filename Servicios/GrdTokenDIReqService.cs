using ApiBanPlaz.models.Entities;
using ApiBanPlaz.models.TokenDl;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using static DebinController;

namespace ApiBanPlaz.Servicios
{


    public class GrdTokenDIReqService
    {
        private readonly BanPlazDbContext _context;

        public GrdTokenDIReqService(BanPlazDbContext context)
        {
            _context = context;
        }

        public async Task<CredApiRs> ObtCredApi()
        {
            var rsList = await _context
                .Set<CredApiRs>()
                .FromSqlRaw("CALL spInfCredApi()")
                .AsNoTracking()
                .ToListAsync();

            return rsList.FirstOrDefault(); // Puede ser null si no existe

            var result = await _context
           .Set<ContNonce>()
           .FromSqlRaw("CALL spGrdContNonce();")
           .AsNoTracking()
           .ToListAsync();

            return result.First().UltNonce;
        }
    }

}
