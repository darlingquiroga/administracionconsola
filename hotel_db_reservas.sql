-- MySQL dump 10.13  Distrib 8.0.22, for Win64 (x86_64)
--
-- Host: localhost    Database: hotel_db
-- ------------------------------------------------------
-- Server version	8.0.27

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `reservas`
--

DROP TABLE IF EXISTS `reservas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reservas` (
  `id_reserva` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `documento` varchar(20) NOT NULL,
  `fecha_nacimiento` date NOT NULL,
  `telefono` varchar(15) NOT NULL,
  `fecha_checkin` date NOT NULL,
  `fecha_checkout` date NOT NULL,
  `numero_habitacion` int NOT NULL,
  `precio` int NOT NULL,
  `estado` varchar(10) NOT NULL,
  PRIMARY KEY (`id_reserva`),
  KEY `numero_habitacion` (`numero_habitacion`),
  CONSTRAINT `reservas_ibfk_1` FOREIGN KEY (`numero_habitacion`) REFERENCES `habitaciones` (`numero_habitacion`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reservas`
--

LOCK TABLES `reservas` WRITE;
/*!40000 ALTER TABLE `reservas` DISABLE KEYS */;
INSERT INTO `reservas` VALUES (1,'dfhgfhbg','fdfhbghb','1565563','2010-02-12','15631635','2024-12-05','2024-12-10',100,10000,'INACTIVA'),(2,'Juan','Pérez','12345678A','1985-06-15','555-123456','2024-12-01','2024-12-05',100,150,'confirmada'),(3,'Ana','López','23456789B','1990-03-10','555-234567','2024-12-02','2024-12-06',101,180,'pendiente'),(4,'Carlos','Gómez','34567890C','1980-07-22','555-345678','2024-12-03','2024-12-07',102,200,'cancelada'),(5,'María','Sánchez','45678901D','1992-11-05','555-456789','2024-12-04','2024-12-08',103,170,'confirmada'),(6,'David','Martínez','56789012E','1988-04-15','555-567890','2024-12-05','2024-12-09',104,190,'pendiente'),(7,'Laura','Hernández','67890123F','1995-09-11','555-678901','2024-12-06','2024-12-10',105,160,'confirmada'),(8,'Pedro','González','78901234G','1982-01-18','555-789012','2024-12-07','2024-12-11',106,210,'confirmada'),(9,'Sofía','Rodríguez','89012345H','1994-02-02','555-890123','2024-12-08','2024-12-12',107,220,'cancelada'),(10,'Javier','Fernández','90123456I','1987-05-24','555-901234','2024-12-09','2024-12-13',108,230,'confirmada'),(11,'Eva','Mendoza','01234567J','1991-12-10','555-012345','2024-12-10','2024-12-14',109,250,'pendiente'),(12,'José','Jiménez','12345678K','1986-08-30','555-123457','2024-12-11','2024-12-15',110,240,'confirmada'),(13,'Marta','García','23456789L','1993-07-03','555-234568','2024-12-12','2024-12-16',200,300,'pendiente'),(14,'Luis','Suárez','34567890M','1983-03-14','555-345679','2024-12-13','2024-12-17',201,310,'confirmada'),(15,'Pablo','Torres','45678901N','1989-11-01','555-456790','2024-12-14','2024-12-18',202,320,'cancelada'),(16,'Isabel','Álvarez','56789012O','1981-06-19','555-567891','2024-12-15','2024-12-19',203,330,'confirmada'),(17,'Juan','Pérez','12345678A','1985-06-15','555-123456','2024-12-01','2024-12-05',100,150,'ACTIVA'),(18,'Ana','López','23456789B','1990-03-10','555-234567','2024-12-02','2024-12-06',101,180,'ACTIVA'),(19,'Carlos','Gómez','34567890C','1980-07-22','555-345678','2024-12-03','2024-12-07',102,200,'INACTIVA'),(20,'María','Sánchez','45678901D','1992-11-05','555-456789','2024-12-04','2024-12-08',103,170,'ACTIVA'),(21,'David','Martínez','56789012E','1988-04-15','555-567890','2024-12-05','2024-12-09',104,190,'ACTIVA'),(22,'Laura','Hernández','67890123F','1995-09-11','555-678901','2024-12-06','2024-12-10',105,160,'ACTIVA'),(23,'Pedro','González','78901234G','1982-01-18','555-789012','2024-12-07','2024-12-11',106,210,'ACTIVA'),(24,'Sofía','Rodríguez','89012345H','1994-02-02','555-890123','2024-12-08','2024-12-12',107,220,'ACTIVA'),(25,'Javier','Fernández','90123456I','1987-05-24','555-901234','2024-12-09','2024-12-13',108,230,'ACTIVA'),(26,'Eva','Mendoza','01234567J','1991-12-10','555-012345','2024-12-10','2024-12-14',109,250,'ACTIVA'),(27,'José','Jiménez','12345678K','1986-08-30','555-123457','2024-12-11','2024-12-15',110,240,'ACTIVA'),(28,'Marta','García','23456789L','1993-07-03','555-234568','2024-12-12','2024-12-16',200,300,'ACTIVA'),(29,'Luis','Suárez','34567890M','1983-03-14','555-345679','2024-12-13','2024-12-17',201,310,'ACTIVA'),(30,'Pablo','Torres','45678901N','1989-11-01','555-456790','2024-12-14','2024-12-18',202,320,'ACTIVA'),(31,'Isabel','Álvarez','56789012O','1981-06-19','555-567891','2024-12-15','2024-12-19',203,330,'INACTIVA'),(32,'FGBDFGBDF','FGBDFBGDF','65651','1993-02-22','63563','2024-12-25','2024-12-29',101,10000,'ACTIVA');
/*!40000 ALTER TABLE `reservas` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-11-28 23:58:05
