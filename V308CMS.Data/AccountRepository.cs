using System;
using System.Collections.Generic;
using System.Linq;
using V308CMS.Common;
using V308CMS.Data.Helpers;

namespace V308CMS.Data
{
    public class AccountRepository
    {


        public AccountRepository()
        {

        }


        public Account LayTinTheoId(int pId)
        {
            using (var entities = new V308CMSEntities())
            {

                return (from p in entities.Account
                        where p.ID == pId
                        select p).FirstOrDefault();
            }
        }

        public ETLogin CheckDangNhap(string  pUsername,string pPassword,string site= Site.home)

        {
            using (var entities = new V308CMSEntities())
            {
                ETLogin mEtLogin = new ETLogin();
                //lay danh sach tin moi dang nhat
                var user = (from p in entities.Account
                          where (p.UserName.ToLower().Equals(pUsername.ToLower()) || p.Email.ToLower().Equals(pUsername.ToLower()) ) && p.Site == site && p.Status == true

                            select p).FirstOrDefault();
                if (user != null)
                {
                    //if (user.Password.Trim().Equals(EncryptionMD5.ToMd5(pPassword.Trim())))
                    if (user.Salt.Length > 0 && user.Password != HashPassword(pPassword.Trim(), user.Salt))
                    {
                        mEtLogin.code = 1;
                        mEtLogin.message = "OK.";
                        mEtLogin.Account = user;
                        mEtLogin.role = int.Parse(user.Role.ToString());
                    }
                    else
                    {
                        mEtLogin.code = 2;
                        mEtLogin.message = "Mật khẩu không chính xác.";
                    }
                }
                else
                {
                    mEtLogin.code = 0;
                    mEtLogin.message = "Không tìm thấy thông tin truy cập.";
                }
                return mEtLogin;
            }
        }
        public ETLogin CheckDangNhapHome(string pUsername, string pPassword)
        {
            using (var entities = new V308CMSEntities())
            {
                ETLogin mEtLogin = new ETLogin();
                //lay danh sach tin moi dang nhat
                var mAccount = (from p in entities.Account
                                where p.UserName.Equals(pUsername)
                                select p).FirstOrDefault();
                if (mAccount != null)
                {
                    if (mAccount.Password.Trim().Equals(EncryptionMD5.ToMd5(pPassword.Trim())))
                    {
                        mEtLogin.code = 1;
                        mEtLogin.message = "OK.";
                        mEtLogin.Account = mAccount;
                    }
                    else
                    {
                        mEtLogin.code = 2;
                        mEtLogin.message = "Mật khẩu không chính xác.";
                    }
                }
                else
                {
                    mEtLogin.code = 0;
                    mEtLogin.message = "Không tìm thấy thông tin truy cập.";
                }
                return mEtLogin;
            }
        }

        public string Insert(string userName, string email, string password, string salt, string fullName, string avatar, string site = Helpers.Site.home)
        {
            using (var entities = new V308CMSEntities())
            {
                var accounts = (from p in entities.Account
                                where p.Email.Equals(email) || p.UserName.Equals(userName)
                                select p).FirstOrDefault();

                if (accounts != null)
                {
                    //if (accounts.FullName != fullName || accounts.UserName != userName || accounts.Avatar != avatar)
                    if (accounts.UserName != userName)
                    {
                        accounts.FullName = fullName;
                        accounts.UserName = userName;
                        accounts.Avatar = avatar;
                        accounts.Site = site;
                        entities.SaveChanges();
                    }
                   
                    return accounts.ID.ToString();
                }
                var mAccount = new Account()
                {
                    Email = email,
                    UserName = userName,
                    Password = HashPassword(password, salt),
                    Salt = salt,
                    FullName = fullName,
                    Avatar = avatar,
                    Status = true
                };
                entities.Account.Add(mAccount);
                entities.SaveChanges();
                return mAccount.ID.ToString();
            }

        }
        public string Insert(string email, string password, string salt, string token, DateTime tokenExpireDate)
        {
            using (var entities = new V308CMSEntities())
            {
                var accounts = (from p in entities.Account
                                where p.Email.Equals(email) || p.UserName.Equals(email)
                                select p).FirstOrDefault();

                if (accounts != null)
                {
                    return "exist";
                }
                var mAccount = new Account()
                {
                    Email = email,
                    UserName = email,
                    Password = HashPassword(password, salt),
                    Salt = salt,
                    Token = token,
                    TokenExpireDate = tokenExpireDate,
                    Status = true,
                    Date = DateTime.Now
                };
                entities.Account.Add(mAccount);
                entities.SaveChanges();
                return "ok";
            }


        }
        public Account FindEmail(string email)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from user in entities.Account
                        where user.Email == email
                        select user
              ).FirstOrDefault();
            }
        }
        public string InsertAffiliate(string email, string password, string fullname, string mobile = "")
        {

            using (var entities = new V308CMSEntities())
            {
                var accounts = (from p in entities.Account
                                where p.Email.Equals(email) || p.UserName.Equals(email)
                                select p).FirstOrDefault();

                if (accounts != null)
                {
                    return "exist";
                }
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
                        Status = true,
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

                SiteRepository config = new SiteRepository();
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

        public string UpdateToken(string email, string token, DateTime tokenExpireDate)
        {
            using (var entities = new V308CMSEntities())
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

        }

        public string Active(string token)
        {
            using (var entities = new V308CMSEntities())
            {
                var checkToken = (from account in entities.Account
                                  where account.Token == token
                                  select account
              ).FirstOrDefault();
                if (checkToken == null)
                {
                    return "invalid";
                }
                if (checkToken.TokenExpireDate < DateTime.Now)
                {
                    return "expire";

                }
                checkToken.Status = true;

                entities.SaveChanges();
                return "ok";
            }


        }

        public string RequestForgotPassword(string email, string forgotPasswordToken, DateTime forgotPasswordTokenExpireDate)
        {
            using (var entities = new V308CMSEntities())
            {
                var checkAccount = (from account in entities.Account
                                    where account.Email == email || account.UserName == email
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


        }

        public string CheckForgotPasswordToken(string token)
        {
            using (var entities = new V308CMSEntities())
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

        }

        public string ChangePassword(string email, string currentPassword, string newPassword)
        {
            using (var entities = new V308CMSEntities())
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


        }

        private string HashPassword(string password, string salt)
        {
            return EncryptionMD5.ToMd5($"{password}|{salt}");
        }
        public string CheckAccount(string email, string password)
        {
            using (var entities = new V308CMSEntities())
            {

                var checkAccount = (from p in entities.Account
                                    where p.Email.Equals(email) || p.UserName.Equals(email)
                                    select p).FirstOrDefault();
                if (checkAccount == null)
                {
                    return "invalid";
                }

                if (checkAccount.Status == false)
                {
                    return "not_active";
                }            
                if (checkAccount.Password != HashPassword(password, checkAccount.Salt))
                {
                    return "invalid";
                }
                return $"{checkAccount.ID}|{checkAccount.Avatar}";
            }


        }
        public Account GetByUserId(int userId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from user in entities.Account
                        where user.ID == userId
                        select user).FirstOrDefault();

            }



        }

        public Account Find(int id)
        {
            using (var entities = new V308CMSEntities()) {
                return (from p in entities.Account
                        where p.ID == id
                        select p).FirstOrDefault();
            }
        }


        public string CheckEmail(string email)
        {
            using (var entities = new V308CMSEntities())
            {
                var accounts = (from p in entities.Account
                                where p.Email.Equals(email) || p.UserName.Equals(email)
                                select p).FirstOrDefault();
                return accounts != null ? "exist" : "not_exist";
            }


        }
        public ETLogin CheckDangKyHome(string pUsername, string pPassword, string pPassWord2, string pEmail, string pFullName, string pMobile)
        {
            using (var entities = new V308CMSEntities())
            {
                ETLogin mEtLogin = new ETLogin();
                //check xem 2 passs co trung nhau ko ?
                if (pPassword.Trim().Equals(pPassWord2.Trim()))
                {
                    //lay danh sach tin moi dang nhat
                    var mAccount = (from p in entities.Account
                                    where p.UserName.Equals(pUsername)
                                    select p).FirstOrDefault();
                    if (mAccount == null)
                    {

                        mAccount = new Account()
                        {
                            Email = pEmail,
                            FullName = pFullName,
                            Password = EncryptionMD5.ToMd5(pPassword),
                            Phone = pMobile,
                            UserName = pUsername,
                            BirthDay = DateTime.Now
                        };
                        entities.AddToAccount(mAccount);
                        entities.SaveChanges();
                        mEtLogin.Account = mAccount;
                        mEtLogin.code = 1;
                        mEtLogin.message = "Đăng ký thành công.";
                    }
                    else
                    {
                        mEtLogin.code = 0;
                        mEtLogin.message = "Tài khoản đã tồn tại.";
                    }
                }
                else
                {
                    mEtLogin.code = 0;
                    mEtLogin.message = "Password không trùng khớp.";
                }
                return mEtLogin;
            }
        }

        public Admin LayAdminTheoId(int pId)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Admin
                        where p.ID == pId
                        select p).FirstOrDefault();
            }
        }
        public Admin LayAdminTheoUserName(string pValue)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Admin
                        where p.UserName.Trim().Equals(pValue)
                        select p).FirstOrDefault();
            }
        }
        public List<Admin> LayAdminTheoTrangAndType(int pcurrent, int psize, int pType)
        {
            using (var entities = new V308CMSEntities())
            {
                if (pType == 0)
                {

                    return (from p in entities.Admin
                            orderby p.ID descending
                            select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                }
                return (from p in entities.Admin
                        where p.Role == pType
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                    .Take(psize).ToList();
            }
        }
        public List<Account> LayAccountTheoTrangAndType(int pcurrent, int psize, int pType)
        {
            using (var entities = new V308CMSEntities())
            {
                if (pType == 0)
                {
                    return (from p in entities.Account
                            orderby p.ID descending
                            select p).Skip((pcurrent - 1) * psize)
                             .Take(psize).ToList();
                }
                return (from p in entities.Account
                        where p.Role == pType
                        orderby p.ID descending
                        select p).Skip((pcurrent - 1) * psize)
                    .Take(psize).ToList();
            }
        }
        public List<Admin> GetListAdminByType(byte type)
        {
            using (var entities = new V308CMSEntities())
            {
                return (from p in entities.Admin
                        where p.Type == type
                        select p).ToList();

            }

        }
        public string UpdateObject(Account data)
        {
            try
            {
                using (var entities = new V308CMSEntities())
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
                        check.affiliate_code = data.affiliate_code;

                        if (data.cmt_back != null && data.cmt_back.Length > 0)
                        {
                            check.cmt_back = data.cmt_back;
                        }
                        else
                        {
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
                    


            }
            catch (Exception ex)
            {
                return ex.ToString();
            }


        }

        public List<Account> getUseridOfByAffiliate(int affiliate_id=0) {
            List<Account> items = new List<Account>();
            using (var entities = new V308CMSEntities())
            {

                var users = entities.Account.Where(p=> p.affiliate_id == affiliate_id);
                items = users.ToList();
            }
            return items;
        }

    }
}