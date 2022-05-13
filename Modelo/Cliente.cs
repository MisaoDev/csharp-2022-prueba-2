using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Cliente
    {
        #region Fields
        private string dirección;
        private DateTime fechaNacimiento;
        private string nombre;
        private string rut;
        private string sexo;
        private string teléfono;
        #endregion

        #region Properties
        public string Dirección
        {
            get { return dirección; }
            set { dirección = value; }
        }


        public DateTime FechaNacimiento
        {
            get { return fechaNacimiento; }
            set { fechaNacimiento = value; }
        }


        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (value.Length == 0)
                    throw new ArgumentException("El nombre no puede estar vacío");
                else
                    nombre = value;
            }
        }


        public string Rut
        {
            get { return rut; }
            set { rut = value; }
        }


        public string Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }


        public string Teléfono
        {
            get { return teléfono; }
            set { teléfono = value; }
        }
        #endregion

        #region Constructors
        public Cliente()
        {
        }

        public Cliente(string dirección, DateTime fechaNacimiento, string nombre, string rut, string sexo, string teléfono)
        {
            Dirección = dirección;
            FechaNacimiento = fechaNacimiento;
            Nombre = nombre;
            Rut = rut;
            Sexo = sexo;
            Teléfono = teléfono;
        }
        #endregion

        #region Methods
        public string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Dirección:          {Dirección}");
            sb.AppendLine($"FechaNacimiento:    {FechaNacimiento}");
            sb.AppendLine($"Nombre:             {Nombre}");
            sb.AppendLine($"Rut:                {Rut}");
            sb.AppendLine($"Sexo:               {Sexo}");
            sb.AppendLine($"Teléfono:           {Teléfono}");
            return sb.ToString();
        }

        public override string ToString()
        {
            return base.ToString();
        }
        #endregion

    }
}
