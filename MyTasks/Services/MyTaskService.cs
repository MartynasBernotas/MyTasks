using Microsoft.EntityFrameworkCore;
using MyTasks.Database;
using MyTasks.Interfaces;
using MyTasks.Models;

namespace MyTasks.Services
{
    public class MyTaskService : ITaskService
    {
        private readonly DatabaseContext _dbContext;
        public MyTaskService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }
        public async Task AddTask(MyTask task)
        {
            _dbContext.Tasks.Add(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddSubTask(MyTask task)
        {
            task.HasSubTasks = false;
            if (task.ParentTaskId != null)
            {
                var parentTask = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == task.ParentTaskId);
                if(parentTask != null)
                {
                    _dbContext.Tasks.Add(task);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteTask(Guid id)
        {
            var task = await _dbContext.Tasks
                .Include(x=> x.SubTasks)
                .FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<MyTask>> GetMainTasks()
        {
            return await _dbContext.Tasks
                .Where(x => x.ParentTaskId == null)
                .Include(x => x.SubTasks)
                .ToListAsync();
        }

        public async Task<IEnumerable<MyTask>> GetAllTasks()
        {
            try
            {
                return await _dbContext.Tasks.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task MarkTaskAsDone(Guid id)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            task.IsDone = true;
            _dbContext.Update(task);
            await _dbContext.SaveChangesAsync();
        }
    }
}
