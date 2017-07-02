﻿using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Common;
using V308CMS.Data.Helpers;

namespace V308CMS.Data
{
    public class AccountRepository
    {
        private V308CMSEntities entities;
        #region["Contructor"]

        public AccountRepository()
        {
            this.entities = new V308CMSEntities();
        }

        public AccountRepository(V308CMSEntities mEntities)
        {
            this.entities = mEntities;
        }

        #endregion
        #region["Vung cac thao tac Dispose"]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.entities != null)
                {
                    this.entities.Dispose();
                    this.entities = null;
                }
            }
        }
        #endregion

        public Account LayTinTheoId(int pId)
        {
            Account mAccount = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mAccount = (from p in entities.Account
                         where p.ID == pId
                         select p).FirstOrDefault();
                return mAccount;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public ETLogin CheckDangNhap(string  pUsername,string pPassword,string site= Site.home)
        {
            Account user = null;
            ETLogin mETLogin=new ETLogin();
            try
            {
                //lay danh sach tin moi dang nhat
                user = (from p in entities.Account
                          where (p.UserName.ToLower().Equals(pUsername.ToLower()) || p.Email.ToLower().Equals(pUsername.ToLower()) ) && p.Site == site && p.Status == true
                            select p).FirstOrDefault();
                if (user != null)
                {
                    if (user.Password.Trim().Equals(EncryptionMD5.ToMd5(pPassword.Trim())))
                    {
                        mETLogin.code = 1;
                        mETLogin.message = "OK.";
                        mETLogin.Account = user;
                        mETLogin.role = int.Parse(user.Role.ToString());
                    }
                    else
                    {
                        mETLogin.code = 2;
                        mETLogin.message = "Mật khẩu không chính xác.";
                    }
                }
                else
                {
                    mETLogin.code = 0;
                    mETLogin.message = "Không tìm thấy thông tin truy cập.";
                }
                return mETLogin;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public ETLogin CheckDangNhapHome(string pUsername, string pPassword)
        {
            Account mAccount = null;
            ETLogin mETLogin = new ETLogin();
            try
            {
                //lay danh sach tin moi dang nhat
                mAccount = (from p in entities.Account
                          where p.UserName.Equals(pUsername)
                          select p).FirstOrDefault();
                if (mAccount != null)
                {
                    if (mAccount.Password.Trim().Equals(EncryptionMD5.ToMd5(pPassword.Trim())))
                    {
                        mETLogin.code = 1;
                        mETLogin.message = "OK.";
                        mETLogin.Account = mAccount;
                    }
                    else
                    {
                        mETLogin.code = 2;
                        mETLogin.message = "Mật khẩu không chính xác.";
                    }
                }
                else
                {
                    mETLogin.code = 0;
                    mETLogin.message = "Không tìm thấy thông tin truy cập.";
                }
                return mETLogin;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

        public string Insert(string email, string password, string salt, string token, DateTime tokenExpireDate)
        {
            var accounts = from p in entities.Account
                        where p.Email.Equals(email) || p.UserName.Equals(email)
                                select p;
            
            if (accounts != null || accounts.Count() < 1) {
                return "exist";
            }
            else {
                var checkAccount = accounts.FirstOrDefault();
                var mAccount = new Account()
                {
                    Email = email,
                    UserName = "",
                    Password = HashPassword(password,salt) ,
                    Salt = salt,
                    Token = token,
                    TokenExpireDate = tokenExpireDate,
                    Status = false
                };
                entities.Account.Add(mAccount);
                entities.SaveChanges();
                return "ok";
            }

        }

        public string InsertAffiliate(string email, string password, string fullname, string mobile="")
        {
           
            email = email.ToLower();
            var accounts = (from p in entities.Account
                           where p.Email.ToLower().Equals(email) || p.UserName.ToLower().Equals(email)
                           select p);

            if (accounts != null || accounts.Count() > 0 )
            {
                return Result.Exists;
            }
            else
            {
                var salt = StringHelper.GenerateString(6);
                var token = getToken(email);

                
                try {
                    var mAccount = new Account()
                    {
                        Email = email,
                        UserName = email,
                        FullName = fullname,
                        Phone = mobile,
                        Password = HashPassword(password, salt),
                        Salt = salt,
                        Token = token,
                        TokenExpireDate = DateTime.Now.AddDays(1),
                        Status = false,
                        Role = 3,
                        Site = Site.affiliate
                    };
                    entities.Account.Add(mAccount);
                    entities.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Console.Write(dbEx);
                }

                SiteRepository config = new SiteRepository(entities);
                var activeAccountUrl = string.Format("{0}account/active", Configs.GetItemConfig("WebDomain") );

                var body =
                        string.Format(
                            "Cảm ơn bạn đã đăng ký tài khoản trên hệ thống của {0}. Mã kích hoạt tài khoản của bạn là {1}. Click vào <a style='color: #007FF0' href='{2}' title='Kích hoạt tài khoản'> đây</a> để kích hoạt tài khoản của bạn.",
                            config.SiteConfig("site-name"), token, activeAccountUrl);
                Email.SendEmail(email, "Đăng ký tài khoản", body);

                return Result.Ok;
            }

        }

        private string getToken(string email, bool forForgotPassword = false)
        {
            return forForgotPassword ? EncryptionMD5.ToMd5(string.Format("{0}|{1}|forgot-die", email, DateTime.Now.Ticks)) : EncryptionMD5.ToMd5(string.Format("{0}|{1}", email, DateTime.Now.Ticks));
        }

        public string UpdateToken(string email, string token,DateTime tokenExpireDate)
        {
            var checkAccount = (from p in entities.Account
                                where p.Email.Equals(email) || p.UserName.Equals(email)
                                select p).FirstOrDefault();
            if (checkAccount == null)
            {
                return "invalid";
            }
            if (checkAccount.Status == true)
            {
                return "active";
            }
           
            checkAccount.Token = token;
            checkAccount.TokenExpireDate = tokenExpireDate;
            entities.SaveChanges();
            return "ok";
        }

        public string Active(string token)
        {
            var checkToken = (from account in entities.Account
                where account.Token == token
                select account
                ).FirstOrDefault();
            if (checkToken == null){
                return "invalid";
            }
            if (checkToken.TokenExpireDate < DateTime.Now){
                return "expire";

            }
            checkToken.Status = true;

            entities.SaveChanges();
            return "ok";

        }

        public string RequestForgotPassword(string email, string forgotPasswordToken, DateTime forgotPasswordTokenExpireDate)
        {

            var checkAccount = (from account in entities.Account
                              where account.Email == email
                              select account
                ).FirstOrDefault();
            if (checkAccount == null)
            {
                return "invalid";
            }
            var newPassword = StringHelper.GenerateString(6);
            checkAccount.ForgotPasswordToken = forgotPasswordToken;
            checkAccount.ForgotPasswordTokenExpireDate = forgotPasswordTokenExpireDate;
            checkAccount.Password = HashPassword(newPassword, checkAccount.Salt);
            entities.SaveChanges();
            return newPassword;
        }

        public string CheckForgotPasswordToken(string token)
        {
            var checkForgotPasswordToken = (from account in entities.Account
                                where account.ForgotPasswordToken == token
                                select account
              ).FirstOrDefault();
            if (checkForgotPasswordToken == null)
            {
                return "invalid";
            }
            if (checkForgotPasswordToken.ForgotPasswordTokenExpireDate < DateTime.Now)
            {
                return "expire";
            }
            return "ok";
        }

        public string ChangePassword(string email, string currentPassword, string newPassword)
        {
            var checkAccount = (from account in entities.Account
                                where account.Email == email
                                select account
                ).FirstOrDefault();
            if (checkAccount == null)
            {
                return "invalid";
            }
            var hashCurrentPassword = HashPassword(currentPassword, checkAccount.Salt);
            if (checkAccount.Password != hashCurrentPassword)
            {
                return "current_wrong";

            }
            checkAccount.Password = HashPassword(newPassword, checkAccount.Salt);           
            entities.SaveChanges();
            return "ok";

        }

        private string HashPassword(string password, string salt)
        {
           return  EncryptionMD5.ToMd5(string.Format("{0}|{1}", password, salt));
        }
        public string CheckAccount(string email, string password)
        {
            var accounts = from p in entities.Account
                                where p.Email.Equals(email) || p.UserName.Equals(email)
                                select p;

            if (accounts == null || accounts.Count() < 1)
            {
                return "invalid";
            }
            var checkAccount = accounts.FirstOrDefault();

            if (checkAccount.Status == false)
            {
                return "not_active";
            }
            var hashPassword = HashPassword(password, checkAccount.Salt);
            if (checkAccount.Password != hashPassword)
            {
                return "invalid";

            }
            return "ok";

        }
        public Account GetByUserId(string userId)
        {
            return (from p in entities.Account
                                where p.Email.Equals(userId) || p.UserName.Equals(userId)
                                select p).FirstOrDefault();
          

        }

        public Account Find(int id)
        {
            return (from p in entities.Account
                    where p.ID == id
                    select p).FirstOrDefault();


        }


        public string CheckEmail(string email)
        {
            var accounts = from p in entities.Account
                                where p.Email.Equals(email) || p.UserName.Equals(email)
                                select p;
            //var checkAccount = ().FirstOrDefault();
            return accounts.Count() > 1 ? "exist" : "not_exist";

        }
        public ETLogin CheckDangKyHome(string pUsername, string pPassword, string pPassWord2, string pEmail, string pFullName, string pMobile)
        {
            Account mAccount = null;
            ETLogin mETLogin = new ETLogin();
            try
            {
                //check xem 2 passs co trung nhau ko ?
                if (pPassword.Trim().Equals(pPassWord2.Trim()))
                {
                    //lay danh sach tin moi dang nhat
                    mAccount = (from p in entities.Account
                                where p.UserName.Equals(pUsername)
                                select p).FirstOrDefault();
                    if (!(mAccount != null))
                    {

                        mAccount = new Account()
                        {
                            Email = pEmail,
                            FullName = pFullName,
                            Password=EncryptionMD5.ToMd5(pPassword),
                            Phone=pMobile,
                            UserName=pUsername,
                            BirthDay=DateTime.Now
                        };
                        entities.AddToAccount(mAccount);
                        entities.SaveChanges();
                        mETLogin.Account = mAccount;
                        mETLogin.code = 1;
                        mETLogin.message = "Đăng ký thành công.";
                    }
                    else
                    {
                        mETLogin.code = 0;
                        mETLogin.message = "Tài khoản đã tồn tại.";
                    }
                }
                else
                {
                    mETLogin.code = 0;
                    mETLogin.message = "Password không trùng khớp.";
                }
                return mETLogin;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }

        public Admin LayAdminTheoId(int pId)
        {
            Admin mAdmin = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mAdmin = (from p in entities.Admin
                            where p.ID == pId
                            select p).FirstOrDefault();
                return mAdmin;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public Admin LayAdminTheoUserName(string pValue)
        {
            Admin mAdmin = null;
            try
            {
                //lay danh sach tin moi dang nhat
                mAdmin = (from p in entities.Admin
                          where p.UserName.Trim().Equals(pValue)
                          select p).FirstOrDefault();
                return mAdmin;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<Admin> LayAdminTheoTrangAndType(int pcurrent, int psize, int pType)
        {
            List<Admin> mList = null;
            try
            {
                if (pType == 0)
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.Admin
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                }
                else
                {
                    mList = (from p in entities.Admin
                             where p.Role==pType
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                           .Take(psize).ToList();
                }
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<Account> LayAccountTheoTrangAndType(int pcurrent, int psize, int pType)
        {
            List<Account> mList = null;
            try
            {
                if (pType == 0)
                {
                    //lay danh sach tin moi dang nhat
                    mList = (from p in entities.Account
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                }
                else
                {
                    mList = (from p in entities.Account
                             where p.Role == pType
                             orderby p.ID descending
                             select p).Skip((pcurrent - 1) * psize)
                           .Take(psize).ToList();
                }
                return mList;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        public List<Admin> GetListAdminByType(byte type)
        {
            return (from p in entities.Admin
                    where p.Type == type
                    select p).ToList();
        }

        public string UpdateObject(Account data)
        {
            try
            {
                var check = (from c in entities.Account
                             where c.ID == data.ID
                             select c
                    ).FirstOrDefault();
                if (check != null)
                {
                    check.FullName = data.FullName;
                    check.Phone = data.Phone;
                    check.Address = data.Address;

                    check.bank_name = data.bank_name;
                    check.bank_number = data.bank_number;
                    check.bank_account = data.bank_account;
                    check.bank_address = data.bank_address;

                    if (data.cmt_back != null && data.cmt_back.Length > 0)
                    {
                        check.cmt_back = data.cmt_back;
                    }
                    else {
                        check.cmt_back = check.cmt_back;
                    }

                    if (data.cmt_front != null && data.cmt_front.Length > 0)
                    {
                        check.cmt_front = data.cmt_front;
                    }
                    
                        
                    entities.SaveChanges();
                    return Result.Ok;
                }
                return Result.Exists;


            }
            catch (Exception ex)
            {
                return ex.ToString();
            }


        }
    }
}