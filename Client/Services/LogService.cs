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
            try
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
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }

        #endregion \AddLogAsync

        #region GetLogAsync

        public async Task<List<Log>> GetLogsAsync()
        {
            try
            {
                return Logs;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \GetLogAsync

        #region LogCountAsync

        public async Task<int> LogCountAsync()
        {
            try
            {
                return Logs.Count;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \LogCountAsync


        #endregion \Methods
    }
}
