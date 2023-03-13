using Dapper;
using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UsuarioDataDA : IUsuarioDataDA
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public async Task<UsuarioData> BuscarUsuarioData(string nombre)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var query = "select * from tblUsuarioData where UPPER(nombre) = UPPER(@nombre)";
                var parameters = new DynamicParameters();
                parameters.Add("@nombre", nombre);

                var usuarioData = await connection.QueryFirstOrDefaultAsync<UsuarioData>(query, parameters);
                return usuarioData;
            }

        }

        public async Task<int> GuardarUsuarioData(UsuarioData usuariodata)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var query = "INSERT INTO tblUsuarioData (nombre,monto) VALUES (@nombre,@monto)";
                var parameters = new DynamicParameters();
                parameters.Add("@nombre", usuariodata.nombre);
                parameters.Add("@monto", usuariodata.monto);

                var registros = await connection.ExecuteAsync(query, parameters);
                return registros;
            }
        }
        public async Task<int> ActualizarUsuarioData(UsuarioData usuariodata)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var query = "UPDATE tblUsuarioData set monto = @monto where nombre = @nombre";
                var parameters = new DynamicParameters();
                parameters.Add("@nombre", usuariodata.nombre);
                parameters.Add("@monto", usuariodata.monto);

                var registros = await connection.ExecuteAsync(query, parameters);
                return registros;
            }
        }
    }
}
