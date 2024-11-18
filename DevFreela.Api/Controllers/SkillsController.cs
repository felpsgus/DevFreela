using Microsoft.AspNetCore.Mvc;

namespace DevFreela.Api.Controllers;

public class SkillsController : BaseController
{
    // GET: api/<SkillsController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<SkillsController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<SkillsController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<SkillsController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<SkillsController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}