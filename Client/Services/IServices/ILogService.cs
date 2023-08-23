using Models;

namespace Services
{
    public interface ILogService
    {
        Task AddLogAsync(Log log);
        Task<List<Log>> GetLogsAsync();
        Task<int> LogCountAsync();
    }
}
