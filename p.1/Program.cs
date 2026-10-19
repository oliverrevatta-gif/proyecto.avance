using System;
using System.Text;

namespace MesaDePartesHospital
{
    class Program
    {
        // DATOS GLOBALES 
        const int MAX = 100;
        static string[] codigos = new string[MAX];
        static string[] nombres = new string[MAX];
        static string[] dnis = new string[MAX];
        static string[] telefonos = new string[MAX];
        static string[] seguros = new string[MAX];
        static string[] tramites = new string[MAX];
        static string[] fechas = new string[MAX];
        static string[] estados = new string[MAX];
        static int total = 0;
        static int contador = 0;

        static string[] listaTramites =
        {
            "Solicitud de cita",
            "Copia de historia clínica",
            "Reclamo o sugerencia",
            "Referencia o contrarreferencia",
            "Certificado médico"
        };
        //  PROGRAMA PRINCIPAL Y MENÚ 
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            int opcion;
            do
            {
                MostrarMenu();
                opcion = LeerOpcion(0, 6);
                switch (opcion)
                {
                    case 1: RegistrarExpediente(); break;
                    case 2: ConsultarPorCodigo(); break;
                    case 3: BuscarPorNombre(); break;
                    case 4: ListarExpedientes(); break;
                    case 5: OrdenarPorCodigo(); break;
                    case 6: OrdenarPorNombre(); break;
                }
            } while (opcion != 0);
            Console.WriteLine("\nGracias por usar la Mesa de Partes Digital. Hasta pronto.");
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n========== MESA DE PARTES DIGITAL - HOSPITAL ==========");
            Console.WriteLine("1. Registrar expediente");
            Console.WriteLine("2. Consultar expediente por código");
            Console.WriteLine("3. Buscar expedientes por nombre");
            Console.WriteLine("4. Listar expedientes");
            Console.WriteLine("5. Ordenar por código ");
            Console.WriteLine("6. Ordenar por nombre ");
            Console.WriteLine("0. Salir");
        }
        // FUNCIONES DE ENTRADA Y VALIDACIÓN 
        static int LeerOpcion(int min, int max)
        {
            int valor;
            Console.Write("Elija una opción: ");
            while (!int.TryParse(Console.ReadLine(), out valor) || valor < min || valor > max)
                Console.Write($"  Opción inválida. Ingrese un número entre {min} y {max}: ");
            return valor;
        }

        static string LeerTexto(string mensaje)
        {
            string texto;
            do
            {
                Console.Write(mensaje);
                texto = (Console.ReadLine() ?? "").Trim();
                if (texto == "") Console.WriteLine("  El campo no puede estar vacío.");
            } while (texto == "");
            return texto;
        }

        static bool SoloDigitos(string texto)
        {
            for (int i = 0; i < texto.Length; i++)
                if (!char.IsDigit(texto[i])) return false;
            return true;
        }

        static bool SoloLetrasYEspacios(string texto)
        {
            for (int i = 0; i < texto.Length; i++)
                if (!char.IsLetter(texto[i]) && texto[i] != ' ') return false;
            return true;
        }

        static string NormalizarNombre(string nombre)
        {
            string[] palabras = nombre.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", palabras).ToUpper();
        }

        static string LeerNombre()
        {
            string nombre;
            bool valido;
            do
            {
                nombre = NormalizarNombre(LeerTexto("Apellidos y nombres: "));
                valido = nombre.Length >= 5 && nombre.IndexOf(' ') > 0 && SoloLetrasYEspacios(nombre);
                if (!valido) Console.WriteLine("  Ingrese al menos un apellido y un nombre (solo letras).");
            } while (!valido);
            return nombre;
        }

        static string LeerDni()
        {
            string dni;
            bool valido;
            do
            {
                dni = LeerTexto("DNI (8 dígitos): ").Replace("-", "").Replace(" ", "");
                valido = dni.Length == 8 && SoloDigitos(dni);
                if (!valido) Console.WriteLine("  DNI inválido: deben ser exactamente 8 números.");
            } while (!valido);
            return dni;
        }

        static string LeerTelefono()
        {
            string telefono;
            bool valido;
            do
            {
                telefono = LeerTexto("Teléfono / Celular: ").Replace(" ", "");
                valido = telefono.Length >= 7 && telefono.Length <= 9 && SoloDigitos(telefono);
                if (!valido) Console.WriteLine("  Teléfono inválido: debe contener entre 7 y 9 dígitos numéricos.");
            } while (!valido);
            return telefono;
        }

        static string ElegirSeguro()
        {
            Console.WriteLine("\n¿Con qué seguro se atiende el paciente?");
            Console.WriteLine("  1. EsSalud");
            Console.WriteLine("  2. SIS (Seguro Integral de Salud)");
            return LeerOpcion(1, 2) == 1 ? "EsSalud" : "SIS";
        }

        static string ElegirTramite()
        {
            Console.WriteLine("\nTipo de trámite:");
            for (int i = 0; i < listaTramites.Length; i++)
                Console.WriteLine($"  {i + 1}. {listaTramites[i]}");
            int op = LeerOpcion(1, listaTramites.Length);
            return listaTramites[op - 1];
        }

    }
}