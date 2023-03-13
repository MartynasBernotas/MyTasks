using System.ComponentModel.DataAnnotations;

namespace MyTasks.Models
{
    public class MyTask
    {
        public Guid Id { get;set; }
        public string Title { get;set; } = string.Empty;
        public bool IsDone { get; set; }
        public bool HasSubTasks { get; set; }
        public IEnumerable<MyTask> SubTasks { get; set; }
        public Guid? ParentTaskId { get; set; }
        public MyTask? ParentTask { get; set; }
    }

    public class MyTaskView
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsDone { get; set; }
        public Guid? ParentTaskId { get; set; }
        public HashSet<MyTaskView> SubTasks { get; set; } = new();
        public bool IsExpanded { get; set; } = false;
        public bool HasSubTasks()
        {
            return SubTasks.Any();
        }
        public MyTaskView(Guid id, string title, bool isDone, Guid? parentTaskId, HashSet<MyTaskView> subTasks)
        {
            Id = id;
            Title = title;
            IsDone = isDone;
            ParentTaskId = parentTaskId;
            SubTasks = subTasks;
        }

        public static MyTaskView FromModel(MyTask task)
        {
            var subTasksList = task.SubTasks is null ? new HashSet<MyTaskView>() : task.SubTasks.Select(x => FromModel(x));
            return new MyTaskView(task.Id, task.Title, task.IsDone, task.ParentTaskId, new HashSet<MyTaskView>(subTasksList));
        }

        public bool HideAddButton { get { return !IsExpanded; }
            set { } 
        }
    }
}
