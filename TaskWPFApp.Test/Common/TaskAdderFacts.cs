namespace TaskWPFApp.Test.Common
{
    using System.Collections.Concurrent;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;
    using TaskWFPApp;
    using TaskWFPApp.Common;

    [TestClass]
    public class TaskAdderFacts
    {
        [TestMethod]
        public void AddTasks_Always_EnqueueTask()
        {
            // Arrange
            var sut = this.CeateSystemUnderTest();

            // Act
            sut.AddTasks(4);

            // Assert
            sut.TaskQueue.workTaskQueue.Count.ShouldBe(4);
        }

        [TestMethod]
        public void StringPalindrome_Always_ReturnsString()
        {
            // Arrange
            var sut = this.CeateSystemUnderTest();
            QueueDetails objQueueDetails = new QueueDetails() { QueueID = 1, RandomString = "vinay" };

            // Act
           var str = sut.StringPalindrome(objQueueDetails);

            // Assert
            str.ShouldNotBeEmpty();
        }

        private TaskAdder CeateSystemUnderTest()
        {
            var g = new TaskQueue(new ConcurrentQueue<Task>(), 100, 10);
            return new TaskAdder(g);
        }
    }
}
