/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 100121
Source Host           : localhost:3306
Source Database       : mpstart

Target Server Type    : MYSQL
Target Server Version : 100121
File Encoding         : 65001

Date: 2017-05-18 23:39:25
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for siteconfig
-- ----------------------------
DROP TABLE IF EXISTS `siteconfig`;
CREATE TABLE `siteconfig` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(255) DEFAULT NULL,
  `content` text CHARACTER SET utf8,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of siteconfig
-- ----------------------------
INSERT INTO `siteconfig` VALUES ('1', 'site-name', 'MP START');
INSERT INTO `siteconfig` VALUES ('2', 'youtube-channel', 'UCk_tjguCCBsVqt4apOylvrQ');
INSERT INTO `siteconfig` VALUES ('3', 'company-footer-contact', '<strong>Tel</strong>: 04.6688.6613<br>\r\n<strong>Hot line</strong>: 096.567.9191<br>\r\n<strong>Email</strong>: mpstartvn@gmail.com<br>\r\n<strong>Opening</strong>: 7:00am - 21:30pm<br>');
INSERT INTO `siteconfig` VALUES ('4', 'company-fullname', 'Công ty CP Thương Mại & Công Nghệ MP Việt Nam');
INSERT INTO `siteconfig` VALUES ('5', 'company-email', 'mpstart@gmail.com');
INSERT INTO `siteconfig` VALUES ('6', 'facebook-page', 'smartphonedailoan290kimma');
INSERT INTO `siteconfig` VALUES ('7', 'site-logo', 'Content/Images/17354710.png');
INSERT INTO `siteconfig` VALUES ('8', 'company-name', 'MP START');
INSERT INTO `siteconfig` VALUES ('9', 'hotline', '0868.18.83.83 ');
INSERT INTO `siteconfig` VALUES ('12', 'company-header-address', '209 Phố Đội Cấn, Phường Đội Cấn, Ba Đình, Hà Nội');
INSERT INTO `siteconfig` VALUES ('13', 'gplus', 'xxx');
INSERT INTO `siteconfig` VALUES ('14', 'zalo', 'xxx');
INSERT INTO `siteconfig` VALUES ('15', 'product-text-view', 'MUA NGAY');
INSERT INTO `siteconfig` VALUES ('16', 'home-text-alias', 'Big Sale');
INSERT INTO `siteconfig` VALUES ('17', 'subscribe-news', 'Nhận Tin');
SET FOREIGN_KEY_CHECKS=1;
