namespace TaskWFPApp
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    public class TaskProcessingArguments
    {
        public bool ISTaskAdded { get; set; }
        public int PendingTaskCount { get; set; }
        public string Message { get; set; }
    }

    public class TaskQueue
    {
        public BlockingCollection<Task> workTaskQueue;

        public TaskQueue(IProducerConsumerCollection<Task> workTaskCollection)
        {
            workTaskQueue = new BlockingCollection<Task>(workTaskCollection);
        }

        public void EnqueueTask(Action action, CancellationToken cancelToken = default)
        {
            var task = new Task(action, cancelToken);
            workTaskQueue.TryAdd(task);
        }

        public void DequeueTask()
        {
            foreach (var task in workTaskQueue.GetConsumingEnumerable())
            {
                if (!task.IsCanceled)
                {
                    task.RunSynchronously();
                }
            }
        }

        public void Close()
        {
            workTaskQueue.CompleteAdding();
        }
    }
}
