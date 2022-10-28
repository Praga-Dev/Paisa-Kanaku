using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Praga.PaisaKanaku.Core.DataEntities.Transactions.Expense
{
    public class ExpenseSaveInfoDb
    {
        public Guid ExpenseBy { get; set; }
        public DateTime ExpenseDate { get; set; }
        public XElement ExpenseData { get; set; }
    }
}
