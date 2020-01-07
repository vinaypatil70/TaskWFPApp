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
        private string numberTask = string.Empty;
        private ObservableCollection<string> queue = new ObservableCollection<string>();
        private List<string> result = new List<string>();

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

        public List<string> Result
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
        }

        public void ClearQueue()
        {
            this.Queue.Clear();
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

            this.Queue.Add($"Task {NumberTask} is added.");

            TaskQueue taskQueue = new TaskQueue(new ConcurrentQueue<Task>());

            Task.Factory.StartNew(() => taskQueue.DequeueTask());

            TaskAdder adderOne = new TaskAdder(taskQueue);
            Task adderTaskOne = Task.Run(() =>
            {
                this.Result = adderOne.AddTasks(countTask);
            });

            Task.WaitAll(adderTaskOne);
            taskQueue.Close();
            NumberTask = string.Empty;
        }
    }
}
