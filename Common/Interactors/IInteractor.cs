using System.Threading.Tasks;
using JetBrains.Annotations;

namespace HeroProject.Common.Interactors
{
    public interface IInteractor<in TRequest, TResponse>
    {
        [NotNull]
        Task<TResponse> ExecuteAsync([NotNull] TRequest request);
    }

    public interface IInteractor<TResponse>
    {
        [NotNull]
        Task<TResponse> Execute();
    }
}