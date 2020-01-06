namespace TaskWFPApp.ViewModel
{
    using Prism.Commands;
    using Prism.Mvvm;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class MainWindowViewModel : BindableBase
    {
        private int taskCount = 0;
        private ObservableCollection<string> queue = new ObservableCollection<string>();
        private List<string> result = new List<string>();

        public ObservableCollection<string> Queue
        {
            get { return queue; }
            set { SetProperty(ref queue, value); }
        }

        public List<string> Result
        {
            get { return result; }
            set { SetProperty(ref result, value); }
        }

        public ICommand cmdStartTask { get; set; }
        public ICommand cmdAddTask { get; set; }
        public ICommand cmdClearQueue { get; set; }

        public MainWindowViewModel()
        {
            this.cmdStartTask = new DelegateCommand(StartTask);
            this.cmdAddTask = new DelegateCommand(AddTask);
            this.cmdClearQueue = new DelegateCommand(ClearQueue);
        }

        public void ClearQueue()
        {
            this.Queue.Clear();
        }

        public void AddTask()
        {
            taskCount++;
            this.Queue.Add(string.Format("Task {0} is added.", taskCount));
        }

        public void StartTask()
        {
            int numOfServicers = 0;
            numOfServicers = 1;

            TaskQueue taskQueue = new TaskQueue(new ConcurrentQueue<Task>(), 100, 10);

            Task[] taskSrvcsArray = new Task[numOfServicers];
            for (int i = 0; i < numOfServicers; i++)
            {
                taskSrvcsArray[i] = Task.Factory.StartNew(() => taskQueue.DequeueTask());
            }

            TaskAdder adderOne = new TaskAdder(taskQueue);
            Task adderTaskOne = Task.Run(() =>
            {
                this.Result = adderOne.AddTasks(taskCount);
            });

            Task.WaitAll(adderTaskOne);
            taskQueue.Close();
            taskCount = 0;
        }
    }
}
