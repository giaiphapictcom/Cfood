using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace V308CMS.Data
{
    public class ETLogin
    {
        private int _code;
        private string _message;
        Admin _Admin;
        Account _Account;
        public int code
        {
            get { return _code; }
            set { _code = value; }
        }

        private int _role;
        public int role
        {
            get { return _role; }
            set { _role = value; }
        }

        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        public Admin Admin
        {
            get { return _Admin; }
            set { _Admin = value; }
        }
        public Account Account
        {
            get { return _Account; }
            set { _Account = value; }
        }
    }
    public class BannerPage
    {
        public List<News> List { get; set; }
    }
    public class CommonModel
    {
        private int _Code;
        private string _Message;
        private string _Html;

        public int Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public string Html
        {
            get { return _Html; }
            set { _Html = value; }
        }
    }
    public class NewsPage
    {
        private int _code;
        private string _message;
        private string _Html;
        private string _HtmlNhomTin;
        private string _HtmlNhomTin2;
        private string _HtmlNhomTin3;
        private News _pNews;
        int _Page;
        int _NextPage;
        int _BackPage;
        bool _IsEnd;
        int _TypeId;
        public NewsGroups NewsGroups { get; set; }
        public List<News> List { get; set; }        
        public int TypeId
        {
            get { return _TypeId; }
            set { _TypeId = value; }
        }
        public NewsPage()
        {
            _Html = "";
            _Page = 0;
            _IsEnd = false;
        }
        public int code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        public News pNews
        {
            get { return _pNews; }
            set { _pNews = value; }
        }
        public string Html
        {
            get { return _Html; }
            set { _Html = value; }
        }
        public string HtmlNhomTin
        {
            get { return _HtmlNhomTin; }
            set { _HtmlNhomTin = value; }
        }
        public string HtmlNhomTin2
        {
            get { return _HtmlNhomTin2; }
            set { _HtmlNhomTin2 = value; }
        }
        public string HtmlNhomTin3
        {
            get { return _HtmlNhomTin3; }
            set { _HtmlNhomTin3 = value; }
        }
        public bool IsEnd
        {
            get
            {
                return _IsEnd;
            }
            set
            {
                _IsEnd = value;
            }
        }
        public int Page
        {
            get
            {
                if (_Page >= 1)
                {
                    return _Page;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                _Page = value;
            }
        }
        public int NextPage
        {
            get
            {
                if (_IsEnd)
                {
                    return _Page;
                }
                else
                {
                    return (_Page + 1);
                }
            }
            set
            {
                _NextPage = value;
            }
        }
        public int BackPage
        {
            get
            {
                if (_Page <= 1)
                {
                    return 1;
                }
                else
                {
                    return (_Page - 1);
                }
            }
            set
            {
                _BackPage = value;
            }
        }
    }

    public class NewsGroupPage
    {
        private int _code;
        private string _message;
        private string _Html;
        private string _HtmlNhomTin;
        private NewsGroups _pNewsGroups;
        public List<News> NewsList;
        public string Name;
        int _Page;
        int _NextPage;
        int _BackPage;
        bool _IsEnd;
        int _TypeId;

        public int TypeId
        {
            get { return _TypeId; }
            set { _TypeId = value; }
        }
        public NewsGroupPage()
        {
            _Html = "";
            _Page = 0;
            _IsEnd = false;
        }
        public int code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        public NewsGroups pNewsGroups
        {
            get { return _pNewsGroups; }
            set { _pNewsGroups = value; }
        }
        public string Html
        {
            get { return _Html; }
            set { _Html = value; }
        }
        public string HtmlNhomTin
        {
            get { return _HtmlNhomTin; }
            set { _HtmlNhomTin = value; }
        }
        public bool IsEnd
        {
            get
            {
                return _IsEnd;
            }
            set
            {
                _IsEnd = value;
            }
        }
        public int Page
        {
            get
            {
                if (_Page >= 1)
                {
                    return _Page;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                _Page = value;
            }
        }
        public int NextPage
        {
            get
            {
                if (_IsEnd)
                {
                    return _Page;
                }
                else
                {
                    return (_Page + 1);
                }
            }
            set
            {
                _NextPage = value;
            }
        }
        public int BackPage
        {
            get
            {
                if (_Page <= 1)
                {
                    return 1;
                }
                else
                {
                    return (_Page - 1);
                }
            }
            set
            {
                _BackPage = value;
            }
        }
    }

    public class ImagesPage
    {
        private int _code;
        private string _message;
        private string _Html;
        private string _HtmlNhom;
        private Image _pImage;
        private ImageType _pImageType;
        int _Page;
        int _NextPage;
        int _BackPage;
        bool _IsEnd;
        int _TypeId;
        public ImagesPage()
        {
            _Html = "";
            _Page = 0;
            _IsEnd = false;
        }
        public int code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        public Image pImage
        {
            get { return _pImage; }
            set { _pImage = value; }
        }
        public ImageType pImageType
        {
            get { return _pImageType; }
            set { _pImageType = value; }
        }
        public string Html
        {
            get { return _Html; }
            set { _Html = value; }
        }
        public string HtmlNhom
        {
            get { return _HtmlNhom; }
            set { _HtmlNhom = value; }
        }
        public bool IsEnd
        {
            get
            {
                return _IsEnd;
            }
            set
            {
                _IsEnd = value;
            }
        }
        public int Page
        {
            get
            {
                if (_Page >= 1)
                {
                    return _Page;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                _Page = value;
            }
        }
        public int NextPage
        {
            get
            {
                if (_IsEnd)
                {
                    return _Page;
                }
                else
                {
                    return (_Page + 1);
                }
            }
            set
            {
                _NextPage = value;
            }
        }
        public int BackPage
        {
            get
            {
                if (_Page <= 1)
                {
                    return 1;
                }
                else
                {
                    return (_Page - 1);
                }
            }
            set
            {
                _BackPage = value;
            }
        }
        public int TypeId
        {
            get { return _TypeId; }
            set { _TypeId = value; }
        }
    }
    public class AccountPage
    {
        private int _code;
        private string _message;
        private string _Html;
        private string _HtmlNhom;
        private Account _pAccount;
        private Admin _pAdmin;
        int _Page;
        int _NextPage;
        int _BackPage;
        bool _IsEnd;
        int _TypeId;
        public AccountPage()
        {
            _Html = "";
            _Page = 0;
            _IsEnd = false;
        }
        public int code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        public Account pAccount
        {
            get { return _pAccount; }
            set { _pAccount = value; }
        }
        public Admin pAdmin
        {
            get { return _pAdmin; }
            set { _pAdmin = value; }
        }
        public string Html
        {
            get { return _Html; }
            set { _Html = value; }
        }
        public string HtmlNhom
        {
            get { return _HtmlNhom; }
            set { _HtmlNhom = value; }
        }
        public bool IsEnd
        {
            get
            {
                return _IsEnd;
            }
            set
            {
                _IsEnd = value;
            }
        }
        public int Page
        {
            get
            {
                if (_Page >= 1)
                {
                    return _Page;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                _Page = value;
            }
        }
        public int NextPage
        {
            get
            {
                if (_IsEnd)
                {
                    return _Page;
                }
                else
                {
                    return (_Page + 1);
                }
            }
            set
            {
                _NextPage = value;
            }
        }
        public int BackPage
        {
            get
            {
                if (_Page <= 1)
                {
                    return 1;
                }
                else
                {
                    return (_Page - 1);
                }
            }
            set
            {
                _BackPage = value;
            }
        }
        public int TypeId
        {
            get { return _TypeId; }
            set { _TypeId = value; }
        }
    }
    public class FilePage
    {
        private int _code;
        private string _message;
        private string _Html;
        private string _HtmlNhom;
        private File _pFile;
        private FileType _pFileType;
        int _Page;
        int _NextPage;
        int _BackPage;
        bool _IsEnd;
        int _TypeId;
        public FilePage()
        {
            _Html = "";
            _Page = 0;
            _IsEnd = false;
        }
        public int code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        public File pFile
        {
            get { return _pFile; }
            set { _pFile = value; }
        }
        public FileType pFileType
        {
            get { return _pFileType; }
            set { _pFileType = value; }
        }
        public string Html
        {
            get { return _Html; }
            set { _Html = value; }
        }
        public string HtmlNhom
        {
            get { return _HtmlNhom; }
            set { _HtmlNhom = value; }
        }
        public bool IsEnd
        {
            get
            {
                return _IsEnd;
            }
            set
            {
                _IsEnd = value;
            }
        }
        public int Page
        {
            get
            {
                if (_Page >= 1)
                {
                    return _Page;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                _Page = value;
            }
        }
        public int NextPage
        {
            get
            {
                if (_IsEnd)
                {
                    return _Page;
                }
                else
                {
                    return (_Page + 1);
                }
            }
            set
            {
                _NextPage = value;
            }
        }
        public int BackPage
        {
            get
            {
                if (_Page <= 1)
                {
                    return 1;
                }
                else
                {
                    return (_Page - 1);
                }
            }
            set
            {
                _BackPage = value;
            }
        }
        public int TypeId
        {
            get { return _TypeId; }
            set { _TypeId = value; }
        }
    }
    public class ProductPage
    {
        private int _code;
        private string _message;
        private string _Html;
        private string _HtmlNhom;
        private string _HtmlNhom2;
        private string _HtmlNhom3;
        private string _HtmlNhom4;
        private string _HtmlProductImage;
        private string _HtmlProductAttribute;
        private Product _pProduct;
        private ProductDistributor _pProductDistributor;
        private ProductManufacturer _pProductManufacturer;
        private ProductType _pProductType;
        private ProductOrder _pProductOrder;
        int _Page;
        int _NextPage;
        int _BackPage;
        bool _IsEnd;
        int _TypeId;
        int _pValue1;
        int _pValue2;
        int _pGroupId;
        public List<ProductType> ProductTypeLt { get; set; }
        public List<Market> MarketList { get; set; }
        public ShopCart ShopCart { get; set; }
        public int Voucher { get; set; }
        public int Market { get; set; }
        public string Key { get; set; }
        public int CountOrder1 { get; set; }
        public int CountOrder2 { get; set; }
        public int CountOrderHuy { get; set; }
        public int CountOrderGiaoMotPhan { get; set; }
        public int Money { get; set; }
        public ProductPage()
        {
            _Html = "";
            _Page = 0;
            _IsEnd = false;
        }
        public int code
        {
            get { return _code; }
            set { _code = value; }
        }
        public int pValue1
        {
            get { return _pValue1; }
            set { _pValue1 = value; }
        }
        public int pValue2
        {
            get { return _pValue2; }
            set { _pValue2 = value; }
        }
        public int pGroupId
        {
            get { return _pGroupId; }
            set { _pGroupId = value; }
        }
        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        public Product pProduct
        {
            get { return _pProduct; }
            set { _pProduct = value; }
        }
        public ProductOrder pProductOrder
        {
            get { return _pProductOrder; }
            set { _pProductOrder = value; }
        }
        public ProductDistributor pProductDistributor
        {
            get { return _pProductDistributor; }
            set { _pProductDistributor = value; }
        }
        public ProductManufacturer pProductManufacturer
        {
            get { return _pProductManufacturer; }
            set { _pProductManufacturer = value; }
        }
        public ProductType pProductType
        {
            get { return _pProductType; }
            set { _pProductType = value; }
        }
        public string Html
        {
            get { return _Html; }
            set { _Html = value; }
        }
        public string HtmlNhom
        {
            get { return _HtmlNhom; }
            set { _HtmlNhom = value; }
        }
        public string HtmlNhom2
        {
            get { return _HtmlNhom2; }
            set { _HtmlNhom2 = value; }
        }
        public string HtmlNhom3
        {
            get { return _HtmlNhom3; }
            set { _HtmlNhom3 = value; }
        }
        public string HtmlNhom4
        {
            get { return _HtmlNhom4; }
            set { _HtmlNhom4 = value; }
        }
        public string HtmlProductAttribute
        {
            get { return _HtmlProductAttribute; }
            set { _HtmlProductAttribute = value; }
        }
        public string HtmlProductImage
        {
            get { return _HtmlProductImage; }
            set { _HtmlProductImage = value; }
        }
        public bool IsEnd
        {
            get
            {
                return _IsEnd;
            }
            set
            {
                _IsEnd = value;
            }
        }
        public int Page
        {
            get
            {
                if (_Page >= 1)
                {
                    return _Page;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                _Page = value;
            }
        }
        public int NextPage
        {
            get
            {
                if (_IsEnd)
                {
                    return _Page;
                }
                else
                {
                    return (_Page + 1);
                }
            }
            set
            {
                _NextPage = value;
            }
        }
        public int BackPage
        {
            get
            {
                if (_Page <= 1)
                {
                    return 1;
                }
                else
                {
                    return (_Page - 1);
                }
            }
            set
            {
                _BackPage = value;
            }
        }
        public int TypeId
        {
            get { return _TypeId; }
            set { _TypeId = value; }
        }
    }

    public class SupportPage
    {
        private int _Code;
        private string _Message;
        private string _Html;
        int _Page;
        int _NextPage;
        int _BackPage;
        bool _IsEnd;
        private string _HtmlNhom;
        private SupportType _pSupportType;
        private Support _pSupport;


        public SupportType pSupportType
        {
            get { return _pSupportType; }
            set { _pSupportType = value; }
        }
        public Support pSupport
        {
            get { return _pSupport; }
            set { _pSupport = value; }
        }
        public string HtmlNhom
        {
            get { return _HtmlNhom; }
            set { _HtmlNhom = value; }
        }
        public int Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public string Html
        {
            get { return _Html; }
            set { _Html = value; }
        }
        public bool IsEnd
        {
            get
            {
                return _IsEnd;
            }
            set
            {
                _IsEnd = value;
            }
        }
        public int Page
        {
            get
            {
                if (_Page >= 1)
                {
                    return _Page;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                _Page = value;
            }
        }
        public int NextPage
        {
            get
            {
                if (_IsEnd)
                {
                    return _Page;
                }
                else
                {
                    return (_Page + 1);
                }
            }
            set
            {
                _NextPage = value;
            }
        }
        public int BackPage
        {
            get
            {
                if (_Page <= 1)
                {
                    return 1;
                }
                else
                {
                    return (_Page - 1);
                }
            }
            set
            {
                _BackPage = value;
            }
        }
    }

    public class ShopCart
    {
        private int _Code;
        private string _Name;
        private int _Price;
        List<Product> _List;
        private int _TotalPrice;
        private int _Voucher;
        private string _VoucherName;
        private int _ServicePrice;
        public Account Account { get; set; }
        public ShopCart()
        {
            this._Code = 0;
            this._Name = "";
            this._Price = 0;
            this._Voucher = 0;
            this._VoucherName = "";
            this._ServicePrice = 0;
            this._List = new List<Product>();
        }
        public int Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        public int ServicePrice
        {
            get { return _ServicePrice; }
            set { _ServicePrice = value; }
        }
        public int Voucher
        {
            get { return _Voucher; }
            set { _Voucher = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string VoucherName
        {
            get { return _VoucherName; }
            set { _VoucherName = value; }
        }
        public int Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        public List<Product> List
        {
            get { return _List; }
            set { _List = value; }
        }
        public int TotalPrice
        {
            get { return _TotalPrice; }
            set { _TotalPrice = value; }
        }
        public double? getTotalPrice()
        {
            double? mValue = 0;
            foreach (Product it in List)
            {
                mValue = (mValue + (it.Number * ((it.Price - ((it.Price / 100) * it.SaleOff)))));
            }
            mValue = (mValue - ((mValue / 100) * Voucher));
            mValue = (mValue + ServicePrice);
            return mValue;
        }
        public double? getTotalBefore()
        {
            double? mValue = 0;
            foreach (Product it in List)
            {
                mValue = (mValue + (it.Number * ((it.Price - ((it.Price / 100) * it.SaleOff)))));
            }
            return mValue;
        }
    }
    public class ShopCartPage
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Html { get; set; }
        public ShopCart ShopCart { get; set; }
    }
    public class ShopCartModel
    {
        private int _Code;
        private string _Message;
        private string _Html;
        private int _TotalPrice;
        private Account _User;
        public int Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
        public Account User
        {
            get { return _User; }
            set { _User = value; }
        }
        public int TotalPrice
        {
            get { return _TotalPrice; }
            set { _TotalPrice = value; }
        }

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        public string Html
        {
            get { return _Html; }
            set { _Html = value; }
        }
    }


    public class UserCheckDangKy
    {
        int _result = 0;
        string _message = "";
        public int result
        {
            get
            {
                return this._result;
            }
            set
            {
                this._result = value;
            }
        }

        public string message
        {
            get
            {
                return this._message;
            }
            set
            {
                this._message = value;
            }
        }
    }

    public class TinTucChuyenBiet
    {

        string _khuyenmai = "";
        string _caudo = "";
        string _salon = "";
        string _tamsu = "";
        public string khuyenmai
        {
            get
            {
                return this._khuyenmai;
            }
            set
            {
                this._khuyenmai = value;
            }
        }

        public string caudo
        {
            get
            {
                return this._caudo;
            }
            set
            {
                this._caudo = value;
            }
        }
        public string salon
        {
            get
            {
                return this._salon;
            }
            set
            {
                this._salon = value;
            }
        }
        public string tamsu
        {
            get
            {
                return this._tamsu;
            }
            set
            {
                this._tamsu = value;
            }
        }
    }

    public class ProductTypePage
    {
        public List<ProductType> List { get; set; }
        public List<ProductType> CategorySub { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }
    }
    public class ProductTypePageContainer
    {
        public List<ProductTypePage> List { get; set; }
        public List<Image> ImageList { get; set; }
        public News mNews { get; set; }
    }
    public class ProductCategoryPage
    {
        public List<Product> List { get; set; }
        public int ProductTotal { get; set; }
        public bool Paging { get; set; }
        public Market Market { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
        public string Value { get; set; }
        ///
        int _Page;
        int _NextPage;
        int _BackPage;
        bool _IsEnd;
        public bool IsEnd
        {
            get
            {
                return _IsEnd;
            }
            set
            {
                _IsEnd = value;
            }
        }
        public int Page
        {
            get
            {
                if (_Page >= 1)
                {
                    return _Page;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                _Page = value;
            }
        }
        public int NextPage
        {
            get
            {
                if (_IsEnd)
                {
                    return _Page;
                }
                else
                {
                    return (_Page + 1);
                }
            }
            set
            {
                _NextPage = value;
            }
        }
        public int BackPage
        {
            get
            {
                if (_Page <= 1)
                {
                    return 1;
                }
                else
                {
                    return (_Page - 1);
                }
            }
            set
            {
                _BackPage = value;
            }
        }
    }
    public class ProductCategoryPageContainer
    {
        public List<ProductCategoryPage> List { get; set; }
        public List<ProductType> ProductTypeList { get; set; }
        public List<Product> ProductList { get; set; }
        public List<Product> Products { get; set; }
        public List<Product> BestSeller { get; set; }
        public List<Brand> Brands { get; set; }
        
        public ProductType ProductType { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
        public string Value { get; set; }
        public int ProductTotal { get; set; }
        ///
        int _Page;
        int _NextPage;
        int _BackPage;
        bool _IsEnd;
        public bool IsEnd
        {
            get
            {
                return _IsEnd;
            }
            set
            {
                _IsEnd = value;
            }
        }
        public int Page
        {
            get
            {
                if (_Page >= 1)
                {
                    return _Page;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                _Page = value;
            }
        }
        public int NextPage
        {
            get
            {
                if (_IsEnd)
                {
                    return _Page;
                }
                else
                {
                    return (_Page + 1);
                }
            }
            set
            {
                _NextPage = value;
            }
        }
        public int BackPage
        {
            get
            {
                if (_Page <= 1)
                {
                    return 1;
                }
                else
                {
                    return (_Page - 1);
                }
            }
            set
            {
                _BackPage = value;
            }
        }
    }

    
    public class CategoryPage{
         public List<Product> Products { get; set; }
         public int ProductTotal { get; set; }
        
    }
    public class ProductCategoryPageBox
    {
        public List<ProductCategoryPage> List { get; set; }
        public Market Market { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
    }
    public class ProductDetailPage
    {
        public Product Product { get; set; }
        public ProductType ProductType { get; set; }
        public List<Product> RelatedList { get; set; }
        public List<Product> MarketList { get; set; }
        public List<Product> DiscountList { get; set; }
        public Market Market { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
        public List<ProductType> ProductTypeList { get; set; }
        public List<Product> ProductLastest { get; set; }
        public List<ProductImage> Images { get; set; }

        
    }
    public class ProductSlideShow
    {
        public List<ProductImage> Images { get; set; }
    }
    public class HeaderPage
    {
        public List<Market> Market { get; set; }
        public Account Account { get; set; }
        public ShopCart ShopCart { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
    }
    public class MarketIndexPage
    {
        public List<MarketProductType> MarketProductTypeList { get; set; }
        public List<Product> ProductList { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
    }
    public class IndexPage
    {
        public List<ProductType> ProductTypeList { get; set; }
        public List<Product> ProductList { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
        public string ImageBanner { get; set; }
    }
    public class IndexPageContainer
    {
        public List<IndexPage> IndexPageList { get; set; }
        public List<Product> BestBuyList { get; set; }
        public List<Product> BestSoCheList { get; set; }
        public List<ProductType> ProductTypeList { get; set; }
        
        public List<Product> ProductLastest { get; set; }

        public string Name { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
    }
    public class MarketPage
    {
        private int _code;
        private string _message;
        private string _Html;
        private string _HtmlNhom;
        private Market _Market;
        private Admin _pAdmin;
        int _Page;
        int _NextPage;
        int _BackPage;
        bool _IsEnd;
        int _TypeId;
        public MarketPage()
        {
            _Html = "";
            _Page = 0;
            _IsEnd = false;
        }
        public int code
        {
            get { return _code; }
            set { _code = value; }
        }

        public string message
        {
            get { return _message; }
            set { _message = value; }
        }
        public Market Market
        {
            get { return _Market; }
            set { _Market = value; }
        }
        public Admin pAdmin
        {
            get { return _pAdmin; }
            set { _pAdmin = value; }
        }
        public string Html
        {
            get { return _Html; }
            set { _Html = value; }
        }
        public string HtmlNhom
        {
            get { return _HtmlNhom; }
            set { _HtmlNhom = value; }
        }
        public bool IsEnd
        {
            get
            {
                return _IsEnd;
            }
            set
            {
                _IsEnd = value;
            }
        }
        public int Page
        {
            get
            {
                if (_Page >= 1)
                {
                    return _Page;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                _Page = value;
            }
        }
        public int NextPage
        {
            get
            {
                if (_IsEnd)
                {
                    return _Page;
                }
                else
                {
                    return (_Page + 1);
                }
            }
            set
            {
                _NextPage = value;
            }
        }
        public int BackPage
        {
            get
            {
                if (_Page <= 1)
                {
                    return 1;
                }
                else
                {
                    return (_Page - 1);
                }
            }
            set
            {
                _BackPage = value;
            }
        }
        public int TypeId
        {
            get { return _TypeId; }
            set { _TypeId = value; }
        }
    }
    public class NhomTin
    {
        int _ID;
        int _Page;
        int _NextPage;
        int _BackPage;
        bool _IsEnd;
        string _GroupName;
        string _Html;
        int _Order;
        List<News> _NewsList;
        public int ID
        {
            get { return _ID; }
            set { this._ID = value; }
        }
        public NhomTin()
        {
            this._NewsList = new List<News>();
            this.Page = 0;
            this.IsEnd = false;
        }
        public string GroupName
        {
            get { return _GroupName; }
            set { this._GroupName = value; }
        }
        public string Html
        {
            get { return _Html; }
            set { this._Html = value; }
        }
        public int Order
        {
            get { return _Order; }
            set { this._Order = value; }
        }
        public List<News> NewsList
        {
            get { return _NewsList; }
            set { this._NewsList = value; }
        }
        public bool IsEnd
        {
            get
            {
                return _IsEnd;
            }
            set
            {
                _IsEnd = value;
            }
        }
        public int Page
        {
            get
            {
                if (_Page >= 1)
                {
                    return _Page;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                _Page = value;
            }
        }
        public int NextPage
        {
            get
            {
                if (_IsEnd)
                {
                    return _Page;
                }
                else
                {
                    return (_Page + 1);
                }
            }
            set
            {
                _NextPage = value;
            }
        }
        public int BackPage
        {
            get
            {
                if (_Page <= 1)
                {
                    return 1;
                }
                else
                {
                    return (_Page - 1);
                }
            }
            set
            {
                _BackPage = value;
            }
        }
    }

    public class DanhSachNhomTin
    {

        List<NhomTin> _List;
        public DanhSachNhomTin()
        {
            this._List = new List<NhomTin>();
        }
        public List<NhomTin> List
        {
            get { return this._List; }
            set { this._List = value; }
        }
    }
    public class ChiTietTin
    {
        int _ID;
        int _GroupID;
        string _Html;
        string _GroupName;
        News _News;
        string _TinLienQuan;
        string _TinMoiNhat;
        string _TinDangDoc;
        public ChiTietTin()
        {

        }
        public int ID
        {
            get { return _ID; }
            set { this._ID = value; }
        }
        public int GroupID
        {
            get { return _GroupID; }
            set { this._GroupID = value; }
        }
        public string Html
        {
            get { return _Html; }
            set { this._Html = value; }
        }
        public string TinLienQuan
        {
            get { return _TinLienQuan; }
            set { this._TinLienQuan = value; }
        }
        public string TinMoiNhat
        {
            get { return _TinMoiNhat; }
            set { this._TinMoiNhat = value; }
        }
        public string TinDangDoc
        {
            get { return _TinDangDoc; }
            set { this._TinDangDoc = value; }
        }
        public string GroupName
        {
            get { return this._GroupName; }
            set { this._GroupName = value; }
        }
        public News News
        {
            get { return this._News; }
            set { this._News = value; }
        }
    }

    public class SinhVienCommomModel
    {
        string _Html;
        public SinhVienCommomModel()
        {

        }
        public string Html
        {
            get { return _Html; }
            set { this._Html = value; }
        }
    }

    public class AdminNhomTin
    {
        string _Html;
        public AdminNhomTin()
        {

        }
        public string Html
        {
            get { return _Html; }
            set { this._Html = value; }
        }
    }

    public class AdminNhomTinChiTiet
    {
        string _Html;
        NewsGroups _NewsGroup;
        public AdminNhomTinChiTiet()
        {

        }
        public string Html
        {
            get { return _Html; }
            set { this._Html = value; }
        }
        public NewsGroups NewsGroup
        {
            get { return _NewsGroup; }
            set { this._NewsGroup = value; }
        }
    }

    public class SearchPage
    {
        int _Page;
        int _NextPage;
        int _BackPage;
        bool _IsEnd;
        public int Type { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Html { get; set; }
        public List<Product> ProductList { get; set; }
        public List<Market> MarketList { get; set; }
        public SearchPage()
        {
            _Page = 0;
            _IsEnd = false;
        }
        public bool IsEnd
        {
            get
            {
                return _IsEnd;
            }
            set
            {
                _IsEnd = value;
            }
        }
        public int Page
        {
            get
            {
                if (_Page >= 1)
                {
                    return _Page;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                _Page = value;
            }
        }
        public int NextPage
        {
            get
            {
                if (_IsEnd)
                {
                    return _Page;
                }
                else
                {
                    return (_Page + 1);
                }
            }
            set
            {
                _NextPage = value;
            }
        }
        public int BackPage
        {
            get
            {
                if (_Page <= 1)
                {
                    return 1;
                }
                else
                {
                    return (_Page - 1);
                }
            }
            set
            {
                _BackPage = value;
            }
        }
    }

    /*
     * QuanNH Add 2017-01
     */
    public class PageLeftColControl
    {
        public List<Product> Recommend { get; set; }
        public List<Product> PromotionHot { get; set; }
        public List<ProductTypePage> LinkCategorys { get; set; }
        public List<News> News { get; set; }
    }

    public class PageFooterControl
    {

        public News ArticleLink { get; set; }
        public NewsGroupPage CategoryWhoSale { get; set; }
        //public List<News> ArticleWhoSale { get; set; }
        public List<NewsGroupPage> NewsCategorys { get; set; }
        public List<NewsGroups> MenusFooter { get; set; }
    }

    public class ProductDetail
    {
        public Product Product { get; set; }
        public List<ProductImage> Images { get; set; }
    }

    /*
     * QuanNH add 2017-0-8
     */
    public class AffiliateHomePage {
        public NewsGroups VideoCategory { get; set; }
        public List<News> Videos { get; set; }
        public List<News> Articles { get; set; }
        public List<Testimonial> Testimonial { get; set; }
        public string[] BrandImages { get; set; }
        public List<Categorys> Categorys { get; set; }
        public List<Brand> Brands { get; set; }
    }
    public class NewsDetailPageContainer
    {
        public News NewsItem { get; set; }
        public News NextNewsItem { get; set; }
        public News PreviousNewsItem { get; set; }
        public List<News> ListNewsMostView { get; set; }
        public string PageTitle { get; set; }
    
    }
    public class NewsIndexPageContainer
    {
        public string PageTitle { get; set; }
        public NewsGroups NewsGroups { get; set; }
        public List<News> ListNews { get; set; }
        public List<News> ListNewsMostView { get; set; }
        public int TotalPage { get; set; }
        int _Page;
        int _NextPage;
        int _BackPage;
        bool _IsEnd;
        public bool IsEnd
        {
            get
            {
                return _IsEnd;
            }
            set
            {
                _IsEnd = value;
            }
        }
        public int Page
        {
            get
            {
                if (_Page >= 1)
                {
                    return _Page;
                }
                else
                {
                    return 1;
                }
            }
            set
            {
                _Page = value;
            }
        }
        public int NextPage
        {
            get
            {
                if (_IsEnd)
                {
                    return _Page;
                }
                else
                {
                    return (_Page + 1);
                }
            }
            set
            {
                _NextPage = value;
            }
        }
        public int BackPage
        {
            get
            {
                if (_Page <= 1)
                {
                    return 1;
                }
                else
                {
                    return (_Page - 1);
                }
            }
            set
            {
                _BackPage = value;
            }
        }

    }

    /*
     * QuanNH add Affiliate
     */
    public class AffiliateProductPage
    {
        public List<Product> Products { get; set; }
        public int ProductTotal { get; set; }
        public int Page { get; set; }

    }
}
