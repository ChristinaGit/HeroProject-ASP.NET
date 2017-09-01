using System.Threading.Tasks;
using JetBrains.Annotations;

namespace HeroProject.Common.Interactors
{
    public interface IActionInteractor<in TRequest>
    {
        [NotNull]
        Task ExecuteAsync([NotNull] TRequest request);
    }

    public interface IActionInteractor
    {
        [NotNull]
        Task ExecuteAsync();
    }
}