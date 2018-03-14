//using Application.Core.Services;
//using Application.Infrastructure.Data;
//using Microsoft.EntityFrameworkCore;
//using Xunit.Abstractions;

//namespace IntegrationTests.Repositories.BlogRepositoryTests
//{
//    public class GetById
//    {
//        private readonly ApplicationDbContext _applicationDbContext;
//        private readonly BlogService _blogService;
//        private readonly ITestOutputHelper _output;

//        public GetById(ITestOutputHelper output)
//        {
//            _output = output;
//            var dbOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
//                .UseInMemoryDatabase(databaseName: "TestApplicationDatabase")
//                .Options;
//            _applicationDbContext = new ApplicationDbContext(dbOptions);

//        }
//    }
//}
