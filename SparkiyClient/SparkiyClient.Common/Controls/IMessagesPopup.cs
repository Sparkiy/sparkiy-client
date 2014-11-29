using System.Threading.Tasks;

namespace SparkiyClient.Common.Controls
{
    public interface IMessagesPopup
    {
        Task AddTemporaryMessageAsync(string message);
    }
}