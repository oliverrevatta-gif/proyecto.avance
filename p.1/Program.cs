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

    }
}