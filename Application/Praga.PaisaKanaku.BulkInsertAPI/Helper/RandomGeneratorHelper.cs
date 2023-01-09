namespace Praga.PaisaKanaku.BulkInsertAPI.Helper
{
    public static class RandomGeneratorHelper
    {
        private static readonly Random _random = new();
        private static readonly string _alpha = "abcdefghijklmnopqrstuvwxyz";
        public static string GetRandomString(int wordCount = 10)
        {
            try
            {
                return new string(Enumerable.Repeat(_alpha, wordCount).Select(s => s[_random.Next(s.Length)]).ToArray());
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static int GetRandomInt(int maxValue = 1000)
        {
            return _random.Next(1, maxValue);
        }


    }
}
