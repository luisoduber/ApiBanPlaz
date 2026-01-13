using ApiBanPlaz.models.TokenDl;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using static DebinController;

namespace ApiBanPlaz.Servicios
{


    public class CredApiService
    {
        private readonly BanPlazDbContext _context;

        public CredApiService(BanPlazDbContext context)
        {
            _context = context;
        }

        public async Task CrearAsync(string PrmApiKey,string  prmApiKeySecret)
        {
            await _context.Database.ExecuteSqlRawAsync(
            "CALL spGrdCredApi({0}, {1})",
                PrmApiKey,
                prmApiKeySecret
            );
        }
    }

}
