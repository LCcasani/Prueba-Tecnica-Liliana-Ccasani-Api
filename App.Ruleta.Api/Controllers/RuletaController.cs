using App.Ruleta.Api.Models;
using BL;
using DAL;
using Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace App.Ruleta.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RuletaController : ApiController
    {
        private List<NumeroRuleta> numeros;
        // GET: api/Ruleta
        public async Task<IHttpActionResult> Get()
        {
            return Ok("running api");
        }

        // GET: api/Ruleta/5
        public async Task<IHttpActionResult> GetNumerosAzar()
        {
            await Task.Run(() =>
            {
                numeros = new List<NumeroRuleta>();
                var random = new Random();
                numeros = Enumerable.Range(0, 36).Select(x => new NumeroRuleta { numero=x, color=((x % 2) == 0 ? "red":"black") }).OrderBy(num => random.Next()).ToList();
            });
            
            return Ok(numeros);
        }

        public async Task<IHttpActionResult> GetUsuarioData(string nombre)
        {
            try
            {
                var usuario = await UsuarioDataBL.BuscarUsuarioData(nombre);

                if(usuario == null) { throw new Exception("No se encontró Usuario"); }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // POST: api/Ruleta
        [HttpPost]
        public IHttpActionResult GetPremio ([FromBody] ResultadoRuleta resultado)
        {

            var usuarioApuesta = UsuarioApuesta.Instance;

            var premio = new Premio();
            if(resultado.color == usuarioApuesta.color && resultado.numero == usuarioApuesta.numero)
            {
                premio.ganaPremio = true;
                premio.montoPremio = usuarioApuesta.montoApuesta * 3;
                premio.montoApostado = usuarioApuesta.montoApuesta;
            }else if (resultado.color == usuarioApuesta.color && resultado.tipoNumero == usuarioApuesta.tipoNumero)
            {
                premio.ganaPremio = true;
                premio.montoPremio = usuarioApuesta.montoApuesta;
                premio.montoApostado = usuarioApuesta.montoApuesta;
            }else if(resultado.color == usuarioApuesta.color)
            {
                premio.ganaPremio = true;
                premio.montoPremio = usuarioApuesta.montoApuesta/2;
                premio.montoApostado = usuarioApuesta.montoApuesta;
            }
            else
            {
                premio.ganaPremio = false;
                premio.montoPremio = usuarioApuesta.montoApuesta * -1;
                premio.montoApostado = usuarioApuesta.montoApuesta;
            }

            return Ok(premio);
        }

        // POST: api/Ruleta
        [HttpPost]
        public async Task<IHttpActionResult> CreateUpdate([FromBody]UsuarioData usuarioData)
        {
            try
            {
                var usuario = await UsuarioDataBL.GrabarUsuarioData(usuarioData);
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public  IHttpActionResult GuardarUsuarioApuesta([FromBody] UsuarioApuesta apuesta)
        {
            try
            {

                UsuarioApuesta.Instance.tipoNumero = apuesta.tipoNumero;
                UsuarioApuesta.Instance.color = apuesta.color;
                UsuarioApuesta.Instance.numero = apuesta.numero;
                UsuarioApuesta.Instance.montoApuesta = apuesta.montoApuesta;
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
