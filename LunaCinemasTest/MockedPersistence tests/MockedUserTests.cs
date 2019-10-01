using System;
using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Controllers;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunaCinemasTest
{
    [TestClass]
    public class MockedUserTests
    {
        private UserController _userController;
        private IUserContext _mockUserContext;

        [TestInitialize]
        public void CreateController()
        {
            _mockUserContext = new MockUserContext();
            _userController = new UserController(new UserHandler(_mockUserContext));
        }

        [TestMethod]
        public void UserAccountCanBeCreated()
        {
            string testUsername = "testuser1";
            Assert.IsTrue(_mockUserContext.FindByUsername(testUsername).Count == 0);
            _userController.AddUser(new User(testUsername,"safestpassword"));
            List<User> actualResult = _mockUserContext.FindByUsername(testUsername);
            Assert.IsTrue(actualResult.Count == 1);
        }

        [TestMethod]
        public void UserAccountCannotBeCreatedWithoutUsername()
        {
            ActionResult<ResponseObject<string>> actualResult =_userController.AddUser(new User("", "safestpassword"));
            Assert.IsFalse(actualResult.Value.successful);
        }

        [TestMethod]
        public void UserAccountCannotBeCreatedWithoutPassword()
        {
            ActionResult<ResponseObject<string>> actualResult = _userController.AddUser(new User("hithere", ""));
            Assert.IsFalse(actualResult.Value.successful);
        }

        [TestMethod]
        public void ResponseFromCreatingUserAccountRepresentsGuid()
        {
            ActionResult<ResponseObject<string>> actualResponse =
                _userController.AddUser(new User("testUsername", "testPassword"));
            try
            {
                Guid.Parse(actualResponse.Value.contentList[0]);
                Assert.IsTrue(actualResponse.Value.successful);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ResponseFromCreatingUserAccountIsValidAccessToken()
        {
            ActionResult<ResponseObject<string>> initialResponse =
                _userController.AddUser(new User("testUsername", "testPassword"));
            try
            {
                string accessToken = initialResponse.Value.contentList[0];
                ActionResult<ResponseObject<object>> actualResponse = _userController.VerifyAccessToken(accessToken);
                Assert.IsTrue(actualResponse.Value.successful);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void InvalidAccessTokensAreNotVerified()
        {
            ActionResult<ResponseObject<object>> actualResponse = _userController.VerifyAccessToken(Guid.NewGuid().ToString());
            Assert.IsFalse(actualResponse.Value.successful);
        }
    }
}