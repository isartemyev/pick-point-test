namespace PickPoint.Lib.Extensions
{
    public static class StringSearchExtensions
    {
        public static bool ContainWordsStartsWithAll(this string source, IEnumerable<string> searchWords)
        {
            if (searchWords == null)
                throw new ArgumentNullException(nameof(searchWords));

            var sourceWords = source.Split(' ');

            var flag = false;
            
            foreach (var searchItem in searchWords)
            {
                flag = sourceWords.Any(sourceItem => sourceItem.StartsWith(searchItem));

                if (!flag)
                {
                    return false;
                }
            }
            
            return flag;
        }
    }
}