/*Moi Chi Tiet Xin Lien He :
   HA VIET HOANG
   HAHOANG611990@YAHOO.COM */


using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using V308CMS.Data.Models;

namespace V308CMS.Data
{
    #region[Bat dau 1  class tblAccount]
    [Table("contact")]
    public class Contact
    {
        private int _Id;
        private string _FullName;
        private string _Email;
        private string _Phone;
        private string _Message;
        private DateTime? _CreatedDate;
        [Key]
        public int ID { get { return _Id; } set { _Id = value; } }
        public string FullName { get { if (String.IsNullOrEmpty(_FullName)) return ""; else return _FullName; } set { _FullName = value; } }
        public string Email { get { if (String.IsNullOrEmpty(_Email)) return ""; else return _Email; } set { _Email = value; } }
        public string Phone { get { if (String.IsNullOrEmpty(_Phone)) return ""; else return _Phone; } set { _Phone = value; } }
        public string Message { get { if (String.IsNullOrEmpty(_Message)) return ""; else return _Message; } set { _Message = value; } }
        public DateTime? CreatedDate { get { if (_CreatedDate == null) return new DateTime(); else return _CreatedDate; } set { if (_CreatedDate != value) { _CreatedDate = value; } } }
    }


        [Table("account")]
    public class Account
    {

        #region[Declare variables]
        private int _ID;
        private string _UserName;
        private string _Password;
        private string _FullName;
        private string _Email;
        private string _Address;
        private string _Phone;
        private bool? _Gender;
        private DateTime? _BirthDay;
        private bool? _Status;
        private string _Avata;
        private int? _Role;
        private DateTime? _Date;
        private string _Salt;
        private string _Token;
        private DateTime _TokenExpireDate;

        private string _ForgotPasswordToken;
        private DateTime __ForgotPasswordTokenExpireDate;

        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string UserName { get { if (String.IsNullOrEmpty(_UserName)) return ""; else return _UserName; } set { _UserName = value; } }
        public string Password { get { if (String.IsNullOrEmpty(_Password)) return ""; else return _Password; } set { _Password = value; } }
        public string FullName { get { if (String.IsNullOrEmpty(_FullName)) return ""; else return _FullName; } set { _FullName = value; } }
        public string Email { get { if (String.IsNullOrEmpty(_Email)) return ""; else return _Email; } set { _Email = value; } }
        public string Address { get { if (String.IsNullOrEmpty(_Address)) return ""; else return _Address; } set { _Address = value; } }
        public string Phone { get { if (String.IsNullOrEmpty(_Phone)) return ""; else return _Phone; } set { _Phone = value; } }
        public bool? Gender { get { if (_Gender == null) return false; else return _Gender; } set { if (_Gender != value) { _Gender = value; } } }
        public DateTime? BirthDay { get { if (_BirthDay == null) return new DateTime(); else return _BirthDay; } set { if (_BirthDay != value) { _BirthDay = value; } } }
        public bool? Status { get { if (_Status == null) return false; else return _Status; } set { if (_Status != value) { _Status = value; } } }
        public string Avata { get { if (String.IsNullOrEmpty(_Avata)) return ""; else return _Avata; } set { _Avata = value; } }
        public int? Role { get { if (_Role == null || _Role < 0) return 0; else return _Role; } set { if (_Role != value) { if (_Role < 0) _Role = 0; else _Role = value; } } }
        public DateTime? Date { get { if (_Date == null) return new DateTime(); else return _Date; } set { if (_Date != value) { _Date = value; } } }     
        public string Salt { get { if (String.IsNullOrEmpty(_Salt)) return ""; else return _Salt; } set { _Salt = value; } }
        public string Token { get { if (String.IsNullOrEmpty(_Token)) return ""; else return _Token; } set { _Token = value; } }
        public DateTime TokenExpireDate { get { if (_TokenExpireDate == null) return new DateTime(); else return _TokenExpireDate; } set { if (_TokenExpireDate != value) { _TokenExpireDate = value; } } }
        public string ForgotPasswordToken { get { if (String.IsNullOrEmpty(_ForgotPasswordToken)) return ""; else return _ForgotPasswordToken; } set { _ForgotPasswordToken = value; } }
        public DateTime ForgotPasswordTokenExpireDate { get { if (__ForgotPasswordTokenExpireDate == null) return new DateTime(); else return __ForgotPasswordTokenExpireDate; } set { if (__ForgotPasswordTokenExpireDate != value) { __ForgotPasswordTokenExpireDate = value; } } }


        #endregion

    }
    #endregion[ket thuc class tblAccount]


    #region[Bat dau 1  class tblMarket]

    [Table("market")]
    public class Market
    {

        #region[Declare variables]
        private int _ID;
        private string _UserName;
        private string _Password;
        private string _FullName;
        private string _Email;
        private string _Address;
        private string _Phone;
        private bool? _Gender;
        private DateTime? _BirthDay;
        private bool? _Status;
        private string _Avata;
        private string _Sumary;
        private int? _Role;
        private int? _UserId;
        private int? _Number;
        private DateTime? _Date;


        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string UserName { get { if (String.IsNullOrEmpty(_UserName)) return ""; else return _UserName; } set { _UserName = value; } }
        public string Password { get { if (String.IsNullOrEmpty(_Password)) return ""; else return _Password; } set { _Password = value; } }
        public string FullName { get { if (String.IsNullOrEmpty(_FullName)) return ""; else return _FullName; } set { _FullName = value; } }
        public string Email { get { if (String.IsNullOrEmpty(_Email)) return ""; else return _Email; } set { _Email = value; } }
        public string Address { get { if (String.IsNullOrEmpty(_Address)) return ""; else return _Address; } set { _Address = value; } }
        public string Phone { get { if (String.IsNullOrEmpty(_Phone)) return ""; else return _Phone; } set { _Phone = value; } }
        public bool? Gender { get { if (_Gender == null) return false; else return _Gender; } set { if (_Gender != value) { _Gender = value; } } }
        public DateTime? BirthDay { get { if (_BirthDay == null) return new DateTime(); else return _BirthDay; } set { if (_BirthDay != value) { _BirthDay = value; } } }
        public bool? Status { get { if (_Status == null) return false; else return _Status; } set { if (_Status != value) { _Status = value; } } }
        public string Avata { get { if (String.IsNullOrEmpty(_Avata)) return ""; else return _Avata; } set { _Avata = value; } }
        public int? Role { get { if (_Role == null || _Role < 0) return 0; else return _Role; } set { if (_Role != value) { if (_Role < 0) _Role = 0; else _Role = value; } } }
        public int? Number { get { if (_Number == null || _Number < 0) return 0; else return _Number; } set { if (_Number != value) { if (_Number < 0) _Number = 0; else _Number = value; } } }
        public string Sumary { get { if (String.IsNullOrEmpty(_Sumary)) return ""; else return _Sumary; } set { _Sumary = value; } }
        public int? UserId { get { if (_UserId == null || _UserId < 0) return 0; else return _UserId; } set { if (_UserId != value) { if (_UserId < 0) _UserId = 0; else _UserId = value; } } }
        
        public DateTime? Date { get { if (_Date == null) return new DateTime(); else return _Date; } set { if (_Date != value) { _Date = value; } } }


        #endregion

    }
    #endregion[ket thuc class tblMarket]


    #region[Bat dau 1  class tblMarketProductType]

    [Table("marketproducttype")]
    public class MarketProductType
    {

        #region[Declare variables]
        private int _ID;
        private string _Name;
        private string _Detail;
        private string _Description;
        private string _Image;
        private int? _Number;
        private bool? _Visible;
        private bool? _Status;
        private int? _Parent;
        private int? _MarketId;
        private DateTime? _Date;
        private string _Level;
        private string _ImageBanner;
        private string _MarketName;
        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { if (String.IsNullOrEmpty(_Name)) return ""; else return _Name; } set { _Name = value; } }
        public string Detail { get { if (String.IsNullOrEmpty(_Detail)) return ""; else return _Detail; } set { _Detail = value; } }
        public string Description { get { if (String.IsNullOrEmpty(_Description)) return ""; else return _Description; } set { _Description = value; } }
        public string MarketName { get { if (String.IsNullOrEmpty(_MarketName)) return ""; else return _MarketName; } set { _MarketName = value; } }

        public string Image { get { if (String.IsNullOrEmpty(_Image)) return ""; else return _Image; } set { _Image = value; } }
        public int? Number { get { if (_Number == null || _Number < 0) return 0; else return _Number; } set { if (_Number != value) { if (_Number < 0) _Number = 0; else _Number = value; } } }
        public bool? Visible { get { if (_Visible == null) return false; else return _Visible; } set { if (_Visible != value) { _Visible = value; } } }
        public bool? Status { get { if (_Status == null) return false; else return _Status; } set { if (_Status != value) { _Status = value; } } }
        public int? Parent { get { if (_Parent == null || _Parent < 0) return 0; else return _Parent; } set { if (_Parent != value) { if (_Parent < 0) _Parent = 0; else _Parent = value; } } }
        public int? MarketId { get { if (_MarketId == null || _MarketId < 0) return 0; else return _MarketId; } set { if (_MarketId != value) { if (_MarketId < 0) _MarketId = 0; else _MarketId = value; } } }

        public DateTime? Date { get { if (_Date == null) return new DateTime(); else return _Date; } set { if (_Date != value) { _Date = value; } } }
        public string Level { get { if (String.IsNullOrEmpty(_Level)) return ""; else return _Level; } set { _Level = value; } }
        public string ImageBanner { get { if (String.IsNullOrEmpty(_ImageBanner)) return ""; else return _ImageBanner; } set { _ImageBanner = value; } }


        #endregion

    }
    #endregion[ket thuc class tblMarketProductType]



    #region[Bat dau 1  class tblAdmin]

    [Table("admin")]
    public class Admin
    {

        #region[Declare variables]
        private int _ID;
        private string _UserName;
        private string _Password;
        private string _Email;
        private string _FullName;
        private bool? _OrderRequest;
        private int? _Role;
        private DateTime? _Date;
        private bool? _Status;
        private bool? _PSanPham;
        private bool? _PTinTuc;
        private bool? _PKhachHang;
        private bool? _PHinhAnh;
        private bool? _PFileUpload;
        private bool? _PTaiKhoan;
        private bool? _PHeThong;
        private bool? _PThungRac;
        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string UserName { get { if (String.IsNullOrEmpty(_UserName)) return ""; else return _UserName; } set { _UserName = value; } }
        public string Password { get { if (String.IsNullOrEmpty(_Password)) return ""; else return _Password; } set { _Password = value; } }
        public string Email { get { if (String.IsNullOrEmpty(_Email)) return ""; else return _Email; } set { _Email = value; } }
        public string FullName { get { if (String.IsNullOrEmpty(_FullName)) return ""; else return _FullName; } set { _FullName = value; } }
        public bool? OrderRequest { get { if (_OrderRequest == null) return false; else return _OrderRequest; } set { if (_OrderRequest != value) { _OrderRequest = value; } } }
        //public int? Role { get { if (_Role == null || _Role < 0) return 0; else return _Role; } set { if (_Role != value) { if (_Role < 0) _Role = 0; else _Role = value; } } }
        public int? Role { get { return int.Parse(_Role.ToString()); } set { _Role = value; } }
        public DateTime? Date { get { if (_Date == null) return new DateTime(); else return _Date; } set { if (_Date != value) { _Date = value; } } }
        public bool? Status { get { if (_Status == null) return false; else return _Status; } set { if (_Status != value) { _Status = value; } } }
        public bool? PSanPham { get { if (_PSanPham == null) return false; else return _PSanPham; } set { if (_PSanPham != value) { _PSanPham = value; } } }
        public bool? PTinTuc { get { if (_PTinTuc == null) return false; else return _PTinTuc; } set { if (_PTinTuc != value) { _PTinTuc = value; } } }
        public bool? PKhachHang { get { if (_PKhachHang == null) return false; else return _PKhachHang; } set { if (_PKhachHang != value) { _PKhachHang = value; } } }
        public bool? PHinhAnh { get { if (_PHinhAnh == null) return false; else return _PHinhAnh; } set { if (_PHinhAnh != value) { _PHinhAnh = value; } } }
        public bool? PFileUpload { get { if (_PFileUpload == null) return false; else return _PFileUpload; } set { if (_PFileUpload != value) { _PFileUpload = value; } } }
        public bool? PTaiKhoan { get { if (_PTaiKhoan == null) return false; else return _PTaiKhoan; } set { if (_PTaiKhoan != value) { _PTaiKhoan = value; } } }
        public bool? PHeThong { get { if (_PHeThong == null) return false; else return _PHeThong; } set { if (_PHeThong != value) { _PHeThong = value; } } }
        public bool? PThungRac { get { if (_PThungRac == null) return false; else return _PThungRac; } set { if (_PThungRac != value) { _PThungRac = value; } } }



        #endregion

    }
    #endregion[ket thuc class tblAdmin]



    #region[Bat dau 1  class tblComment]

    [Table("comment")]
    public class Comment
    {

        #region[Declare variables]
        private int _ID;
        private int? _ProductID;
        private int? _AccountID;
        private string _Detail;
        private bool? _Status;
        private int? _AdminId;
        private DateTime? _Date;



        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public int? ProductID { get { if (_ProductID == null || _ProductID < 0) return 0; else return _ProductID; } set { if (_ProductID != value) { if (_ProductID < 0) _ProductID = 0; else _ProductID = value; } } }
        public int? AccountID { get { if (_AccountID == null || _AccountID < 0) return 0; else return _AccountID; } set { if (_AccountID != value) { if (_AccountID < 0) _AccountID = 0; else _AccountID = value; } } }
        public string Detail { get { if (String.IsNullOrEmpty(_Detail)) return ""; else return _Detail; } set { _Detail = value; } }
        public bool? Status { get { if (_Status == null) return false; else return _Status; } set { if (_Status != value) { _Status = value; } } }
        public int? AdminId { get { if (_AdminId == null || _AdminId < 0) return 0; else return _AdminId; } set { if (_AdminId != value) { if (_AdminId < 0) _AdminId = 0; else _AdminId = value; } } }
        public DateTime? Date { get { if (_Date == null) return new DateTime(); else return _Date; } set { if (_Date != value) { _Date = value; } } }


        #endregion

    }
    #endregion[ket thuc class tblComment]



    #region[Bat dau 1  class tblConfig]

    [Table("config")]
    public class Config
    {

        #region[Declare variables]
        private int _ID;
        private string _Mail_Smtp;
        private int? _Mail_Port;
        private string _Mail_Info;
        private string _Mail_Noreply;
        private string _Mail_Password;
        private string _PlaceHead;
        private string _PlaceBody;
        private string _GoogleId;
        private string _Contact;
        private string _Copyright;
        private string _Title;
        private string _Description;
        private string _Keyword;
        private string _Lang;



        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Mail_Smtp { get { if (String.IsNullOrEmpty(_Mail_Smtp)) return ""; else return _Mail_Smtp; } set { _Mail_Smtp = value; } }
        public int? Mail_Port { get { if (_Mail_Port == null || _Mail_Port < 0) return 0; else return _Mail_Port; } set { if (_Mail_Port != value) { if (_Mail_Port < 0) _Mail_Port = 0; else _Mail_Port = value; } } }
        public string Mail_Info { get { if (String.IsNullOrEmpty(_Mail_Info)) return ""; else return _Mail_Info; } set { _Mail_Info = value; } }
        public string Mail_Noreply { get { if (String.IsNullOrEmpty(_Mail_Noreply)) return ""; else return _Mail_Noreply; } set { _Mail_Noreply = value; } }
        public string Mail_Password { get { if (String.IsNullOrEmpty(_Mail_Password)) return ""; else return _Mail_Password; } set { _Mail_Password = value; } }
        public string PlaceHead { get { if (String.IsNullOrEmpty(_PlaceHead)) return ""; else return _PlaceHead; } set { _PlaceHead = value; } }
        public string PlaceBody { get { if (String.IsNullOrEmpty(_PlaceBody)) return ""; else return _PlaceBody; } set { _PlaceBody = value; } }
        public string GoogleId { get { if (String.IsNullOrEmpty(_GoogleId)) return ""; else return _GoogleId; } set { _GoogleId = value; } }
        public string Contact { get { if (String.IsNullOrEmpty(_Contact)) return ""; else return _Contact; } set { _Contact = value; } }
        public string Copyright { get { if (String.IsNullOrEmpty(_Copyright)) return ""; else return _Copyright; } set { _Copyright = value; } }
        public string Title { get { if (String.IsNullOrEmpty(_Title)) return ""; else return _Title; } set { _Title = value; } }
        public string Description { get { if (String.IsNullOrEmpty(_Description)) return ""; else return _Description; } set { _Description = value; } }
        public string Keyword { get { if (String.IsNullOrEmpty(_Keyword)) return ""; else return _Keyword; } set { _Keyword = value; } }
        public string Lang { get { if (String.IsNullOrEmpty(_Lang)) return ""; else return _Lang; } set { _Lang = value; } }


        #endregion

    }
    #endregion[ket thuc class tblConfig]



    #region[Bat dau 1  class tblDomain]

    [Table("domain")]
    public class Domain
    {

        #region[Declare variables]
        private int _ID;
        private string _DomainLink;
        private int? _TypeProductID;
        private string _FileCss;



        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string DomainLink { get { if (String.IsNullOrEmpty(_DomainLink)) return ""; else return _DomainLink; } set { _DomainLink = value; } }
        public int? TypeProductID { get { if (_TypeProductID == null || _TypeProductID < 0) return 0; else return _TypeProductID; } set { if (_TypeProductID != value) { if (_TypeProductID < 0) _TypeProductID = 0; else _TypeProductID = value; } } }
        public string FileCss { get { if (String.IsNullOrEmpty(_FileCss)) return ""; else return _FileCss; } set { _FileCss = value; } }


        #endregion

    }
    #endregion[ket thuc class tblDomain]



    #region[Bat dau 1  class tblFile]

    [Table("file")]
    public class File
    {

        #region[Declare variables]
        private int _ID;
        private string _Title;
        private string _Summary;
        private string _FileName;
        private int? _TypeID;
        private int? _Number;
        private DateTime? _Date;
        private string _LinkFile;
        private int? _Value;
        private bool? _Status;
        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Title { get { if (String.IsNullOrEmpty(_Title)) return ""; else return _Title; } set { _Title = value; } }
        public string Summary { get { if (String.IsNullOrEmpty(_Summary)) return ""; else return _Summary; } set { _Summary = value; } }
        public string FileName { get { if (String.IsNullOrEmpty(_FileName)) return ""; else return _FileName; } set { _FileName = value; } }
        public int? TypeID { get { if (_TypeID == null || _TypeID < 0) return 0; else return _TypeID; } set { if (_TypeID != value) { if (_TypeID < 0) _TypeID = 0; else _TypeID = value; } } }
        public int? Number { get { if (_Number == null || _Number < 0) return 0; else return _Number; } set { if (_Number != value) { if (_Number < 0) _Number = 0; else _Number = value; } } }
        public DateTime? Date { get { if (_Date == null) return new DateTime(); else return _Date; } set { if (_Date != value) { _Date = value; } } }
        public string LinkFile { get { if (String.IsNullOrEmpty(_LinkFile)) return ""; else return _LinkFile; } set { _LinkFile = value; } }
        public int? Value { get { if (_Value == null || _Value < 0) return 0; else return _Value; } set { if (_Value != value) { if (_Value < 0) _Value = 0; else _Value = value; } } }
        public bool? Status { get { if (_Status == null) return false; else return _Status; } set { if (_Status != value) { _Status = value; } } }


        #endregion

    }
    #endregion[ket thuc class tblFile]



    #region[Bat dau 1  class tblFileType]

    [Table("filetype")]
    public class FileType
    {

        #region[Declare variables]
        private int _ID;
        private string _Name;
        private string _Summary;
        private int? _ParentID;
        private string _Level;
        private DateTime? _Date;
        private int? _Number;
        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { if (String.IsNullOrEmpty(_Name)) return ""; else return _Name; } set { _Name = value; } }
        public string Summary { get { if (String.IsNullOrEmpty(_Summary)) return ""; else return _Summary; } set { _Summary = value; } }
        public int? ParentID { get { if (_ParentID == null || _ParentID < 0) return 0; else return _ParentID; } set { if (_ParentID != value) { if (_ParentID < 0) _ParentID = 0; else _ParentID = value; } } }
        public string Level { get { if (String.IsNullOrEmpty(_Level)) return ""; else return _Level; } set { _Level = value; } }
        public DateTime? Date { get { if (_Date == null) return new DateTime(); else return _Date; } set { if (_Date != value) { _Date = value; } } }
        public int? Number { get { if (_Number == null || _Number < 0) return 0; else return _Number; } set { if (_Number != value) { if (_Number < 0) _Number = 0; else _Number = value; } } }


        #endregion

    }
    #endregion[ket thuc class tblFileType]



    #region[Bat dau 1  class tblImage]

    [Table("image")]
    public class Image
    {

        #region[Declare variables]
        private int _ID;
        private string _Name;
        private string _Link;
        private string _LinkImage;
        private int? _TypeID;
        private string _Summary;
        private DateTime? _Date;
        private int? _Col;


        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { if (String.IsNullOrEmpty(_Name)) return ""; else return _Name; } set { _Name = value; } }
        public string Link { get { if (String.IsNullOrEmpty(_Link)) return ""; else return _Link; } set { _Link = value; } }
        public string LinkImage { get { if (String.IsNullOrEmpty(_LinkImage)) return ""; else return _LinkImage; } set { _LinkImage = value; } }
        public int? TypeID { get { if (_TypeID == null || _TypeID < 0) return 0; else return _TypeID; } set { if (_TypeID != value) { if (_TypeID < 0) _TypeID = 0; else _TypeID = value; } } }
        public string Summary { get { if (String.IsNullOrEmpty(_Summary)) return ""; else return _Summary; } set { _Summary = value; } }
        public DateTime? Date { get { if (_Date == null) return new DateTime(); else return _Date; } set { if (_Date != value) { _Date = value; } } }
        public int? Col { get { if (_Col == null || _Col < 0) return 0; else return _Col; } set { if (_Col != value) { if (_Col < 0) _Col = 0; else _Col = value; } } }

        #endregion

    }
    #endregion[ket thuc class tblImage]



    #region[Bat dau 1  class tblImageType]

    [Table("imagetype")]
    public class ImageType
    {

        #region[Declare variables]
        private int _ID;
        private string _Name;
        private int? _Number;
        private string _Size;
        private DateTime? _Date;
        private int? _Parent;
        private string _Image;
        private string _Level;
        private string _Alias;

        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { if (String.IsNullOrEmpty(_Name)) return ""; else return _Name; } set { _Name = value; } }
        public int? Number { get { if (_Number == null || _Number < 0) return 0; else return _Number; } set { if (_Number != value) { if (_Number < 0) _Number = 0; else _Number = value; } } }
        public string Size { get { if (String.IsNullOrEmpty(_Size)) return ""; else return _Size; } set { _Size = value; } }
        public DateTime? Date { get { if (_Date == null) return new DateTime(); else return _Date; } set { if (_Date != value) { _Date = value; } } }
        public int? Parent { get { if (_Parent == null || _Parent < 0) return 0; else return _Parent; } set { if (_Parent != value) { if (_Parent < 0) _Parent = 0; else _Parent = value; } } }
        public string Image { get { if (String.IsNullOrEmpty(_Image)) return ""; else return _Image; } set { _Image = value; } }
        public string Level { get { if (String.IsNullOrEmpty(_Level)) return ""; else return _Level; } set { _Level = value; } }
        public string Alias { get { if (String.IsNullOrEmpty(_Alias)) return ""; else return _Alias; } set { _Alias = value; } }


        #endregion

    }
    #endregion[ket thuc class tblImageType]



    #region[Bat dau 1  class tblLog]

    [Table("log")]
    public class Log
    {

        #region[Declare variables]
        private int _ID;
        private string _Title;
        private DateTime? _Time;
        private double? _Value;
        private int? _LogTypeID;



        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Title { get { if (String.IsNullOrEmpty(_Title)) return ""; else return _Title; } set { _Title = value; } }
        public DateTime? Time { get { if (_Time == null) return new DateTime(); else return _Time; } set { if (_Time != value) { _Time = value; } } }
        public double? Value { get { if (_Value == null || _Value < 0) return 0; else return _Value; } set { if (_Value != value) { if (_Value < 0) _Value = 0; else _Value = value; } } }
        public int? LogTypeID { get { if (_LogTypeID == null || _LogTypeID < 0) return 0; else return _LogTypeID; } set { if (_LogTypeID != value) { if (_LogTypeID < 0) _LogTypeID = 0; else _LogTypeID = value; } } }


        #endregion

    }
    #endregion[ket thuc class tblLog]



    #region[Bat dau 1  class tblLogType]

    [Table("logtype")]
    public class LogType
    {

        #region[Declare variables]
        private int _ID;
        private string _Name;
        private int? _Number;



        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { if (String.IsNullOrEmpty(_Name)) return ""; else return _Name; } set { _Name = value; } }
        public int? Number { get { if (_Number == null || _Number < 0) return 0; else return _Number; } set { if (_Number != value) { if (_Number < 0) _Number = 0; else _Number = value; } } }


        #endregion

    }
    #endregion[ket thuc class tblLogType]



    #region[Bat dau 1  class tblNews]

    [Table("news")]
    public class News
    {

        #region[Declare variables]
        private int _ID;
        private string _Title;
        private string _Summary;
        private string _Image;
        private string _Detail;
        private int? _TypeID;
        private int? _Order;
        private DateTime? _Date;
        private bool? _Status;
        private string _Description;
        private string _Keyword;
        private int? _Views;
        private bool? _Featured;
        private bool? _Slider;
        private bool? _Hot;
        private bool? _Fast;
        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Title { get { if (String.IsNullOrEmpty(_Title)) return ""; else return _Title; } set { _Title = value; } }
        public string Summary { get { if (String.IsNullOrEmpty(_Summary)) return ""; else return _Summary; } set { _Summary = value; } }
        public string Image { get { if (String.IsNullOrEmpty(_Image)) return ""; else return _Image; } set { _Image = value; } }
        public string Detail { get { if (String.IsNullOrEmpty(_Detail)) return ""; else return _Detail; } set { _Detail = value; } }
        public int? TypeID { get { if (_TypeID == null || _TypeID < 0) return 0; else return _TypeID; } set { if (_TypeID != value) { if (_TypeID < 0) _TypeID = 0; else _TypeID = value; } } }
        public int? Order { get { if (_Order == null || _Order < 0) return 0; else return _Order; } set { if (_Order != value) { if (_Order < 0) _Order = 0; else _Order = value; } } }
        public DateTime? Date { get { if (_Date == null) return new DateTime(); else return _Date; } set { if (_Date != value) { _Date = value; } } }
        public bool? Status { get { if (_Status == null) return false; else return _Status; } set { if (_Status != value) { _Status = value; } } }
        public string Description { get { if (String.IsNullOrEmpty(_Description)) return ""; else return _Description; } set { _Description = value; } }
        public string Keyword { get { if (String.IsNullOrEmpty(_Keyword)) return ""; else return _Keyword; } set { _Keyword = value; } }
        public int? Views { get { if (_Views == null || _Views < 0) return 0; else return _Views; } set { if (_Views != value) { if (_Views < 0) _Views = 0; else _Views = value; } } }
        public bool? Featured { get { if (_Featured == null) return false; else return _Featured; } set { if (_Featured != value) { _Featured = value; } } }
        public bool? Slider { get { if (_Slider == null) return false; else return _Slider; } set { if (_Slider != value) { _Slider = value; } } }
        public bool? Hot { get { if (_Hot == null) return false; else return _Hot; } set { if (_Hot != value) { _Hot = value; } } }
        public bool? Fast { get { if (_Fast == null) return false; else return _Fast; } set { if (_Fast != value) { _Fast = value; } } }
        [ForeignKey("TypeID")]
        [Required]
        public virtual NewsGroups NewsGroup { get; set; }
        #endregion

    }
    #endregion[ket thuc class tblNews]



    #region[Bat dau 1  class tblNewsGroups]

    [Table("newsgroups")]
    public class NewsGroups
    {
        public NewsGroups()
        {
            ListNews = new List<News>();
        }

        #region[Declare variables]
        private int _ID;
        private string _Name;
        private string _Alias;
        private bool? _Status;
        private int? _Number;
        private bool? _Visible;
        private int? _Parent;
        private DateTime? _Date;
        private string _Level;
        private string _Link;
        private string _Description;

        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { if (String.IsNullOrEmpty(_Name)) return ""; else return _Name; } set { _Name = value; } }
        public string Alias { get { if (String.IsNullOrEmpty(_Alias)) return ""; else return _Alias; } set { _Alias = value; } }
        public bool? Status { get { if (_Status == null) return false; else return _Status; } set { if (_Status != value) { _Status = value; } } }
        public int? Number { get { if (_Number == null || _Number < 0) return 0; else return _Number; } set { if (_Number != value) { if (_Number < 0) _Number = 0; else _Number = value; } } }
        public bool? Visible { get { if (_Visible == null) return false; else return _Visible; } set { if (_Visible != value) { _Visible = value; } } }
        public int? Parent { get { if (_Parent == null || _Parent < 0) return 0; else return _Parent; } set { if (_Parent != value) { if (_Parent < 0) _Parent = 0; else _Parent = value; } } }
        public DateTime? Date { get { if (_Date == null) return new DateTime(); else return _Date; } set { if (_Date != value) { _Date = value; } } }
        public string Level { get { if (String.IsNullOrEmpty(_Level)) return ""; else return _Level; } set { _Level = value; } }
        public string Link { get { if (String.IsNullOrEmpty(_Link)) return ""; else return _Link; } set { _Link = value; } }
        public string Description { get { if (String.IsNullOrEmpty(_Description)) return ""; else return _Description; } set { _Description = value; } }

        public virtual ICollection<News> ListNews { get; set; }
        #endregion

    }
    #endregion[ket thuc class tblNewsGroups]



    #region[Bat dau 1  class tblPage]

    [Table("page")]
    public class Page
    {

        #region[Declare variables]
        private int _ID;
        private string _Name;
        private string _Link;
        private int? _TypeID;
        private int? _Role;



        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { if (String.IsNullOrEmpty(_Name)) return ""; else return _Name; } set { _Name = value; } }
        public string Link { get { if (String.IsNullOrEmpty(_Link)) return ""; else return _Link; } set { _Link = value; } }
        public int? TypeID { get { if (_TypeID == null || _TypeID < 0) return 0; else return _TypeID; } set { if (_TypeID != value) { if (_TypeID < 0) _TypeID = 0; else _TypeID = value; } } }
        public int? Role { get { if (_Role == null || _Role < 0) return 0; else return _Role; } set { if (_Role != value) { if (_Role < 0) _Role = 0; else _Role = value; } } }


        #endregion

    }
    #endregion[ket thuc class tblPage]



    #region[Bat dau 1  class tblPageType]

    [Table("pagetype")]
    public class PageType
    {

        #region[Declare variables]
        private int _ID;
        private string _Name;



        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { if (String.IsNullOrEmpty(_Name)) return ""; else return _Name; } set { _Name = value; } }


        #endregion

    }
    #endregion[ket thuc class tblPageType]



    #region[Bat dau 1  class tblProduct]

    [Table("product")]
    public class Product
    {
        public Product()
        {
            ProductImages = new List<ProductImage>();
            ProductColor = new List<ProductColor>();
            ProductSize = new List<ProductSize>();
            ProductAttribute = new List<ProductAttribute>();
            ProductSaleOff = new List<ProductSaleOff>();
        }
        #region[Declare variables]
        private int _ID;
        private string _Name;
        private double? _Price;
        private double? _Price2;
        private double? _Price3;
        private string _Detail;
        private string _Image;
        private int? _Number;
        private bool? _Visible;
        private bool? _Status;
        private int? _Type;
        private string _Summary;
        private int? _Unit;
        private string _Unit1;
        private string _Unit2;
        private string _Unit3;
        private string _SeriNumber;
        private int? _View;
        private int? _AccountId;
        private int? _Manufacturer;
        private int? _Distributor;
        private DateTime? _Date;
        private int? _Buy;
        private int? _SaleOff;
        private bool? _Hot;
        private int? _BaoHanh;
        private int? _Size;
        private int? _Power;
        private int? _Group;
        private string _Description;
        private string _Keyword;
        private string _Choice1;
        private string _Choice2;
        private string _Choice3;
        private string _Choice4;
        private int? _Transport1;
        private int? _Transport2;
        private int? _Transport12;
        private int? _Transport22;
        private int? _Answer;
        private int? _MarketId;
        private int _Quantity;
        private string _Code;
        private int? _BrandId;
        private string _Country;
        private string _Store;
        private double? _Npp;
        private string _Width;
        private string _Height;
        private string _Depth;
        private string _Title;
        private string _WarrantyTime;
        private string _ExpireDate;
        public string _Weight;
        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { if (String.IsNullOrEmpty(_Name)) return ""; else return _Name; } set { _Name = value; } }
        public double? Price
        {
            get
            {
                if (_Price == null || _Price < 0)
                    return 0;
                else
                {
                    if (Unit == 0 || Unit == 1)
                        return _Price;
                    else if (Unit == 2)
                        return _Price2;
                    else
                        return _Price3;
                }
            }
            set { if (_Price != value) { if (_Price < 0) _Price = 0; else _Price = value; } }
        }
        public double? Price2 { get { if (_Price2 == null || _Price2 < 0) return 0; else return _Price2; } set { if (_Price2 != value) { if (_Price2 < 0) _Price2 = 0; else _Price2 = value; } } }
        public double? Price3 { get { if (_Price3 == null || _Price3 < 0) return 0; else return _Price3; } set { if (_Price3 != value) { if (_Price3 < 0) _Price3 = 0; else _Price3 = value; } } }
        public string Detail { get { if (String.IsNullOrEmpty(_Detail)) return ""; else return _Detail; } set { _Detail = value; } }
        public string Image { get { if (String.IsNullOrEmpty(_Image)) return ""; else return _Image; } set { _Image = value; } }
        public int? Number { get { if (_Number == null || _Number < 0) return 0; else return _Number; } set { if (_Number != value) { if (_Number < 0) _Number = 0; else _Number = value; } } }
        public bool? Visible { get { if (_Visible == null) return false; else return _Visible; } set { if (_Visible != value) { _Visible = value; } } }
        public bool? Status { get { if (_Status == null) return false; else return _Status; } set { if (_Status != value) { _Status = value; } } }
        public int? Type { get { if (_Type == null || _Type < 0) return 0; else return _Type; } set { if (_Type != value) { if (_Type < 0) _Type = 0; else _Type = value; } } }
        public string Summary { get { if (String.IsNullOrEmpty(_Summary)) return ""; else return _Summary; } set { _Summary = value; } }
        public string Unit1 { get { if (String.IsNullOrEmpty(_Unit1)) return ""; else return _Unit1; } set { _Unit1 = value; } }
        public string Unit2 { get { if (String.IsNullOrEmpty(_Unit2)) return ""; else return _Unit2; } set { _Unit2 = value; } }
        public string Unit3 { get { if (String.IsNullOrEmpty(_Unit3)) return ""; else return _Unit3; } set { _Unit3 = value; } }
        public string SeriNumber { get { if (String.IsNullOrEmpty(_SeriNumber)) return ""; else return _SeriNumber; } set { _SeriNumber = value; } }
        public int? View { get { if (_View == null || _View < 0) return 0; else return _View; } set { if (_View != value) { if (_View < 0) _View = 0; else _View = value; } } }
        public int? AccountId { get { if (_AccountId == null || _AccountId < 0) return 0; else return _AccountId; } set { if (_AccountId != value) { if (_AccountId < 0) _AccountId = 0; else _AccountId = value; } } }
        public int? Manufacturer { get { if (_Manufacturer == null || _Manufacturer < 0) return 0; else return _Manufacturer; } set { if (_Manufacturer != value) { if (_Manufacturer < 0) _Manufacturer = 0; else _Manufacturer = value; } } }
        public int? Distributor { get { if (_Distributor == null || _Distributor < 0) return 0; else return _Distributor; } set { if (_Distributor != value) { if (_Distributor < 0) _Distributor = 0; else _Distributor = value; } } }
        public DateTime? Date { get { if (_Date == null) return new DateTime(); else return _Date; } set { if (_Date != value) { _Date = value; } } }
        public int? Buy { get { if (_Buy == null || _Buy < 0) return 0; else return _Buy; } set { if (_Buy != value) { if (_Buy < 0) _Buy = 0; else _Buy = value; } } }
        public bool? Hot { get { if (_Hot == null) return false; else return _Hot; } set { if (_Hot != value) { _Hot = value; } } }
        public int? SaleOff { get { if (_SaleOff == null || _SaleOff < 0) return 0; else return _SaleOff; } set { if (_SaleOff != value) { if (_SaleOff < 0) _SaleOff = 0; else _SaleOff = value; } } }
        public int? BaoHanh { get { if (_BaoHanh == null || _BaoHanh < 0) return 0; else return _BaoHanh; } set { if (_BaoHanh != value) { if (_BaoHanh < 0) _BaoHanh = 0; else _BaoHanh = value; } } }
        public int? Size { get { if (_Size == null || _Size < 0) return 0; else return _Size; } set { if (_Size != value) { if (_Size < 0) _Size = 0; else _Size = value; } } }
        public int? Power { get { if (_Power == null || _Power < 0) return 0; else return _Power; } set { if (_Power != value) { if (_Power < 0) _Power = 0; else _Power = value; } } }
        public int? Group { get { if (_Group == null || _Group < 0) return 0; else return _Group; } set { if (_Group != value) { if (_Group < 0) _Group = 0; else _Group = value; } } }
        public string Description { get { if (String.IsNullOrEmpty(_Description)) return ""; else return _Description; } set { _Description = value; } }
        public string Keyword { get { if (String.IsNullOrEmpty(_Keyword)) return ""; else return _Keyword; } set { _Keyword = value; } }
        public string Choice1 { get { if (String.IsNullOrEmpty(_Choice1)) return ""; else return _Choice1; } set { _Choice1 = value; } }
        public string Choice2 { get { if (String.IsNullOrEmpty(_Choice2)) return ""; else return _Choice2; } set { _Choice2 = value; } }
        public string Choice3 { get { if (String.IsNullOrEmpty(_Choice3)) return ""; else return _Choice3; } set { _Choice3 = value; } }
        public string Choice4 { get { if (String.IsNullOrEmpty(_Choice4)) return ""; else return _Choice4; } set { _Choice4 = value; } }
        public int? Answer { get { if (_Answer == null || _Answer < 0) return 0; else return _Answer; } set { if (_Answer != value) { if (_Answer < 0) _Answer = 0; else _Answer = value; } } }
        public int? MarketId { get { if (_MarketId == null || _MarketId < 0) return 0; else return _MarketId; } set { if (_MarketId != value) { if (_MarketId < 0) _MarketId = 0; else _MarketId = value; } } }
        public int? Transport1 { get { if (_Transport1 == null || _Transport1 < 0) return 0; else return _Transport1; } set { if (_Transport1 != value) { if (_Transport1 < 0) _Transport1 = 0; else _Transport1 = value; } } }
        public int? Transport2 { get { if (_Transport2 == null || _Transport2 < 0) return 0; else return _Transport2; } set { if (_Transport2 != value) { if (_Transport2 < 0) _Transport2 = 0; else _Transport2 = value; } } }
        public int? Transport12 { get { if (_Transport12 == null || _Transport12 < 0) return 0; else return _Transport12; } set { if (_Transport12 != value) { if (_Transport12 < 0) _Transport12 = 0; else _Transport12 = value; } } }
        public int? Transport22 { get { if (_Transport22 == null || _Transport22 < 0) return 0; else return _Transport22; } set { if (_Transport22 != value) { if (_Transport22 < 0) _Transport22 = 0; else _Transport22 = value; } } }
        public int? Unit { get { if (_Unit == null || _Unit < 0) return 0; else return _Unit; } set { if (_Unit != value) { if (_Unit < 0) _Unit = 0; else _Unit = value; } } }
        public int Quantity { get { if (_Quantity < 0) return 0; else return _Quantity; } set { if (_Quantity != value) { if (_Quantity < 0) _Quantity = 0; else _Quantity = value; } } }
        public int? BrandId { get { if (_BrandId == null || _BrandId < 0) return 0; else return _BrandId; } set { if (_BrandId != value) { if (_BrandId < 0) _BrandId = 0; else _BrandId = value; } } }
        public string Country { get { if (String.IsNullOrEmpty(_Country)) return ""; else return _Country; } set { _Country = value; } }
        public string Store { get { if (String.IsNullOrEmpty(_Store)) return ""; else return _Store; } set { _Store = value; } }
        public double? Npp { get { if (_Npp == null || _Npp < 0) return 0; else return _Npp; } set { if (_Npp != value) { if (_Npp < 0) _Npp = 0; else _Npp = value; } } }
        public string Width { get { if (String.IsNullOrEmpty(_Width)) return ""; else return _Width; } set { _Width = value; } }
        public string Height { get { if (String.IsNullOrEmpty(_Height)) return ""; else return _Height; } set { _Height = value; } }
        public string Depth { get { if (String.IsNullOrEmpty(_Depth)) return ""; else return _Depth; } set { _Depth = value; } }
        public string Title { get { if (String.IsNullOrEmpty(_Title)) return ""; else return _Title; } set { _Title = value; } }
        public string WarrantyTime { get { if (String.IsNullOrEmpty(_WarrantyTime)) return ""; else return _WarrantyTime; } set { _WarrantyTime = value; } }
        public string ExpireDate { get { if (String.IsNullOrEmpty(_ExpireDate)) return ""; else return _ExpireDate; } set { _ExpireDate = value; } }
        public string Weight { get { if (String.IsNullOrEmpty(_Weight)) return ""; else return _Weight; } set { _Weight = value; } }

        #endregion
        public int getReview()
        {

            return V308CMS.Common.RamdomUltis.getRamdom(2, 5);
        }
        [ForeignKey("Type")]
        [Required]
        public virtual ProductType ProductType { get; set; }
        //add by toaihv
        public virtual ICollection<ProductImage> ProductImages { get; set; }

        public virtual ICollection<ProductColor> ProductColor { get; set; }
        public virtual ICollection<ProductSize> ProductSize { get; set; }

        public virtual ICollection<ProductAttribute> ProductAttribute { get; set; }
        [ForeignKey("Manufacturer")]
        public virtual ProductManufacturer ProductManufacturer { get; set; }

        public virtual ICollection<ProductSaleOff> ProductSaleOff { get; set; }
    }
    #endregion[ket thuc class tblProduct]



    #region[Bat dau 1  class tblProductAttribute]

    [Table("productattribute")]
    public class ProductAttribute
    {

        #region[Declare variables]
        private int _ID;
        private int? _CateAttributeID;
        private int? _ProductID;
        private string _Value;
        private string _Name;


        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public int? CateAttributeID { get { if (_CateAttributeID == null || _CateAttributeID < 0) return 0; else return _CateAttributeID; } set { if (_CateAttributeID != value) { if (_CateAttributeID < 0) _CateAttributeID = 0; else _CateAttributeID = value; } } }
        public int? ProductID { get { if (_ProductID == null || _ProductID < 0) return 0; else return _ProductID; } set { if (_ProductID != value) { if (_ProductID < 0) _ProductID = 0; else _ProductID = value; } } }
        public string Value { get { if (String.IsNullOrEmpty(_Value)) return ""; else return _Value; } set { _Value = value; } }
        public string Name { get { if (String.IsNullOrEmpty(_Name)) return ""; else return _Name; } set { _Name = value; } }

        [ForeignKey("ProductID")]
        [Required]
        public virtual Product Product { get; set; }

        #endregion

    }
    #endregion[ket thuc class tblProductAttribute]



    #region[Bat dau 1  class tblProductDistributor]

    [Table("productdistributor")]
    public class ProductDistributor
    {

        #region[Declare variables]
        private int _ID;
        private string _Name;
        private string _Detail;
        private string _Image;
        private bool? _Status;
        private int? _Number;
        private bool? _Visible;
        private DateTime? _Date;


        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { if (String.IsNullOrEmpty(_Name)) return ""; else return _Name; } set { _Name = value; } }
        public string Detail { get { if (String.IsNullOrEmpty(_Detail)) return ""; else return _Detail; } set { _Detail = value; } }
        public string Image { get { if (String.IsNullOrEmpty(_Image)) return ""; else return _Image; } set { _Image = value; } }
        public bool? Status { get { if (_Status == null) return false; else return _Status; } set { if (_Status != value) { _Status = value; } } }
        public int? Number { get { if (_Number == null || _Number < 0) return 0; else return _Number; } set { if (_Number != value) { if (_Number < 0) _Number = 0; else _Number = value; } } }
        public bool? Visible { get { if (_Visible == null) return false; else return _Visible; } set { if (_Visible != value) { _Visible = value; } } }
        public DateTime? Date { get { if (_Date == null) return new DateTime(); else return _Date; } set { if (_Date != value) { _Date = value; } } }


        #endregion

    }
    #endregion[ket thuc class tblProductDistributor]



    #region[Bat dau 1  class tblProductImage]

    [Table("productimage")]
    public class ProductImage
    {

        #region[Declare variables]
        private int _ID;
        private string _Name;
        private int? _Number;
        private int? _ProductID;
        private string _Title;



        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { if (String.IsNullOrEmpty(_Name)) return ""; else return _Name; } set { _Name = value; } }
        public int? Number { get { if (_Number == null || _Number < 0) return 0; else return _Number; } set { if (_Number != value) { if (_Number < 0) _Number = 0; else _Number = value; } } }
        public int? ProductID { get { if (_ProductID == null || _ProductID < 0) return 0; else return _ProductID; } set { if (_ProductID != value) { if (_ProductID < 0) _ProductID = 0; else _ProductID = value; } } }
        public string Title { get { if (String.IsNullOrEmpty(_Title)) return ""; else return _Title; } set { _Title = value; } }

        //add by toaihv
        [ForeignKey("ProductID")]
        [Required]
        public virtual  Product Product { get; set; }

        #endregion

    }
    #endregion[ket thuc class tblProductWishlist]
    #region[Bat dau 1  class tblProductWishlist]

    [Table("productwishlist")]
    public class ProductWishlist
    {

        #region[Declare variables]
        private int _ID;
        private string _UserID;      
        private string _ListProduct;      
        #endregion
        #region[Public Properties]
        [Key]
        public int Id { get { return _ID; } set { _ID = value; } }
        public string UserId { get { if (String.IsNullOrEmpty(_UserID)) return ""; else return _UserID; } set { _UserID = value; } }
        public string ListProduct { get { if (String.IsNullOrEmpty(_ListProduct)) return ""; else return _ListProduct; } set { _ListProduct = value; } }
        #endregion

    }
    #endregion[ket thuc class tblProductWishlist]



    #region[Bat dau 1  class tblProductManufacturer]

    [Table("productmanufacturer")]
    public class ProductManufacturer
    {

        #region[Declare variables]
        private int _ID;
        private string _Name;
        private string _Image;
        private string _Detail;
        private bool? _Status;
        private bool? _Visible;
        private int? _Number;
        private DateTime? _Date;


        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { if (String.IsNullOrEmpty(_Name)) return ""; else return _Name; } set { _Name = value; } }
        public string Image { get { if (String.IsNullOrEmpty(_Image)) return ""; else return _Image; } set { _Image = value; } }
        public string Detail { get { if (String.IsNullOrEmpty(_Detail)) return ""; else return _Detail; } set { _Detail = value; } }
        public bool? Status { get { if (_Status == null) return false; else return _Status; } set { if (_Status != value) { _Status = value; } } }
        public bool? Visible { get { if (_Visible == null) return false; else return _Visible; } set { if (_Visible != value) { _Visible = value; } } }
        public int? Number { get { if (_Number == null || _Number < 0) return 0; else return _Number; } set { if (_Number != value) { if (_Number < 0) _Number = 0; else _Number = value; } } }
        public DateTime? Date { get { if (_Date == null) return new DateTime(); else return _Date; } set { if (_Date != value) { _Date = value; } } }

        #endregion

    }
    #endregion[ket thuc class tblProductManufacturer]



    #region[Bat dau 1  class tblProductOrder]

    [Table("productorder")]
    public class ProductOrder
    {

        #region[Declare variables]
        private int _ID;
        private DateTime? _Date;
        private string _Detail;
        private string _FullName;
        private string _Email;
        private string _Phone;
        private string _Address;
        private int? _AccountID;
        private int? _AdminId;
        private int? _Status;
        private int? _ProductID;
        private int? _Count;
        private double? _Price;
        private string _ProductDetail;


        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public DateTime? Date { get { if (_Date == null) return new DateTime(); else return _Date; } set { if (_Date != value) { _Date = value; } } }
        public string Detail { get { if (String.IsNullOrEmpty(_Detail)) return ""; else return _Detail; } set { _Detail = value; } }
        public string FullName { get { if (String.IsNullOrEmpty(_FullName)) return ""; else return _FullName; } set { _FullName = value; } }
        public string Email { get { if (String.IsNullOrEmpty(_Email)) return ""; else return _Email; } set { _Email = value; } }
        public string Phone { get { if (String.IsNullOrEmpty(_Phone)) return ""; else return _Phone; } set { _Phone = value; } }
        public string Address { get { if (String.IsNullOrEmpty(_Address)) return ""; else return _Address; } set { _Address = value; } }
        public int? AccountID { get { if (_AccountID == null || _AccountID < 0) return 0; else return _AccountID; } set { if (_AccountID != value) { if (_AccountID < 0) _AccountID = 0; else _AccountID = value; } } }
        public int? AdminId { get { if (_AdminId == null || _AdminId < 0) return 0; else return _AdminId; } set { if (_AdminId != value) { if (_AdminId < 0) _AdminId = 0; else _AdminId = value; } } }
        
        public int? Status { get { if (_Status == null) return 0; else return _Status; } set { if (_Status != value) { _Status = value; } } }
        public int? ProductID { get { if (_ProductID == null || _ProductID < 0) return 0; else return _ProductID; } set { if (_ProductID != value) { if (_ProductID < 0) _ProductID = 0; else _ProductID = value; } } }
        public int? Count { get { if (_Count == null || _Count < 0) return 0; else return _Count; } set { if (_Count != value) { if (_Count < 0) _Count = 0; else _Count = value; } } }
        public double? Price { get { if (_Price == null || _Price < 0) return 0; else return _Price; } set { if (_Price != value) { if (_Price < 0) _Price = 0; else _Price = value; } } }
        public string ProductDetail { get { if (String.IsNullOrEmpty(_ProductDetail)) return ""; else return _ProductDetail; } set { _ProductDetail = value; } }

        #endregion

    }
    [Table("productorder_detail")]
    public class productorder_detail
    {

        #region[Declare variables]
        private int _ID;
        private int? OrderId;
        private int? ItemId;
        private string ItemName;
        private int? ItemQty;
        private double? ItemPrice;
        
        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }

        public int? order_id { get { if (OrderId == null || OrderId < 0) return 0; else return OrderId; } set { if (OrderId != value) { if (OrderId < 0) OrderId = 0; else OrderId = value; } } }
        public int? item_id { get { if (ItemId == null || ItemId < 0) return 0; else return ItemId; } set { if (ItemId != value) { if (ItemId < 0) ItemId = 0; else ItemId = value; } } }
        public string item_name { get { if (String.IsNullOrEmpty(ItemName)) return ""; else return ItemName; } set { ItemName = value; } }
        public double? item_price { get { if (ItemPrice == null || ItemPrice < 0) return 0; else return ItemPrice; } set { if (ItemPrice != value) { if (ItemPrice < 0) ItemPrice = 0; else ItemPrice = value; } } }
        public int? item_qty { get { if (ItemQty == null || ItemQty < 0) return 0; else return ItemQty; } set { if (ItemQty != value) { if (ItemQty < 0) ItemQty = 0; else ItemQty = value; } } }

       
        #endregion

    }
    
    #endregion[ket thuc class tblProductOrder]



    #region[Bat dau 1  class tblProductSaleOff]

    [Table("productsaleoff")]
    public class ProductSaleOff
    {

        #region[Declare variables]
        private int _ID;
        private int? _ProductID;
        private DateTime? _StartTime;
        private DateTime? _EndTime;
        private double? _Percent;

        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public int? ProductID { get { if (_ProductID == null || _ProductID < 0) return 0; else return _ProductID; } set { if (_ProductID != value) { if (_ProductID < 0) _ProductID = 0; else _ProductID = value; } } }
        public DateTime? StartTime { get { if (_StartTime == null) return new DateTime(); else return _StartTime; } set { if (_StartTime != value) { _StartTime = value; } } }
        public DateTime? EndTime { get { if (_EndTime == null) return new DateTime(); else return _EndTime; } set { if (_EndTime != value) { _EndTime = value; } } }
        public double? Percent { get { if (_Percent == null || _Percent < 0) return 0; else return _Percent; } set { if (_Percent != value) { if (_Percent < 0) _Percent = 0; else _Percent = value; } } }


        #endregion

    }
    #endregion[ket thuc class tblProductSaleOff]



    #region[Bat dau 1  class tblProductType]

    [Table("producttype")]
    public class ProductType
    {
        public ProductType()
        {
            ListProduct = new List<Product>();
            ListProductBrand = new List<Brand>();
        }

        #region[Declare variables]
        private int _ID;
        private string _Name;
        private string _Icon;
        private string _ColorTheme;
        private string _Detail;
        private string _Description;
        private string _Image;
        private int? _Number;
        private bool? _Visible;
        private bool? _Status;
        private int? _Parent;
        private DateTime? _Date;
        private string _Level;
        private string _ImageBanner;
        private string _TypeBanner;
        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { if (String.IsNullOrEmpty(_Name)) return ""; else return _Name; } set { _Name = value; } }
        public string Detail { get { if (String.IsNullOrEmpty(_Detail)) return ""; else return _Detail; } set { _Detail = value; } }
        public string Description { get { if (String.IsNullOrEmpty(_Description)) return ""; else return _Description; } set { _Description = value; } }
        public string Image { get { if (String.IsNullOrEmpty(_Image)) return ""; else return _Image; } set { _Image = value; } }
        public int? Number { get { if (_Number == null || _Number < 0) return 0; else return _Number; } set { if (_Number != value) { if (_Number < 0) _Number = 0; else _Number = value; } } }
        public bool? Visible { get { if (_Visible == null) return false; else return _Visible; } set { if (_Visible != value) { _Visible = value; } } }
        public bool? Status { get { if (_Status == null) return false; else return _Status; } set { if (_Status != value) { _Status = value; } } }
        public int? Parent { get { if (_Parent == null || _Parent < 0) return 0; else return _Parent; } set { if (_Parent != value) { if (_Parent < 0) _Parent = 0; else _Parent = value; } } }
        public DateTime? Date { get { if (_Date == null) return new DateTime(); else return _Date; } set { if (_Date != value) { _Date = value; } } }
        public string Level { get { if (String.IsNullOrEmpty(_Level)) return ""; else return _Level; } set { _Level = value; } }
        public string ImageBanner { get { if (String.IsNullOrEmpty(_ImageBanner)) return ""; else return _ImageBanner; } set { _ImageBanner = value; } }
        public string TypeBanner { get { if (String.IsNullOrEmpty(_TypeBanner)) return ""; else return _TypeBanner; } set { _TypeBanner = value; } }
        public string Icon { get { if (String.IsNullOrEmpty(_Icon)) return ""; else return _Icon; } set { _Icon = value; } }
        public string ColorTheme { get { if (String.IsNullOrEmpty(_ColorTheme)) return ""; else return _ColorTheme; } set { _ColorTheme = value; } }

        public virtual ICollection<Product> ListProduct { get; set; }
        public virtual ICollection<Brand> ListProductBrand { get; set; }

        #endregion

    }
    #endregion[ket thuc class tblProductType]



    #region[Bat dau 1  class tblQuestion]

    [Table("question")]
    public class Question
    {

        #region[Declare variables]
        private int _ID;
        private string _Title;
        private string _Detail;
        private DateTime? _DateCreate;
        private DateTime? _DateModify;
        private bool? _Status;
        private int? _AccountID;
        private int? _TypeID;
        private int? _View;
        private bool? _Check;
        private DateTime? _DateAnswer;



        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Title { get { if (String.IsNullOrEmpty(_Title)) return ""; else return _Title; } set { _Title = value; } }
        public string Detail { get { if (String.IsNullOrEmpty(_Detail)) return ""; else return _Detail; } set { _Detail = value; } }
        public DateTime? DateCreate { get { if (_DateCreate == null) return new DateTime(); else return _DateCreate; } set { if (_DateCreate != value) { _DateCreate = value; } } }
        public DateTime? DateModify { get { if (_DateModify == null) return new DateTime(); else return _DateModify; } set { if (_DateModify != value) { _DateModify = value; } } }
        public bool? Status { get { if (_Status == null) return false; else return _Status; } set { if (_Status != value) { _Status = value; } } }
        public int? AccountID { get { if (_AccountID == null || _AccountID < 0) return 0; else return _AccountID; } set { if (_AccountID != value) { if (_AccountID < 0) _AccountID = 0; else _AccountID = value; } } }
        public int? TypeID { get { if (_TypeID == null || _TypeID < 0) return 0; else return _TypeID; } set { if (_TypeID != value) { if (_TypeID < 0) _TypeID = 0; else _TypeID = value; } } }
        public int? View { get { if (_View == null || _View < 0) return 0; else return _View; } set { if (_View != value) { if (_View < 0) _View = 0; else _View = value; } } }
        public bool? Check { get { if (_Check == null) return false; else return _Check; } set { if (_Check != value) { _Check = value; } } }
        public DateTime? DateAnswer { get { if (_DateAnswer == null) return new DateTime(); else return _DateAnswer; } set { if (_DateAnswer != value) { _DateAnswer = value; } } }


        #endregion

    }
    #endregion[ket thuc class tblQuestion]



    #region[Bat dau 1  class tblQuestionAnswer]

    [Table("questionanswer")]
    public class QuestionAnswer
    {

        #region[Declare variables]
        private int _ID;
        private string _Detail;
        private DateTime? _DateCreate;
        private DateTime? _DateModify;
        private int? _AdminID;
        private int? _AccountID;
        private int? _QuestionID;
        private bool? _Status;
        private bool? _Check;



        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Detail { get { if (String.IsNullOrEmpty(_Detail)) return ""; else return _Detail; } set { _Detail = value; } }
        public DateTime? DateCreate { get { if (_DateCreate == null) return new DateTime(); else return _DateCreate; } set { if (_DateCreate != value) { _DateCreate = value; } } }
        public DateTime? DateModify { get { if (_DateModify == null) return new DateTime(); else return _DateModify; } set { if (_DateModify != value) { _DateModify = value; } } }
        public int? AdminID { get { if (_AdminID == null || _AdminID < 0) return 0; else return _AdminID; } set { if (_AdminID != value) { if (_AdminID < 0) _AdminID = 0; else _AdminID = value; } } }
        public int? AccountID { get { if (_AccountID == null || _AccountID < 0) return 0; else return _AccountID; } set { if (_AccountID != value) { if (_AccountID < 0) _AccountID = 0; else _AccountID = value; } } }
        public int? QuestionID { get { if (_QuestionID == null || _QuestionID < 0) return 0; else return _QuestionID; } set { if (_QuestionID != value) { if (_QuestionID < 0) _QuestionID = 0; else _QuestionID = value; } } }
        public bool? Status { get { if (_Status == null) return false; else return _Status; } set { if (_Status != value) { _Status = value; } } }
        public bool? Check { get { if (_Check == null) return false; else return _Check; } set { if (_Check != value) { _Check = value; } } }


        #endregion

    }
    #endregion[ket thuc class tblQuestionAnswer]



    #region[Bat dau 1  class tblQuestionType]

    [Table("questiontype")]
    public class QuestionType
    {

        #region[Declare variables]
        private int _ID;
        private string _Name;
        private string _Summary;
        private int? _Number;
        private int? _ParentID;



        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { if (String.IsNullOrEmpty(_Name)) return ""; else return _Name; } set { _Name = value; } }
        public string Summary { get { if (String.IsNullOrEmpty(_Summary)) return ""; else return _Summary; } set { _Summary = value; } }
        public int? Number { get { if (_Number == null || _Number < 0) return 0; else return _Number; } set { if (_Number != value) { if (_Number < 0) _Number = 0; else _Number = value; } } }
        public int? ParentID { get { if (_ParentID == null || _ParentID < 0) return 0; else return _ParentID; } set { if (_ParentID != value) { if (_ParentID < 0) _ParentID = 0; else _ParentID = value; } } }


        #endregion

    }
    #endregion[ket thuc class tblQuestionType]


    #region[Bat dau 1  class tblSEO]

    [Table("seo")]
    public class SEO
    {

        #region[Declare variables]
        private int _ID;
        private int? _TypeSEO;
        private string _Link;
        private string _Title;
        private string _Description;
        private string _Keywords;
        private int? _IDObject;



        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public int? TypeSEO { get { if (_TypeSEO == null || _TypeSEO < 0) return 0; else return _TypeSEO; } set { if (_TypeSEO != value) { if (_TypeSEO < 0) _TypeSEO = 0; else _TypeSEO = value; } } }
        public string Link { get { if (String.IsNullOrEmpty(_Link)) return ""; else return _Link; } set { _Link = value; } }
        public string Title { get { if (String.IsNullOrEmpty(_Title)) return ""; else return _Title; } set { _Title = value; } }
        public string Description { get { if (String.IsNullOrEmpty(_Description)) return ""; else return _Description; } set { _Description = value; } }
        public string Keywords { get { if (String.IsNullOrEmpty(_Keywords)) return ""; else return _Keywords; } set { _Keywords = value; } }
        public int? IDObject { get { if (_IDObject == null || _IDObject < 0) return 0; else return _IDObject; } set { if (_IDObject != value) { if (_IDObject < 0) _IDObject = 0; else _IDObject = value; } } }


        #endregion

    }
    #endregion[ket thuc class tblSEO]



    #region[Bat dau 1  class tblSEOType]

    [Table("seotype")]
    public class SEOType
    {

        #region[Declare variables]
        private int _ID;
        private string _Name;



        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { if (String.IsNullOrEmpty(_Name)) return ""; else return _Name; } set { _Name = value; } }


        #endregion

    }
    #endregion[ket thuc class tblSEOType]



    #region[Bat dau 1  class tblSupport]

    [Table("support")]
    public class Support
    {

        #region[Declare variables]
        private int _ID;
        private string _Name;
        private string _Phone;
        private string _Nick;
        private int? _TypeID;
        private string _Email;
        private DateTime? _Date;


        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { if (String.IsNullOrEmpty(_Name)) return ""; else return _Name; } set { _Name = value; } }
        public string Phone { get { if (String.IsNullOrEmpty(_Phone)) return ""; else return _Phone; } set { _Phone = value; } }
        public string Nick { get { if (String.IsNullOrEmpty(_Nick)) return ""; else return _Nick; } set { _Nick = value; } }
        public int? TypeID { get { if (_TypeID == null || _TypeID < 0) return 0; else return _TypeID; } set { if (_TypeID != value) { if (_TypeID < 0) _TypeID = 0; else _TypeID = value; } } }
        public string Email { get { if (String.IsNullOrEmpty(_Email)) return ""; else return _Email; } set { _Email = value; } }
        public DateTime? Date { get { if (_Date == null) return new DateTime(); else return _Date; } set { if (_Date != value) { _Date = value; } } }


        #endregion

    }
    #endregion[ket thuc class tblSupport]



    #region[Bat dau 1  class tblSupportType]

    [Table("supporttype")]
    public class SupportType
    {

        #region[Declare variables]
        private int _ID;
        private string _Name;
        private int? _Number;
        private string _Pattern;
        private string _Parameter;
        private DateTime? _Date;


        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public string Name { get { if (String.IsNullOrEmpty(_Name)) return ""; else return _Name; } set { _Name = value; } }
        public int? Number { get { if (_Number == null || _Number < 0) return 0; else return _Number; } set { if (_Number != value) { if (_Number < 0) _Number = 0; else _Number = value; } } }
        public string Pattern { get { if (String.IsNullOrEmpty(_Pattern)) return ""; else return _Pattern; } set { _Pattern = value; } }
        public string Parameter { get { if (String.IsNullOrEmpty(_Parameter)) return ""; else return _Parameter; } set { _Parameter = value; } }
        public DateTime? Date { get { if (_Date == null) return new DateTime(); else return _Date; } set { if (_Date != value) { _Date = value; } } }


        #endregion

    }
    #endregion[ket thuc class tblSupportType]


    #region[Bat dau 1  class tblVEmail]

    [Table("vemail")]
    public class VEmail
    {

        #region[Declare variables]
        private int _ID;
        private int? _Type;
        private string _Value;
        private DateTime? _CreatedDate;
        private bool? _State;
        #endregion
        #region[Public Properties]
        [Key]
        public int ID { get { return _ID; } set { _ID = value; } }
        public int? Type { get { if (_Type == null || _Type < 0) return 0; else return _Type; } set { if (_Type != value) { if (_Type < 0) _Type = 0; else _Type = value; } } }
        public string Value { get { if (String.IsNullOrEmpty(_Value)) return ""; else return _Value; } set { _Value = value; } }
        public DateTime? CreatedDate { get { if (_CreatedDate == null) return new DateTime(); else return _CreatedDate; } set { if (_CreatedDate != value) { _CreatedDate = value; } } }
        public bool? State { get { if (_State == null) return false; else return _State; } set { if (_State != value) { _State = value; } } }
        #endregion

    }
    #endregion[ket thuc class tblVEmail]

    //#region[Bat dau tblSiteConfig]

    [Table("siteconfig")]
    public class SiteConfig
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }


        //#endregion

    }
    //#endregion[ket thuc class tblSiteConfig]

    #region Testimonial Table
    [Table("testimonial")]
    public class Testimonial
    {
        private int _id;
        private string _taget;
        private string _fullname;
        private string _avartar;
        private string _mobile;
        private string _content;
        private int? _order;
        private int? _status;

        [Key]
        public int id { get { return _id; } set { _id = value; } }
        public string taget { get { if (String.IsNullOrEmpty(_taget)) return ""; else return _taget; } set { _taget = value; } }
        public string fullname { get { if (String.IsNullOrEmpty(_fullname)) return ""; else return _fullname; } set { _fullname = value; } }
        public string avartar { get { if (String.IsNullOrEmpty(_avartar)) return ""; else return _avartar; } set { _avartar = value; } }
        public string mobile { get { if (String.IsNullOrEmpty(_mobile)) return ""; else return _mobile; } set { _mobile = value; } }
        public string content { get { if (String.IsNullOrEmpty(_content)) return ""; else return _content; } set { _content = value; } }
        public int? order { get { if (_order == null || _order < 0) return 0; else return _order; } set { if (_order != value) { if (_order < 0) _order = 0; else _order = value; } } }
        public int? status { get { if (_status == null || _status < 0) return 0; else return _status; } set { if (_status != value) { if (_status < 0) _status = 0; else _status = value; } } }


    }
    #endregion

    #region Categorys Table
    [Table("categorys")]
    public class Categorys
    {
        private int _id;
        private string _name;
        private string _summary;
        private string _image;
        private int? _order;
        private int? _status;

        [Key]
        public int id { get { return _id; } set { _id = value; } }
        public string name { get { if (String.IsNullOrEmpty(_name)) return ""; else return _name; } set { _name = value; } }
        public string image { get { if (String.IsNullOrEmpty(_image)) return ""; else return _image; } set { _image = value; } }
        public string summary { get { if (String.IsNullOrEmpty(_summary)) return ""; else return _summary; } set { _summary = value; } }
        public int? order { get { if (_order == null || _order < 0) return 0; else return _order; } set { if (_order != value) { if (_order < 0) _order = 0; else _order = value; } } }
        public int? status { get { if (_status == null || _status < 0) return 0; else return _status; } set { if (_status != value) { if (_status < 0) _status = 0; else _status = value; } } }


    }
    #endregion


    #region[Bat dau 1  class tblProductImage]

    [Table("product_brand")]
    public class Brand
    {

        #region[Declare variables]
        private int _ID;
        private int _Category;
        private string _Name;
        private string _Image;
        private int _Status;
        #endregion

        #region[Public Properties]
        [Key]
        public int id { get { return _ID; } set { _ID = value; } }
        public int category_default { get { if (_Category == null || _Category < 0) return 0; else return _Category; } set { if (_Category != value) { if (_Category < 0) _Category = 0; else _Category = value; } } }
        public string name { get { if (String.IsNullOrEmpty(_Name)) return ""; else return _Name; } set { _Name = value; } }
        public string image { get { if (String.IsNullOrEmpty(_Image)) return ""; else return _Image; } set { _Image = value; } }
        public int status { get { if (_Status == null || _Status < 0) return 0; else return _Status; } set { if (_Status != value) { if (_Status < 0) _Status = 0; else _Status = value; } } }

        [ForeignKey("category_default")]

        public virtual ProductType ProductType { get; set; }
        #endregion

    }
    #endregion[ket thuc class tblProductImage]

    public class RpOrderWithStatusDetail
    {

        public string NgayThang { get; set; }

        public Int32 TongTaoMoi { get; set; }

        public Int32 TongThanhCong { get; set; }

        public Int32 TongGiaoMotPhan { get; set; }

        public Int32 TongHuy { get; set; }

    }
}
