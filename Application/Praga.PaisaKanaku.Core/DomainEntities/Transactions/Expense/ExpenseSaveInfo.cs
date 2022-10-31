using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.Expense
{
    public class ExpenseSaveInfo
    {
        public DateTime ExpenseDate { get; set; }
        public List<ExpenseItemBaseInfo> ExpenseItemBaseInfoList { get; set; } = new();
    }
}
