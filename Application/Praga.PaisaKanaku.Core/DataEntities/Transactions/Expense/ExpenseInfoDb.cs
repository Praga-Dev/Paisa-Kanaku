﻿using Praga.PaisaKanaku.Core.Common.Model;

namespace Praga.PaisaKanaku.Core.DataEntities.Transactions.Expense
{
    public class ExpenseInfoDB : BaseInfo
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
    }
}
