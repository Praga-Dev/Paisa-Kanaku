﻿namespace Praga.PaisaKanaku.Core.DTO.Transactions.ExpenseFamilyWellbeing
{
    public class ExpenseFamilyWellbeingSaveRequestDTO
    {
        public Guid Id { get; set; }
        public Guid ExpenseInfoId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid ExpenseByInfoId { get; set; }
        public Guid RecipientInfoId { get; set; }
        public double ExpenseAmount { get; set; }
        public string Description { get; set; }
    }
}