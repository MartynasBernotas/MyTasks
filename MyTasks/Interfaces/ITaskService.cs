using MyTasks.Models;

namespace MyTasks.Interfaces
{
    public interface ITaskService
    {
        public Task<IEnumerable<MyTask>> GetAllTasks();
        public Task AddTask(MyTask task);
        public Task AddSubTask(MyTask task);
        public Task MarkTaskAsDone(Guid id);
        public Task<IEnumerable<MyTask>> GetMainTasks();
        public Task DeleteTask(Guid id);
    }
}
