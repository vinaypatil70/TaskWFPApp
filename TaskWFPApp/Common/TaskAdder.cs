namespace TaskWFPApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TaskAdder
    {
        public TaskQueue TaskQueue;

        public TaskAdder(TaskQueue taskQueue)
        {
            TaskQueue = taskQueue;
        }

        public List<string> AddTasks(int numTasks)
        {
            List<string> lstResult = new List<string>();
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
                    lstResult.Add(this.StringPalindrome(queue)); 
                });
            }

            return lstResult;
        }

        public string StringPalindrome(QueueDetails queue)
        {
            var reversedString = new string(queue.RandomString.Reverse().ToArray());
            var isPalindrome = (queue.RandomString == reversedString) ? "" : "Not";
            return string.Format("Task {0} : {1} is {2} palindrome", queue.QueueID, queue.RandomString, isPalindrome);
        }
    }

    public class QueueDetails
    {
        public int QueueID { get; set; }
        public string RandomString { get; set; }
    }
}
