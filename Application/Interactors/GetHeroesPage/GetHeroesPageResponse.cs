using System.Collections.Generic;
using HeroProject.Common;
using JetBrains.Annotations;

namespace HeroProject.Application.Interactors.GetHeroesPage
{
    public sealed class GetHeroesPageResponse
    {
        public GetHeroesPageResponse([NotNull] IEnumerable<PageItem> items, int startIndex, int pageCount)
        {
            Preconditions.RequireNotNull(items, nameof(items));

            Items = items;
            StartIndex = startIndex;
            PageCount = pageCount;
        }

        [NotNull]
        public IEnumerable<PageItem> Items { get; }

        public int StartIndex { get; }

        public int PageCount { get; }

        public sealed class PageItem
        {
            public PageItem(int id, [NotNull] string name, [CanBeNull] string avatarFileName)
            {
                Preconditions.RequireNotNull(name, nameof(name));

                Id = id;
                Name = name;
                AvatarFileName = avatarFileName;
            }

            public int Id { get; }

            [NotNull]
            public string Name { get; }

            [CanBeNull]
            public string AvatarFileName { get; }
        }
    }
}