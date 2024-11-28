-- MySQL dump 10.13  Distrib 8.0.22, for Win64 (x86_64)
--
-- Host: localhost    Database: hoteldb
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
-- Table structure for table `habitaciones`
--

DROP TABLE IF EXISTS `habitaciones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `habitaciones` (
  `id_habitacion` int NOT NULL AUTO_INCREMENT,
  `numero_habitacion` varchar(20) NOT NULL,
  `tipo_habitacion` varchar(50) NOT NULL,
  `precio` decimal(10,2) NOT NULL,
  `disponibilidad` enum('Disponible','No Disponible') DEFAULT 'Disponible',
  PRIMARY KEY (`id_habitacion`),
  UNIQUE KEY `numero_habitacion` (`numero_habitacion`)
) ENGINE=InnoDB AUTO_INCREMENT=63 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `habitaciones`
--

LOCK TABLES `habitaciones` WRITE;
/*!40000 ALTER TABLE `habitaciones` DISABLE KEYS */;
INSERT INTO `habitaciones` VALUES (1,'100','Simple',10000.00,'Disponible'),(2,'101','Simple',10000.00,'Disponible'),(3,'102','Simple',10000.00,'Disponible'),(4,'103','Simple',10000.00,'Disponible'),(5,'104','Simple',10000.00,'Disponible'),(20,'105','Simple',10000.00,'Disponible'),(21,'106','Simple',10000.00,'Disponible'),(22,'107','Simple',10000.00,'Disponible'),(23,'108','Simple',10000.00,'Disponible'),(24,'109','Simple',10000.00,'Disponible'),(25,'110','Simple',10000.00,'Disponible'),(26,'111','Simple',10000.00,'Disponible'),(27,'112','Simple',10000.00,'Disponible'),(28,'113','Simple',10000.00,'Disponible'),(29,'114','Simple',10000.00,'Disponible'),(30,'115','Simple',10000.00,'Disponible'),(31,'200','Doble',18000.00,'Disponible'),(32,'201','Doble',18000.00,'Disponible'),(33,'202','Doble',18000.00,'Disponible'),(34,'203','Doble',18000.00,'Disponible'),(35,'204','Doble',18000.00,'Disponible'),(36,'205','Doble',18000.00,'Disponible'),(37,'206','Doble',18000.00,'Disponible'),(38,'207','Doble',18000.00,'Disponible'),(39,'208','Doble',18000.00,'Disponible'),(40,'209','Doble',18000.00,'Disponible'),(41,'210','Doble',18000.00,'Disponible'),(42,'211','Doble',18000.00,'Disponible'),(43,'212','Doble',18000.00,'Disponible'),(44,'213','Doble',18000.00,'Disponible'),(45,'214','Doble',18000.00,'Disponible'),(46,'215','Doble',18000.00,'Disponible'),(47,'300','Suite',25000.00,'Disponible'),(48,'301','Suite',25000.00,'Disponible'),(49,'302','Suite',25000.00,'Disponible'),(50,'303','Suite',25000.00,'Disponible'),(51,'304','Suite',25000.00,'Disponible'),(52,'305','Suite',25000.00,'Disponible'),(53,'306','Suite',25000.00,'Disponible'),(54,'307','Suite',25000.00,'Disponible'),(55,'308','Suite',25000.00,'Disponible'),(56,'309','Suite',25000.00,'Disponible'),(57,'310','Suite',25000.00,'Disponible'),(58,'311','Suite',25000.00,'Disponible'),(59,'312','Suite',25000.00,'Disponible'),(60,'313','Suite',25000.00,'Disponible'),(61,'314','Suite',25000.00,'Disponible'),(62,'315','Suite',25000.00,'Disponible');
/*!40000 ALTER TABLE `habitaciones` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-11-28  0:48:38
