using System.Collections.Generic;
using HeroProject.Common;
using HeroProject.Presentation.Shared.ViewModels;
using JetBrains.Annotations;

namespace HeroProject.Presentation.Hero.ViewModels
{
    public class ListViewModel
    {
        public ListViewModel(
            [NotNull] IEnumerable<Item> items,
            [NotNull] PageViewModel page)
        {
            Preconditions.RequireNotNull(items, nameof(items));
            Preconditions.RequireNotNull(page, nameof(page));

            Items = items;
            Page = page;
        }

        [NotNull]
        public IEnumerable<Item> Items { get; }

        [NotNull]
        public PageViewModel Page { get; }

        public sealed class Item
        {
            public Item(int id, [NotNull] string name, [CanBeNull] string avatarFileName)
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