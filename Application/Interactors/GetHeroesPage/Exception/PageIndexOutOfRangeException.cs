using HeroProject.Common.Interactors;

namespace HeroProject.Application.Interactors.GetHeroesPage.Exception
{
    public sealed class PageIndexOutOfRangeException : InteractorException
    {
        public PageIndexOutOfRangeException(int pageCount)
        {
            PageCount = pageCount;
        }

        public int PageCount { get; }
    }
}