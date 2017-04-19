using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace V308CMS.Data
{
    public class V308Connection
    {
        private static volatile V308Connection instance;
        private static string myConnectionString = "";
        private static double _timePeriod = 0.0;
        private static string _server = "";
        private static string _database = "";
        private static string _port = "";
        private static string _username = "";
        private static string _password = "";
        private static string _serverId = "";
        private static int _portCache = 112211;
        private int _mTxtIdUserDemo = 0;
        private int _mTxtIdHeroDemo = 0;
        private int _mTxtIdLandDemo = 0;
        private int _txtIdLeagueDemo = 0;
        private static string _serverCache = "";//
        private V308Connection() { }

        public static V308Connection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (myConnectionString)
                    {
                        if (instance == null)
                            instance = new V308Connection();
                    }
                }

                return instance;
            }
        }

        public  string ConnectionString
        {
            get { return myConnectionString; }
            set { myConnectionString = value; }
        }
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }
        public string Database
        {
            get { return _database; }
            set { _database = value; }
        }
        public string Port
        {
            get { return _port; }
            set { _port = value; }
        }
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public double TimePeriod
        {
            get { return _timePeriod; }
            set { _timePeriod = value; }
        }
        public string ServerId
        {
            get { return _serverId; }
            set { _serverId = value; }
        }
        public string ServerCache
        {
            get { return _serverCache; }
            set { _serverCache = value; }
        }
        public int PortCache
        {
            get { return _portCache; }
            set { _portCache = value; }
        }

        public int txtIdLeagueDemo
        {
            get { return _txtIdLeagueDemo; }
            set { _txtIdLeagueDemo = value; }
        }
        public int mTxtIdUserDemo
        {
            get { return _mTxtIdUserDemo; }
            set { _mTxtIdUserDemo = value; }
        }
        public int mTxtIdHeroDemo
        {
            get { return _mTxtIdHeroDemo; }
            set { _mTxtIdHeroDemo = value; }
        }
        public int mTxtIdLandDemo
        {
            get { return _mTxtIdLandDemo; }
            set { _mTxtIdLandDemo = value; }
        }
        
    }
}