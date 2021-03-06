using System;
using FlickrNet;
using NUnit.Framework;

namespace FlickrNet45.Tests
{
    /// <summary>
    /// Summary description for FlickrPhotoSetGetPhotos
    /// </summary>
    [TestFixture]
    public class PhotosetsGetPhotosTests: BaseTest
    {
        [Test]
        public void PhotosetsGetPhotosBasicTest()
        {
            PhotosetPhotoCollection set = Instance.PhotosetsGetPhotos("72157618515066456", PhotoSearchExtras.All, PrivacyFilter.None, 1, 10);

            Assert.AreEqual(8, set.Total, "NumberOfPhotos should be 8.");
            Assert.AreEqual(8, set.Count, "Should be 8 photos returned.");
        }

        [Test]
        public void PhotosetsGetPhotosMachineTagsTest()
        {
            PhotosetPhotoCollection set = Instance.PhotosetsGetPhotos("72157594218885767", PhotoSearchExtras.MachineTags, PrivacyFilter.None, 1, 10);

            bool machineTagsFound = false;

            foreach (Photo p in set)
            {
                if (!String.IsNullOrEmpty(p.MachineTags))
                {
                    machineTagsFound = true;
                    break;
                }
            }

            Assert.IsTrue(machineTagsFound, "No machine tags were found in the photoset");
        }

        [Test]
        public void PhotosetsGetPhotosFilterMediaTest()
        {
            // https://www.flickr.com/photos/sgoralnick/sets/72157600283870192/
            // Set contains videos and photos
            PhotosetPhotoCollection theset = Instance.PhotosetsGetPhotos("72157600283870192", PhotoSearchExtras.Media, PrivacyFilter.None, 1, 100, MediaType.Videos);

            foreach (Photo p in theset)
            {
                Assert.AreEqual("video", p.Media, "Should be video.");
            }

            PhotosetPhotoCollection theset2 = Instance.PhotosetsGetPhotos("72157600283870192", PhotoSearchExtras.Media, PrivacyFilter.None, 1, 100, MediaType.Photos);
            foreach (Photo p in theset2)
            {
                Assert.AreEqual("photo", p.Media, "Should be photo.");
            }

        }

        [Test]
        public void PhotosetsGetPhotosWebUrlTest()
        {
            PhotosetPhotoCollection theset = Instance.PhotosetsGetPhotos("72157618515066456");

            foreach(Photo p in theset)
            {
                Assert.IsNotNull(p.UserId, "UserId should not be null.");
                Assert.AreNotEqual(String.Empty, p.UserId, "UserId should not be an empty string.");
                string url = "https://www.flickr.com/photos/" + p.UserId + "/" + p.PhotoId + "/";
                Assert.AreEqual(url, p.WebUrl);
            }
        }

        [Test]
        public void PhotosetsGetPhotosPrimaryPhotoTest()
        {
            PhotosetPhotoCollection theset = Instance.PhotosetsGetPhotos("72157618515066456", 1, 100);

            Assert.IsNotNull(theset.PrimaryPhotoId, "PrimaryPhotoId should not be null.");

            if (theset.Total < theset.PerPage)
            {
                Photo primary = null;
                foreach (Photo p in theset)
                {
                    if (p.PhotoId == theset.PrimaryPhotoId)
                    {
                        primary = p;
                        break;
                    }
                }

                Assert.IsNotNull(primary, "Primary photo should have been found.");
            }
        }

        [Test]
        [Category("AccessTokenRequired")]
        public void PhotosetsGetPhotosOrignalTest()
        {
            var photos = AuthInstance.PhotosetsGetPhotos("72157623027759445", PhotoSearchExtras.AllUrls);

            foreach (var photo in photos)
            {
                Assert.IsNotNull(photo.OriginalUrl, "Original URL should not be null.");
            }
        }
    }
}
