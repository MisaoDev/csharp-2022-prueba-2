using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public abstract class Reserva
    {
        #region Fields
        protected static int correlativo = 0;
        protected static int precioBase = 40_000;

        private string códigoReserva;
        private Cliente nombrePasajero;
        private int nroHabitación;
        private short díasReserva;
        private DateTime fechaReserva;
        #endregion

        #region Properties
        public string CódigoReserva
        {
            get { return códigoReserva; }
            set { códigoReserva = value; }
        }


        public Cliente NombrePasajero
        {
            get { return nombrePasajero; }
            set { nombrePasajero = value; }
        }

        public int NroHabitación
        {
            get { return nroHabitación; }
            set
            {
                if (1 <= value && value <= 30)
                    nroHabitación = value;
                else
                    throw new ArgumentException("El número de habitación debe estar entre 1 y 30");
            }
        }

        public short DíasReserva
        {
            get { return díasReserva; }
            set { díasReserva = value; }
        }


        public DateTime FechaReserva
        {
            get { return fechaReserva; }
            set
            {
                if (value < DateTime.Now)
                    throw new ArgumentException("La fecha no puede ser anterior al día de hoy");
                else
                    fechaReserva = value;
            }
        }

        #endregion

        #region Constructors
        protected Reserva()
        {
        }

        protected Reserva(Cliente nombrePasajero, int nroHabitación, short díasReserva, DateTime fechaReserva)
        {
            NombrePasajero = nombrePasajero;
            NroHabitación = nroHabitación;
            DíasReserva = díasReserva;
            FechaReserva = fechaReserva;
        }
        #endregion

        #region Methods
        public int valorReserva()
        {
            return precioBase * getFechaMultiplier();
        }

        public virtual string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Código Reserva:     {CódigoReserva}");
            sb.AppendLine($"Nro Habitación:     {NroHabitación}");
            sb.AppendLine($"Días Reserva:       {DíasReserva}");
            sb.AppendLine($"Fecha Reserva:      {FechaReserva}");
            sb.AppendLine("\n  [ Pasajero ]");
            sb.Append(nombrePasajero.MostrarDatos());
            return sb.ToString();
        }

        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

        #region Protected Methods
        protected string formatFecha()
        {
            return string.Format(
                "{0:00}{1:00}{2:0000}",
                FechaReserva.Day,
                FechaReserva.Month,
                FechaReserva.Year
            );
        }

        protected int getFechaMultiplier()
        {
            int[] mesesPeak = { 1, 2, 8, 12 };
            return mesesPeak.Contains(FechaReserva.Month) ? 2 : 1;
        }
        #endregion

    }
}
