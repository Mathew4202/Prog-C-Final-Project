using System;
using System.Threading.Tasks;
using Final_Project.Data;
using Final_Project.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinalProject.Tests
{
    [TestClass]
    public class ReviewServiceTests
    {
        private ReviewService _reviewService = null!;

        [TestInitialize]
        public void Setup()
        {
            var context = new ImdbContext();
            _reviewService = new ReviewService(context);
        }

        [TestMethod]
        public async Task AddReviewAsync_Throws_WhenTitleIdIsEmpty()
        {
            bool thrown = false;

            try
            {
                await _reviewService.AddReviewAsync("", "Mathew", 5, "Good movie");
            }
            catch (ArgumentException)
            {
                thrown = true;
            }

            Assert.IsTrue(thrown);
        }

        [TestMethod]
        public async Task AddReviewAsync_Throws_WhenReviewerNameIsEmpty()
        {
            bool thrown = false;

            try
            {
                await _reviewService.AddReviewAsync("tt0000001", "", 5, "Good movie");
            }
            catch (ArgumentException)
            {
                thrown = true;
            }

            Assert.IsTrue(thrown);
        }

        [TestMethod]
        public async Task AddReviewAsync_Throws_WhenScoreTooLow()
        {
            bool thrown = false;

            try
            {
                await _reviewService.AddReviewAsync("tt0000001", "Mathew", 0, "Bad score");
            }
            catch (ArgumentException)
            {
                thrown = true;
            }

            Assert.IsTrue(thrown);
        }

        [TestMethod]
        public async Task AddReviewAsync_Throws_WhenScoreTooHigh()
        {
            bool thrown = false;

            try
            {
                await _reviewService.AddReviewAsync("tt0000001", "Mathew", 6, "Bad score");
            }
            catch (ArgumentException)
            {
                thrown = true;
            }

            Assert.IsTrue(thrown);
        }

        [TestMethod]
        public async Task AddReviewAsync_Throws_WhenReviewTextIsEmpty()
        {
            bool thrown = false;

            try
            {
                await _reviewService.AddReviewAsync("tt0000001", "Mathew", 5, "");
            }
            catch (ArgumentException)
            {
                thrown = true;
            }

            Assert.IsTrue(thrown);
        }
    }
}