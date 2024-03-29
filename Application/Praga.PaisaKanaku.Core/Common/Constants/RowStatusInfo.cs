﻿namespace Praga.PaisaKanaku.Core.Common.Constants
{
    public static class RowStatusInfo
    {
        public static readonly Dictionary<string, string> RowStatusDictionary = new()
        {
            { "A", "ROW_STATUS_ACTIVE" },
            { "I", "ROW_STATUS_INACTIVE" },
            { "D", "ROW_STATUS_DELETED"},
        };

        public const char ROW_STATUS_ACTIVE = 'A';
        public const char ROW_STATUS_INACTIVE = 'I';
        public const char ROW_STATUS_DELETE = 'D';
    }
}
