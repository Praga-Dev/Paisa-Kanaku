﻿using Praga.PaisaKanaku.Core.Common.Model;
using Praga.PaisaKanaku.Core.DomainEntities.Setup;
using Praga.PaisaKanaku.Core.DomainEntities.Setup.Product;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseProduct
{
    public class ExpenseProductInfo : BaseInfo
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public ProductBaseInfo ProductBaseInfo { get; set; }
        public MemberInfo ExpenseByInfo { get; set; }
        public int Quantity { get; set; }
        public double ExpenseAmount { get; set; }
        public string Description { get; set; }
    }
}