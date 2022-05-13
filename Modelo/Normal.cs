using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Normal : Reserva
    {

        public Normal()
        {
        }

        public Normal(Cliente nombrePasajero, int nroHabitación, short díasReserva, DateTime fechaReserva)
            : base(nombrePasajero, nroHabitación, díasReserva, fechaReserva)
        {
            string fecha = formatFecha();
            string número = string.Format("{0:00}", ++correlativo);
            CódigoReserva = $"HSPL-{fecha}-{número}";
        }

        public new int valorReserva()
        {
            return precioBase * getFechaMultiplier();
        }

        public override string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("  [ Habitación Simple ]");
            sb.Append(base.MostrarDatos());
            sb.AppendFormat("\nValor Total:        ${0:0,000}\n", valorReserva());
            return sb.ToString();
        }

    }
}
