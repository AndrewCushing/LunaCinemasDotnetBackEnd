using LunaCinemasBackEndInDotNet.BusinessLogic;
using LunaCinemasBackEndInDotNet.Controllers;
using LunaCinemasBackEndInDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace LunaCinemasTest
{
    [TestClass]
    public class CommentTest
    {
        private ILunaCinemasDatabaseSettings GetCorrectSettings()
        {
            return new LunaCinemasDatabaseSettings()
            {
                FilmsCollectionName = "film",
                ReviewsCollectionName = "review",
                CommentsCollectionName = "comment",
                ShowingsCollectionName = "showing",
                ConnectionString = "mongodb://localhost:27017",
                DatabaseName = "LunaCinemas"
            };
        }

        [Fact]
        public void CommentCanBeSubmittedAndRetrieved()
        {
            //CommentsController controller = new CommentsController(new CommentFilter(GetCorrectSettings()));
            //ActionResult<ResponseObject<object>> getCommentsResult = controller.GetComments("5d650036280b7e2dc0b0d130");
            //Assert.IsTrue(getCommentsResult.Value.successful);
            //Assert.IsTrue(getCommentsResult.Value.contentList.Count > 0);
            //Assert.AreEqual($"Retrieved {getCommentsResult.Value.contentList.Count-2} comments for that review.", getCommentsResult.Value.body);
            //int commentCount = getCommentsResult.Value.contentList.Count;
            //ActionResult<ResponseObject<object>> insertCommentResult = controller.AddComment("5d650036280b7e2dc0b0d130","Test user 123", "This is a test comment. Blah");
            //Assert.IsTrue(insertCommentResult.Value.successful);
            //Assert.AreEqual(commentCount+1, insertCommentResult.Value.contentList.Count);
            //Assert.AreEqual($"Retrieved {insertCommentResult.Value.contentList.Count-2} comments for that review.", insertCommentResult.Value.body);
        }
    }
}