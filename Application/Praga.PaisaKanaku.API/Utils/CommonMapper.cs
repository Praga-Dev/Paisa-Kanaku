using Praga.PaisaKanaku.API.API_Models.Setup;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;

namespace Praga.PaisaKanaku.API.Utils
{
    public class CommonMapper
    {
        #region Brand

        public static BrandInfo GetBrandInfo(BrandInfo_API brandInfo_API)
        {
            if (brandInfo_API == null)
            {
                return new BrandInfo();
            }

            return new BrandInfo()
            {
                Id = brandInfo_API.Id,
                Name = brandInfo_API.Name
            };
        }

        public static BrandInfo_API GetBrandInfoAPI(BrandInfo brandInfo)
        {
            if (brandInfo == null)
            {
                return new BrandInfo_API();
            }

            return new BrandInfo_API()
            {
                Id = brandInfo.Id,
                Name = brandInfo.Name
            };
        }

        public static List<BrandInfo> GetBrandInfoList(List<BrandInfo_API> brandInfoList_API)
        {
            if (brandInfoList_API == null || !brandInfoList_API.Any())
            {
                return new List<BrandInfo>();
            }

            return brandInfoList_API
                .Select(b => new BrandInfo()
                {
                    Id = b.Id,
                    Name = b.Name
                }).ToList();
        }

        public static List<BrandInfo_API> GetBrandInfoListAPI(List<BrandInfo> brandInfoList)
        {
            if (brandInfoList == null || !brandInfoList.Any())
            {
                return new List<BrandInfo_API>();
            }

            return brandInfoList
                .Select(b => new BrandInfo_API()
                {
                    Id = b.Id,
                    Name = b.Name
                }).ToList();
        }

        #endregion

        #region Member

        public static MemberInfo GetMemberInfo(MemberInfo_API memberInfo_API)
        {
            if (memberInfo_API == null)
            {
                return new MemberInfo();
            }

            return new MemberInfo()
            {
                Id = memberInfo_API.Id,
                Name = memberInfo_API.Name
            };
        }

        public static MemberInfo_API GetMemberInfoAPI(MemberInfo memberInfo)
        {
            if (memberInfo == null)
            {
                return new MemberInfo_API();
            }

            return new MemberInfo_API()
            {
                Id = memberInfo.Id,
                Name = memberInfo.Name
            };
        }

        public static List<MemberInfo> GetMemberInfoList(List<MemberInfo_API> memberInfoList_API)
        {
            if (memberInfoList_API == null || !memberInfoList_API.Any())
            {
                return new List<MemberInfo>();
            }

            return memberInfoList_API
                .Select(b => new MemberInfo()
                {
                    Id = b.Id,
                    Name = b.Name
                }).ToList();
        }

        public static List<MemberInfo_API> GetMemberInfoListAPI(List<MemberInfo> memberInfoList)
        {
            if (memberInfoList == null || !memberInfoList.Any())
            {
                return new List<MemberInfo_API>();
            }

            return memberInfoList
                .Select(b => new MemberInfo_API()
                {
                    Id = b.Id,
                    Name = b.Name
                }).ToList();
        }


        #endregion

        #region ProductCategory

        public static ProductCategoryInfo GetProductCategoryInfo(ProductCategoryInfo_API productCategoryInfo_API)
        {
            if (productCategoryInfo_API == null)
            {
                return new ProductCategoryInfo();
            }

            return new ProductCategoryInfo()
            {
                Id = productCategoryInfo_API.Id,
                Name = productCategoryInfo_API.Name
            };
        }

        public static ProductCategoryInfo_API GetProductCategoryInfoAPI(ProductCategoryInfo productCategoryInfo)
        {
            if (productCategoryInfo == null)
            {
                return new ProductCategoryInfo_API();
            }

            return new ProductCategoryInfo_API()
            {
                Id = productCategoryInfo.Id,
                Name = productCategoryInfo.Name
            };
        }

        public static List<ProductCategoryInfo> GetProductCategoryInfoList(List<ProductCategoryInfo_API> productCategoryInfoList_API)
        {
            if (productCategoryInfoList_API == null || !productCategoryInfoList_API.Any())
            {
                return new List<ProductCategoryInfo>();
            }

            return productCategoryInfoList_API
                .Select(b => new ProductCategoryInfo()
                {
                    Id = b.Id,
                    Name = b.Name
                }).ToList();
        }

        public static List<ProductCategoryInfo_API> GetProductCategoryInfoListAPI(List<ProductCategoryInfo> productCategoryInfoList)
        {
            if (productCategoryInfoList == null || !productCategoryInfoList.Any())
            {
                return new List<ProductCategoryInfo_API>();
            }

            return productCategoryInfoList
                .Select(b => new ProductCategoryInfo_API()
                {
                    Id = b.Id,
                    Name = b.Name
                }).ToList();
        }


        #endregion


        #region Product

        public static ProductInfo GetProductInfo(ProductInfo_API productInfo_API)
        {
            if (productInfo_API == null)
            {
                return new ProductInfo();
            }

            return new ProductInfo()
            {
                Id = productInfo_API.Id,
                Name = productInfo_API.Name,
                BrandInfo = new()
                {
                    Id = productInfo_API.BrandInfo_API != null ? productInfo_API.BrandInfo_API.Id : Guid.Empty,
                    Name = productInfo_API.BrandInfo_API != null ? productInfo_API.BrandInfo_API.Name : string.Empty
                },
                Description = productInfo_API.Description,
                ExpenseTypeInfo = new()
                {
                    ExpenseType = productInfo_API.ExpenseTypeInfo != null ? productInfo_API.ExpenseTypeInfo.ExpenseType : String.Empty,
                    ExpenseTypeValue = productInfo_API.ExpenseTypeInfo != null ? productInfo_API.ExpenseTypeInfo.ExpenseTypeValue : String.Empty
                },
                PreferredTimePeriodInfo = new()
                {
                    TimePeriodType = productInfo_API.PreferredRecurringTimePeriodInfo != null ? productInfo_API.PreferredRecurringTimePeriodInfo.TimePeriodType : String.Empty,
                    TimePeriodTypeValue = productInfo_API.PreferredRecurringTimePeriodInfo != null ? productInfo_API.PreferredRecurringTimePeriodInfo.TimePeriodTypeValue : String.Empty,
                },
                Price = productInfo_API.Price,
                ProductCategoryInfo = new()
                {
                    Id = productInfo_API.ProductCategoryInfo_API != null ? productInfo_API.ProductCategoryInfo_API.Id : Guid.Empty,
                    Name = productInfo_API.ProductCategoryInfo_API != null ? productInfo_API.ProductCategoryInfo_API.Name : string.Empty
                }
            };
        }

        public static ProductInfo_API GetProductInfoAPI(ProductInfo productInfo)
        {
            if (productInfo == null)
            {
                return new ProductInfo_API();
            }

            return new ProductInfo_API()
            {
                Id = productInfo.Id,
                Name = productInfo.Name ?? string.Empty,
                BrandInfo_API = new()
                {
                    Id = productInfo.BrandInfo != null ? productInfo.BrandInfo.Id : Guid.Empty,
                    Name = productInfo.BrandInfo != null ? productInfo.BrandInfo.Name : string.Empty
                },
                Description = productInfo.Description ?? string.Empty,
                ExpenseTypeInfo = new()
                {
                    ExpenseType = productInfo.ExpenseTypeInfo != null ? productInfo.ExpenseTypeInfo.ExpenseType : String.Empty,
                    ExpenseTypeValue = productInfo.ExpenseTypeInfo != null ? productInfo.ExpenseTypeInfo.ExpenseTypeValue : String.Empty
                },
                PreferredRecurringTimePeriodInfo = new()
                {
                    TimePeriodType = productInfo.PreferredTimePeriodInfo != null ? productInfo.PreferredTimePeriodInfo.TimePeriodType : String.Empty,
                    TimePeriodTypeValue = productInfo.PreferredTimePeriodInfo != null ? productInfo.PreferredTimePeriodInfo.TimePeriodTypeValue : String.Empty,
                },
                Price = productInfo.Price,
                ProductCategoryInfo_API = new()
                {
                    Id = productInfo.ProductCategoryInfo != null ? productInfo.ProductCategoryInfo.Id : Guid.Empty,
                    Name = productInfo.ProductCategoryInfo != null ? productInfo.ProductCategoryInfo.Name : string.Empty
                }
            };
        }

        public static List<ProductInfo> GetProductInfoList(List<ProductInfo_API> productInfoList_API)
        {
            if (productInfoList_API == null || !productInfoList_API.Any())
            {
                return new List<ProductInfo>();
            }

            return productInfoList_API
                .Select(productInfo_API => new ProductInfo()
                {
                    Id = productInfo_API.Id,
                    Name = productInfo_API.Name,
                    BrandInfo = new()
                    {
                        Id = productInfo_API.BrandInfo_API != null ? productInfo_API.BrandInfo_API.Id : Guid.Empty,
                        Name = productInfo_API.BrandInfo_API != null ? productInfo_API.BrandInfo_API.Name : string.Empty
                    },
                    Description = productInfo_API.Description,
                    ExpenseTypeInfo = new()
                    {
                        ExpenseType = productInfo_API.ExpenseTypeInfo != null ? productInfo_API.ExpenseTypeInfo.ExpenseType : String.Empty,
                        ExpenseTypeValue = productInfo_API.ExpenseTypeInfo != null ? productInfo_API.ExpenseTypeInfo.ExpenseTypeValue : String.Empty
                    },
                    PreferredTimePeriodInfo = new()
                    {
                        TimePeriodType = productInfo_API.PreferredRecurringTimePeriodInfo != null ? productInfo_API.PreferredRecurringTimePeriodInfo.TimePeriodType : String.Empty,
                        TimePeriodTypeValue = productInfo_API.PreferredRecurringTimePeriodInfo != null ? productInfo_API.PreferredRecurringTimePeriodInfo.TimePeriodTypeValue : String.Empty,
                    },
                    Price = productInfo_API.Price,
                    ProductCategoryInfo = new()
                    {
                        Id = productInfo_API.ProductCategoryInfo_API != null ? productInfo_API.ProductCategoryInfo_API.Id : Guid.Empty,
                        Name = productInfo_API.ProductCategoryInfo_API != null ? productInfo_API.ProductCategoryInfo_API.Name : string.Empty
                    }
                }).ToList();
        }

        public static List<ProductInfo_API> GetProductInfoListAPI(List<ProductInfo> productInfoList)
        {
            if (productInfoList == null || !productInfoList.Any())
            {
                return new List<ProductInfo_API>();
            }

            return productInfoList
                .Select(productInfo => new ProductInfo_API()
                {
                    Id = productInfo.Id,
                    Name = productInfo.Name ?? string.Empty,
                    BrandInfo_API = new()
                    {
                        Id = productInfo.BrandInfo != null ? productInfo.BrandInfo.Id : Guid.Empty,
                        Name = productInfo.BrandInfo != null ? productInfo.BrandInfo.Name : string.Empty
                    },
                    Description = productInfo.Description ?? string.Empty,
                    ExpenseTypeInfo = new()
                    {
                        ExpenseType = productInfo.ExpenseTypeInfo != null ? productInfo.ExpenseTypeInfo.ExpenseType : String.Empty,
                        ExpenseTypeValue = productInfo.ExpenseTypeInfo != null ? productInfo.ExpenseTypeInfo.ExpenseTypeValue : String.Empty
                    },
                    PreferredRecurringTimePeriodInfo = new()
                    {
                        TimePeriodType = productInfo.PreferredTimePeriodInfo != null ? productInfo.PreferredTimePeriodInfo.TimePeriodType : String.Empty,
                        TimePeriodTypeValue = productInfo.PreferredTimePeriodInfo != null ? productInfo.PreferredTimePeriodInfo.TimePeriodTypeValue : String.Empty,
                    },
                    Price = productInfo.Price,
                    ProductCategoryInfo_API = new()
                    {
                        Id = productInfo.ProductCategoryInfo != null ? productInfo.ProductCategoryInfo.Id : Guid.Empty,
                        Name = productInfo.ProductCategoryInfo != null ? productInfo.ProductCategoryInfo.Name : string.Empty
                    }
                }).ToList();

        }

        #endregion
    }
}
