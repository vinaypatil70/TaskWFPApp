namespace TaskWFPApp
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    public class TaskQueue
    {
        private ConcurrentQueue<Task> workTaskQueue;

        public TaskQueue(IProducerConsumerCollection<Task> workTaskCollection)
        {
            workTaskQueue = new ConcurrentQueue<Task>(workTaskCollection);
        }

        public void EnqueueTask(Action action, CancellationToken cancelToken = default)
        {
            var task = new Task(action, cancelToken);
            workTaskQueue.Enqueue(task);
        }

        public void DequeueTask()
        {
            while (true)
            {
                try
                {
                    Task task;
                    if (workTaskQueue.TryDequeue(out task)) { task.RunSynchronously(); }
                }
                catch (NullReferenceException ex)
                {
                    string w = ex.Message;
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
