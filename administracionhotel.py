import mysql.connector
import re
from datetime import datetime

# CONEXÍON BD
def conectar_bd():
    return mysql.connector.connect(
        host="localhost",
        user="root",  
        password="root",  
        database="hotel_db"
    )

#FUNCIÓN NOMBRES
def validar_nombre_apellido(valor):
    return bool(re.match("^[A-Za-záéíóúÁÉÍÓÚñÑ' ]{2,}$", valor))

#VALIDACION DNI
def validar_documento(documento):
    return documento.isdigit() and 5 <= len(documento) <= 10

# VALIDACIÓN TEL
def validar_telefono(telefono):
    return telefono.isdigit() and 8 <= len(telefono) <= 15

# VALIDACION FECHAS 
def validar_fecha(fecha_str, tipo="nacimiento"):
    try:
        fecha = datetime.strptime(fecha_str, "%d-%m-%Y")
        if tipo == "nacimiento":
            return fecha <= datetime.today()  
        elif tipo == "checkin_checkout":
            return fecha > datetime.today()  
    except ValueError:
        return False

# PRECIO PREESTABLECIDO
def obtener_precio(tipo_habitacion):
    precios = {"SIMPLE": 10000, "DOBLE": 18000, "SUITE": 25000}
    return precios.get(tipo_habitacion.upper(), 0)

# VALIDACIÓN DE DATOS
def obtener_dato_valido(mensaje, funcion_validacion, tipo="texto"):
    while True:
        valor = input(mensaje)
        if tipo == "texto" and funcion_validacion(valor):
            return valor
        elif tipo == "numero" and valor.isdigit() and funcion_validacion(valor):
            return valor
        print("Valor inválido. Intente nuevamente.")

# PRIMER MENÚ 1
def menu_principal():
    while True:
        print("\n--- MENÚ PRINCIPAL ---")
        print("1. Gestión de Reservas")
        print("2. Gestión de Habitaciones")
        print("3. Salir")
        opcion = input("Seleccione una opción: ")

        if opcion == "1":
            menu_reservas()
        elif opcion == "2":
            menu_habitaciones()
        elif opcion == "3":
            print("Saliendo del sistema...")
            break
        else:
            print("Opción no válida, intente nuevamente.")
            
# MENÚ 1.2 GESTION DE HABITACIONES           
def menu_habitaciones():
    while True:
        print("\n-- MENÚ GESTIÓN DE HABITACIONES --")
        print("1. Consultar Habitaciones por Número")
        print("2. Consultar Habitaciones por Tipo(Simple-Ddoble-Suite)")
        print("3. Consultar Habitaciones por Estado(DISPONIBLE SI/NO)")
        print("4. Consultar Habitaciones con Ordenamiento")
        print("5. Volver al Menú Principal")
        
        opcion = input("Seleccione una opción: ")

        if opcion == "1":
            consultar_habitaciones_por_numero()
        elif opcion == "2":
            consultar_habitaciones_por_tipo()
        elif opcion == "3":
            consultar_habitaciones_por_estado()
        elif opcion == "4":
            consultar_habitaciones_con_ordenamiento()
        elif opcion == "5":
            break
        else:
            print("Opción no válida, intente nuevamente.")
#CONSULTA 2.1 POR N° HABITACION
def consultar_habitaciones_por_numero():
    numero_habitacion = input("Ingrese el número de habitación: ")
    conexion = conectar_bd()
    cursor = conexion.cursor()
    cursor.execute("SELECT * FROM habitaciones WHERE numero_habitacion = %s", (numero_habitacion,))
    habitaciones = cursor.fetchall()
    for habitacion in habitaciones:
        print(f"Num. Habitación: {habitacion[0]}, Tipo: {habitacion[1]}, Disponible: {habitacion[2]}")
    conexion.close()
# CONSULTA 2.2 POR TIPO DE HABITACIÓN
def consultar_habitaciones_por_tipo():
    tipo = input("Ingrese el tipo de habitación (SIMPLE, DOBLE, SUITE): ").upper()
    conexion = conectar_bd()
    cursor = conexion.cursor()
    cursor.execute("SELECT * FROM habitaciones WHERE tipo = %s", (tipo,))
    habitaciones = cursor.fetchall()
    for habitacion in habitaciones:
        print(f"Num. Habitación: {habitacion[0]}, Tipo: {habitacion[1]}, Disponible: {habitacion[2]}")
    conexion.close()
# CONSULTA 2.3 POR ESTADO DE HABITACIÓN
def consultar_habitaciones_por_estado():
    estado = input("Ingrese el estado de la habitación (SI/NO): ").upper()
    conexion = conectar_bd()
    cursor = conexion.cursor()
    cursor.execute("SELECT * FROM habitaciones WHERE disponible = %s", (estado,))
    habitaciones = cursor.fetchall()
    for habitacion in habitaciones:
        print(f"Num. Habitación: {habitacion[0]}, Tipo: {habitacion[1]}, Disponible: {habitacion[2]}")
    conexion.close()
#  MENU 2.4 ORDENAMINETO
def consultar_habitaciones_con_ordenamiento():
    print("\nOpciones de ordenamiento para habitaciones:")
    print("1. Por número de habitación (ascendente)")
    print("2. Por disponibilidad (disponibles primero)")

    orden = input("Seleccione una opción de ordenamiento (1, 2): ")

    conexion = conectar_bd()
    cursor = conexion.cursor()

    if orden == "1":
        cursor.execute("SELECT * FROM habitaciones ORDER BY numero_habitacion ASC;") 
    elif orden == "2":
        cursor.execute("SELECT * FROM habitaciones ORDER BY (disponible = 'SI') DESC, tipo ASC;")
    else:
        print("Opción no válida.")
        conexion.close()
        return

    habitaciones = cursor.fetchall()
    for habitacion in habitaciones:
        print(f"Num. Habitación: {habitacion[0]}, Tipo: {habitacion[1]}, Disponible: {habitacion[2]}")

    conexion.close()
# MENÚ 1. GESTIÓN DE RESERVAS
def menu_reservas():
    while True:
        print("\n--- MENÚ GESTIÓN DE RESERVAS ---")
        print("1. Agregar Reserva")
        print("2. Consultar Reservas")
        print("3. Modificar Reserva")
        print("4. Eliminar Reserva")
        print("5. Volver al Menú Principal")
        opcion = input("Seleccione una opción: ")

        if opcion == "1":
            agregar_reserva()
        elif opcion == "2":
            consultar_reservas()
        elif opcion == "3":
            modificar_reserva()
        elif opcion == "4":
            eliminar_reserva()
        elif opcion == "5":
            break
        else:
            print("Opción no válida, intente nuevamente.")

# AGREGAR 1.1 RESERVA
def agregar_reserva():
    nombre = obtener_dato_valido("Ingrese el nombre (mínimo 2 caracteres, solo letras): ", validar_nombre_apellido)
    apellido = obtener_dato_valido("Ingrese el apellido (mínimo 2 caracteres, solo letras): ", validar_nombre_apellido)
    documento = obtener_dato_valido("Ingrese el número de documento (5-10 dígitos): ", validar_documento, tipo="numero")
    telefono = obtener_dato_valido("Ingrese el número de teléfono (8-15 dígitos): ", validar_telefono, tipo="numero")

    fecha_nacimiento = obtener_dato_valido("Ingrese la fecha de nacimiento (DD-MM-YYYY): ", lambda x: validar_fecha(x, "nacimiento"))
    fecha_checkin_str = obtener_dato_valido("Ingrese la fecha de check-in (DD-MM-YYYY): ", lambda x: validar_fecha(x, "checkin_checkout"))
    fecha_checkout_str = obtener_dato_valido("Ingrese la fecha de check-out (DD-MM-YYYY): ", lambda x: validar_fecha(x, "checkin_checkout"))

    tipo_habitacion = input("Ingrese el tipo de habitación (SIMPLE, DOBLE, SUITE): ").upper()
    while tipo_habitacion not in ["SIMPLE", "DOBLE", "SUITE"]:
        print("Tipo de habitación inválido.")
        tipo_habitacion = input("Ingrese el tipo de habitación (SIMPLE, DOBLE, SUITE): ").upper()

    confirmacion = input("¿Desea continuar con la reserva? (S/N): ").upper()
    if confirmacion == "N":
        print("Reserva cancelada por el usuario.")
        return

    # CONVERTIR MODO DE ENTRADA A LA DE LA BASE DE DATOS
    fecha_nacimiento = datetime.strptime(fecha_nacimiento, "%d-%m-%Y").strftime("%Y/%m/%d")
    fecha_checkin = datetime.strptime(fecha_checkin_str, "%d-%m-%Y").strftime("%Y/%m/%d")
    fecha_checkout = datetime.strptime(fecha_checkout_str, "%d-%m-%Y").strftime("%Y/%m/%d")

    conexion = conectar_bd()
    cursor = conexion.cursor()

    precio = obtener_precio(tipo_habitacion)

    cursor.execute("SELECT numero_habitacion FROM habitaciones WHERE tipo = %s AND disponible = 'SI' LIMIT 1", (tipo_habitacion,))# SOLO SI ESTAN DISPONIBLES
    habitacion_disponible = cursor.fetchone()

    if habitacion_disponible:
        numero_habitacion = habitacion_disponible[0]
        cursor.execute("INSERT INTO reservas (nombre, apellido, documento, fecha_nacimiento, telefono, fecha_checkin, fecha_checkout, numero_habitacion, precio, estado) VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, 'ACTIVA')", 
                       (nombre, apellido, documento, fecha_nacimiento, telefono, fecha_checkin, fecha_checkout, numero_habitacion, precio))
        cursor.execute("UPDATE habitaciones SET disponible = 'NO' WHERE numero_habitacion = %s", (numero_habitacion,))

        conexion.commit()
        print(f"Reserva realizada con éxito para {nombre} {apellido} en la habitación {numero_habitacion}.")
    else:
        print("No hay habitaciones disponibles para el tipo seleccionado.")

    conexion.close()

# CONSULTA 1.2 RESERVAS
# CONSULTA 1.2 RESERVAS
# CONSULTA 1.2 RESERVAS
def consultar_reservas():
    conexion = conectar_bd()
    cursor = conexion.cursor()

    # OPCIÓN DE FILTRADO
    while True:
        filtro = input("¿Por qué desea consultar las reservas? (nombre, apellido, documento): ").lower()

        if filtro in ['nombre', 'apellido', 'documento']:
            valor_filtro = input(f"Ingrese el valor para {filtro}: ")

            # Consulta SQL ajustada sin fechas
            consulta = f"SELECT * FROM reservas WHERE estado = 'ACTIVA' AND {filtro} LIKE %s"
            
            cursor.execute(consulta, (f"%{valor_filtro}%",))
            break
        else:
            print("Opción de filtro.Elija una de las 3 opciones . intente nuevamente por favor.")
    
    reservas = cursor.fetchall()

    if reservas:
        for reserva in reservas:
            print(f"ID: {reserva[0]}, Nombre: {reserva[1]} {reserva[2]}, Documento: {reserva[3]}, Fecha Check-in: {reserva[6]}, Fecha Check-out: {reserva[7]}, Habitación: {reserva[8]}, Precio: {reserva[9]}")
    else:
        print("No se encontraron reservas con los criterios especificados.")
    
    conexion.close()



# MODIFICAR 1.3  RESERVA
def modificar_reserva():
    id_reserva = input("Ingrese el ID de la reserva a modificar: ")
    conexion = conectar_bd()
    cursor = conexion.cursor()

    cursor.execute("SELECT * FROM reservas WHERE id_reserva = %s AND estado = 'ACTIVA'", (id_reserva,))# RESERVAS ACTIVAS
    reserva = cursor.fetchone()

    if reserva:
        # NOMBRES DE LAS COLUMNAS DE LA BD
        column_names = [desc[0] for desc in cursor.description]

        print("\n--- Datos de la Reserva Encontrada ---")
        for column_name, value in zip(column_names, reserva):
            print(f"{column_name}: {value}")

        # MODIFICA CHECK-IN
        while True:
            fecha_checkin_str = input("\nIngrese la nueva fecha de check-in (DD-MM-YYYY) o 'cancelar' para salir: ")
            if fecha_checkin_str.lower() == 'cancelar':
                print("Modificación cancelada.")
                conexion.close()
                return  

            try:
                fecha_checkin = datetime.strptime(fecha_checkin_str, "%d-%m-%Y")
                if fecha_checkin <= datetime.today():
                    print("Fecha de check-in inválida. No puede ser en el pasado.")
                else:
                    break  
            except ValueError:
                print("Formato de fecha inválido. Por favor ingrese la fecha en el formato DD-MM-YYYY.")

        fecha_checkin = fecha_checkin.strftime("%Y/%m/%d")

        # MODIFICA CHECK-OUT
        while True:
            fecha_checkout_str = input("\nIngrese la nueva fecha de check-out (DD-MM-YYYY) o 'cancelar' para salir: ")
            if fecha_checkout_str.lower() == 'cancelar':
                print("Modificación cancelada.")
                conexion.close()
                return  

            try:
                fecha_checkout = datetime.strptime(fecha_checkout_str, "%d-%m-%Y")
                fecha_checkin_dt = datetime.strptime(fecha_checkin, "%Y/%m/%d")
                
                if fecha_checkout <= fecha_checkin_dt:
                    print("Fecha de check-out inválida. Debe ser posterior a la fecha de check-in.")
                else:
                    break  
            except ValueError:
                print("Formato de fecha inválido. Por favor ingrese la fecha en el formato DD-MM-YYYY.")

        fecha_checkout = fecha_checkout.strftime("%Y/%m/%d")

        confirmacion = input(f"\n¿Está seguro que desea modificar esta reserva? (S/N): ").upper()
        if confirmacion == "N":
            print("Modificación cancelada.")
            conexion.close()
            return  

        # ACTUALIZA RESERVA
        cursor.execute("UPDATE reservas SET fecha_checkin = %s, fecha_checkout = %s WHERE id_reserva = %s", 
                       (fecha_checkin, fecha_checkout, id_reserva))
        conexion.commit()
        print("\nReserva modificada con éxito.")
    else:
        print("No se encontró una reserva activa con ese ID.")

    conexion.close()


# ELIMINA RESERVA 1.4 
def eliminar_reserva():
    id_reserva = input("Ingrese el ID de la reserva a eliminar: ")
    conexion = conectar_bd()
    cursor = conexion.cursor()

    # CONSULTA DE LA RESERVA A CANCELAR
    cursor.execute("SELECT * FROM reservas WHERE id_reserva = %s AND estado = 'ACTIVA'", (id_reserva,))
    reserva = cursor.fetchone()

    if reserva:
        #RESERVA A CANCELAR
        print("\n--- Datos de la Reserva a Cancelar ---")
        print(f"ID: {reserva[0]}")
        print(f"Nombre: {reserva[1]} {reserva[2]}")
        print(f"Documento: {reserva[3]}")
        print(f"Fecha de Nacimiento: {reserva[4]}")
        print(f"Teléfono: {reserva[5]}")
        print(f"Fecha de Check-in: {reserva[6]}")
        print(f"Fecha de Check-out: {reserva[7]}")
        print(f"Número de Habitación: {reserva[8]}")
        print(f"Precio: {reserva[9]}")
        
        
        confirmacion = input("\n¿Está seguro de cancelar esta reserva? (S/N): ").upper()
        
        if confirmacion == "S":
            # CANCELACIÓN
            cursor.execute("UPDATE reservas SET estado = 'CANCELADA' WHERE id_reserva = %s", (id_reserva,))
            
            # CAMBIA DISPONIBILIDAD DE LA HABITACIÓN
            numero_habitacion = reserva[8]  
            cursor.execute("UPDATE habitaciones SET disponible = 'SI' WHERE numero_habitacion = %s", (numero_habitacion,))
            conexion.commit()
            print("Reserva cancelada con éxito.")
        else:
            print("Operación cancelada. La reserva no ha sido modificada.")
    else:
        print("Reserva no encontrada o ya está cancelada.")
    
    conexion.close()


menu_principal()
