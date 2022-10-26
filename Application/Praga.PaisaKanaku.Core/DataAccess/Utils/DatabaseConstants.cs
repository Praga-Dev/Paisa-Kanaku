namespace Praga.PaisaKanaku.Core.DataAccess.Utils
{
    public static class DatabaseConstants
    {
        // Lookups
        public const string USP_EXPENSE_TYPE_INFO_GET = "Lookups.usp_v1_ExpenseTypeInfo_Get";
        public const string USP_LIQUID_MEASURE_INFO_GET = "Lookups.usp_v1_LiquidMeasureInfo_Get";
        public const string USP_MEASURE_TYPE_INFO_GET = "Lookups.usp_v1_MeasureTypeInfo_Get";
        public const string USP_PAYMENT_METHOD_INFO_GET = "Lookups.usp_v1_PaymentMethodInfo_Get";
        public const string USP_WEIGHT_MEASURE_INFO_GET = "Lookups.usp_v1_WeightMeasureInfo_Get";
        public const string USP_TIME_PERIOD_TYPE_INFO_GET = "Lookups.usp_v1_TimePeriodTypeInfo_Get";

        #region Setup Region

        // Brand
        public const string USP_BRAND_INFO_SAVE = "Setup.usp_V1_BrandInfo_Save";
        public const string USP_BRAND_INFO_GET = "Setup.usp_V1_BrandInfo_Get";
        public const string USP_BRAND_INFO_GET_BY_ID = "Setup.usp_V1_BrandInfo_Get_By_Id";

        // Member
        public const string USP_MEMBER_INFO_SAVE = "Setup.usp_V1_MemberInfo_Save";
        public const string USP_MEMBER_INFO_GET = "Setup.usp_V1_MemberInfo_Get";
        public const string USP_MEMBER_INFO_GET_BY_ID = "Setup.usp_V1_MemberInfo_Get_By_Id";

        // Product
        public const string USP_PRODUCT_INFO_SAVE = "Setup.usp_V1_ProductInfo_Save";
        public const string USP_PRODUCT_INFO_GET = "Setup.usp_V1_ProductInfo_Get";
        public const string USP_PRODUCT_INFO_GET_BY_ID = "Setup.usp_V1_ProductInfo_Get_By_Id";

        // Product Category
        public const string USP_PRODUCT_CATEGORY_INFO_SAVE = "Setup.usp_V1_ProductCategoryInfo_Save";
        public const string USP_PRODUCT_CATEGORY_INFO_GET = "Setup.usp_V1_ProductCategoryInfo_Get";
        public const string USP_PRODUCT_CATEGORY_INFO_GET_BY_ID = "Setup.usp_V1_ProductCategoryInfo_Get_By_Id";

        // Expense
        public const string USP_EXPENSE_INFO_PRODUCT_SAVE = "Transactions.usp_V1_ExpenseInfo_Product_Save";
        public const string USP_TEMP_EXPENSE_INFO_PRODUCT_GET = "Transactions.usp_V1_TempExpenseInfo_Product_Get";
        public const string USP_TEMP_EXPENSE_INFO_PRODUCT_SAVE = "Transactions.usp_V1_TempExpenseInfo_Product_Save";

        #endregion
    }
}
