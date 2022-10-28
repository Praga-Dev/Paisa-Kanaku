using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.Expense
{
    public class ExpenseSaveInfo
    {
        public Guid ExpenseBy { get; set; }
        public DateTime DateOfExpense { get; set; }
        public List<ExpenseItemBaseInfo> ExpenseItemBaseInfoList { get; set; } = new();
    }
}
