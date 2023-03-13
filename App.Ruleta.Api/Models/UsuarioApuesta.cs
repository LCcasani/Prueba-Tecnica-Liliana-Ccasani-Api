using App.Ruleta.Api.Enums;

namespace App.Ruleta.Api.Models
{
    public class UsuarioApuesta
    {

        public Color color { get; set; }
        public TipoNumero tipoNumero { get; set; }
        public int? numero { get; set; }
        public decimal montoApuesta { get; set; }
        public decimal montoSaldo { get; set; }

        private static UsuarioApuesta instance = null;
        protected UsuarioApuesta() { }
        public static UsuarioApuesta Instance { get
            {
                if(instance == null) instance= new UsuarioApuesta();
                return instance;
            } 
        }


    }
}