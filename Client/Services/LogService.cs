using Models;

namespace Services
{
    public class LogService : ILogService
    {
        #region Constructor

        public LogService()
        {
            Logs = new List<Log>();
        }

        #endregion \Constructor

        #region Properties
        public List<Log> Logs { get; set; }

        #endregion \Properties


        #region Methods

        #region AddLogAsync

        public async Task AddLogAsync(Log log)
        {
            await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(log.Message))
                {
                    return;
                }

                Logs.Insert(index: 0, log);
            });
        }

        #endregion \AddLogAsync

        #region GetLogAsync

        public async Task<List<Log>> GetLogsAsync()
        {
            return Logs;
        }

        #endregion \GetLogAsync

        #region LogCountAsync
        
        public async Task<int> LogCountAsync()
        {
            return Logs.Count;
        }

        #endregion \LogCountAsync


        #endregion \Methods
    }
}
