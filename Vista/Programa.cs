using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vista
{
    internal class Programa
    {

        private static readonly string HORIZONTAL_LINE = new String('=', 70);
        private const int _DOBLE = 2;

        #region Lista y Singleton
        private static List<Reserva> listaReserva;

        public static List<Reserva> ListaReserva()
        {
            listaReserva = listaReserva ?? new List<Reserva>();
            return listaReserva;
        }
        #endregion

        #region Main
        static void Main(string[] args)
        {
            Thread[] threads = new Thread[3];
            ThreadStart[] threadStarters =
            {
                new ThreadStart(AgregarReserva),
                new ThreadStart(EliminarReserva),
                new ThreadStart(MostrarReserva),
            };

            showWelcome();

            short option = 0;

            do
            {
                do
                {
                    Console.WriteLine("\nMenú principal");
                    Console.WriteLine(HORIZONTAL_LINE);
                    Console.WriteLine("1. Ingresar reserva");
                    Console.WriteLine("2. Eliminar reserva");
                    Console.WriteLine("3. Mostrar reservas ingresadas");
                    Console.WriteLine("4. Salir");
                    Console.Write("\n>");
                } while (!short.TryParse(Console.ReadLine(), out option));

                if (option == 4) break;
                if (option < 1 || option > 3)
                {
                    Console.WriteLine("Opción inválida");
                    continue;
                }

                Thread thread = threads[option - 1];
                ThreadStart threadStart = threadStarters[option - 1];

                thread = new Thread(threadStart);
                thread.Start();
                thread.Join();

                if (thread.ThreadState == ThreadState.Running
                    || thread.ThreadState == ThreadState.Stopped
                    || thread.ThreadState == ThreadState.Suspended)
                {
                    thread.Abort();
                    thread = null;
                }

            } while (option != 4);

            Console.WriteLine("Pulse una tecla para continuar");
            Console.ReadKey();
        }
        #endregion

        #region CRUD Actions
        static void AgregarReserva()
        {
            try
            {
                Reserva r = inputReserva();
                if (r != null)
                {
                    ListaReserva().Add(r);
                    Console.WriteLine("\nReserva creada exitosamente");
                    Console.WriteLine(HORIZONTAL_LINE);
                    Console.Write(r.MostrarDatos());
                    Console.WriteLine(HORIZONTAL_LINE);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"/!\ ERROR /!\");
                Console.WriteLine(e.Message);
            }
        }

        static void EliminarReserva()
        {
            if (ListaReserva().Count == 0)
            {
                Console.WriteLine("No existen reservas para eliminar\n");
                return;
            }

            string código = "";
            Reserva reserva = default;

            do
            {
                do
                {
                    Console.WriteLine("Ingrese el código de la reserva");
                    código = Console.ReadLine();
                } while (código == "");

                reserva = ListaReserva().FirstOrDefault(r => r.CódigoReserva == código);

                if (reserva != null)
                    ListaReserva().Remove(reserva);
                else
                    Console.WriteLine("No existe una reserva con el código ingresado\n");

            } while (reserva == default);
        }
        static void MostrarReserva()
        {
            Console.WriteLine("\nLista de reservas");
            Console.WriteLine(HORIZONTAL_LINE);
            foreach (Reserva r in ListaReserva())
            {
                Console.Write(r.MostrarDatos());
                Console.WriteLine(HORIZONTAL_LINE);
            }
            if (ListaReserva().Count == 0)
                Console.WriteLine("No se encontraron reservas ):\n");
        }
        #endregion

        #region Input Flows
        static Reserva inputReserva()
        {
            short option = 0;
            while (true)
            {
                do
                {
                    Console.WriteLine("\nIngresar reserva");
                    Console.WriteLine(HORIZONTAL_LINE);
                    Console.WriteLine("1. Ingresar reserva normal");
                    Console.WriteLine("2. Ingresar reserva doble");
                    Console.WriteLine("3. Ingresar reserva VIP");
                    Console.WriteLine("4. Ingresar reserva Premium");
                    Console.WriteLine("5. Volver");
                    Console.Write("\n>");
                } while (!short.TryParse(Console.ReadLine(), out option));

                if (option == 5)
                    return null;

                if (option < 1 || option > 4)
                {
                    Console.WriteLine("Opción inválida");
                    continue;
                }

                break;
            }
            Console.WriteLine("\n");

            int nroHabitación = default;
            short díasReserva = default;
            DateTime fechaReserva = default;
            Cliente nombrePasajero = null;
            Cliente nombrePasajero2 = null;

            Console.WriteLine("\nDatos de la reserva");
            Console.WriteLine(HORIZONTAL_LINE);

            do
            {
                Console.WriteLine("N° de habitación (1-30)");
            } while (!int.TryParse(Console.ReadLine(), out nroHabitación));

            do
            {
                Console.WriteLine("\nDías a reservar");
            } while (!short.TryParse(Console.ReadLine(), out díasReserva));


            do
            {
                Console.WriteLine("\nFecha de reserva");
            } while (!DateTime.TryParse(Console.ReadLine(), out fechaReserva));

            try
            {
                nombrePasajero = inputCliente();
                if (option == _DOBLE)
                {
                    Console.WriteLine("\nIngrese el segundo pasajero:");
                    nombrePasajero2 = inputCliente();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(@"/!\ ERROR /!\");
                Console.WriteLine(e.Message);
            }

            Reserva reserva = null;
            try
            {
                switch (option)
                {
                    case 1:
                        reserva = new Normal(nombrePasajero, nroHabitación, díasReserva, fechaReserva);
                        break;
                    case 2:
                        reserva = new Doble(nombrePasajero, nroHabitación, díasReserva, fechaReserva, nombrePasajero2);
                        break;
                    case 3:
                        reserva = new Vip(nombrePasajero, nroHabitación, díasReserva, fechaReserva);
                        break;
                    case 4:
                        reserva = new Premium(nombrePasajero, nroHabitación, díasReserva, fechaReserva);
                        break;
                    default:
                        break;
                }
                return reserva;
            }
            catch (Exception)
            {
                throw;
            }

        }

        static Cliente inputCliente()
        {
            string rut = default;
            string nombre = default;
            string sexo = default;
            DateTime fechaNacimiento = default;
            string dirección = default;
            string teléfono = default;

            Console.WriteLine("\nDatos del pasajero");
            Console.WriteLine(HORIZONTAL_LINE);

            do
            {
                Console.WriteLine("RUT");
                rut = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(rut));

            do
            {
                Console.WriteLine("\nNombre");
                nombre = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(nombre));

            do
            {
                Console.WriteLine("\nSexo");
                sexo = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(sexo));

            do
            {
                Console.WriteLine("\nFecha nacimiento");
            } while (!DateTime.TryParse(Console.ReadLine(), out fechaNacimiento));

            do
            {
                Console.WriteLine("\nDirección");
                dirección = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(dirección));

            do
            {
                Console.WriteLine("\nTeléfono");
                teléfono = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(teléfono));

            try
            {
                return new Cliente(dirección, fechaNacimiento, nombre, rut, sexo, teléfono);
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

        static void showWelcome()
        {
            Console.WriteLine(@"
   _    _       _       _   __  __                                   
  | |  | |     | |     | | |  \/  |            Elías Ugarte / 357V                              
  | |__| | ___ | |_ ___| | | \  / | __ _ _ __   __ _  __ _  ___ _ __ 
  |  __  |/ _ \| __/ _ \ | | |\/| |/ _` | '_ \ / _` |/ _` |/ _ \ '__|
  | |  | | (_) | ||  __/ | | |  | | (_| | | | | (_| | (_| |  __/ |   
  |_|  |_|\___/ \__\___|_| |_|  |_|\__,_|_| |_|\__,_|\__, |\___|_|   
                                                      __/ |          
                                                     |___/           ");
        }

    }
}
