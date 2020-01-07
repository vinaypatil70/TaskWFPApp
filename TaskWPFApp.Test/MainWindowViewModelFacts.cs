namespace TaskWPFApp.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;
    using System.Threading;
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
        public void AddTask_ProccessTask_ReturnsResult()
        {
            // Arrange
            var vm = new MainWindowViewModel();
            vm.NumberTask = "1";

            // Act
            vm.AddTask();
            Thread.Sleep(100);

            // Assert
            vm.Result.Count.ShouldBe(1);
        }
    }
}
