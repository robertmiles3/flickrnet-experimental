﻿using System;
using NUnit.Framework;
using FlickrNet;

namespace FlickrNet45.Tests
{
    /// <summary>
    /// Summary description for InterestingnessGetListTests
    /// </summary>
    [TestFixture]
    public class InterestingnessTests : BaseTest
    {
        
        [Test]
        public void InterestingnessGetListTestsBasicTest()
        {
            DateTime date = DateTime.Today.AddDays(-2);
            DateTime datePlusOne = date.AddDays(1);

            PhotoCollection photos = Instance.InterestingnessGetList(date, PhotoSearchExtras.All, 1, 100);

            Assert.IsNotNull(photos, "Photos should not be null.");

            Assert.IsTrue(photos.Count > 50 && photos.Count <= 100, "Count should be at least 50, but not more than 100.");
        }

        [Test]
        public void InterestingnessGetListAsyncTest()
        {
            var photos = Instance.InterestingnessGetListAsync(null, PhotoSearchExtras.DateTaken | PhotoSearchExtras.DateUploaded | PhotoSearchExtras.OwnerName | PhotoSearchExtras.Views | PhotoSearchExtras.AllUrls, 1, 300).Result;

            Assert.IsNotNull(photos);
            Assert.AreNotEqual(0, photos.Count, "Should return some photos.");
        }
    }
}
