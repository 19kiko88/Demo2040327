using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Demo.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        public static List<Info> MyFriendList = new List<Info>
        {
            new Info(){ Id = 999, Mobile = "111", Name = "JoJo"},
        };

        public class Info
        {
            public int? Id { get; set; }
            public string Name { get; set; }
            public string Mobile { get; set; }
        }

        public class RequestDto
        {
            public int? Id { get; set; }
            public string Name { get; set; }
            public string Mobile { get; set; }
        }



        // GET: api/<CRUDController>
        [HttpGet]
        public IEnumerable<Info> Get()
        {
            return MyFriendList.ToList();
        }


        // GET api/<CRUDController>/5
        [HttpGet("{id}")]
        public string Get_FromRoute(int id)
        {
            var data = MyFriendList.Where(c => c.Id == id).FirstOrDefault();
            if (data != null)
            {
                return $"Id：{data.Id}, Name：{data.Name}, Mobile：{data.Mobile}.";
            }
            return "no data";
        }

        // POST api/<CRUDController>
        [HttpPost]
        public string Post([FromBody] string value)
        {
            var rnd = new Random();
            MyFriendList.Add(new Info
            { Id = rnd.Next(1, 9999), Name = value, Mobile = "0912345678" });
            return "Post Sucess.";
        }


        // PUT api/<CRUDController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] string value)
        {
            var data = MyFriendList.Where(c => c.Id == id).FirstOrDefault();
            if (data != null)
            {
                data.Name = value;
                return "Put Success.";
            }
            else { return "Put Fail."; }
        }

        [HttpPut]
        public string Put_FromBody([FromBody] RequestDto para)
        {
            var data = MyFriendList.Where(c => c.Id == para.Id).FirstOrDefault();
            if (data != null)
            {
                data.Name = para.Name;
                return "Put Success.";
            }
            else { return "Put Fail."; }
        }



        // DELETE api/<CRUDController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            var data = MyFriendList.Where(c => c.Id == id).FirstOrDefault();
            if (data != null)
            {
                MyFriendList.Remove(data);
                return "Delete Success.";
            }
            else { return "Delete Fail."; }
        }

    }
}
