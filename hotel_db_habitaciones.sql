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
-- Table structure for table `habitaciones`
--

DROP TABLE IF EXISTS `habitaciones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `habitaciones` (
  `numero_habitacion` int NOT NULL,
  `tipo` varchar(10) NOT NULL,
  `disponible` varchar(3) NOT NULL,
  PRIMARY KEY (`numero_habitacion`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `habitaciones`
--

LOCK TABLES `habitaciones` WRITE;
/*!40000 ALTER TABLE `habitaciones` DISABLE KEYS */;
INSERT INTO `habitaciones` VALUES (100,'SIMPLE','NO'),(101,'SIMPLE','NO'),(102,'SIMPLE','SI'),(103,'SIMPLE','SI'),(104,'SIMPLE','SI'),(105,'SIMPLE','SI'),(106,'SIMPLE','SI'),(107,'SIMPLE','SI'),(108,'SIMPLE','SI'),(109,'SIMPLE','SI'),(110,'SIMPLE','SI'),(200,'DOBLE','SI'),(201,'DOBLE','SI'),(202,'DOBLE','SI'),(203,'DOBLE','SI'),(204,'DOBLE','SI'),(205,'DOBLE','SI'),(206,'DOBLE','SI'),(207,'DOBLE','SI'),(208,'DOBLE','SI'),(209,'DOBLE','SI'),(210,'DOBLE','SI'),(300,'SUITE','SI'),(301,'SUITE','SI'),(302,'SUITE','SI'),(303,'SUITE','SI'),(304,'SUITE','SI'),(305,'SUITE','SI'),(306,'SUITE','SI'),(307,'SUITE','SI'),(308,'SUITE','SI'),(309,'SUITE','SI'),(310,'SUITE','SI');
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

-- Dump completed on 2024-11-28 23:58:05
