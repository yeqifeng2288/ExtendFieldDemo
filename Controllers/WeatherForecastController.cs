using ExtendFieldDemo.DatabaseAccess;
using ExtendFieldDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExtendFieldDemo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly DefaultDbContext _defaultDbContext;
        private readonly ExtendFieldDbContext _extendFieldDbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DefaultDbContext defaultDbContext, ExtendFieldDbContext extendFieldDbContext)
        {
            _logger = logger;
            _defaultDbContext = defaultDbContext;
            _extendFieldDbContext = extendFieldDbContext;
        }

        /// <summary>
        /// ≤‚ ‘∂¡»°°£
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var books = _defaultDbContext.Books;
            if (!books.Any())
            {
                books.Add(new Book { Title = "test" });
            }
            else
            {
                var book = books.First();
                var fieName = "NewField";
                book.AddOrUpdateExtendValue(fieName, 999);
                var filed = book.GetExtendValue(fieName);
                var fieldValue = book[fieName];

                var dbSet = book.GetExtendFieldDeSet();
                var extendQuery = dbSet.Where(o => o["ModelId"].Equals(1) && o["NewField"].Equals(999))
                      .Select(o => new { Id = o["ModelId"], NewField = o["NewField"] });
                var result = books.Join(extendQuery, o => o.Id, o => o.Id, (l, r) =>
                    new { l.Id, l.Title, r.NewField }
                )
                .ToList();
            }

            _defaultDbContext.SaveChanges();

            return new List<string>
            {
                "≤‚ ‘"
            };
        }

        /// <summary>
        /// ≤‚ ‘∏¸∏ƒÃÌº”°£
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Change()
        {
            _extendFieldDbContext.ExtendFieldModels.Add(new ExtendFieldModel
            {
                FieldType = FieldType.IntType,
                FieldName = "NewField",
                TableName = "BookExtendField",
                ModelType = nameof(Book)
            });

            //_extendFieldDbContext.ExtendFieldModels.Add(new ExtendFieldModel
            //{
            //    FieldType = FieldType.StringType,
            //    FieldName = "NewField2",
            //    TableName = "BookExtendField",
            //    ModelType = nameof(Book)
            //});

            ////_extendFieldDbContext.ExtendFieldModels.Add(new ExtendFieldModel
            ////{
            ////    FieldType = FieldType.StringType,
            ////    FieldName = "NewField3",
            ////    TableName = "BookExtendField",
            ////    ModelType = nameof(Book)
            ////});
            ////var entities = _extendFieldDbContext.ExtendFieldModels.Where(o => o.FieldName == "NewField3");
            ////_extendFieldDbContext.RemoveRange(entities);
            _extendFieldDbContext.SaveChanges();
            return new List<string>
            {
                "≤‚ ‘"
            };
        }
    }
}