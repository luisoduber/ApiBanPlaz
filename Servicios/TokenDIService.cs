using ApiBanPlaz.models.Entities;
using ApiBanPlaz.models.TokenDl;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static DebinController;

namespace ApiBanPlaz.Servicios
{


    public class TokenDIService
    {
        private readonly BanPlazDbContext _context;

        public TokenDIService(BanPlazDbContext context)
        {
            _context = context;
        }

        public async Task<int> GrdTokenDIAsync(
            string prmMoneda,
            string prmCanal,
            string prmTvalidacion_p,
            string prmIdentificacion_p,
            string prmCuenta_cobrador,
            string prmCuenta_pagador,
            string prmTelefono_pagador,
            string prmCod_banco_p,
            string prmMonto,
            string prmDireccion_ip,
            string prmCadReq)
        {
            var result = await _context
           .Set<TokenDI>()
           .FromSqlRaw(
                    "CALL spGrdTokenDIReq({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10})",
                     prmMoneda,
                     prmCanal,
                     prmTvalidacion_p,
                     prmIdentificacion_p,
                     prmCuenta_cobrador,
                     prmCuenta_pagador,
                     prmTelefono_pagador,
                     prmCod_banco_p,
                     prmMonto,
                     prmDireccion_ip,
                     prmCadReq
                    ).AsNoTracking()
           .ToListAsync();
            return result.First().IdTokenDI;
        }


        public async Task<bool> GrdTokenDIRespAsync(
            int prmIdTokenDI,
            string prmCodigoRespuesta,
            string prmDescripcionCliente,
            string prmDescripcionSistema,
            string prmFechaHora,
            string prmCadResp)
        {
            try
            {
                // ExecuteSqlRawAsync devuelve el número de filas afectadas
                var rsDat = await _context.Database.ExecuteSqlRawAsync(
                    "CALL spGrdTokenDIResp({0}, {1}, {2}, {3}, {4}, {5})",
                    prmIdTokenDI,
                    prmCodigoRespuesta,
                    prmDescripcionCliente,
                    prmDescripcionSistema,
                    prmFechaHora,
                    prmCadResp
                );

                // Si se insertó al menos una fila, devolvemos true
                return rsDat > 0;
            }
            catch (Exception ex)
            {
                // Aquí puedes loguear el error (ex.Message) si algo falla

                Debug.WriteLine("Error: "+ex.Message);
                return false;
            }
        }
    }

}
