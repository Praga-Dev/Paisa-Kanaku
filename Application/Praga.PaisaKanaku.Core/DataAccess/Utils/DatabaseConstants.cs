﻿namespace Praga.PaisaKanaku.Core.DataAccess.Utils
{
    public static class DatabaseConstants
    {
        // Common
        public const string USP_ROW_STATUS_UPDATE = "common.usp_v1_RowStatus_Update";

        // Lookups
        public const string USP_EXPENSE_TYPE_INFO_GET = "Lookups.usp_v1_ExpenseTypeInfo_Get";
        public const string USP_MEASURE_TYPE_INFO_GET = "Lookups.usp_v1_MeasureTypeInfo_Get";
        public const string USP_METRIC_SYSTEM_INFO_GET = "Lookups.usp_v1_MetricSystemInfo_Get";
        public const string USP_PAYMENT_METHOD_INFO_GET = "Lookups.usp_v1_PaymentMethodInfo_Get";
        public const string USP_WEIGHT_MEASURE_INFO_GET = "Lookups.usp_v1_WeightMeasureInfo_Get";
        public const string USP_TIME_PERIOD_TYPE_INFO_GET = "Lookups.usp_v1_TimePeriodTypeInfo_Get";
        public const string USP_PRODUCT_CATEGORY_INFO_GET = "Lookups.usp_v1_ProductCategoryInfo_Get";
        public const string USP_RELATIONSHIP_TYPE_INFO_GET = "Lookups.usp_v1_RelationshipTypeInfo_Get";
        public const string USP_MEASURE_TYPE_INFO_BY_GROCERY_INFO_ID_GET = "Lookups.usp_v1_MeasureTypeInfo_By_GroceryInfoId_Get";
        public const string USP_TRANSPORT_MODE_INFO_GET = "Lookups.usp_v1_TransportModeInfo_Get";
        public const string USP_TRAVEL_SERVICE_INFO_GET = "Lookups.usp_v1_TravelServiceInfo_Get";


        // Brand
        public const string USP_BRAND_INFO_SAVE = "Setup.usp_V1_BrandInfo_Save";
        public const string USP_BRAND_INFO_GET = "Setup.usp_V1_BrandInfo_Get";
        public const string USP_BRAND_INFO_GET_BY_ID = "Setup.usp_V1_BrandInfo_Get_By_Id";

        // Member
        public const string USP_MEMBER_INFO_SAVE = "Setup.usp_V1_MemberInfo_Save";
        public const string USP_MEMBER_INFO_GET = "Setup.usp_V1_MemberInfo_Get";
        public const string USP_MEMBER_INFO_GET_BY_ID = "Setup.usp_V1_MemberInfo_Get_By_Id";
        public const string USP_GET_MANAGE_EXPENSES_MEMBER_INFO = "Setup.usp_V1_GetManagesExpenseMemberInfo";

        // Product
        public const string USP_PRODUCT_INFO_SAVE = "Setup.usp_V1_ProductInfo_Save";
        public const string USP_PRODUCT_INFO_GET = "Setup.usp_V1_ProductInfo_Get";
        public const string USP_PRODUCT_INFO_GET_BY_ID = "Setup.usp_V1_ProductInfo_Get_By_Id";

        // Grocery
        public const string USP_GROCERY_INFO_SAVE = "Setup.usp_V1_GroceryInfo_Save";
        public const string USP_GROCERY_INFO_GET = "Setup.usp_V1_GroceryInfo_Get";
        public const string USP_GROCERY_INFO_GET_BY_ID = "Setup.usp_V1_GroceryInfo_Get_By_Id";

        // OutdoorFoodVendor
        public const string USP_OUTDOOR_FOOD_VENDOR_INFO_SAVE = "Setup.usp_OutdoorFoodVendorInfo_Save";
        public const string USP_OUTDOOR_FOOD_VENDOR_INFO_GET = "Setup.usp_OutdoorFoodVendorInfo_Get";
        public const string USP_OUTDOOR_FOOD_VENDOR_INFO_GET_BY_ID = "Setup.usp_OutdoorFoodVendorInfo_Get_By_Id";

        // Expense
        public const string USP_EXPENSE_DELETE_BY_TYPE = "Transactions.usp_V1_Expense_Delete_By_Type";
        public const string USP_EXPENSE_INFO_GET = "Transactions.usp_V1_ExpenseInfo_Get";

        // ExpenseProduct
        public const string USP_EXPENSE_PRODUCT_INFO_SAVE = "Transactions.usp_ExpenseProductInfo_Save";
        public const string USP_EXPENSE_PRODUCT_INFO_GET_SUM_AMOUNT_BY_DATE = "Transactions.usp_ExpenseProductInfo_Get_SumAmountByDate";
        public const string USP_EXPENSE_INFO_PRODUCT_GET_BY_DATE = "Transactions.usp_ExpenseInfo_Product_Get_ByDate";
        public const string USP_EXPENSE_PRODUCT_INFO_GET_BY_ID = "Transactions.usp_ExpenseProductInfo_Get_ById";

        // ExpenseGrocery
        public const string USP_EXPENSE_GROCERY_INFO_SAVE = "Transactions.usp_ExpenseGroceryInfo_Save";
        public const string USP_EXPENSE_GROCERY_INFO_GET_SUM_AMOUNT_BY_DATE = "Transactions.usp_ExpenseGroceryInfo_Get_SumAmountByDate";
        public const string USP_EXPENSE_GROCERY_INFO_GET_BY_DATE = "Transactions.usp_ExpenseGroceryInfo_Get_ByDate";
        public const string USP_EXPENSE_GROCERY_INFO_GET_BY_ID = "Transactions.usp_ExpenseGroceryInfo_Get_ById";

        // ExpenseFamilyWellbeing
        public const string USP_EXPENSE_FAMILY_FUND_INFO_SAVE = "Transactions.usp_ExpenseFamilyWellbeingInfo_Save";
        public const string USP_EXPENSE_FAMILY_FUND_INFO_GET_SUM_AMOUNT_BY_DATE = "Transactions.usp_ExpenseFamilyWellbeingInfo_Get_SumAmountByDate";
        public const string USP_EXPENSE_FAMILY_FUND_INFO_GET_BY_DATE = "Transactions.usp_ExpenseFamilyWellbeingInfo_Get_ByDate";
        public const string USP_EXPENSE_FAMILY_FUND_INFO_GET_BY_ID = "Transactions.usp_ExpenseFamilyWellbeingInfo_Get_ById";

        // ExpenseOutdoorFood
        public const string USP_EXPENSE_OUTDOOR_FOOD_INFO_SAVE = "Transactions.usp_ExpenseOutdoorFoodInfo_Save";
        public const string USP_EXPENSE_OUTDOOR_FOOD_INFO_GET_SUM_AMOUNT_BY_DATE = "Transactions.usp_ExpenseOutdoorFoodInfo_Get_SumAmountByDate";
        public const string USP_EXPENSE_OUTDOOR_FOOD_INFO_GET_BY_DATE = "Transactions.usp_ExpenseOutdoorFoodInfo_Get_ByDate";
        public const string USP_EXPENSE_OUTDOOR_FOOD_INFO_GET_BY_ID = "Transactions.usp_ExpenseOutdoorFoodInfo_Get_ById";

        // ExpenseTravel
        public const string USP_EXPENSE_TRAVEL_INFO_SAVE = "Transactions.usp_ExpenseTravelInfo_Save";
        public const string USP_EXPENSE_TRAVEL_INFO_GET_SUM_AMOUNT_BY_DATE = "Transactions.usp_ExpenseTravelInfo_Get_SumAmountByDate";
        public const string USP_EXPENSE_TRAVEL_INFO_GET_BY_DATE = "Transactions.usp_ExpenseTravelInfo_Get_ByDate";
        public const string USP_EXPENSE_TRAVEL_INFO_GET_BY_ID = "Transactions.usp_ExpenseTravelInfo_Get_ById";

        // ExpenseUtility
        public const string USP_EXPENSE_UTILITY_INFO_SAVE = "Transactions.usp_ExpenseUtilityInfo_Save";
        public const string USP_EXPENSE_UTILITY_INFO_GET_SUM_AMOUNT_BY_DATE = "Transactions.usp_ExpenseUtilityInfo_Get_SumAmountByDate";
        public const string USP_EXPENSE_UTILITY_INFO_GET_BY_DATE = "Transactions.usp_ExpenseUtilityInfo_Get_ByDate";
        public const string USP_EXPENSE_UTILITY_INFO_GET_BY_ID = "Transactions.usp_ExpenseUtilityInfo_Get_ById";

        // BillType
        public const string USP_BILL_TYPE_INFO_SAVE = "Setup.usp_V1_BillTypeInfo_Save";
        public const string USP_BILL_TYPE_INFO_GET = "Setup.usp_V1_BillTypeInfo_Get";
        public const string USP_BILL_TYPE_INFO_GET_BY_ID = "Setup.usp_V1_BillTypeInfo_Get_By_Id";

        // RepairType
        public const string USP_REPAIR_TYPE_INFO_SAVE = "Setup.usp_V1_RepairTypeInfo_Save";
        public const string USP_REPAIR_TYPE_INFO_GET = "Setup.usp_V1_RepairTypeInfo_Get";
        public const string USP_REPAIR_TYPE_INFO_GET_BY_ID = "Setup.usp_V1_RepairTypeInfo_Get_By_Id";

        // Utility
        public const string USP_UTILITY_INFO_SAVE = "Setup.usp_V1_UtilityInfo_Save";
        public const string USP_UTILITY_INFO_GET = "Setup.usp_V1_UtilityInfo_Get";
        public const string USP_UTILITY_INFO_GET_BY_ID = "Setup.usp_V1_UtilityInfo_Get_By_Id";

        // Group
        public const string USP_GROUP_INFO_SAVE = "Setup.usp_V1_GroupInfo_Save";
        public const string USP_GROUP_INFO_GET = "Setup.usp_V1_GroupInfo_Get";
        public const string USP_GROUP_INFO_GET_BY_ID = "Setup.usp_V1_GroupInfo_Get_By_Id";
    }
}
