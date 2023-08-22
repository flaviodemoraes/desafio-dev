CREATE DATABASE  IF NOT EXISTS `db_desafio` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `db_desafio`;
-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: localhost    Database: db_desafio
-- ------------------------------------------------------
-- Server version	8.1.0

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
-- Table structure for table `__EFMigrationsHistory`
--

DROP TABLE IF EXISTS `__EFMigrationsHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__EFMigrationsHistory`
--

LOCK TABLES `__EFMigrationsHistory` WRITE;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory` VALUES ('20230821184118_InitialMigrations','7.0.10');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Lojas`
--

DROP TABLE IF EXISTS `Lojas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Lojas` (
  `LojaId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `NomeLoja` varchar(19) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `NomeProprietario` varchar(14) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`LojaId`),
  UNIQUE KEY `IX_Lojas_NomeLoja` (`NomeLoja`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Lojas`
--

LOCK TABLES `Lojas` WRITE;
/*!40000 ALTER TABLE `Lojas` DISABLE KEYS */;
/*!40000 ALTER TABLE `Lojas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Operacoes`
--

DROP TABLE IF EXISTS `Operacoes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Operacoes` (
  `OperacaoId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `TipoTransacaoId` int NOT NULL,
  `LojaId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
  `DataOcorrencia` datetime(6) NOT NULL,
  `Valor` decimal(65,30) NOT NULL,
  `Cpf` char(11) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CartaoTransacao` char(12) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `HoraOcorrencia` char(8) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`OperacaoId`),
  KEY `IX_Operacoes_LojaId` (`LojaId`),
  KEY `IX_Operacoes_TipoTransacaoId` (`TipoTransacaoId`),
  CONSTRAINT `FK_Operacoes_Lojas_LojaId` FOREIGN KEY (`LojaId`) REFERENCES `Lojas` (`LojaId`) ON DELETE CASCADE,
  CONSTRAINT `FK_Operacoes_TipoTransacoes_TipoTransacaoId` FOREIGN KEY (`TipoTransacaoId`) REFERENCES `TipoTransacoes` (`Tipo`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Operacoes`
--

LOCK TABLES `Operacoes` WRITE;
/*!40000 ALTER TABLE `Operacoes` DISABLE KEYS */;
/*!40000 ALTER TABLE `Operacoes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TipoTransacoes`
--

DROP TABLE IF EXISTS `TipoTransacoes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `TipoTransacoes` (
  `Tipo` int NOT NULL AUTO_INCREMENT,
  `Descricao` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Natureza` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Sinal` char(1) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Tipo`),
  UNIQUE KEY `IX_TipoTransacoes_Tipo` (`Tipo`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TipoTransacoes`
--

LOCK TABLES `TipoTransacoes` WRITE;
/*!40000 ALTER TABLE `TipoTransacoes` DISABLE KEYS */;
INSERT INTO `TipoTransacoes` VALUES (1,'Débito','Entrada','+'),(2,'Boleto','Saída','-'),(3,'Financiamento','Saída','-'),(4,'Crédito','Entrada','+'),(5,'Recebimento Empréstimo','Entrada','+'),(6,'Vendas','Entrada','+'),(7,'Recebimento TED','Entrada','+'),(8,'Recebimento DOC','Entrada','+'),(9,'Aluguel','Saída','-');
/*!40000 ALTER TABLE `TipoTransacoes` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-08-21 15:43:28
