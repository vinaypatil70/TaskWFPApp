namespace TaskWFPApp.ViewModel
{
    using Prism.Commands;
    using Prism.Mvvm;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Windows.Threading;
    using TaskWFPApp.Common;

    public class MainWindowViewModel : BindableBase
    {
        private string numberTask = string.Empty;
        private ObservableCollection<string> queue = new ObservableCollection<string>();
        private ObservableCollection<string> result = new ObservableCollection<string>();
        private readonly Dispatcher dispatcher;
        public TaskQueue TaskQueue = new TaskQueue(new ConcurrentQueue<Task>());

        public string NumberTask
        {
            get => numberTask;
            set => SetProperty(ref numberTask, value);
        }

        public ObservableCollection<string> Queue
        {
            get => queue;
            set => SetProperty(ref queue, value);
        }

        public ObservableCollection<string> Result
        {
            get => result;
            set => SetProperty(ref result, value);
        }

        public ICommand cmdStartTask { get; set; }
        public ICommand cmdAddTask { get; set; }
        public ICommand cmdClearQueue { get; set; }

        public MainWindowViewModel()
        {
            this.cmdAddTask = new DelegateCommand(AddTask);
            this.cmdClearQueue = new DelegateCommand(ClearQueue);
            this.dispatcher = Dispatcher.CurrentDispatcher;
        }

        public void ClearQueue()
        {
            this.Queue.Clear();
            this.Result.Clear();
        }

        public void AddTask()
        {
            var countTask = 0;
            if (int.TryParse(NumberTask, out countTask)) { }
            else
            {
                this.Result.Add($"Please enter number as input");
                return;
            }

            this.Queue.Add($"{NumberTask} Task is added.");
            AddTasks(countTask);
            Task.Factory.StartNew(() => TaskQueue.DequeueTask());
            NumberTask = string.Empty;
        }

        public void AddTasks(int numTasks)
        {
            Random random = new Random();
            for (int i = 1; i <= numTasks; i++)
            {
                var queue = new QueueDetails
                {
                    QueueID = i,
                    RandomString = new string(Enumerable.Repeat("ABCDEFGH", 3).Select(s => s[random.Next(s.Length)]).ToArray())
                };

                this.TaskQueue.EnqueueTask(() =>
                {

                    this.StringPalindrome(queue);
                });
            }
            
        }

        public void StringPalindrome(QueueDetails queue)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            var reversedString = new string(queue.RandomString.Reverse().ToArray());
            var isPalindrome = (queue.RandomString == reversedString) ? "" : "not";
            Thread.Sleep(1000);

            dispatcher.Invoke(() =>
            {
                this.Result.Add($"{queue.RandomString} is {isPalindrome} palindrome");
                this.Result.Add($"........Task {queue.QueueID} Completed........");
                this.Result.Add(string.Empty);
            });
        }
    }
}
