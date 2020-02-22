using System.Collections.Generic;
using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Controllers;
using LunaCinemasBackEndInDotNet.Models;
using LunaCinemasTest.MockPersistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunaCinemasTest
{
    [TestClass]
    public class MockedCommentTests
    {
        private static MockFilmContext _filmContext;
        private static MockReviewContext _reviewContext;
        private static MockCommentContext _commentContext;
        private static CommentsController _commentsController;

        [TestInitialize]
        public void CreateMockContexts()
        {
            _filmContext = new MockFilmContext();
            _reviewContext = new MockReviewContext();
            _commentContext = new MockCommentContext();
            _commentsController = new CommentsController(new CommentFilter(_commentContext, _reviewContext, _filmContext));
        }
        
        [TestMethod]
        public void CommentsCanBeSubmitted()
        {
            _filmContext.AddFindByIdResult(new Film(){Id = "5d650036280b7e2dc0b0d121"});
            _reviewContext.AddReview(new Review(){FilmId = "5d650036280b7e2dc0b0d121", Id = "5544653234567543", Username = "sdg", ReviewBody = "sdfisdugh", Rating = "erere", JavaClass = "eiorytie"});
            ActionResult<ResponseObject<object>> actualResponse = _commentsController.AddComment(new Comment("5544653234567543", "Test user", "Blah"));
            Assert.AreEqual(true, actualResponse.Value.successful);
            Assert.AreEqual(1, _commentContext.FindByReviewId("5544653234567543").Count);
            Assert.AreEqual("Test user", _commentContext.FindByReviewId("5544653234567543")[0].Username);
        }

        [TestMethod]
        public void CommentsCanBeRetrievedByReviewId()
        {
            _filmContext.AddFindByIdResult(new Film() { Id = "1234", Title = "The Matrix" });
            _reviewContext.AddReview(new Review() { FilmId = "1234", Username = "Jeff", ReviewBody = "Great film", Id = "38534023" });
            for (int i = 0; i < 13; i++)
            {
                _commentContext.AddComment(new Comment("38534023", "Sally", "I hate you"));
            }
            ActionResult<ResponseObject<object>> actualResponse = _commentsController.GetComments("38534023");
            Assert.IsTrue(actualResponse.Value.successful);
            Assert.IsNotNull(actualResponse.Value.contentList);
            Assert.AreEqual(15, actualResponse.Value.contentList.Count);
            Assert.AreEqual(typeof(Comment), actualResponse.Value.contentList[4].GetType());
        }

        [TestMethod]
        public void ProhibitedWordsInReviewsAreCensoredWhenReviewIsRetrieved()
        {
            _filmContext.AddFindByIdResult(new Film() { Id = "1234", Title = "The Matrix" });
            _reviewContext.AddReview(new Review() { FilmId = "1234", Username = "Jeff", ReviewBody = "Great film", Id = "38534023" });
            for (int i = 0; i < 13; i++)
            {
                _commentsController.AddComment(new Comment("38534023", "Sally", "I hate you naTweSt"));
            }
            ActionResult<ResponseObject<object>> actualResponse = _commentsController.GetComments("38534023");
            Assert.IsTrue(actualResponse.Value.successful);
            Assert.IsNotNull(actualResponse.Value.contentList);
            Assert.AreEqual(15, actualResponse.Value.contentList.Count);
            Assert.AreEqual("I hate you *******", ((Comment)actualResponse.Value.contentList[4]).Body);
        }
    }
}