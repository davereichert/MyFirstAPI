using Microsoft.AspNetCore.Mvc;

namespace MyFirstWebAppMitAPI.Controllers
{

    [ApiController]

    // das ist der oute-Name, der in der Swagger-Dokumentation angezeigt wird
    [Route("api/[controller]")]


    public class ValuesController : ControllerBase
    {
        private static List<string> dataList = new List<string>();
        [HttpGet("all")]
        public ActionResult<IEnumerable<string>> GetAll()
        {
            return Ok(dataList);
        }


        // GET: Get data by index
        [HttpGet("{index}")]
        public ActionResult<string> Get(int index)
        {
            if (index >= 0 && index < dataList.Count)
            {
                return dataList[index];
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Add data to the list
        [HttpPost("add")]
        public ActionResult<string> Add([FromBody] string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return BadRequest("The provided value is null or empty.");
            }

            try
            {
                dataList.Add(value);
                return Ok("Added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // DELETE: Remove a specific value from the list

        [HttpDelete("delete/{index}")]
        public ActionResult<string> Delete(int index)
        {
            if (index >= 0 && index < dataList.Count)
            {
                string removedValue = dataList[index];
                dataList.RemoveAt(index);
                return Ok($"Removed value '{removedValue}' at index {index} successfully");
            }
            else
            {
                return NotFound($"Index {index} not found");
            }
        }


        // PUT: Update data at a specific index with a new value
        [HttpPut("update/{index}")]
        public ActionResult<string> Update(int index, [FromBody] string newValue)
        {
            if (index >= 0 && index < dataList.Count)
            {
                dataList[index] = newValue;
                return Ok($"Updated index {index} successfully");
            }
            else
            {
                return NotFound($"Index {index} not found");
            }
        }
    }
}
