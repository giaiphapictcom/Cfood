/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 100121
Source Host           : localhost:3306
Source Database       : mpstart

Target Server Type    : MYSQL
Target Server Version : 100121
File Encoding         : 65001

Date: 2017-05-18 23:41:20
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for newsgroups
-- ----------------------------
DROP TABLE IF EXISTS `newsgroups`;
CREATE TABLE `newsgroups` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Alias` varchar(250) DEFAULT NULL,
  `Name` varchar(250) DEFAULT NULL,
  `description` varchar(250) DEFAULT NULL,
  `Status` bit(1) DEFAULT NULL,
  `Number` int(11) DEFAULT NULL,
  `Visible` bit(1) DEFAULT NULL,
  `Parent` int(11) DEFAULT NULL,
  `Date` datetime DEFAULT NULL,
  `Level` varchar(256) DEFAULT NULL,
  `Link` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=67 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of newsgroups
-- ----------------------------
INSERT INTO `newsgroups` VALUES ('22', null, 'Mở Shop', null, '', '1', '\0', '0', '2016-12-03 11:16:37', '1000110009', 'http://sale.cleanfoodvn.vn');
INSERT INTO `newsgroups` VALUES ('23', null, 'Footer', null, '', '1', '', '0', null, null, null);
INSERT INTO `newsgroups` VALUES ('27', null, 'Cơ hội làm đại lý', null, '', '4', '', '23', null, null, null);
INSERT INTO `newsgroups` VALUES ('29', null, 'Danh Sách Đại Lý', null, '', '5', '', '23', '2017-03-27 12:50:11', null, null);
INSERT INTO `newsgroups` VALUES ('54', null, 'VỀ MP START', null, '', '1', null, '23', '2017-04-21 00:34:44', null, null);
INSERT INTO `newsgroups` VALUES ('55', null, 'CHĂM SÓC KHÁCH HÀNG', null, '', '2', '', '23', '2017-03-27 12:40:10', null, null);
INSERT INTO `newsgroups` VALUES ('56', null, 'HOẠT ĐỘNG MP START', null, '', '3', '', '23', '2017-03-27 12:41:20', null, null);
INSERT INTO `newsgroups` VALUES ('57', null, 'DIỄN ĐÀN & RAO VẶT', null, '', '0', '\0', '0', '2017-05-03 21:16:02', '1000110010', 'dien-dan-dao-vat.html');
INSERT INTO `newsgroups` VALUES ('58', null, 'TIN TỨC', null, '', '0', '\0', '0', '2017-03-27 12:19:12', '1000110011', 'tin-tuc.html');
INSERT INTO `newsgroups` VALUES ('59', null, 'MP AFFILIATE', null, '', '0', '\0', '0', '2017-03-27 12:20:24', '1000110012', 'http://affiliate.mpstart.vn/');
INSERT INTO `newsgroups` VALUES ('60', null, 'LIÊN HỆ ', null, '', '0', '\0', '0', '2017-05-03 21:15:29', '1000110013', 'lien-he.html');
INSERT INTO `newsgroups` VALUES ('61', null, 'CƠ HỘI HỢP TÁC', null, '', '0', '\0', '0', '2017-03-27 12:23:38', '1000110014', '');
INSERT INTO `newsgroups` VALUES ('62', null, 'LIÊN HỆ', null, '', '0', '\0', '0', '2017-03-27 12:24:27', '1000110015', '');
INSERT INTO `newsgroups` VALUES ('64', null, 'Video', null, '', null, null, '0', null, null, 'video.html');
INSERT INTO `newsgroups` VALUES ('65', 'affiliate-video', 'Affiliate Video', '<span style=\"color:#fd393d;\">Afiliate MP Start</span> một mô hình đột phá và tổng hợp của các hình thái Online hứa hẹn sẽ tạo nên kỳ tích trong các Star Up Việt', '', '0', '\0', '0', '2017-05-07 23:56:41', '1000110016', '');
INSERT INTO `newsgroups` VALUES ('66', 'affiliate-news', 'Affiliate Bài Viết', '', '', '0', '\0', '0', '2017-05-07 23:57:35', '1000110017', '');
SET FOREIGN_KEY_CHECKS=1;
