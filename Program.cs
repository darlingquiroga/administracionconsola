using MySql.Data.MySqlClient;
using System;
using System.Text.RegularExpressions;

namespace AdministracionHotel
{
    class Program
    {
        private static string connectionString = "Server=localhost;username=root;password=root;database=hoteldb";

        static void Main(string[] args)
        {
            MenuPrincipal();
        }

        static void MenuPrincipal()
        {
            string opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("Bienvenido al sistema de gestión del hotel.");
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Gestión de Usuarios");
                Console.WriteLine("2. Gestión de Reservas");
                Console.WriteLine("3. Gestión de Habitaciones");
                Console.WriteLine("4. Salir");

                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        GestionUsuarios();
                        break;
                    case "2":
                        GestionReservas();
                        break;
                    case "3":
                        GestionHabitaciones();
                        break;
                    case "4":
                        Console.WriteLine("Saliendo...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }

            } while (opcion != "4");
        }

        // Menú de Gestión de Usuarios
        static void GestionUsuarios()
        {
            string opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("Gestión de Usuarios");
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Agregar Usuario");
                Console.WriteLine("2. Modificar Usuario");
                Console.WriteLine("3. Consultar Usuarios");
                Console.WriteLine("4. Eliminar Usuario");
                Console.WriteLine("5. Volver al Menú Principal");

                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarUsuario();
                        break;
                    case "2":
                        ModificarUsuario();
                        break;
                    case "3":
                        ConsultarUsuarios();
                        break;
                    case "4":
                        EliminarUsuario();
                        break;
                    case "5":
                        Console.WriteLine("Volver al menú principal...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }

            } while (opcion != "5");
        }
        static void AgregarUsuario()
        {
            string nombre = ObtenerDato("Nombre del usuario");
            string apellido = ObtenerDato("Apellido del usuario");
            string documento = ObtenerDocumento();
            DateTime fechaNacimiento = ObtenerFechaNacimiento();
            string telefono = ObtenerTelefono();

            string query = "INSERT INTO Usuarios (nombre, apellido, numero_documento, fecha_nacimiento, telefono, estado) " +
                           "VALUES (@nombre, @apellido, @documento, @fecha_nacimiento, @telefono, 'activo')";

            try
            {
                using (var conexion = Conectar())
                {
                    if (conexion == null)
                    {
                        Console.WriteLine("No se pudo establecer la conexión con la base de datos.");
                        return;
                    }

                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@apellido", apellido);
                    cmd.Parameters.AddWithValue("@documento", documento);
                    cmd.Parameters.AddWithValue("@fecha_nacimiento", fechaNacimiento);
                    cmd.Parameters.AddWithValue("@telefono", telefono);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        Console.WriteLine("Usuario agregado con éxito.");
                        // Pedimos una confirmación al usuario para asegurarnos de que se agregó correctamente
                        Console.WriteLine("¿Desea ver los detalles del usuario agregado? (S/N): ");
                        string confirmacion = Console.ReadLine()?.ToUpper();
                        if (confirmacion == "S")
                        {
                            Console.WriteLine("Detalles del usuario:");
                            Console.WriteLine($"Nombre: {nombre} {apellido}");
                            Console.WriteLine($"Documento: {documento}");
                            Console.WriteLine($"Fecha de Nacimiento: {fechaNacimiento.ToString("dd/MM/yyyy")}");
                            Console.WriteLine($"Teléfono: {telefono}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se pudo agregar el usuario.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar usuario: " + ex.Message);
            }
        }


        static string ObtenerDato(string mensaje)
        {
            string dato;
            while (true)
            {
                Console.Write($"Ingrese {mensaje}: ");
                dato = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(dato) && dato.Length > 2 && Regex.IsMatch(dato, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+(?:[\s']+[a-zA-ZáéíóúÁÉÍÓÚñÑ]+)*$"))
                    break;
                else
                    Console.WriteLine("Error: El nombre o apellido debe tener más de 2 caracteres y solo contener letras. ");
            }
            return dato;
        }

        static string ObtenerDocumento()
        {
            string documento;
            while (true)
            {
                Console.Write("Ingrese el documento (solo números): ");
                documento = Console.ReadLine();
                if (Regex.IsMatch(documento, @"^\d{5,10}$") && documento.Length > 5)
                    break;
                else
                    Console.WriteLine("El documento debe contener solo números y tener entre 5 y 10 Dígitos.");
            }
            return documento;
        }
        static string ObtenerTelefono()
        {
            string telefono;
            while (true)
            {
                Console.Write("Ingrese el teléfono (solo números): ");
                telefono = Console.ReadLine();
                if (Regex.IsMatch(telefono, @"^\d{5,15}$") && telefono.Length > 5)
                    break;
                else
                    Console.WriteLine("El telefono debe contener solo números y tener entre 5 y 15 Dígitos.");
            }
            return telefono;
        }
        static string Obtenerid(string mensaje)
        {
            string id;
            while (true)
            {
                Console.Write($"{mensaje}: ");  // Mostrar el mensaje personalizado al usuario
                id = Console.ReadLine();
                if (Regex.IsMatch(id, @"^\d{1,3}$") && id.Length > 0)  // Permitir hasta 3 dígitos
                    break;
                else
                    Console.WriteLine("El ID debe contener solo números y hasta 3 dígitos.");
            }
            return id;
        }

        static DateTime ObtenerFechaNacimiento()
        {
            DateTime fechaNacimiento;
            while (true)
            {
                Console.Write("Ingrese la fecha de nacimiento (dd/MM/yyyy): ");
                if (DateTime.TryParse(Console.ReadLine(), out fechaNacimiento))
                {
                    if (fechaNacimiento <= DateTime.Now)
                        break;
                    else
                        Console.WriteLine("La fecha no puede ser en el futuro.");
                }
                else
                    Console.WriteLine("Fecha inválida.");
            }
            return fechaNacimiento;
        }

        static void ModificarUsuario()
        {
            // Pedir el ID del usuario a modificar
            string idUsuario = Obtenerid("ID del usuario a modificar");  // Pasar el mensaje como argumento

            // Consultar los datos actuales del usuario
            string query = "SELECT id_usuario, nombre, apellido, numero_documento, telefono FROM Usuarios WHERE id_usuario = @idUsuario";

            using (var conexion = Conectar())
            {
                if (conexion == null)
                {
                    Console.WriteLine("No se pudo establecer la conexión con la base de datos.");
                    return;
                }

                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Mostrar los datos actuales del usuario
                        Console.WriteLine("\nDatos actuales del usuario:");
                        Console.WriteLine($"ID: {reader["id_usuario"]}, Nombre: {reader["nombre"]}, Apellido: {reader["apellido"]}, Documento: {reader["numero_documento"]}, Teléfono: {reader["telefono"]}");

                        // Cerrar el lector después de mostrar los datos
                        reader.Close();

                        // Pedir los nuevos datos, permitiendo al usuario dejar en blanco el campo que no desea modificar
                        string nuevoNombre = ObtenerNuevoValorODejarPorDefecto("Nuevo nombre (deje en blanco para no modificar)", reader["nombre"].ToString());
                        string nuevoApellido = ObtenerNuevoValorODejarPorDefecto("Nuevo apellido (deje en blanco para no modificar)", reader["apellido"].ToString());
                        string nuevoDocumento = ObtenerNuevoValorODejarPorDefecto("Nuevo documento (deje en blanco para no modificar)", reader["numero_documento"].ToString());
                        string nuevoTelefono = ObtenerNuevoValorODejarPorDefecto("Nuevo teléfono (deje en blanco para no modificar)", reader["telefono"].ToString());

                        // Mostrar los datos que se van a actualizar para depuración
                        Console.WriteLine("Datos a modificar:");
                        Console.WriteLine($"Nuevo Nombre: {nuevoNombre}");
                        Console.WriteLine($"Nuevo Apellido: {nuevoApellido}");
                        Console.WriteLine($"Nuevo Documento: {nuevoDocumento}");
                        Console.WriteLine($"Nuevo Teléfono: {nuevoTelefono}");

                        // Verificar si realmente hay algún cambio
                        bool hayCambio = nuevoNombre != reader["nombre"].ToString() ||
                                         nuevoApellido != reader["apellido"].ToString() ||
                                         nuevoDocumento != reader["numero_documento"].ToString() ||
                                         nuevoTelefono != reader["telefono"].ToString();

                        if (!hayCambio)
                        {
                            Console.WriteLine("No se detectaron cambios. No se realizará ninguna modificación.");
                            return;  // Si no hubo cambios, salir
                        }

                        // Realizar la actualización en la base de datos
                        string updateQuery = "UPDATE Usuarios SET nombre = @nombre, apellido = @apellido, numero_documento = @documento, telefono = @telefono WHERE id_usuario = @idUsuario";

                        // Preparar el comando de actualización
                        MySqlCommand updateCmd = new MySqlCommand(updateQuery, conexion);
                        updateCmd.Parameters.AddWithValue("@nombre", string.IsNullOrEmpty(nuevoNombre) ? reader["nombre"].ToString() : nuevoNombre);
                        updateCmd.Parameters.AddWithValue("@apellido", string.IsNullOrEmpty(nuevoApellido) ? reader["apellido"].ToString() : nuevoApellido);
                        updateCmd.Parameters.AddWithValue("@documento", string.IsNullOrEmpty(nuevoDocumento) ? reader["numero_documento"].ToString() : nuevoDocumento);
                        updateCmd.Parameters.AddWithValue("@telefono", string.IsNullOrEmpty(nuevoTelefono) ? reader["telefono"].ToString() : nuevoTelefono);
                        updateCmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                        // Ejecutar la actualización
                        int filasAfectadas = updateCmd.ExecuteNonQuery();

                        // Verificar si se actualizó correctamente
                        if (filasAfectadas > 0)
                        {
                            Console.WriteLine("Usuario modificado con éxito.");
                        }
                        else
                        {
                            Console.WriteLine("No se pudo modificar el usuario. Verifique que los datos no sean iguales.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se encontró un usuario con ese ID.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al modificar el usuario: " + ex.Message);
                    Console.WriteLine("Detalles de la excepción: " + ex.StackTrace);  // Mostrar detalles completos de la excepción
                }
            }

            Console.WriteLine("Presione Enter para volver al menú principal...");
            Console.ReadLine(); // Esperar que el usuario presione Enter
        }






        // Función para obtener datos de entrada, pero con un valor por defecto
        static string ObtenerNuevoValorODejarPorDefecto(string mensaje, string valorActual)
        {
            Console.Write($"{mensaje} (actual: {valorActual}): ");
            string nuevoValor = Console.ReadLine();
            return string.IsNullOrEmpty(nuevoValor) ? valorActual : nuevoValor;
        }

        static void ConsultarUsuarios()
        {
            string query = "SELECT id_usuario, nombre, apellido, numero_documento, telefono FROM Usuarios";
            using (var conexion = Conectar())
            {
                if (conexion == null)
                {
                    Console.WriteLine("No se pudo establecer la conexión con la base de datos.");
                    return;
                }

                MySqlCommand cmd = new MySqlCommand(query, conexion);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["id_usuario"]}, Nombre: {reader["nombre"]}, Apellido: {reader["apellido"]}, Documento: {reader["numero_documento"]}, Teléfono: {reader["telefono"]}");
                }
            }
            Console.ReadKey();
        }

        static void EliminarUsuario()
        {
            int idUsuario = int.Parse(Obtenerid("ID del usuario a eliminar"));

            string query = "DELETE FROM Usuarios WHERE id_usuario = @idUsuario";
            try
            {
                using (var conexion = Conectar())
                {
                    if (conexion == null)
                    {
                        Console.WriteLine("No se pudo establecer la conexión con la base de datos.");
                        return;
                    }

                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        Console.WriteLine("Usuario eliminado con éxito.");
                    }
                    else
                    {
                        Console.WriteLine("No se pudo eliminar el usuario.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar usuario: " + ex.Message);
            }
        }

        static MySqlConnection Conectar()
        {
            try
            {
                var conexion = new MySqlConnection(connectionString);
                conexion.Open();
                Console.WriteLine("Conexión exitosa a la base de datos.");
                return conexion;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error de conexión: {ex.Message}");
                return null;
            }
        }


        // Menú de Gestión de Reservas
        static void GestionReservas()
        {
            string opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("Gestión de Reservas");
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Modificar Reserva");
                Console.WriteLine("2. Consultar Reservas");
                Console.WriteLine("3. Eliminar Reserva");
                Console.WriteLine("4. Volver al Menú Principal");

                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        ModificarReserva();
                        break;
                    case "2":
                        ConsultarReservas();
                        break;
                    case "3":
                        EliminarReserva();
                        break;
                    case "4":
                        Console.WriteLine("Volver al menú principal...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }

            } while (opcion != "4");
        }
        static void ModificarReserva()
{
    // Pedir el ID de la reserva a modificar
    int idReserva = int.Parse(ObtenerDato("ID de la reserva a modificar"));

    // Consultar los datos actuales de la reserva
    string query = "SELECT id_reserva, id_usuario, id_habitacion, fecha_reserva, fecha_checkin, fecha_checkout, tipo_habitacion, total, estado " +
                   "FROM reservas WHERE id_reserva = @idReserva";

    using (var conexion = Conectar())
    {
        if (conexion == null)
        {
            Console.WriteLine("No se pudo establecer la conexión con la base de datos.");
            return;
        }

        MySqlCommand cmd = new MySqlCommand(query, conexion);
        cmd.Parameters.AddWithValue("@idReserva", idReserva);
        MySqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            // Mostrar los datos actuales de la reserva
            Console.WriteLine("\nDatos actuales de la reserva:");
            Console.WriteLine($"ID Reserva: {reader["id_reserva"]}, Usuario ID: {reader["id_usuario"]}, Habitacion ID: {reader["id_habitacion"]}, " +
                              $"Fecha Reserva: {reader["fecha_reserva"]}, Fecha Check-In: {reader["fecha_checkin"]}, Fecha Check-Out: {reader["fecha_checkout"]}, " +
                              $"Tipo de Habitación: {reader["tipo_habitacion"]}, Total: {reader["total"]}, Estado: {reader["estado"]}");

            // Cerrar el lector después de mostrar los datos
            reader.Close();

            // Pedir los nuevos datos para modificar, permitiendo al usuario dejar en blanco el campo que no desea modificar
            string nuevaFechaCheckin = ObtenerDatoConValorPorDefecto("Nueva fecha de Check-In (deje en blanco para no modificar)", reader["fecha_checkin"].ToString());
            string nuevaFechaCheckout = ObtenerDatoConValorPorDefecto("Nueva fecha de Check-Out (deje en blanco para no modificar)", reader["fecha_checkout"].ToString());
            string nuevoEstado = ObtenerDatoConValorPorDefecto("Nuevo estado (deje en blanco para no modificar)", reader["estado"].ToString());

            // También puedes permitir modificar los otros campos si es necesario, como el tipo de habitación o el total
            string nuevoTipoHabitacion = ObtenerDatoConValorPorDefecto("Nuevo tipo de habitación (deje en blanco para no modificar)", reader["tipo_habitacion"].ToString());
            string nuevoTotal = ObtenerDatoConValorPorDefecto("Nuevo total (deje en blanco para no modificar)", reader["total"].ToString());

            // Realizar la actualización en la base de datos
            string updateQuery = "UPDATE reservas SET fecha_checkin = @fecha_checkin, fecha_checkout = @fecha_checkout, " +
                                 "estado = @estado, tipo_habitacion = @tipo_habitacion, total = @total WHERE id_reserva = @idReserva";

            try
            {
                MySqlCommand updateCmd = new MySqlCommand(updateQuery, conexion);
                updateCmd.Parameters.AddWithValue("@fecha_checkin", string.IsNullOrEmpty(nuevaFechaCheckin) ? reader["fecha_checkin"].ToString() : nuevaFechaCheckin);
                updateCmd.Parameters.AddWithValue("@fecha_checkout", string.IsNullOrEmpty(nuevaFechaCheckout) ? reader["fecha_checkout"].ToString() : nuevaFechaCheckout);
                updateCmd.Parameters.AddWithValue("@estado", string.IsNullOrEmpty(nuevoEstado) ? reader["estado"].ToString() : nuevoEstado);
                updateCmd.Parameters.AddWithValue("@tipo_habitacion", string.IsNullOrEmpty(nuevoTipoHabitacion) ? reader["tipo_habitacion"].ToString() : nuevoTipoHabitacion);
                updateCmd.Parameters.AddWithValue("@total", string.IsNullOrEmpty(nuevoTotal) ? reader["total"].ToString() : nuevoTotal);
                updateCmd.Parameters.AddWithValue("@idReserva", idReserva);

                int filasAfectadas = updateCmd.ExecuteNonQuery();
                if (filasAfectadas > 0)
                {
                    Console.WriteLine("Reserva modificada con éxito.");
                }
                else
                {
                    Console.WriteLine("No se pudo modificar la reserva.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar la reserva: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("No se encontró una reserva con ese ID.");
        }
    }
    Console.ReadKey();
}
        static void ConsultarReservas()
        {
            // Consulta SQL para obtener todas las reservas
            string query = "SELECT id_reserva, id_usuario, id_habitacion, fecha_reserva, fecha_checkin, fecha_checkout, tipo_habitacion, total, estado " +
                           "FROM reservas";

            using (var conexion = Conectar())
            {
                if (conexion == null)
                {
                    Console.WriteLine("No se pudo establecer la conexión con la base de datos.");
                    return;
                }

                MySqlCommand cmd = new MySqlCommand(query, conexion);
                MySqlDataReader reader = cmd.ExecuteReader();

                // Verificar si hay registros
                if (reader.HasRows)
                {
                    Console.WriteLine("\nConsultando las reservas...\n");

                    // Imprimir los encabezados de las columnas
                    Console.WriteLine($"{"ID Reserva",-15}{"ID Usuario",-15}{"ID Habitacion",-15}{"Fecha Reserva",-20}{"Fecha Check-In",-20}{"Fecha Check-Out",-20}{"Tipo Habitacion",-20}{"Total",-10}{"Estado"}");

                    // Leer e imprimir cada fila de resultados
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["id_reserva"],-15}{reader["id_usuario"],-15}{reader["id_habitacion"],-15}{reader["fecha_reserva"],-20}{reader["fecha_checkin"],-20}{reader["fecha_checkout"],-20}{reader["tipo_habitacion"],-20}{reader["total"],-10}{reader["estado"]}");
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron reservas.");
                }

                reader.Close(); // Cerrar el reader
            }
            Console.ReadKey(); // Esperar a que el usuario presione una tecla antes de continuar
        }

        // Función para obtener datos de entrada, pero con un valor por defecto
        static string ObtenerDatoConValorPorDefecto(string mensaje, string valorActual)
{
    Console.Write($"{mensaje} (actual: {valorActual}): ");
    string nuevoValor = Console.ReadLine();
    return string.IsNullOrEmpty(nuevoValor) ? valorActual : nuevoValor;
}
        static void EliminarReserva()
{
    Console.Clear();
    Console.WriteLine("Eliminar Reserva");

    // Solicitar el ID de la reserva que se desea eliminar
    int idReserva = ObtenerIdReserva();

    // Confirmar con el usuario antes de proceder
    Console.WriteLine("¿Está seguro de que desea eliminar esta reserva? (S/N): ");
    string confirmacion = Console.ReadLine().ToUpper();

    if (confirmacion == "S")
    {
        // Proceder a eliminar la reserva
        string query = "DELETE FROM reservas WHERE id_reserva = @id_reserva";

        using (var conexion = Conectar())
        {
            if (conexion == null)
            {
                Console.WriteLine("No se pudo establecer la conexión con la base de datos.");
                return;
            }

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id_reserva", idReserva);

                // Ejecutar la consulta
                int filasAfectadas = cmd.ExecuteNonQuery();

                if (filasAfectadas > 0)
                {
                    Console.WriteLine("Reserva eliminada con éxito.");
                }
                else
                {
                    Console.WriteLine("No se encontró ninguna reserva con ese ID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al intentar eliminar la reserva: " + ex.Message);
            }
        }
    }
    else
    {
        Console.WriteLine("Operación cancelada.");
    }

    Console.ReadKey(); // Esperar a que el usuario presione una tecla
}

private static int ObtenerIdReserva()
{
    int idReserva;

    while (true)
    {
        Console.Write("Ingrese el ID de la reserva que desea eliminar: ");
        string input = Console.ReadLine();

        // Validar si la entrada es un número válido
        if (int.TryParse(input, out idReserva) && idReserva > 0)
        {
            return idReserva;
        }
        else
        {
            Console.WriteLine("Por favor, ingrese un ID válido (un número entero positivo).");
        }
    }
}


        // Menú de Gestión de Habitaciones
        static void GestionHabitaciones()
        {
            string opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("Gestión de Habitaciones");
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Consultar por N° de Habitación");
                Console.WriteLine("2. Consultar por Tipo de Habitación");
                Console.WriteLine("3. Consultar por Disponibilidad");
                Console.WriteLine("4. Volver al Menú Principal");

                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        ConsultarNumeroHabitacion();
                        break;
                    case "2":
                        ConsultarTipoHabitacion();
                        break;
                    case "3":
                        ConsultarDisponibilidad();
                        break;
                    case "4":
                        Console.WriteLine("Volver al menú principal...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }

            } while (opcion != "4");
        }

       
        // Métodos de consulta y modificación de reservas y habitaciones se agregarían de forma similar...
        static void ConsultarNumeroHabitacion()
        {
            Console.Clear();
            Console.WriteLine("Consultar Habitaciones por Número");

            // Solicitar el número de habitación
            int numeroHabitacion = ObtenerNumeroHabitacion();

            // Consulta SQL para obtener la información de la habitación
            string query = "SELECT id_habitacion, numero_habitacion, tipo_habitacion, precio, disponibilidad " +
                           "FROM Habitaciones WHERE numero_habitacion = @numero_habitacion";

            using (var conexion = Conectar())
            {
                if (conexion == null)
                {
                    Console.WriteLine("No se pudo establecer la conexión con la base de datos.");
                    return;
                }

                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@numero_habitacion", numeroHabitacion);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    // Verificar si se encontró alguna habitación
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["id_habitacion"]}, " +
                                              $"Número de Habitación: {reader["numero_habitacion"]}, " +
                                              $"Tipo de Habitación: {reader["tipo_habitacion"]}, " +
                                              $"Precio: {reader["precio"]}, " +
                                              $"Disponibilidad: {reader["disponibilidad"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se encontró ninguna habitación con ese número.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar las habitaciones: " + ex.Message);
                }
            }
            Console.ReadKey();  // Esperar a que el usuario presione una tecla
        }

        // Método para obtener el número de habitación
        static int ObtenerNumeroHabitacion()
        {
            int numeroHabitacion;
            while (true)
            {
                Console.Write("Ingrese el número de habitación: ");
                if (int.TryParse(Console.ReadLine(), out numeroHabitacion) && numeroHabitacion > 0)
                    break;
                else
                    Console.WriteLine("Número inválido. Debe ser un número mayor a 0.");
            }
            return numeroHabitacion;
        }
        static void ConsultarTipoHabitacion()
        {
            Console.Clear();
            Console.WriteLine("Consultar Habitaciones por Tipo");

            // Solicitar el tipo de habitación
            string tipoHabitacion = ObtenerTipoHabitacion();

            // Consulta SQL para obtener las habitaciones por tipo
            string query = "SELECT id_habitacion, numero_habitacion, tipo_habitacion, precio, disponibilidad " +
                           "FROM Habitaciones WHERE tipo_habitacion = @tipo_habitacion";

            using (var conexion = Conectar())
            {
                if (conexion == null)
                {
                    Console.WriteLine("No se pudo establecer la conexión con la base de datos.");
                    return;
                }

                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@tipo_habitacion", tipoHabitacion);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    // Verificar si se encontraron habitaciones del tipo solicitado
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["id_habitacion"]}, " +
                                              $"Número de Habitación: {reader["numero_habitacion"]}, " +
                                              $"Tipo de Habitación: {reader["tipo_habitacion"]}, " +
                                              $"Precio: {reader["precio"]}, " +
                                              $"Disponibilidad: {reader["disponibilidad"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No se encontraron habitaciones del tipo {tipoHabitacion}.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar las habitaciones: " + ex.Message);
                }
            }
            Console.ReadKey();  // Esperar a que el usuario presione una tecla
        }

        // Método para obtener el tipo de habitación
        static string ObtenerTipoHabitacion()
        {
            string tipoHabitacion;
            while (true)
            {
                Console.Write("Ingrese el tipo de habitación (Simple, Doble, Suite): ");
                tipoHabitacion = Console.ReadLine().ToUpper();
                if (tipoHabitacion == "SIMPLE" || tipoHabitacion == "DOBLE" || tipoHabitacion == "SUITE")
                    break;
                else
                    Console.WriteLine("Tipo inválido. Debe ser 'Simple', 'Doble' o 'Suite'.");
            }
            return tipoHabitacion;
        }
        static void ConsultarDisponibilidad()
        {
            Console.Clear();
            Console.WriteLine("Consultar Habitaciones por Disponibilidad");

            // Solicitar la disponibilidad de las habitaciones
            string disponibilidad = ObtenerDisponibilidad();

            // Consulta SQL para obtener las habitaciones por disponibilidad
            string query = "SELECT id_habitacion, numero_habitacion, tipo_habitacion, precio, disponibilidad " +
                           "FROM Habitaciones WHERE disponibilidad = @disponibilidad";

            using (var conexion = Conectar())
            {
                if (conexion == null)
                {
                    Console.WriteLine("No se pudo establecer la conexión con la base de datos.");
                    return;
                }

                try
                {
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@disponibilidad", disponibilidad);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    // Verificar si se encontraron habitaciones con la disponibilidad solicitada
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["id_habitacion"]}, " +
                                              $"Número de Habitación: {reader["numero_habitacion"]}, " +
                                              $"Tipo de Habitación: {reader["tipo_habitacion"]}, " +
                                              $"Precio: {reader["precio"]}, " +
                                              $"Disponibilidad: {reader["disponibilidad"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No se encontraron habitaciones con disponibilidad {disponibilidad}.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al consultar las habitaciones: " + ex.Message);
                }
            }
            Console.ReadKey();  // Esperar a que el usuario presione una tecla
        }

        // Método para obtener la disponibilidad de las habitaciones
        static string ObtenerDisponibilidad()
        {
            string disponibilidad;
            while (true)
            {
                Console.Write("Ingrese la disponibilidad (Disponible/NoDisponible): ");
                disponibilidad = Console.ReadLine().ToUpper();
                if (disponibilidad == "DISPONIBLE" || disponibilidad == "NODISPONIBLE")
                    break;
                else
                    Console.WriteLine("Disponibilidad inválida. Debe ser 'Disponible' o 'NoDisponible'.");
            }
            return disponibilidad;
        }

    }
}
