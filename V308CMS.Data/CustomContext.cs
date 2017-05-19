using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace V308CMS.Data
{
    #region Contexts
    public partial class V308CMSEntities : DbContext
    {
        #region Constructors

        public V308CMSEntities()
            : base("V308CMSConnectionString")
        {

        }
        public V308CMSEntities(string connectionString)
            : base(connectionString)
        {
            //cai nay giup chi ra rang ta dung provider MySql.Data.MySqlClient
            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("MySql.Data.MySqlClient");
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Ignore<IncludeMetadataConvention>();
            modelBuilder.Entity<ProductImage>()
            .HasRequired<Product>(p => p.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(p => p.ProductID);
        }
        #endregion
        #region ObjectSet Properties

        public DbSet<Account> Account
        {
            get;
            set;
        }
        public DbSet<VEmail> VEmail
        {
            get;
            set;
        }
        public DbSet<Market> Market
        {
            get;
            set;
        }
        public DbSet<MarketProductType> MarketProductType
        {
            get;
            set;
        }
        public DbSet<Admin> Admin
        {
            get;
            set;
        }
        public DbSet<Comment> Comment
        {
            get;
            set;
        }
        public DbSet<Config> Config
        {
            get;
            set;
        }
        public DbSet<Domain> Domain
        {
            get;
            set;
        }
        public DbSet<File> File
        {
            get;
            set;
        }
        public DbSet<FileType> FileType
        {
            get;
            set;
        }
        public DbSet<Image> Image
        {
            get;
            set;
        }
        public DbSet<ImageType> ImageType
        {
            get;
            set;
        }
        public DbSet<Log> Log
        {
            get;
            set;
        }
        public DbSet<LogType> LogType
        {
            get;
            set;
        }
        public DbSet<News> News
        {
            get;
            set;
        }
        public DbSet<NewsGroups> NewsGroups
        {
            get;
            set;
        }
        public DbSet<Page> Page
        {
            get;
            set;
        }
        public DbSet<PageType> PageType
        {
            get;
            set;
        }
        public DbSet<Product> Product
        {
            get;
            set;
        }
        public DbSet<ProductAttribute> ProductAttribute
        {
            get;
            set;
        }
        public DbSet<ProductDistributor> ProductDistributor
        {
            get;
            set;
        }
        public DbSet<ProductImage> ProductImage
        {
            get;
            set;
        }
        public DbSet<ProductManufacturer> ProductManufacturer
        {
            get;
            set;
        }
        public DbSet<ProductOrder> ProductOrder
        {
            get;
            set;
        }
        public DbSet<ProductSaleOff> ProductSaleOff
        {
            get;
            set;
        }
        public DbSet<ProductType> ProductType
        {
            get;
            set;
        }
        public DbSet<Question> Question
        {
            get;
            set;
        }
        public DbSet<QuestionAnswer> QuestionAnswer
        {
            get;
            set;
        }
        public DbSet<QuestionType> QuestionType
        {
            get;
            set;
        }
        public DbSet<Role> Role
        {
            get;
            set;
        }
        public DbSet<SEO> SEO
        {
            get;
            set;
        }
        public DbSet<SEOType> SEOType
        {
            get;
            set;
        }
        public DbSet<Support> Support
        {
            get;
            set;
        }
        public DbSet<SupportType> SupportType
        {
            get;
            set;
        }

        public DbSet<SiteConfig> SiteConfig
        {
            get;
            set;
        }

        public DbSet<Testimonial> Testimonial
        {
            get;
            set;
        }

        public DbSet<Categorys> Categorys
        {
            get;
            set;
        }

        public DbSet<Brand> Brand
        {
            get;
            set;
        }
        public DbSet<ProductWishlist> ProductWishlist
        {
            get;
            set;
        }

        #endregion
        #region AddTo Methods

        public void DeleteObject(Object entity)
        {
            if (entity != null)
            {
                string m = entity.GetType().Name.Trim();//Lay ten cua Kieu truyen vao MarketProductType

                if (typeof(Account).Name.ToString().Equals(m))
                {
                    this.Account.Remove((Account)entity);
                }
                else if (typeof(Admin).Name.ToString().Equals(m))
                {
                    this.Admin.Remove((Admin)entity);
                }
                else if (typeof(MarketProductType).Name.ToString().Equals(m))
                {
                    this.MarketProductType.Remove((MarketProductType)entity);
                }
                else if (typeof(Comment).Name.ToString().Equals(m))
                {
                    this.Comment.Remove((Comment)entity);
                }
                else if (typeof(Config).Name.ToString().Equals(m))
                {
                    this.Config.Remove((Config)entity);
                }
                else if (typeof(Domain).Name.ToString().Equals(m))
                {
                    this.Domain.Remove((Domain)entity);
                }
                else if (typeof(File).Name.ToString().Equals(m))
                {
                    this.File.Remove((File)entity);
                }
                else if (typeof(FileType).Name.ToString().Equals(m))
                {
                    this.FileType.Remove((FileType)entity);
                }
                else if (typeof(Image).Name.ToString().Equals(m))
                {
                    this.Image.Remove((Image)entity);
                }
                else if (typeof(ImageType).Name.ToString().Equals(m))
                {
                    this.ImageType.Remove((ImageType)entity);
                }
                else if (typeof(Log).Name.ToString().Equals(m))
                {
                    this.Log.Remove((Log)entity);
                }
                else if (typeof(LogType).Name.ToString().Equals(m))
                {
                    this.LogType.Remove((LogType)entity);
                }
                else if (typeof(News).Name.ToString().Equals(m))
                {
                    this.News.Remove((News)entity);
                }
                else if (typeof(NewsGroups).Name.ToString().Equals(m))
                {
                    this.NewsGroups.Remove((NewsGroups)entity);
                }
                else if (typeof(Page).Name.ToString().Equals(m))
                {
                    this.Page.Remove((Page)entity);
                }
                else if (typeof(PageType).Name.ToString().Equals(m))
                {
                    this.PageType.Remove((PageType)entity);
                }
                else if (typeof(Product).Name.ToString().Equals(m))
                {
                    this.Product.Remove((Product)entity);
                }
                else if (typeof(ProductAttribute).Name.ToString().Equals(m))
                {
                    this.ProductAttribute.Remove((ProductAttribute)entity);
                }
                else if (typeof(ProductDistributor).Name.ToString().Equals(m))
                {
                    this.ProductDistributor.Remove((ProductDistributor)entity);
                }
                else if (typeof(ProductImage).Name.ToString().Equals(m))
                {
                    this.ProductImage.Remove((ProductImage)entity);
                }
                else if (typeof(ProductManufacturer).Name.ToString().Equals(m))
                {
                    this.ProductManufacturer.Remove((ProductManufacturer)entity);
                }
                else if (typeof(ProductOrder).Name.ToString().Equals(m))
                {
                    this.ProductOrder.Remove((ProductOrder)entity);
                }
                else if (typeof(ProductSaleOff).Name.ToString().Equals(m))
                {
                    this.ProductSaleOff.Remove((ProductSaleOff)entity);
                }
                else if (typeof(ProductType).Name.ToString().Equals(m))
                {
                    this.ProductType.Remove((ProductType)entity);
                }
                else if (typeof(Question).Name.ToString().Equals(m))
                {
                    this.Question.Remove((Question)entity);
                }
                else if (typeof(QuestionAnswer).Name.ToString().Equals(m))
                {
                    this.QuestionAnswer.Remove((QuestionAnswer)entity);
                }
                else if (typeof(QuestionType).Name.ToString().Equals(m))
                {
                    this.QuestionType.Remove((QuestionType)entity);
                }
                else if (typeof(Role).Name.ToString().Equals(m))
                {
                    this.Role.Remove((Role)entity);
                }
                else if (typeof(SEO).Name.ToString().Equals(m))
                {
                    this.SEO.Remove((SEO)entity);
                }
                else if (typeof(SEOType).Name.ToString().Equals(m))
                {
                    this.SEOType.Remove((SEOType)entity);
                }
                else if (typeof(Support).Name.ToString().Equals(m))
                {
                    this.Support.Remove((Support)entity);
                }
                else if (typeof(SupportType).Name.ToString().Equals(m))
                {
                    this.SupportType.Remove((SupportType)entity);
                }
                else if (typeof(Market).Name.ToString().Equals(m))
                {
                    this.Market.Remove((Market)entity);
                }
                else if (typeof(VEmail).Name.ToString().Equals(m))
                {
                    this.VEmail.Remove((VEmail)entity);
                }
            }
        }

        public void AddToAccount(Account Account)
        {
            this.Account.Add(Account); //MarketProductType
        }
        public void AddToVEmail(VEmail VEmail)
        {
            this.VEmail.Add(VEmail);
        }
        public void AddToMarket(Market Market)
        {
            this.Market.Add(Market);
        }
        public void AddToMarketProductType(MarketProductType MarketProductType)
        {
            this.MarketProductType.Add(MarketProductType);
        }
        public void AddToAdmin(Admin Admin)
        {
            this.Admin.Add(Admin);
        }
        public void AddToComment(Comment Comment)
        {
            this.Comment.Add(Comment);
        }
        public void AddToConfig(Config Config)
        {
            this.Config.Add(Config);
        }
        public void AddToDomain(Domain Domain)
        {
            this.Domain.Add(Domain);
        }
        public void AddToFile(File File)
        {
            this.File.Add(File);
        }
        public void AddToFileType(FileType FileType)
        {
            this.FileType.Add(FileType);
        }
        public void AddToImage(Image Image)
        {
            this.Image.Add(Image);
        }
        public void AddToImageType(ImageType ImageType)
        {
            this.ImageType.Add(ImageType);
        }
        public void AddToLog(Log Log)
        {
            this.Log.Add(Log);
        }
        public void AddToLogType(LogType LogType)
        {
            this.LogType.Add(LogType);
        }
        public void AddToNews(News News)
        {
            this.News.Add(News);
        }
        public void AddToNewsGroups(NewsGroups NewsGroups)
        {
            this.NewsGroups.Add(NewsGroups);
        }
        public void AddToPage(Page Page)
        {
            this.Page.Add(Page);
        }
        public void AddToPageType(PageType PageType)
        {
            this.PageType.Add(PageType);
        }
        public void AddToProduct(Product Product)
        {
            this.Product.Add(Product);
        }
        public void AddToProductAttribute(ProductAttribute ProductAttribute)
        {
            this.ProductAttribute.Add(ProductAttribute);
        }
        public void AddToProductDistributor(ProductDistributor ProductDistributor)
        {
            this.ProductDistributor.Add(ProductDistributor);
        }
        public void AddToProductImage(ProductImage ProductImage)
        {
            this.ProductImage.Add(ProductImage);
        }
        public void AddToProductManufacturer(ProductManufacturer ProductManufacturer)
        {
            this.ProductManufacturer.Add(ProductManufacturer);
        }
        public void AddToProductOrder(ProductOrder ProductOrder)
        {
            this.ProductOrder.Add(ProductOrder);
        }
        public void AddToProductSaleOff(ProductSaleOff ProductSaleOff)
        {
            this.ProductSaleOff.Add(ProductSaleOff);
        }
        public void AddToProductType(ProductType ProductType)
        {
            this.ProductType.Add(ProductType);
        }
        public void AddToQuestion(Question Question)
        {
            this.Question.Add(Question);
        }
        public void AddToQuestionAnswer(QuestionAnswer QuestionAnswer)
        {
            this.QuestionAnswer.Add(QuestionAnswer);
        }
        public void AddToQuestionType(QuestionType QuestionType)
        {
            this.QuestionType.Add(QuestionType);
        }
        public void AddToRole(Role Role)
        {
            this.Role.Add(Role);
        }
        public void AddToSEO(SEO SEO)
        {
            this.SEO.Add(SEO);
        }
        public void AddToSEOType(SEOType SEOType)
        {
            this.SEOType.Add(SEOType);
        }
        public void AddToSupport(Support Support)
        {
            this.Support.Add(Support);
        }
        public void AddToSupportType(SupportType SupportType)
        {
            this.SupportType.Add(SupportType);
        }
        #endregion
        
    }
    #endregion
}