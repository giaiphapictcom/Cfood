using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using V308CMS.Common;

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
        public ETLogin CheckDangNhap(string  pUsername,string pPassword)
        {
            Admin mAdmin = null;
            ETLogin mETLogin=new ETLogin();
            try
            {
                //lay danh sach tin moi dang nhat
                mAdmin = (from p in entities.Admin
                          where p.UserName.Equals(pUsername) || p.Email.Equals(pUsername)
                            select p).FirstOrDefault();
                if (mAdmin != null)
                {
                    if (mAdmin.Password.Trim().Equals(EncryptionMD5.ToMd5(pPassword.Trim())))
                    {
                        mETLogin.code = 1;
                        mETLogin.message = "OK.";
                        mETLogin.Admin = mAdmin;
                        mETLogin.role = int.Parse(mAdmin.Role.ToString());
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
    }
}