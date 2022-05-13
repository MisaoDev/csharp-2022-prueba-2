using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Doble : Reserva
    {
        private static double precioMultiplier = 1.25;

        private Cliente nombrePasajero2;

        public Cliente NombrePasajero2
        {
            get { return nombrePasajero2; }
            set { nombrePasajero2 = value; }
        }

        public Doble()
        {
        }

        public Doble(Cliente nombrePasajero, int nroHabitación, short díasReserva, DateTime fechaReserva, Cliente nombrePasajero2)
            : base(nombrePasajero, nroHabitación, díasReserva, fechaReserva)
        {
            string fecha = formatFecha();
            string número = string.Format("{0:00}", ++correlativo);
            CódigoReserva = $"HDBL-{fecha}-{número}";
            NombrePasajero2 = nombrePasajero2;
        }

        public new int valorReserva()
        {
            return (int)(precioBase * precioMultiplier * getFechaMultiplier());
        }

        public override string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("  [ Habitación Doble ]");
            sb.Append(base.MostrarDatos());
            sb.AppendLine("\n  [ Pasajero 2 ]");
            sb.Append(nombrePasajero2.MostrarDatos());
            sb.AppendFormat("\nValor Total:        ${0:0,000}\n", valorReserva());
            return sb.ToString();
        }

    }
}
