using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniCoreAspNet_DanielBustos.Models
{
    public class Pase
    {
        public int PaseID { get; set; }
        public int UsuarioID { get; set; }
        public DateTime FechaCompra { get; set; }
        public string TtipoPase { get; set; }
        public DateTime? FinTentativo { get; set; }
        public int? pasesRestantes { get; set; }
        public double? saldoRestante { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
