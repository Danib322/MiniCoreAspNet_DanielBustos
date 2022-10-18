using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniCoreAspNet_DanielBustos.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Pword { get; set; }
        public ICollection<Pase> Pases { get; set; }
    }
}
