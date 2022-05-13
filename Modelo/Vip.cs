using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Vip : Reserva
    {
        private static double precioMultiplier = 1.50;

        public Vip()
        {
        }

        public Vip(Cliente nombrePasajero, int nroHabitación, short díasReserva, DateTime fechaReserva)
            : base(nombrePasajero, nroHabitación, díasReserva, fechaReserva)
        {
            string fecha = formatFecha();
            string número = string.Format("{0:00}", ++correlativo);
            CódigoReserva = $"HVIP-{fecha}-{número}";
        }

        public new int valorReserva()
        {
            return (int)(precioBase * precioMultiplier * getFechaMultiplier());
        }

        public override string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("  [ Habitación VIP ]");
            sb.Append(base.MostrarDatos());
            sb.AppendFormat("\nValor Total:        ${0:0,000}\n", valorReserva());
            return sb.ToString();
        }

    }
}
