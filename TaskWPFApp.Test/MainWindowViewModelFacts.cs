namespace TaskWPFApp.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;
    using System.Threading;
    using System.Threading.Tasks;
    using TaskWFPApp.Common;
    using TaskWFPApp.ViewModel;

    [TestClass]
    public class MainWindowViewModelFacts
    {
        [TestMethod]
        public void ClearQueue_Always_EmptyQueueCollection()
        {
            // Arrange
            var vm = new MainWindowViewModel();

            // Act
            vm.ClearQueue();

            // Assert
            vm.Queue.Count.ShouldBe(0);
        }

        [TestMethod]
        public void AddTask_NumberTaskIsInteger_IncreaseTaskCount()
        {
            // Arrange
            var vm = new MainWindowViewModel();
            vm.NumberTask = "1";

            // Act
            vm.AddTask();

            // Assert
            vm.Queue.Count.ShouldBe(1);
        }

        [TestMethod]
        public void AddTask_NumberTaskIsNotInteger_DoNothing()
        {
            // Arrange
            var vm = new MainWindowViewModel();
            vm.NumberTask = "vinay";

            // Act
            vm.AddTask();

            // Assert
            vm.Queue.Count.ShouldBe(0);
        }

        [TestMethod]
        public void AddTasks_Always_EnqueueTask()
        {
            // Arrange
            var vm = new MainWindowViewModel();
            vm.NumberTask = "1";

            // Act
            vm.AddTask();

            // Assert
            vm.Queue.Count.ShouldBe(1);
        }

        [TestMethod]
        public void StringPalindrome_Always_ReturnsString()
        {
            // Arrange
            var vm = new MainWindowViewModel();
            QueueDetails objQueueDetails = new QueueDetails() { QueueID = 1, RandomString = "vinay" };

            // Act
            vm.StringPalindrome(objQueueDetails);

            // Assert
            vm.Result.ShouldNotBeEmpty();
        }
    }
}
