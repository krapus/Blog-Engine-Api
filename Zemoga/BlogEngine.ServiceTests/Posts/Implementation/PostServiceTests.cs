using BlogEngine.DataAccess.Repository.Posts.Interface;
using BlogEngine.Model.Settings;
using BlogEngine.Service.Posts.Interface;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Threading.Tasks;

namespace BlogEngine.Service.Posts.Implementation.Tests
{
    [TestClass()]
    public class PostServiceTests
    {
        private IPostRepository _postRepository;
        private IOptions<AppSettings> _settings;
        private IPostService _postService;

        [TestInitialize()]
        public void Init()
        {
            //Arrange
            _postRepository = Substitute.For<IPostRepository>();
            _settings = Options.Create(new AppSettings
            {
                Messages = new Messages()
                {
                    PostCreationSucessfull = "Post created!!"
                }
            });

            _postService = new PostService(_postRepository, _settings);
        }

        [TestMethod()]
        public async Task CreatePostTestAsync()
        {
            // Mock Data
            _postRepository.CreatePost(new DataAccess.Models.Post()
            {
                Title = "Testing Case",
                Content = "First Post",
                Published = DateTime.Now,
                IdUser = 4,
                IdStatus = 2
            }).ReturnsForAnyArgs(1);

            var result = await _postService.CreatePost(new Model.Request.PostRequest() { Content = "First Post", Title = "Testing Case" }, 4);

            // Asserts            
            Assert.AreEqual(null, result.Error);
            Assert.AreEqual("Post created!!", result.Message);
        }
    }
}
