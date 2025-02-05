using Microsoft.AspNetCore.Mvc;
using NewsLetter.Models;


namespace NewsLetter.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class NewsTablesController : ControllerBase
    {
        static List<NewsTable> news = new List<NewsTable>
        {
           new NewsTable { Id = 1, Title = "News 1", Description = "Description 1", Author = "Author 1" },
           new NewsTable { Id = 2, Title = "News 2", Description = "Description 2", Author = "Author 2" },
           new NewsTable { Id = 3, Title = "News 3", Description = "Description 3", Author = "Author 3" },
           new NewsTable { Id = 4, Title = "News 4", Description = "Description 4", Author = "Author 4" },
           new NewsTable { Id = 5, Title = "News 5", Description = "Description 5", Author = "Author 2" }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            if (news.Count == 0)
            {
                return NotFound();
            }
            return Ok(news);
        }


        // Get a news by id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            NewsTable n1 = news.FirstOrDefault(n => n.Id == id);
            if (n1 == null)
            {
                return NotFound($"News with Id {id} is not found.");
            }
            return Ok(n1);
        }

        // Get a news by title
        [HttpGet("{title}")]
        public IActionResult GetByTitle(string title)
        {
            NewsTable n1 = news.FirstOrDefault(n => n.Title == title);
            if (n1 == null)
            {
                return NotFound($"News with Title {title} is not found.");
            }
            return Ok(n1);
        }

        // Get a news by author
        [HttpGet("{author}")]
        public IActionResult GetByAuthor(string author)
        {
            List<NewsTable> n1 = news.Where(n => n.Author == author).ToList();
            if (n1 == null)
                return NotFound($"News with Author {author} is not found.");

            return Ok(n1);
        }

        // Create a new news
        [HttpPost]
        public IActionResult Post([FromBody] NewsTable newsTable)
        {
            NewsTable n1 = news.FirstOrDefault(n => n.Id == newsTable.Id);
            if (n1 != null)
                return Conflict("News with Id " + newsTable.Id + " already exists.");

            news.Add(newsTable);
            return NoContent();
        }

        [HttpPut("{id:int}")] // Update a news
        public IActionResult Put(int id, [FromBody] NewsTable newsTable)
        {
            NewsTable n1 = news.FirstOrDefault(n => n.Id == id);
            if (n1 == null)
            {
                return NotFound($"News with Id {id} is not found.");
            }

            n1.Id = newsTable.Id;
            n1.Title = newsTable.Title;
            n1.Description = newsTable.Description;
            n1.Author = newsTable.Author;

            return NoContent();
        }

        [HttpDelete("{id:int}")] // Delete a news
        public IActionResult Delete(int id)
        {
            NewsTable n1 = news.FirstOrDefault(n => n.Id == id);
            if (n1 == null)
            {
                return NotFound($"News with Id {id} is not found.");
            }
            news.Remove(n1);
            return NoContent();
        }


    }
}
