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
        // REGISTRO DE EXPEDIENTES 
        static string GenerarCodigo(string seguro)
        {
            contador++;
            string prefijo = seguro == "EsSalud" ? "ESS" : "SIS";
            return prefijo + "-" + DateTime.Now.Year + "-" + contador.ToString().PadLeft(4, '0');
        }

        static bool ExisteDuplicado(string dni, string tramite)
        {
            for (int i = 0; i < total; i++)
                if (dnis[i] == dni && tramites[i] == tramite) return true;
            return false;
        }

        static void RegistrarExpediente()
        {
            Console.WriteLine("\n--- REGISTRAR EXPEDIENTE ---");
            if (total == MAX)
            {
                Console.WriteLine("La mesa de partes alcanzó su capacidad máxima.");
                return;
            }
            string seguro = ElegirSeguro();
            string nombre = LeerNombre();
            string dni = LeerDni();

            // Validación: Verificar si el DNI pertenece a otra persona
            for (int i = 0; i < total; i++)
            {
                if (dnis[i] == dni && nombres[i] != nombre)
                {
                    Console.WriteLine($"\n  Este DNI ya está ingresado con otra persona ({nombres[i]}).");
                    return;
                }
            }

            string telefono = LeerTelefono(); 
            string tramite = ElegirTramite();

            if (ExisteDuplicado(dni, tramite))
            {
                Console.WriteLine("\n  Ya existe un expediente de este paciente con el mismo trámite.");
                return;
            }

            codigos[total] = GenerarCodigo(seguro);
            nombres[total] = nombre;
            dnis[total] = dni;
            telefonos[total] = telefono; 
            seguros[total] = seguro;
            tramites[total] = tramite;
            fechas[total] = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            estados[total] = "Recibido";
            total++;

            Console.WriteLine("\n  Expediente registrado correctamente.");
            Console.WriteLine("  Código de seguimiento: " + codigos[total - 1]);
        }
        //  CONSULTA Y BÚSQUEDA 
        static bool FormatoCodigoValido(string codigo)
        {
            if (codigo.Length != 13) return false;
            string prefijo = codigo.Substring(0, 3);
            return (prefijo == "ESS" || prefijo == "SIS") && codigo[3] == '-' && codigo[8] == '-';
        }

        static int BuscarPorCodigo(string codigo)
        {
            for (int i = 0; i < total; i++)
                if (codigos[i] == codigo) return i;
            return -1;
        }

        static void MostrarExpediente(int pos)
        {
            Console.WriteLine("\n  Código   : " + codigos[pos]);
            Console.WriteLine("  Paciente : " + nombres[pos] + " (DNI " + dnis[pos] + ")");
            Console.WriteLine("  Teléfono : " + telefonos[pos]); 
            Console.WriteLine("  Seguro   : " + seguros[pos]);
            Console.WriteLine("  Trámite  : " + tramites[pos]);
            Console.WriteLine("  Fecha    : " + fechas[pos]);
            Console.WriteLine("  Estado   : " + estados[pos]);
        }

        static void ConsultarPorCodigo()
        {
            Console.WriteLine("\n--- CONSULTAR POR CÓDIGO ---");
            string codigo = LeerTexto("Código (ej. ESS-2026-0001): ").ToUpper();
            if (!FormatoCodigoValido(codigo))
            {
                Console.WriteLine("  Formato inválido. Use PREFIJO-AÑO-0000 (ESS o SIS).");
                return;
            }
            int pos = BuscarPorCodigo(codigo);
            if (pos == -1) Console.WriteLine("  No se encontró ningún expediente con ese código.");
            else MostrarExpediente(pos);
        }

        static void BuscarPorNombre()
        {
            Console.WriteLine("\n--- BUSCAR POR NOMBRE ---");
            string texto = LeerTexto("Nombre o apellido a buscar: ").ToUpper();
            int encontrados = 0;
            for (int i = 0; i < total; i++)
            {
                if (nombres[i].IndexOf(texto) >= 0)
                {
                    MostrarExpediente(i);
                    encontrados++;
                }
            }
            Console.WriteLine($"\n  Coincidencias encontradas: {encontrados}");
        }

        static void ListarExpedientes()
        {
            Console.WriteLine("\n--- LISTADO DE EXPEDIENTES ---");
            if (total == 0)
            {
                Console.WriteLine("  No hay expedientes registrados.");
                return;
            }
            Console.WriteLine("  " + "CÓDIGO".PadRight(15) + "PACIENTE".PadRight(28) + "TELÉFONO".PadRight(12) + "SEGURO".PadRight(9) + "TRÁMITE");
            for (int i = 0; i < total; i++)
                Console.WriteLine("  " + codigos[i].PadRight(15) + nombres[i].PadRight(28) + telefonos[i].PadRight(12) + seguros[i].PadRight(9) + tramites[i]);
        }
        //  ORDENAMIENTO 
        static void Intercambiar(string[] arreglo, int a, int b)
        {
            string temp = arreglo[a];
            arreglo[a] = arreglo[b];
            arreglo[b] = temp;
        }

        static void IntercambiarRegistros(int a, int b)
        {
            Intercambiar(codigos, a, b);
            Intercambiar(nombres, a, b);
            Intercambiar(dnis, a, b);
            Intercambiar(telefonos, a, b);
            Intercambiar(seguros, a, b);
            Intercambiar(tramites, a, b);
            Intercambiar(fechas, a, b);
            Intercambiar(estados, a, b);
        }

        static void OrdenarPorCodigo()
        {
            for (int i = 0; i < total - 1; i++)
                for (int j = 0; j < total - 1 - i; j++)
                    if (string.CompareOrdinal(codigos[j], codigos[j + 1]) > 0)
                        IntercambiarRegistros(j, j + 1);
            Console.WriteLine("\n  Expedientes ordenados por código (burbuja).");
            ListarExpedientes();
        }

        static void OrdenarPorNombre()
        {
            for (int i = 0; i < total - 1; i++)
            {
                int menor = i;
                for (int j = i + 1; j < total; j++)
                    if (string.CompareOrdinal(nombres[j], nombres[menor]) < 0) menor = j;
                if (menor != i) IntercambiarRegistros(i, menor);
            }
            Console.WriteLine("\n  Expedientes ordenados por nombre (selección).");
            ListarExpedientes();
        }

    }
}