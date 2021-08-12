using System;
using System.Threading.Tasks;
using PandaTechEShop.ViewModels;
using Xunit;

namespace PandaTechEShop.UnitTests.Tests.ViewModels
{
    public class TestMainPageViewModelTests
    {
        [Fact]
        public async Task Test1()
        {
            // Arrange
            var testMainPageViewModel = new TestMainPageViewModel(null, null);

            // Act
            //await testMainPageViewModel.SaveCommand.ExecuteAsync();

            // Assert
            // TODO - confirm only executed once
        }
    }
}
