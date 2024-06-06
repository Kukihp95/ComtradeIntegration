using NUnit.Framework;
using NUnit.Framework.Legacy;
using RestSharp;
using System.Net;

namespace ApiTestingProject
{
    [TestFixture]
    public class ApiTests
    {
        private RestClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new RestClient("https://fakerestapi.azurewebsites.net/api/v1");
        }

        [Test]
        public void Test_GetAllBooks()
        {
            var request = new RestRequest("Books", Method.Get);
            var response = _client.Execute(request);

            ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            ClassicAssert.IsNotNull(response.Content);
        }

        [Test]
        public void Test_GetBookById()
        {
            var request = new RestRequest("Books/1", Method.Get);
            var response = _client.Execute(request);

            ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            ClassicAssert.IsNotNull(response.Content);
        }

        [Test]
        public void Test_CreateBook()
        {
            var request = new RestRequest("Books", Method.Post);
            request.AddJsonBody(new
            {
                title = "New Book",
                description = "Description of the new book",
                pageCount = 123,
                excerpt = "Excerpt of the new book",
                publishDate = "2023-01-01T00:00:00.000Z"
            });

            var response = _client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created).Or.EqualTo(HttpStatusCode.OK), "Unexpected status code");
        }

        [Test]
        public void Test_UpdateBook()
        {
            var request = new RestRequest("Books/1", Method.Put);
            request.AddJsonBody(new
            {
                id = 1,
                title = "Updated Book",
                description = "Updated description",
                pageCount = 456,
                excerpt = "Updated excerpt",
                publishDate = "2024-01-01T00:00:00.000Z"
            });

            var response = _client.Execute(request);

            ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void Test_DeleteBook()
        {
            var request = new RestRequest("Books/1", Method.Delete);
            var response = _client.Execute(request);

            ClassicAssert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
