using System;
using System.Text;

namespace MesaDePartesHospital
{
    class Program
    {
        // BLOQUE 1: DATOS GLOBALES 
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

    }
}