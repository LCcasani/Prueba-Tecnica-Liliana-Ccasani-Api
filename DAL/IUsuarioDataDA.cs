using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUsuarioDataDA
    {
        Task<int> GuardarUsuarioData(UsuarioData usuariodata);
        Task<int> ActualizarUsuarioData(UsuarioData usuariodata);
        Task<UsuarioData> BuscarUsuarioData(string nombre);
    }
}
