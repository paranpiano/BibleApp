using BibleApp.Models;
using BibleApp.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tests
{
    public class Tests
    {

        public IRestService<BibleVerse> RestWebService =>
            DependencyService.Get<IRestService<BibleVerse>>() ?? new RestService();
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void RestWebService_RefreshDataAsync_Test()
        {
            var verses = RestWebService.RefreshDataAsync("korean_1_Ge");
            Assert.Pass();

        }
    }
}