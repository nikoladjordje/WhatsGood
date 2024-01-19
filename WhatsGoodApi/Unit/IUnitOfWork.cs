using WhatsGoodApi.Repository.IRepository;

namespace WhatsGoodApi.Unit
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IMessageRepository Message { get; }
        IFriendshipRepository Friendship { get; }
        Task Save();
    }
}
