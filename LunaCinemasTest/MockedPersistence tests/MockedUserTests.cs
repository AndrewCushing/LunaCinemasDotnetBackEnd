using System;
using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Controllers;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasBackEndInDotNet.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunaCinemasTest.MockedPersistence_tests
{
    [TestClass]
    public class MockedUserTests
    {
        private UserController _userController;
        private IUserContext _mockUserContext;

        [TestInitialize]
        public void CreateControllerAndMockContext()
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

        [TestMethod]
        public void UserCanLoginWithValidCredentials()
        {
            CreateTestUser("testUser", "testPassword");
            ActionResult<ResponseObject<string>> actualResponse =
                _userController.AttemptLogin(new[] {"testUser", "testPassword"});
            Assert.IsTrue(actualResponse.Value.successful);
            string accessToken = actualResponse.Value.contentList[0];
            Assert.IsTrue(_userController.VerifyAccessToken(accessToken).Value.successful);
        }

        [TestMethod]
        public void UserCannotLoginWithInvalidUsernameAndPassword()
        {
            CreateTestUser("testUser", "testPassword");
            ActionResult<ResponseObject<string>> actualResponse =
                _userController.AttemptLogin(new[] { "sadsf", "ffdd" });
            Assert.IsFalse(actualResponse.Value.successful);
            Assert.IsNull(actualResponse.Value.contentList);
        }

        [TestMethod]
        public void UserCannotLoginWithInvalidPassword()
        {
            CreateTestUser("testUser", "testPassword");
            ActionResult<ResponseObject<string>> actualResponse =
                _userController.AttemptLogin(new[] { "testUser", "ffdd" });
            Assert.IsFalse(actualResponse.Value.successful);
            Assert.IsNull(actualResponse.Value.contentList);
        }

        [TestMethod]
        public void OnceUserLogsOutTokenCannotBeUsed()
        {
            string accessToken = _userController.AddUser(new User("bob", "pass")).Value.contentList[0];
            ActionResult<ResponseObject<object>> actualResponse = _userController.Logout(accessToken);
            Assert.IsTrue(actualResponse.Value.successful);
            ActionResult<ResponseObject<object>> attemptToReuseToken = _userController.VerifyAccessToken(accessToken);
            Assert.IsFalse(attemptToReuseToken.Value.successful);
        }

        [TestMethod]
        public void IfUserLogsInWhileLoggedInThenOnlyLatestTokenCanBeUsed()
        {
            CreateTestUser("bob", "pass");
            string accessToken1 = _userController.AttemptLogin(new [] {"bob", "pass"}).Value.contentList[0];
            ActionResult<ResponseObject<object>> firstLoginResponse = _userController.Logout(accessToken1);
            Assert.IsTrue(firstLoginResponse.Value.successful);
            string accessToken2 = _userController.AttemptLogin(new[] { "bob", "pass" }).Value.contentList[0];
            ActionResult<ResponseObject<object>> secondLoginResponse = _userController.Logout(accessToken2);
            Assert.IsTrue(secondLoginResponse.Value.successful);
            ActionResult<ResponseObject<object>> attemptToReuseToken1 = _userController.VerifyAccessToken(accessToken1);
            Assert.IsFalse(attemptToReuseToken1.Value.successful);
            ActionResult<ResponseObject<object>> attemptToReuseToken2 = _userController.VerifyAccessToken(accessToken2);
            Assert.IsTrue(attemptToReuseToken2.Value.successful);
        }

        [TestMethod]
        public void UserCanBeDeleted()
        {

        }

        [TestMethod]
        public void OnceUserIsDeletedAccessTokenCannotBeVerified()
        {

        }

        [TestMethod]
        public void OnceUserIsDeletedCredentialsAreNotRecognised()
        {

        }

        private void CreateTestUser(string username, string password)
        {
            _userController.AddUser(new User(username, password));
        }
    }
}