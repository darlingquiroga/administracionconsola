using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

class HotelSystem
{
    static string connectionString = "Server=localhost;Database=hotel_db;User ID=root;Password=root;";

    
    static MySqlConnection ConectarBD()
    {
        return new MySqlConnection(connectionString);
    }

   
    static bool ValidarNombreApellido(string nombre)
    {
        return Regex.IsMatch(nombre, @"^[A-Za-záéíóúÁÉÍÓÚñÑ' ]{3,}$");
    }

    static bool ValidarDocumento(string documento)
    {
        return documento.All(char.IsDigit) && documento.Length >= 5 && documento.Length <= 10;
    }

    static bool ValidarTelefono(string telefono)
    {
        return telefono.All(char.IsDigit) && telefono.Length >= 5 && telefono.Length <= 10;
    }

    static bool ValidarFechaNacimiento(string fechaStr)
    {
        DateTime fecha;
        return DateTime.TryParseExact(fechaStr, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out fecha) && fecha <= DateTime.Now;
    }

    static bool ValidarFechaCheckinCheckout(string fechaStr)
    {
        DateTime fecha;
        return DateTime.TryParseExact(fechaStr, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out fecha) && fecha > DateTime.Now;
    }

    
    static decimal ObtenerPrecio(string tipoHabitacion)
    {
        switch (tipoHabitacion.ToUpper())
        {
            case "SIMPLE": return 10000;
            case "DOBLE": return 18000;
            case "SUITE": return 25000;
            default: return 0;
        }
    }

    
    static void MenuHabitaciones()
    {
        while (true)
        {
            Console.WriteLine("\n--- MENÚ GESTIÓN DE HABITACIONES ---");
            Console.WriteLine("1. Consultar Habitaciones por Número");
            Console.WriteLine("2. Consultar Habitaciones por Tipo");
            Console.WriteLine("3. Consultar Habitaciones por Estado");
            Console.WriteLine("4. Consultar Habitaciones con Ordenamiento");
            Console.WriteLine("5. Volver al Menú Principal");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    ConsultarHabitacionesPorNumero();
                    break;
                case "2":
                    ConsultarHabitacionesPorTipo();
                    break;
                case "3":
                    ConsultarHabitacionesPorEstado();
                    break;
                case "4":
                    ConsultarHabitacionesConOrdenamiento();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Opción no válida, intente nuevamente.");
                    break;
            }
        }
    }

 
    static void ConsultarHabitacionesPorNumero()
    {
        Console.Write("Ingrese el número de habitación: ");
        string numeroHabitacion = Console.ReadLine();

        using (var conexion = ConectarBD())
        {
            conexion.Open();
            var query = "SELECT * FROM habitaciones WHERE numero_habitacion = @numeroHabitacion";
            var cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@numeroHabitacion", numeroHabitacion);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Num. Habitación: {reader["numero_habitacion"]}, Tipo: {reader["tipo"]}, Disponible: {reader["disponible"]}");
                }
            }
        }
    }

   
    static void ConsultarHabitacionesPorTipo()
    {
        Console.Write("Ingrese el tipo de habitación (SIMPLE, DOBLE, SUITE): ");
        string tipo = Console.ReadLine().ToUpper();

        using (var conexion = ConectarBD())
        {
            conexion.Open();
            var query = "SELECT * FROM habitaciones WHERE tipo = @tipo";
            var cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@tipo", tipo);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Num. Habitación: {reader["numero_habitacion"]}, Tipo: {reader["tipo"]}, Disponible: {reader["disponible"]}");
                }
            }
        }
    }

    
    static void ConsultarHabitacionesPorEstado()
    {
        Console.Write("Ingrese el estado de la habitación (SI/NO): ");
        string estado = Console.ReadLine().ToUpper();

        using (var conexion = ConectarBD())
        {
            conexion.Open();
            var query = "SELECT * FROM habitaciones WHERE disponible = @estado";
            var cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@estado", estado);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Num. Habitación: {reader["numero_habitacion"]}, Tipo: {reader["tipo"]}, Disponible: {reader["disponible"]}");
                }
            }
        }
    }

    
    static void ConsultarHabitacionesConOrdenamiento()
    {
        Console.WriteLine("\nOpciones de ordenamiento para habitaciones:");
        Console.WriteLine("1. Por número de habitación (ascendente)");
        Console.WriteLine("2. Por tipo de habitación (alfabéticamente)");
        Console.WriteLine("3. Por disponibilidad (disponibles primero)");

        string orden = Console.ReadLine();

        using (var conexion = ConectarBD())
        {
            conexion.Open();
            MySqlCommand cmd = null;

            switch (orden)
            {
                case "1":
                    cmd = new MySqlCommand("SELECT * FROM habitaciones ORDER BY numero_habitacion ASC;", conexion);
                    break;
                case "2":
                    cmd = new MySqlCommand("SELECT * FROM habitaciones ORDER BY tipo ASC;", conexion);
                    break;
                case "3":
                    cmd = new MySqlCommand("SELECT * FROM habitaciones ORDER BY (disponible = 'SI') DESC, tipo ASC;", conexion);
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    return;
            }

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Num. Habitación: {reader["numero_habitacion"]}, Tipo: {reader["tipo"]}, Disponible: {reader["disponible"]}");
                }
            }
        }
    }

    
    static void MenuReservas()
    {
        while (true)
        {
            Console.WriteLine("\n--- MENÚ GESTIÓN DE RESERVAS ---");
            Console.WriteLine("1. Agregar Reserva");
            Console.WriteLine("2. Consultar Reservas");
            Console.WriteLine("3. Modificar Reserva");
            Console.WriteLine("4. Eliminar Reserva");
            Console.WriteLine("5. Volver al Menú Principal");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    AgregarReserva();
                    break;
                case "2":
                    ConsultarReservas();
                    break;
                case "3":
                    ModificarReserva();
                    break;
                case "4":
                    EliminarReserva();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Opción no válida, intente nuevamente.");
                    break;
            }
        }
    }

   
    static void AgregarReserva()
    {
        Console.Write("Ingrese el nombre: ");
        string nombre = Console.ReadLine();
        while (!ValidarNombreApellido(nombre))
        {
            Console.WriteLine("Nombre inválido. Debe contener solo letras y más de dos caracteres.");
            nombre = Console.ReadLine();
        }

        Console.Write("Ingrese el apellido: ");
        string apellido = Console.ReadLine();
        while (!ValidarNombreApellido(apellido))
        {
            Console.WriteLine("Apellido inválido. Debe contener solo letras y más de dos caracteres.");
            apellido = Console.ReadLine();
        }

        Console.Write("Ingrese el número de documento: ");
        string documento = Console.ReadLine();
        while (!ValidarDocumento(documento))
        {
            Console.WriteLine("Documento inválido. Debe ser numérico y tener entre 5 y 10 dígitos.");
            documento = Console.ReadLine();
        }

        Console.Write("Ingrese el número de teléfono: ");
        string telefono = Console.ReadLine();
        while (!ValidarTelefono(telefono))
        {
            Console.WriteLine("Teléfono inválido. Debe ser numérico y tener entre 5 y 10 dígitos.");
            telefono = Console.ReadLine();
        }

        Console.Write("Ingrese la fecha de nacimiento (DD-MM-YYYY): ");
        string fechaNacimiento = Console.ReadLine();
        while (!ValidarFechaNacimiento(fechaNacimiento))
        {
            Console.WriteLine("Fecha de nacimiento inválida. Debe ser en el formato DD-MM-YYYY y no puede ser en el futuro.");
            fechaNacimiento = Console.ReadLine();
        }

        Console.Write("Ingrese la fecha de check-in (DD-MM-YYYY): ");
        string fechaCheckin = Console.ReadLine();
        while (!ValidarFechaCheckinCheckout(fechaCheckin))
        {
            Console.WriteLine("Fecha de check-in inválida. Debe ser en el futuro.");
            fechaCheckin = Console.ReadLine();
        }

        Console.Write("Ingrese la fecha de check-out (DD-MM-YYYY): ");
        string fechaCheckout = Console.ReadLine();
        while (!ValidarFechaCheckinCheckout(fechaCheckout) || DateTime.Parse(fechaCheckout) <= DateTime.Parse(fechaCheckin))
        {
            Console.WriteLine("Fecha de check-out inválida. Debe ser posterior a la fecha de check-in.");
            fechaCheckout = Console.ReadLine();
        }

        Console.Write("Ingrese el tipo de habitación (SIMPLE, DOBLE, SUITE): ");
        string tipoHabitacion = Console.ReadLine().ToUpper();
        while (tipoHabitacion != "SIMPLE" && tipoHabitacion != "DOBLE" && tipoHabitacion != "SUITE")
        {
            Console.WriteLine("Tipo de habitación inválido.");
            tipoHabitacion = Console.ReadLine().ToUpper();
        }

        Console.Write("¿Desea continuar con la reserva? (S/N): ");
        string confirmacion = Console.ReadLine().ToUpper();
        if (confirmacion == "N")
        {
            Console.WriteLine("Reserva cancelada.");
            return;
        }

        using (var conexion = ConectarBD())
        {
            conexion.Open();
            var precio = ObtenerPrecio(tipoHabitacion);

            
            var query = "SELECT numero_habitacion FROM habitaciones WHERE tipo = @tipo AND disponible = 'SI' LIMIT 1";
            var cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@tipo", tipoHabitacion);

            var habitacionDisponible = cmd.ExecuteScalar();
            if (habitacionDisponible != null)
            {
                string numeroHabitacion = habitacionDisponible.ToString();
                var insertQuery = "INSERT INTO reservas (nombre, apellido, documento, fecha_nacimiento, telefono, fecha_checkin, fecha_checkout, numero_habitacion, precio, estado) " +
                                  "VALUES (@nombre, @apellido, @documento, @fechaNacimiento, @telefono, @fechaCheckin, @fechaCheckout, @numeroHabitacion, @precio, 'ACTIVA')";
                var insertCmd = new MySqlCommand(insertQuery, conexion);
                insertCmd.Parameters.AddWithValue("@nombre", nombre);
                insertCmd.Parameters.AddWithValue("@apellido", apellido);
                insertCmd.Parameters.AddWithValue("@documento", documento);
                insertCmd.Parameters.AddWithValue("@fechaNacimiento", DateTime.ParseExact(fechaNacimiento, "dd-MM-yyyy", null).ToString("yyyy-MM-dd"));
                insertCmd.Parameters.AddWithValue("@telefono", telefono);
                insertCmd.Parameters.AddWithValue("@fechaCheckin", DateTime.ParseExact(fechaCheckin, "dd-MM-yyyy", null).ToString("yyyy-MM-dd"));
                insertCmd.Parameters.AddWithValue("@fechaCheckout", DateTime.ParseExact(fechaCheckout, "dd-MM-yyyy", null).ToString("yyyy-MM-dd"));
                insertCmd.Parameters.AddWithValue("@numeroHabitacion", numeroHabitacion);
                insertCmd.Parameters.AddWithValue("@precio", precio);

   
                insertCmd.ExecuteNonQuery();

                
                var updateQuery = "UPDATE habitaciones SET disponible = 'NO' WHERE numero_habitacion = @numeroHabitacion";
                var updateCmd = new MySqlCommand(updateQuery, conexion);
                updateCmd.Parameters.AddWithValue("@numeroHabitacion", numeroHabitacion);
                updateCmd.ExecuteNonQuery();

                Console.WriteLine($"Reserva realizada con éxito para {nombre} {apellido} en la habitación {numeroHabitacion}.");
            }
            else
            {
                Console.WriteLine("No hay habitaciones disponibles para el tipo seleccionado.");
            }
        }
    }

    
    static void ConsultarReservas()
    {
        using (var conexion = ConectarBD())
        {
            conexion.Open();
            Console.WriteLine("¿Cómo desea ordenar las reservas? (nombre, apellido, fecha_checkin, fecha_checkout): ");
            string orden = Console.ReadLine().ToLower();

            if (orden == "nombre" || orden == "apellido" || orden == "fecha_checkin" || orden == "fecha_checkout")
            {
                var query = $"SELECT * FROM reservas WHERE estado = 'ACTIVA' ORDER BY {orden}";
                var cmd = new MySqlCommand(query, conexion);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id_reserva"]}, Nombre: {reader["nombre"]} {reader["apellido"]}, Documento: {reader["documento"]}, " +
                                          $"Fecha Check-in: {reader["fecha_checkin"]}, Fecha Check-out: {reader["fecha_checkout"]}, Habitación: {reader["numero_habitacion"]}, Precio: {reader["precio"]}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Opción de ordenamiento inválida.");
            }
        }
    }

    
    static void ModificarReserva()
    {
        Console.Write("Ingrese el ID de la reserva a modificar: ");
        string idReserva = Console.ReadLine();

        using (var conexion = ConectarBD())
        {
            conexion.Open();
            var query = "SELECT * FROM reservas WHERE id_reserva = @idReserva";
            var cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@idReserva", idReserva);
            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Console.WriteLine($"Reserva encontrada: {reader["id_reserva"]}, Nombre: {reader["nombre"]} {reader["apellido"]}");

                
                reader.Close();

                string fechaCheckin = "";
                string fechaCheckout = "";
                bool fechasValidas = false;

                while (!fechasValidas)
                {
                    Console.Write("Ingrese la nueva fecha de check-in (DD-MM-YYYY): ");
                    fechaCheckin = Console.ReadLine();
                    fechasValidas = ValidarFechaCheckinCheckout(fechaCheckin);
                    if (!fechasValidas)
                    {
                        Console.WriteLine("Fecha de check-in inválida.");
                    }
                }

                fechasValidas = false;
                while (!fechasValidas)
                {
                    Console.Write("Ingrese la nueva fecha de check-out (DD-MM-YYYY): ");
                    fechaCheckout = Console.ReadLine();
                    if (DateTime.Parse(fechaCheckout) <= DateTime.Parse(fechaCheckin))
                    {
                        Console.WriteLine("Fecha de check-out inválida. Debe ser posterior a la fecha de check-in.");
                    }
                    else
                    {
                        fechasValidas = true;
                    }
                }

                var updateQuery = "UPDATE reservas SET fecha_checkin = @fechaCheckin, fecha_checkout = @fechaCheckout WHERE id_reserva = @idReserva";
                var updateCmd = new MySqlCommand(updateQuery, conexion);
                updateCmd.Parameters.AddWithValue("@fechaCheckin", DateTime.ParseExact(fechaCheckin, "dd-MM-yyyy", null).ToString("yyyy-MM-dd"));
                updateCmd.Parameters.AddWithValue("@fechaCheckout", DateTime.ParseExact(fechaCheckout, "dd-MM-yyyy", null).ToString("yyyy-MM-dd"));
                updateCmd.Parameters.AddWithValue("@idReserva", idReserva);

                updateCmd.ExecuteNonQuery();
                Console.WriteLine("Reserva modificada con éxito.");
            }
            else
            {
                Console.WriteLine("Reserva no encontrada.");
            }
        }
    }
    static void EliminarReserva()
    {
        Console.Write("Ingrese el ID de la reserva a eliminar (poner inactiva): ");
        string idReserva = Console.ReadLine();

        using (var conexion = ConectarBD())
        {
            conexion.Open();
            var query = "SELECT * FROM reservas WHERE id_reserva = @idReserva";
            var cmd = new MySqlCommand(query, conexion);
            cmd.Parameters.AddWithValue("@idReserva", idReserva);
            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Console.WriteLine($"Reserva encontrada: {reader["id_reserva"]}, Nombre: {reader["nombre"]} {reader["apellido"]}");
                reader.Close();

                Console.Write("¿Está seguro que desea poner inactiva esta reserva? (S/N): ");
                string confirmacion = Console.ReadLine().ToUpper();

                if (confirmacion == "S")
                {
                    var updateQuery = "UPDATE reservas SET estado = 'INACTIVA' WHERE id_reserva = @idReserva";
                    var updateCmd = new MySqlCommand(updateQuery, conexion);
                    updateCmd.Parameters.AddWithValue("@idReserva", idReserva);
                    updateCmd.ExecuteNonQuery();

                    Console.WriteLine("Reserva marcada como inactiva.");
                }
                else
                {
                    Console.WriteLine("Eliminación cancelada.");
                }
            }
            else
            {
                Console.WriteLine("Reserva no encontrada.");
            }
        }
    }

    // Menú principal
    static void MenuPrincipal()
    {
        while (true)
        {
            Console.WriteLine("\n--- MENÚ PRINCIPAL ---");
            Console.WriteLine("1. Gestión de Reservas");
            Console.WriteLine("2. Gestión de Habitaciones");
            Console.WriteLine("3. Salir");
            string opcion = Console.ReadLine();

            if (opcion == "1")
            {
                MenuReservas();
            }
            else if (opcion == "2")
            {
                MenuHabitaciones();
            }
            else if (opcion == "3")
            {
                Console.WriteLine("Saliendo del sistema...");
                break;
            }
            else
            {
                Console.WriteLine("Opción no válida, intente nuevamente.");
            }
        }
    }

    static void Main(string[] args)
    {
        MenuPrincipal();
    }
}

