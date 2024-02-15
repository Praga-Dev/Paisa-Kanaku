namespace Praga.PaisaKanaku.Core.Common.Model
{
    public class BaseInfo : RowInfo
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
