using Praga.PaisaKanaku.Core.DomainEntities.Setup;
using Praga.PaisaKanaku.Core.DomainEntities.Transactions.Common;

namespace Praga.PaisaKanaku.Core.DomainEntities.Transactions.ExpenseFamilyWellbeing
{
    public class ExpenseFamilyWellbeingInfo : ExpenseBaseInfo
    {
        public MemberInfo RecipientInfo { get; set; } = new();
    }
}
