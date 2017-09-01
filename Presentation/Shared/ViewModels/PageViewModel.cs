namespace HeroProject.Presentation.Shared.ViewModels
{
    public class PageViewModel
    {
        public PageViewModel(int index, int shift, int size, int pageCount)
        {
            Shift = shift;
            Size = size;
            Index = index;
            PageCount = pageCount;
        }

        public int Shift { get; }
        public int Size { get; }
        public int Index { get; }
        public int PageCount { get; }
    }
}