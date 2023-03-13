using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UsuarioDataBL
    {

        public static async Task<UsuarioData> BuscarUsuarioData(string nombre)
        {
            IUsuarioDataDA percistence = new UsuarioDataDA();

            return await percistence.BuscarUsuarioData(nombre);
        }

        public static async Task<UsuarioData> GrabarUsuarioData(UsuarioData usuarioData)
        {
            IUsuarioDataDA percistence = new UsuarioDataDA();

            var existUsuarioData = await percistence.BuscarUsuarioData(usuarioData.nombre);
            if(existUsuarioData != null)
            {
                if (!usuarioData.existeUsuario)
                {
                    existUsuarioData.monto += usuarioData.monto;
                }
                else
                {
                    existUsuarioData.monto = usuarioData.monto;
                }

                var result = await percistence.ActualizarUsuarioData(existUsuarioData);
                if (result <= 0) throw new Exception("Error al Actualizar usuario");
                existUsuarioData.existeUsuario = true;
                return existUsuarioData;
            }
            else
            {
                var result = await percistence.GuardarUsuarioData(usuarioData);
                if (result <= 0) throw new Exception("Error al grabar usuario");
                usuarioData.existeUsuario = true;
                return usuarioData;
            }
        }
    }
}
