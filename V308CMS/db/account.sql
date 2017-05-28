/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 100121
Source Host           : localhost:3306
Source Database       : mpstart

Target Server Type    : MYSQL
Target Server Version : 100121
File Encoding         : 65001

Date: 2017-05-22 11:22:59
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for account
-- ----------------------------
DROP TABLE IF EXISTS `account`;
CREATE TABLE `account` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(50) DEFAULT NULL,
  `Password` varchar(50) NOT NULL,
  `FullName` varchar(50) DEFAULT NULL,
  `Email` varchar(50) NOT NULL,
  `Address` varchar(250) DEFAULT NULL,
  `Phone` varchar(50) DEFAULT NULL,
  `Gender` bit(1) DEFAULT NULL,
  `BirthDay` datetime DEFAULT NULL,
  `Status` bit(1) DEFAULT NULL,
  `Avata` varchar(50) DEFAULT NULL,
  `Role` int(11) DEFAULT NULL,
  `Date` datetime DEFAULT NULL,
  `Token` char(50) DEFAULT NULL,
  `TokenExpireDate` datetime NOT NULL DEFAULT '0000-00-00 00:00:00' ON UPDATE CURRENT_TIMESTAMP,
  `Salt` char(10) NOT NULL,
  `ForgotPasswordToken` char(50) DEFAULT NULL,
  `ForgotPasswordTokenExpireDate` datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`),
  KEY `UserName` (`UserName`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8;
SET FOREIGN_KEY_CHECKS=1;
