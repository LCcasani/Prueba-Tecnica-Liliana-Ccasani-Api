using App.Ruleta.Api.Enums;

namespace App.Ruleta.Api.Models
{
    public class ResultadoRuleta
    {
        public Color color { get; set; }
        public TipoNumero tipoNumero { get; set; }
        public int numero { get; set; }
    }
}