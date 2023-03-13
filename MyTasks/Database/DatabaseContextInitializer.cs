using MyTasks.Models;

namespace MyTasks.Database
{
    public class DatabaseContextInitializer
    {
        public static void Initialize(DatabaseContext context)
        {

            context.Database.EnsureCreated();
            if (context.Tasks.Any())
            {
                return;   // DB has been seeded
            }

            var authors = new List<MyTask>
            {
                new MyTask { Id = Guid.NewGuid(), Title="MyTask", IsDone = false}
            };

            authors.ForEach(a => context.Tasks.Add(a));
            context.SaveChanges();

        }
    }
}
