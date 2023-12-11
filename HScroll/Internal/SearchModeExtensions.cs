namespace HScroll.Internal
{
    static class SearchModeExtensions
    {
        public static bool IsValidValue(this ESearchMode value)
        {
#pragma warning disable IDE0066 // Convert switch statement to expression
            switch (value)
#pragma warning restore IDE0066 // Convert switch statement to expression
            {
            case ESearchMode.DescendantsFirst:
            case ESearchMode.AncestorsFirst:
            case ESearchMode.DescendantsOnly:
            case ESearchMode.AncestorsOnly:
                return true;
            default:
                return false;
            }
        }
    }
}
