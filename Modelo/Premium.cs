using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Premium : Reserva
    {
        private static double precioMultiplier = 2.00;

        public Premium()
        {
        }

        public Premium(Cliente nombrePasajero, int nroHabitación, short díasReserva, DateTime fechaReserva)
            : base(nombrePasajero, nroHabitación, díasReserva, fechaReserva)
        {
            string fecha = formatFecha();
            string número = string.Format("{0:00}", ++correlativo);
            CódigoReserva = $"HPRM-{fecha}-{número}";
        }

        public new int valorReserva()
        {
            return (int)(precioBase * precioMultiplier * getFechaMultiplier());
        }

        public override string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("  [ Habitación Premium ***** ]");
            sb.Append(base.MostrarDatos());
            sb.AppendFormat("\nValor Total:        ${0:0,000}\n", valorReserva());
            return sb.ToString();
        }

    }
}
