using System;
using System.Linq.Expressions;
using NUnit.Framework;
using Moq;
using POAFGVApp.ViewModels;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;

namespace POAFGVApp.Tests
{
    [TestFixture]
    public class LoginViewModelTests
    {
        [Test]
        public void Test_Login_With_Correct_Credentials()
        {

            /*
			 * public LoginPageViewModel(INavigationService navigationService,
                                  IPageDialogService pageDialogService,
                                  IUserApplicationService userService,
                                  ISettings settings)
            */

            //Arrange
            Mock<IUserApplicationService> mockUserService = new Mock<IUserApplicationService>();
            Mock<INavigationService> mockNavigationService = new Mock<INavigationService>();
            Mock<IPageDialogService> mockIPagDialogService = new Mock<IPageDialogService>();
            Mock<IPersonalSettings> mockSettings = new Mock<IPersonalSettings>();

            var users = new List<User>() { new User { Login = "admin", Password = "admin" } };

            mockUserService.Setup(user => user.GetAllWithChildrenByPredicateAsync(It.IsAny<Expression<Func<User, bool>>>()))
                           .ReturnsAsync(users);
            mockUserService.Setup(user => user.InsertAsync(It.IsAny<User>()))
                           .ReturnsAsync(1);

            //Act
            var _viewModel = new LoginPageViewModel(mockNavigationService.Object,
                                                    mockIPagDialogService.Object,
                                                    mockUserService.Object,
                                                    mockSettings.Object)
            {
                Login = "admin",
                Password = "admin"
            };
            _viewModel.DoLoginCmd.Execute();

            mockUserService.Verify(x => x.GetAllWithChildrenByPredicateAsync(It.IsAny<Expression<Func<User, bool>>>()), Times.Exactly(10));
            mockUserService.Verify(x => x.InsertAsync(It.IsAny<User>()), Times.AtLeastOnce());


        }
    }
}
