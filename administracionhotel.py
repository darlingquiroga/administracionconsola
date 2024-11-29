import mysql.connector
import re
from datetime import datetime

# Conectar a la base de datos MySQL
def conectar_bd():
    return mysql.connector.connect(
        host="localhost",
        user="root",  # Cambia esto por tu usuario de MySQL
        password="root",  # Cambia esto por tu contraseña de MySQL
        database="hotel_db"
    )

# Validaciones
def validar_nombre_apellido(nombre):
    return bool(re.match("^[A-Za-záéíóúÁÉÍÓÚñÑ' ]{3,}$", nombre))

def validar_documento(documento):
    return documento.isdigit() and 5 <= len(documento) <= 10

def validar_telefono(telefono):
    return telefono.isdigit() and 5 <= len(telefono) <= 10

def validar_fecha_nacimiento(fecha_str):
    try:
        fecha = datetime.strptime(fecha_str, "%d-%m-%Y")
        return fecha <= datetime.today()  # Fecha de nacimiento no puede ser en el futuro
    except ValueError:
        return False

def validar_fecha_checkin_checkout(fecha_str):
    try:
        fecha = datetime.strptime(fecha_str, "%d-%m-%Y")
        return fecha > datetime.today()  # Check-in y Check-out deben ser en el futuro
    except ValueError:
        return False

def obtener_precio(tipo_habitacion):
    precios = {"SIMPLE": 10000, "DOBLE": 18000, "SUITE": 25000}
    return precios.get(tipo_habitacion.upper(), 0)

# Menú de Gestión de Habitaciones
def menu_habitaciones():
    while True:
        print("\n--- MENÚ GESTIÓN DE HABITACIONES ---")
        print("1. Consultar Habitaciones por Número")
        print("2. Consultar Habitaciones por Tipo")
        print("3. Consultar Habitaciones por Estado")
        print("4. Volver al Menú Principal")
        opcion = input("Seleccione una opción: ")

        if opcion == "1":
            consultar_habitaciones_por_numero()
        elif opcion == "2":
            consultar_habitaciones_por_tipo()
        elif opcion == "3":
            consultar_habitaciones_por_estado()
        elif opcion == "4":
            break
        else:
            print("Opción no válida, intente nuevamente.")

# Consultar habitaciones por número
def consultar_habitaciones_por_numero():
    tipo = input("Ingrese el tipo de habitación (SIMPLE, DOBLE, SUITE): ").upper()
    rango = {"SIMPLE": (100, 110), "DOBLE": (200, 210), "SUITE": (300, 310)}
    if tipo in rango:
        conexion = conectar_bd()
        cursor = conexion.cursor()
        query = f"SELECT * FROM habitaciones WHERE numero_habitacion BETWEEN {rango[tipo][0]} AND {rango[tipo][1]}"
        cursor.execute(query)
        habitaciones = cursor.fetchall()
        for habitacion in habitaciones:
            print(f"Num. Habitación: {habitacion[0]}, Tipo: {habitacion[1]}, Disponible: {habitacion[2]}")
        conexion.close()
    else:
        print("Tipo de habitación inválido.")

# Consultar habitaciones por tipo
def consultar_habitaciones_por_tipo():
    tipo = input("Ingrese el tipo de habitación (SIMPLE, DOBLE, SUITE): ").upper()
    conexion = conectar_bd()
    cursor = conexion.cursor()
    cursor.execute("SELECT * FROM habitaciones WHERE tipo = %s", (tipo,))
    habitaciones = cursor.fetchall()
    for habitacion in habitaciones:
        print(f"Num. Habitación: {habitacion[0]}, Tipo: {habitacion[1]}, Disponible: {habitacion[2]}")
    conexion.close()

# Consultar habitaciones por estado
def consultar_habitaciones_por_estado():
    estado = input("Ingrese el estado de la habitación (SI/NO): ").upper()
    conexion = conectar_bd()
    cursor = conexion.cursor()
    cursor.execute("SELECT * FROM habitaciones WHERE disponible = %s", (estado,))
    habitaciones = cursor.fetchall()
    for habitacion in habitaciones:
        print(f"Num. Habitación: {habitacion[0]}, Tipo: {habitacion[1]}, Disponible: {habitacion[2]}")
    conexion.close()

# Menú de Gestión de Reservas
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

# Agregar reserva
def agregar_reserva():
    nombre = input("Ingrese el nombre (mínimo 3 caracteres, solo letras): ")
    while not validar_nombre_apellido(nombre):
        print("Nombre inválido. Debe contener solo letras y tener más de dos caracteres.")
        nombre = input("Ingrese el nombre (mínimo 3 caracteres, solo letras): ")

    apellido = input("Ingrese el apellido (mínimo 3 caracteres, solo letras): ")
    while not validar_nombre_apellido(apellido):
        print("Apellido inválido. Debe contener solo letras y tener más de dos caracteres.")
        apellido = input("Ingrese el apellido (mínimo 3 caracteres, solo letras): ")

    documento = input("Ingrese el número de documento (5-10 dígitos): ")
    while not validar_documento(documento):
        print("Documento inválido. Debe ser numérico y tener entre 5 y 10 dígitos.")
        documento = input("Ingrese el número de documento (5-10 dígitos): ")

    telefono = input("Ingrese el número de teléfono (5-10 dígitos): ")
    while not validar_telefono(telefono):
        print("Teléfono inválido. Debe ser numérico y tener entre 5 y 10 dígitos.")
        telefono = input("Ingrese el número de teléfono (5-10 dígitos): ")

    fecha_nacimiento = input("Ingrese la fecha de nacimiento (DD-MM-YYYY): ")
    while not validar_fecha_nacimiento(fecha_nacimiento):
        print("Fecha de nacimiento inválida. Debe ser en el formato DD-MM-YYYY y no puede ser en el futuro.")
        fecha_nacimiento = input("Ingrese la fecha de nacimiento (DD-MM-YYYY): ")

    # Convertir la fecha de nacimiento a formato YYYY/MM/DD
    fecha_nacimiento = datetime.strptime(fecha_nacimiento, "%d-%m-%Y").strftime("%Y/%m/%d")

    fecha_checkin = input("Ingrese la fecha de check-in (DD-MM-YYYY): ")
    while not validar_fecha_checkin_checkout(fecha_checkin):
        print("Fecha de check-in inválida. Debe ser en el futuro.")
        fecha_checkin = input("Ingrese la fecha de check-in (DD-MM-YYYY): ")

    # Convertir la fecha de check-in a formato YYYY/MM/DD
    fecha_checkin = datetime.strptime(fecha_checkin, "%d-%m-%Y").strftime("%Y/%m/%d")

    fecha_checkout = input("Ingrese la fecha de check-out (DD-MM-YYYY): ")
    while not validar_fecha_checkin_checkout(fecha_checkout) or fecha_checkout <= fecha_checkin:
        print("Fecha de check-out inválida. Debe ser posterior a la fecha de check-in.")
        fecha_checkout = input("Ingrese la fecha de check-out (DD-MM-YYYY): ")

    # Convertir la fecha de check-out a formato YYYY/MM/DD
    fecha_checkout = datetime.strptime(fecha_checkout, "%d-%m-%Y").strftime("%Y/%m/%d")

    tipo_habitacion = input("Ingrese el tipo de habitación (SIMPLE, DOBLE, SUITE): ").upper()
    while tipo_habitacion not in ["SIMPLE", "DOBLE", "SUITE"]:
        print("Tipo de habitación inválido.")
        tipo_habitacion = input("Ingrese el tipo de habitación (SIMPLE, DOBLE, SUITE): ").upper()

    confirmacion = input("¿Desea continuar con la reserva? (S/N): ").upper()
    if confirmacion == "N":
        print("Reserva cancelada por el usuario.")
        return

    conexion = conectar_bd()
    cursor = conexion.cursor()

    # Obtener el precio basado en el tipo de habitación
    precio = obtener_precio(tipo_habitacion)

    # Verificar disponibilidad
    cursor.execute("SELECT numero_habitacion FROM habitaciones WHERE tipo = %s AND disponible = 'SI' LIMIT 1", (tipo_habitacion,))
    habitacion_disponible = cursor.fetchone()

    if habitacion_disponible:
        numero_habitacion = habitacion_disponible[0]
        cursor.execute("INSERT INTO reservas (nombre, apellido, documento, fecha_nacimiento, telefono, fecha_checkin, fecha_checkout, numero_habitacion, precio, estado) VALUES (%s, %s, %s, %s, %s, %s, %s, %s, %s, 'ACTIVA')", 
                       (nombre, apellido, documento, fecha_nacimiento, telefono, fecha_checkin, fecha_checkout, numero_habitacion, precio))
        cursor.execute("UPDATE habitaciones SET disponible = 'NO' WHERE numero_habitacion = %s", (numero_habitacion,))
        conexion.commit()
        print("Reserva realizada con éxito.")
    else:
        print("No hay habitaciones disponibles para el tipo seleccionado.")

    conexion.close()

# Consultar reservas
def consultar_reservas():
    conexion = conectar_bd()
    cursor = conexion.cursor()

    orden = input("¿Cómo desea ordenar las reservas? (nombre, apellido, checkin, checkout): ").lower()
    if orden in ['nombre', 'apellido', 'checkin', 'checkout']:
        cursor.execute(f"SELECT * FROM reservas WHERE estado = 'ACTIVA' ORDER BY {orden}")
    else:
        print("Opción de ordenamiento inválida. Mostrando todas las reservas.")
        cursor.execute("SELECT * FROM reservas WHERE estado = 'ACTIVA'")

    reservas = cursor.fetchall()
    for reserva in reservas:
        print(f"ID: {reserva[0]}, Nombre: {reserva[1]} {reserva[2]}, Documento: {reserva[3]}, Fecha Check-in: {reserva[6]}, Fecha Check-out: {reserva[7]}, Habitación: {reserva[8]}, Precio: {reserva[9]}")

    conexion.close()

# Modificar reserva
def modificar_reserva(): 
    id_reserva = input("Ingrese el ID de la reserva a modificar: ")
    conexion = conectar_bd()
    cursor = conexion.cursor()
    cursor.execute("SELECT * FROM reservas WHERE id_reserva = %s", (id_reserva,))
    reserva = cursor.fetchone()

    if reserva:
        print(f"Reserva encontrada: {reserva}")
        
        # Modificar la validación de la fecha de check-in
        while True:
            fecha_checkin_str = input("Ingrese la nueva fecha de check-in (DD-MM-YYYY): ")
            try:
                fecha_checkin = datetime.strptime(fecha_checkin_str, "%d-%m-%Y")
                if fecha_checkin <= datetime.today():
                    print("Fecha de check-in inválida. No puede ser en el pasado.")
                else:
                    break
            except ValueError:
                print("Formato de fecha inválido. Por favor ingrese la fecha en el formato DD-MM-YYYY.")
        
        # Convertir la fecha de check-in al formato YYYY/MM/DD
        fecha_checkin = fecha_checkin.strftime("%Y/%m/%d")

        # Validar la fecha de check-out
        while True:
            fecha_checkout_str = input("Ingrese la nueva fecha de check-out (DD-MM-YYYY): ")
            try:
                fecha_checkout = datetime.strptime(fecha_checkout_str, "%d-%m-%Y")
                # Convertir fecha_checkin a datetime para la comparación
                fecha_checkin_dt = datetime.strptime(fecha_checkin, "%Y/%m/%d")
                if fecha_checkout <= fecha_checkin_dt:
                    print("Fecha de check-out inválida. Debe ser posterior a la fecha de check-in.")
                else:
                    break
            except ValueError:
                print("Formato de fecha inválido. Por favor ingrese la fecha en el formato DD-MM-YYYY.")
        
        # Convertir la fecha de check-out al formato YYYY/MM/DD
        fecha_checkout = fecha_checkout.strftime("%Y/%m/%d")

        confirmacion = input(f"¿Está seguro que desea modificar esta reserva? (S/N): ").upper()
        if confirmacion == "N":
            print("Modificación cancelada.")
            return
        
        cursor.execute("UPDATE reservas SET fecha_checkin = %s, fecha_checkout = %s WHERE id_reserva = %s", 
                       (fecha_checkin, fecha_checkout, id_reserva))
        conexion.commit()
        print("Reserva modificada con éxito.")
    else:
        print("Reserva no encontrada.")

    conexion.close()


# Eliminar reserva (ponerla inactiva)
def eliminar_reserva():
    id_reserva = input("Ingrese el ID de la reserva a eliminar (poner inactiva): ")
    conexion = conectar_bd()
    cursor = conexion.cursor()
    cursor.execute("SELECT * FROM reservas WHERE id_reserva = %s", (id_reserva,))
    reserva = cursor.fetchone()

    if reserva:
        confirmacion = input(f"¿Está seguro que desea poner inactiva esta reserva? (S/N): ").upper()
        if confirmacion == "N":
            print("Eliminación cancelada.")
            return
        cursor.execute("UPDATE reservas SET estado = 'INACTIVA' WHERE id_reserva = %s", (id_reserva,))
        conexion.commit()
        print("Reserva marcada como inactiva.")
    else:
        print("Reserva no encontrada.")
    
    conexion.close()

# Menú principal
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

# Ejecutar el menú principal
if __name__ == "__main__":
    menu_principal()
