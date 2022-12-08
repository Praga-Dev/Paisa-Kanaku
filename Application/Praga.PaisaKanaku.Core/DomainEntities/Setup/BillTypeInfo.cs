using Praga.PaisaKanaku.Core.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Praga.PaisaKanaku.Core.DomainEntities.Setup
{
    public class BillTypeInfo : BaseInfo
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
