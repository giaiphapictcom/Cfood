using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace V308CMS.Common
{
    public static class AppSetting
    {

        public static int goldcanbangtainguyen = ValidateInput.IsInt(ConfigurationManager.AppSettings["goldcanbangtainguyen"]);



        //thoi gian dung thay doi cho viec xay dung, nang cap, xin quan...
        public static double TimeWork = ValidateInput.IsDouble(ConfigurationManager.AppSettings["TimeWork"]);
        public static double TimeTrainingArmy = ValidateInput.IsDouble(ConfigurationManager.AppSettings["TimeTrainingArmy"]);
        public static double TimeResearchArmy = ValidateInput.IsDouble(ConfigurationManager.AppSettings["TimeResearchArmy"]);
        public static double TimeUpdateArmy = ValidateInput.IsDouble(ConfigurationManager.AppSettings["TimeUpdateArmy"]);
        public static double TimeMoveLand = ValidateInput.IsDouble(ConfigurationManager.AppSettings["TimeMoveLand"]);
        public static string username = ConfigurationManager.AppSettings["username"];
        public static string password = ConfigurationManager.AppSettings["password"];
        public static string ServerId = ConfigurationManager.AppSettings["ServerId"];
        public static string GameCode = ConfigurationManager.AppSettings["GameCode"];//GameKey
        public static string GameKey = ConfigurationManager.AppSettings["GameKey"];
        public static string URL = ConfigurationManager.AppSettings["URL"];
        public static string G10 = ConfigurationManager.AppSettings["G10"];
        public static string G30 = ConfigurationManager.AppSettings["G30"];
        public static string G100 = ConfigurationManager.AppSettings["G100"];
        public static string G250 = ConfigurationManager.AppSettings["G250"];
        public static string G600 = ConfigurationManager.AppSettings["G600"];
        public static string G1000 = ConfigurationManager.AppSettings["G1000"];
        public static string G2000 = ConfigurationManager.AppSettings["G2000"];
        public static string URLPC = ConfigurationManager.AppSettings["URLPC"];
        public static string Version = ConfigurationManager.AppSettings["version"];
        public static int IdDemo = Convert.ToInt32(ConfigurationManager.AppSettings["IdDemo"]);
        public static int IdAdmin = Convert.ToInt32(ConfigurationManager.AppSettings["IdAdmin"]);
        public static int BaoVeTanThu = Convert.ToInt32(ConfigurationManager.AppSettings["BaoVeTanThu"]);
        public static int LandDemo = Convert.ToInt32(ConfigurationManager.AppSettings["LandDemo"]);
        public static int HeroDemo = Convert.ToInt32(ConfigurationManager.AppSettings["HeroDemo"]);
        public static int LeagueDemo = Convert.ToInt32(ConfigurationManager.AppSettings["LeagueDemo"]);
        public static int GMID = Convert.ToInt32(ConfigurationManager.AppSettings["GMID"]);
        public static string privateKey = ConfigurationManager.AppSettings["privateKeyGame"];
        public static string URLID = ConfigurationManager.AppSettings["URLID"];

        public static string MaxPossition = ConfigurationManager.AppSettings["MaxPossition"];
        public static int DinhChien = Convert.ToInt32(ConfigurationManager.AppSettings["DinhChien"]);
        public static int DinhCho = Convert.ToInt32(ConfigurationManager.AppSettings["DinhCho"]);

        public static string partnerid = ConfigurationManager.AppSettings["partnerid"];
        public static string PartnerCode = ConfigurationManager.AppSettings["PartnerCode"];

        public static string ServerName = ConfigurationManager.AppSettings["ServerName"];
        //thoi gian hoa binh bao vẹ tan thu
        public static int TimePeace = ValidateInput.IsInt(ConfigurationManager.AppSettings["TimePeace"]);
        public static double demolitionWorks = 0.05;
        public static double demolitionWorksTime = 0.5;
        public static double[] arrayStrong_ThuongHoi = { 0, 0.10, 0.20, 0.30, 0.40, 0.50, 0.60, 0.70, 0.80, 0.90, 1.00, 1.10, 1.20, 1.30, 1.40, 1.50, 1.60, 1.70, 1.80, 1.90, 2.00 };
        public static double[] arrayStrong_ThuongHoi_PhuSa = { 0, 0.20, 0.40, 0.60, 0.80, 0.10, 1.20, 1.40, 1.60, 1.80, 2.00, 2.20, 2.40, 2.60, 2.80, 3.00, 3.20, 3.40, 3.60, 3.80, 4.00 };
        public static int[] arrayPositionBuil = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 };
        public static double[] arrayTimeByNha_Chinh = { 5, 1, 0.96, 0.93, 0.9, 0.86, 0.83, 0.8, 0.77, 0.75, 0.72, 0.69, 0.67, 0.64, 0.62, 0.6, 0.58, 0.56, 0.54, 0.52, 0.5 };
        //sức chứa tài nguyên
        public static int[] arrayResource_Kho_Hang = { 1100, 1200, 1700, 2300, 3100, 4000, 5000, 6300, 7800, 9600, 11800, 14400, 17600, 21400, 25900, 31300, 37900, 45700, 55100, 66400, 80000 };
        public static int[] arrayResource_Kho_Hang_Lon = { 0, 3600, 5100, 6900, 9300, 12000, 15000, 18900, 23400, 28800, 35400, 43200, 52800, 64200, 77700, 93900, 113700, 137100, 165300, 199200, 240000 };
        //sức chứa lúa
        public static int[] arrayRice_Vua_Lua = { 1100, 1200, 1700, 2300, 3100, 4000, 5000, 6300, 7800, 9600, 11800, 14400, 17600, 21400, 25900, 31300, 37900, 45700, 54800, 66400, 80000 };
        public static int[] arrayRice_Vua_Lua_Lon = { 0, 3600, 5100, 6900, 9300, 12000, 15000, 18900, 23400, 28800, 35400, 43200, 52800, 64200, 77700, 93900, 113700, 137100, 165300, 199200, 240000 };
        //sức chứa hầm ngầm
        public static int[] array_HamNgam_ThuongSon = { 0, 160, 208, 272, 352, 448, 576, 736, 960, 1232, 1600 };
        public static int[] array_HamNgam_CanHai_PhuSa = { 0, 80, 104, 136, 176, 224, 288, 368, 480, 616, 800 };
        //phòng thủ tường thành
        public static double[] arrayDeferen_TuongThanh_CanHai = { 0, 0.02, 0.04, 0.06, 0.08, 0.1, 0.13, 0.15, 0.17, 0.2, 0.22, 0.24, 0.27, 0.29, 0.32, 0.35, 0.37, 0.4, 0.43, 0.46, 0.49 };
        public static double[] arrayDeferen_TuongThanh_PhuSa = { 0, 0.03, 0.06, 0.09, 0.13, 0.16, 0.19, 0.23, 0.27, 0.3, 0.34, 0.38, 0.43, 0.47, 0.51, 0.56, 0.6, 0.65, 0.7, 0.75, 0.81 };
        public static double[] arrayDeferen_TuongThanh_ThuongSon = { 0, 0.03, 0.05, 0.08, 0.1, 0.13, 0.16, 0.19, 0.22, 0.25, 0.28, 0.31, 0.34, 0.38, 0.41, 0.45, 0.48, 0.52, 0.56, 0.6, 0.64 };
        //thời gian đào tạo của nhà công xưởng
        public static double[] arrayTrainingTimeBy_CongXuong = { 0, 1, 0.9, 0.81, 0.73, 0.66, 0.59, 0.53, 0.48, 0.43, 0.39, 0.35, 0.31, 0.28, 0.25, 0.23, 0.21, 0.19, 0.17, 0.15, 0.14 };
       //thời gian đào tạo tại trại lính
        public static double[] arrayTraining_TraiLinh = { 0, 1, 0.9, 0.81, 0.73, 0.66, 0.59, 0.53, 0.48, 0.43, 0.39, 0.35, 0.31, 0.28, 0.25, 0.23, 0.21, 0.19, 0.17, 0.15, 0.14 };
        //thời gian đào tạo tại đại binh doanh
        public static double[] arrayTraining_DaiBinhDoanh = { 0, 1, 0.9, 0.81, 0.73, 0.66, 0.59, 0.53, 0.48, 0.43, 0.39, 0.35, 0.32, 0.28, 0.25, 0.23, 0.21, 0.19, 0.17, 0.15, 0.14 };
        //thời gian đào tạo tại kỵ quân doanh
        public static double[] arrayTraining_KyQuanDoanh = { 0, 1, 0.9, 0.81, 0.73, 0.66, 0.59, 0.53, 0.48, 0.43, 0.39, 0.35, 0.32, 0.28, 0.25, 0.23, 0.21, 0.19, 0.17, 0.15, 0.14 };
        //thời gian đào tạo tại huấn kỵ doanh
        public static double[] arrayTraining_HuanKyDoanh = { 0, 1, 0.9, 0.81, 0.73, 0.66, 0.59, 0.53, 0.48, 0.43, 0.39, 0.35, 0.32, 0.28, 0.25, 0.23, 0.21, 0.19, 0.17, 0.15, 0.14 };
        //thoi gian dao tao ma truong
        public static double[] arrayTraining_MaTruong = { 0, 0.01, 0.02, 0.03, 0.04, 0.05, 0.06, 0.07, 0.08, 0.09, 0.1, 0.12, 0.13, 0.14, 0.15, 0.16, 0.17, 0.18, 0.19, 0.20, 0.14 };
        //tăng tốc độ di chuyển bởi lôi đài
        public static double[] arraySpeed_LoiDai = { 1, 1.1, 1.2, 1.3, 1.4, 1.5, 1.6, 1.7, 1.8, 1.9, 2, 2.1, 2.2, 2.3, 2.4, 2.5, 2.6, 2.7, 2.8, 2.9, 3 };
        //gia tăng tấn công bởi lò nấu rượu
        public static double[] arrayAttack_XuongNauRuou = { 0, 0.01, 0.02, 0.03, 0.04, 0.05, 0.06, 0.07, 0.08, 0.09, 0.1 };
        //sức bền công trình
        public static double[] arrayStrengthWork_XuongCatDa = { 0, 1.1, 1.2, 1.3, 1.4, 1.5, 1.6, 1.7, 1.8, 1.9, 2, 2.1, 2.2, 2.3, 2.4, 2.5, 2.6, 2.7, 2.8, 2.9, 3 };
       
        
        //thoi gian xin Bay trong xuong bay theo level
        public static double[] arrayTrapTraining = { 0, 1, 0.9, 0.81, 0.73, 0.66, 0.59, 0.53, 0.48, 0.43, 0.39, 0.35, 0.31, 0.28, 0.25, 0.23, 0.21, 0.19, 0.17, 0.15, 0.14 };
        //lò nướng bánh gia tăng sản lượng
        public static double[] arrayRice_LoNuongBanh = { 0, 0.05, 0.1, 0.15, 0.2, 0.25 };
        public static double[] arrayRice_NhaXayXat = { 0, 0.05, 0.1, 0.15, 0.2, 0.25 };
        public static double[] arrayRock_LoNung = { 0, 0.05, 0.1, 0.15, 0.2, 0.25 };
        public static double[] arrayMetal_LoLuyenKim = { 0, 0.05, 0.1, 0.15, 0.2, 0.25 };
        public static double[] arrayWood_XuongMoc = { 0, 0.05, 0.1, 0.15, 0.2, 0.25 };

        public static int[] array_SanLuong_lua = { 2, 5, 9, 15, 22, 33, 50, 70, 100, 145, 200, 280, 375, 495, 635, 800, 1000, 1300, 1600, 2000, 2450 };
        public static int[] array_SanLuong_Go = { 2, 5, 9, 15, 22, 33, 50, 70, 100, 145, 200, 280, 375, 495, 635, 800, 1000, 1300, 1600, 2000, 2450 };
        public static int[] array_SanLuong_Dat = { 2, 5, 9, 15, 22, 33, 50, 70, 100, 145, 200, 280, 375, 495, 635, 800, 1000, 1300, 1600, 2000, 2450 };
        public static int[] array_SanLuong_Sat = { 2, 5, 9, 15, 22, 33, 50, 70, 100, 145, 200, 280, 375, 495, 635, 800, 1000, 1300, 1600, 2000, 2450 };
        public static int GoldFastBuild = 2;
        public static int GoldExchangeNPC = 3;
        public static int GoldFastUpdateArmy = 5;

        public static int[] array_SobayTheoCap = { 0, 10, 22, 35, 49, 64, 80, 97, 115, 134, 154, 175, 197, 218, 241, 265, 290, 316, 343, 371, 400 };

        //thời gian rao bán tính theo giờ
        public static int MaxTimeMarket = 24;
        public static int MinForeSaleResource = 100;

        public static string FestivalData = "[{\"Wood\":\"6400\",\"Rock\":\"6650\",\"Metal\":\"5940\",\"Food\":\"1340\",\"Culture\":\"500\",\"Time\":\"86400\",\"Level\":\"5\"},{\"Wood\":\"29700\",\"Rock\":\"33250\",\"Metal\":\"32000\",\"Food\":\"6700\",\"Culture\":\"2000\",\"Time\":\"216000\",\"Level\":\"10\"}]";

        public static int[] FestivalTimeSmall = {0, 86400, 83290, 80291, 77401, 74614, 71928, 69339, 66843, 64436, 62117, 59880, 57725, 55647, 53643, 51712, 49850, 48056, 3126, 44658, 43050 };
        public static int[] FestivalTimeLarge = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 155291, 149701, 144312, 139116, 134108, 129280, 124626, 120140, 115815, 111645, 107626 };
        //diem van hoa de mo lang moi
        public static int[] CultureBeginVillage = { 0, 2000, 8000, 20000, 39000, 65000, 99000, 141000, 191000, 251000, 319000, 397000, 486000, 584000, 692000, 811000, 941000, 1082000, 1234000, 1397000, 1572000, 1759000, 1957000, 2168000, 2391000, 2627000, 2874000, 3135000, 3409000, 3695000, 3995000, 4308000, 4634000, 4974000, 5327000, 5695000, 6076000, 6471000, 6881000, 7304000, 7742000, 8195000, 8662000, 9143000, 9640000, 10151000, 10677000, 11219000, 11775000, 12347000, 12935000, 13537000, 14156000, 14790000, 15439000, 16105000, 16786000, 17484000, 18197000, 18927000, 19673000, 20435000, 21214000, 22009000, 22821000, 23649000, 24495000, 25357000, 26236000, 27131000, 28044000, 28974000, 29922000, 30886000, 31868000, 32867000, 33884000, 34918000, 35970000, 37039000, 38127000, 39232000, 40354000, 41495000, 42654000, 43831000, 45026000, 46240000, 47471000, 48721000, 49989000, 51276000, 52581000, 53905000, 55248000, 56609000, 57989000, 59387000, 60805000, 62242000, 63697000, 65172000, 66665000, 68178000, 69710000, 71262000, 72832000, 74422000, 76032000, 77661000, 79309000, 80977000, 82665000, 84372000, 86100000, 87847000, 89613000, 91400000, 93207000, 95034000, 96881000, 98748000, 100635000, 102542000, 104470000 };       
    }

    public static class NPCSetting
    {
        public static double TimeWork = Convert.ToDouble(ConfigurationManager.AppSettings["TimeAdd"]);
    }
}