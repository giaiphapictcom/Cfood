/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 100121
Source Host           : localhost:3306
Source Database       : mpstart

Target Server Type    : MYSQL
Target Server Version : 100121
File Encoding         : 65001

Date: 2017-05-18 23:41:27
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for news
-- ----------------------------
DROP TABLE IF EXISTS `news`;
CREATE TABLE `news` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(255) DEFAULT NULL,
  `Summary` varchar(255) DEFAULT NULL,
  `Image` varchar(255) DEFAULT NULL,
  `Detail` longtext,
  `TypeID` int(11) DEFAULT NULL,
  `Date` datetime DEFAULT NULL,
  `Status` bit(1) DEFAULT NULL,
  `Order` int(11) DEFAULT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `Keyword` varchar(255) DEFAULT NULL,
  `Views` int(11) DEFAULT NULL,
  `Featured` bit(1) DEFAULT NULL,
  `Slider` bit(1) DEFAULT NULL,
  `Hot` bit(1) DEFAULT NULL,
  `Fast` bit(1) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=214 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of news
-- ----------------------------
INSERT INTO `news` VALUES ('186', 'Tại thành phố Hồ Chí Minh', 'Tầng B1, Vincom Mega Mall Thảo Điền, 159-161 Xa Lộ Hà Nội,\nPhường Thảo Điền, Quận 2, Thành Phố Hồ Chí Minh.', '', '', '29', '2017-03-26 05:54:12', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('188', 'Giới thiệu về mpstart.vn', '', '', '', '54', '2017-03-27 12:38:08', '', '1', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('189', 'Quy chế hoạt động', '', '', '', '54', '2017-03-27 12:30:20', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('190', 'Trung tâm hỗ trợ khách hàng', '', '', '', '54', '2017-03-27 12:31:00', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('191', 'Liên hệ với MP Start', '', '', '', '54', '2017-03-27 12:36:10', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('192', 'Đăng ký trở thành nhà cung cấp', '', '', '', '54', '2017-03-27 12:37:01', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('193', 'Thông báo từ MP Start', '', '', '', '54', '2017-04-21 00:35:54', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('194', 'Kiểm tra đơn hàng', '', '', '', '55', '2017-03-27 12:39:39', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('195', 'Chính sách bảo mật thanh toán', '', '', '', '55', '2017-03-27 12:42:32', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('196', 'Chính sách thanh toán', '', '', '', '55', '2017-03-27 12:43:22', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('197', 'Chính sách giao hàng', '', '', '', '55', '2017-03-27 12:43:11', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('198', 'Chính sách đổi trả', '', '', '', '55', '2017-03-27 12:43:42', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('199', 'Chính sách bảo hành', '', '', '', '55', '2017-03-27 12:44:01', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('200', 'Câu hỏi thường gặp', '', '', '', '55', '2017-03-27 12:44:26', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('201', 'Liên kết web site', '', '', '', '56', '2017-03-27 12:45:07', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('202', 'Câu hỏi thường gặp', '', '', '', '56', '2017-03-27 12:45:46', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('203', 'Báo chí viết về chúng tôi', '', '', '', '56', '2017-03-27 12:47:43', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('204', 'Quy trình tuyển nhà phân phối', '', '', '', '56', '2017-03-27 12:48:47', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('205', 'Mô hình hoạt động', '', '', '', '56', '2017-03-27 12:49:27', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('207', 'Affiliate marketing', 'QjP3Ka08eT8', '/Content/Images/Upload/files/affiliate-la-gi.png', '<iframe frameborder=\"0\" height=\"315\" src=\"https://www.youtube.com/embed/QjP3Ka08eT8\" width=\"560\"></iframe>', '64', '2017-04-25 01:48:16', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('209', 'How to Start Affiliate Marketing in India', 'rkhjh_VtepA', '/Content/Images/Upload/files/0105(1).png', '<iframe width=\"560\" height=\"315\" src=\"https://www.youtube.com/embed/rkhjh_VtepA\" frameborder=\"0\" allowfullscreen></iframe>', '64', '2017-04-25 01:49:58', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('210', 'Zero To $1 Million On Amazon In 12 Months', '-Y35mxePPCc', '', '<iframe frameborder=\"0\" height=\"315\" src=\"https://www.youtube.com/embed/-Y35mxePPCc\" width=\"560\"></iframe>', '64', '2017-04-25 01:52:06', '', '0', '', '', '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('211', 'Sustainable Style: Just Lovely Handbags', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis dignissim erat ut laoreet pharetra. Proin mauris mi, egestas eget nibh sit amet, egestas vulputate dui. Sed egestas non sem at sagittis. Mauris augue metus, posuere at porttitor eget, auctor se', '/Content/Images/Upload/files/affiliate-la-gi.png', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis dignissim erat ut laoreet pharetra. Proin mauris mi, egestas eget nibh sit amet, egestas vulputate dui. Sed egestas non sem at sagittis. Mauris augue metus, posuere at porttitor eget, auctor sed tortor. In lobortis ligula vitae odio luctus, posuere luctus lectus...\r\n\r\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Duis dignissim erat ut laoreet pharetra. Proin mauris mi, egestas eget nibh sit amet, egestas vulputate dui. Sed egestas non sem at sagittis. Mauris augue metus, posuere at porttitor eget, auctor sed tortor. In lobortis ligula vitae odio luctus, posuere luctus lectus...\r\n\r\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Duis dignissim erat ut laoreet pharetra. Proin mauris mi, egestas eget nibh sit amet, egestas vulputate dui. Sed egestas non sem at sagittis. Mauris augue metus, posuere at porttitor eget, auctor sed tortor. In lobortis ligula vitae odio luctus, posuere luctus lectus...', '58', '2017-05-17 15:21:18', '', '0', null, null, '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('212', 'Aenean Egestas Mauris Eget Libero Porta', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis dignissim erat ut laoreet pharetra. Proin mauris mi, egestas eget nibh sit amet, egestas vulputate dui. Sed egestas non sem at sagittis. Mauris augue metus, posuere at porttitor eget, auctor se', '/Content/Images/Upload/files/affiliate-la-gi.png', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis dignissim erat ut laoreet pharetra. Proin mauris mi, egestas eget nibh sit amet, egestas vulputate dui. Sed egestas non sem at sagittis. Mauris augue metus, posuere at porttitor eget, auctor sed tortor. In lobortis ligula vitae odio luctus, posuere luctus lectus...\r\n\r\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Duis dignissim erat ut laoreet pharetra. Proin mauris mi, egestas eget nibh sit amet, egestas vulputate dui. Sed egestas non sem at sagittis. Mauris augue metus, posuere at porttitor eget, auctor sed tortor. In lobortis ligula vitae odio luctus, posuere luctus lectus...\r\n\r\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Duis dignissim erat ut laoreet pharetra. Proin mauris mi, egestas eget nibh sit amet, egestas vulputate dui. Sed egestas non sem at sagittis. Mauris augue metus, posuere at porttitor eget, auctor sed tortor. In lobortis ligula vitae odio luctus, posuere luctus lectus...', '58', '2017-05-17 15:23:12', '', '0', '', null, '0', '\0', '\0', '\0', '\0');
INSERT INTO `news` VALUES ('213', 'Etiam Porta Quam Dignissim Pretium', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis dignissim erat ut laoreet pharetra. Proin mauris mi, egestas eget nibh sit amet, egestas vulputate dui. Sed egestas non sem at sagittis. Mauris augue metus, posuere at porttitor eget, auctor se', '/Content/Images/Upload/files/affiliate-la-gi.png', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis dignissim erat ut laoreet pharetra. Proin mauris mi, egestas eget nibh sit amet, egestas vulputate dui. Sed egestas non sem at sagittis. Mauris augue metus, posuere at porttitor eget, auctor sed tortor. In lobortis ligula vitae odio luctus, posuere luctus lectus...\r\n\r\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Duis dignissim erat ut laoreet pharetra. Proin mauris mi, egestas eget nibh sit amet, egestas vulputate dui. Sed egestas non sem at sagittis. Mauris augue metus, posuere at porttitor eget, auctor sed tortor. In lobortis ligula vitae odio luctus, posuere luctus lectus...\r\n\r\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Duis dignissim erat ut laoreet pharetra. Proin mauris mi, egestas eget nibh sit amet, egestas vulputate dui. Sed egestas non sem at sagittis. Mauris augue metus, posuere at porttitor eget, auctor sed tortor. In lobortis ligula vitae odio luctus, posuere luctus lectus...', '58', '2017-05-17 15:24:31', '', '0', null, null, '0', '\0', '\0', '\0', '\0');
SET FOREIGN_KEY_CHECKS=1;
