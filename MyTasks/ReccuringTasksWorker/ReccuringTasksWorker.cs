namespace MyTasks.ReccuringTasksWorker
{
    public class ReccuringTasksWorker : IHostedService, IDisposable
    {
        private Timer _timer;
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            //_timer = new Timer(AddReccuringTask, null, TimeSpan.Zero,
            // TimeSpan.FromSeconds(5));
        }

        private async Task AddReccuringTask()
        {
            return;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
