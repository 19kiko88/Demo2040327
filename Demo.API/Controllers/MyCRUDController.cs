using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Demo.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class MyCRUDController : ControllerBase
    {
        public static List<Info> _MyFriendList = new List<Info>
        {
            new Info(){ Id = 1001, Mobile = "111", Name = "JoJo"},
        };


        //[HttpGet]
        [HttpPost]
        [Route("{name}/{mobile}")]
        public string Create(string name, string mobile)
        {
            var rnd = new Random();
            var rndNo = rnd.Next(1, 1000);
            var res = string.Empty;

            try
            {
                _MyFriendList.Add(new Info()
                {
                    Id = rndNo,
                    Name = name,
                    Mobile = mobile,
                    CrateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                });
                res = "create done";
            }
            catch (Exception ex)
            {
                res = $"create failed. {ex.Message}";
            }

            return res;
        }

        public List<Info> Read()
        {
            return _MyFriendList;
        }


        /*
         * FromRoute
         * https://localhost:44359/MyCRUD/Read_FromRoute/JoJo
         */
        [HttpGet("{id}")]
        public List<Info> Read_FromRoute(int id)
        {
            return _MyFriendList;
        }

        /*
         * FromQueryString
         * https://localhost:44359/MyCRUD/Read_FromQuery?id=1001&name=
         */
        [HttpGet]
        public List<Info> Read_FromQuery([FromQuery] int? id, string? name)
        {
            var list = _MyFriendList.AsEnumerable();

            if (id.HasValue)
            {
                list = list.Where(c => c.Id == id);
            }

            if (!string.IsNullOrEmpty(name))
            {
                list = list.Where(c => c.Name == name);
            }

            return list.ToList();            

        }



        [HttpPost]
        [Route("{id}/{mobile}")]
        public string Update(int id, string mobile)
        {
            var res = string.Empty;
            try
            {
                var friend = _MyFriendList.Where(c => c.Id == id).FirstOrDefault();
                if (friend != null)
                {
                    friend.Mobile = mobile;
                    friend.ModifyTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                }
                else
                {
                    return "data not found!!";
                }
                res = "update done";
            }
            catch (Exception ex)
            {
                res = "update failed";
            }

            return res;
        }

        [HttpGet]
        //[Route("{id}")]
        public string Delete(int id)
        {
            var res = string.Empty;
            try
            {
                var friend = _MyFriendList.Where(c => c.Id == id).FirstOrDefault();
                if (friend != null)
                {
                    _MyFriendList.Remove(friend);
                }
                else
                {
                    return "data not found!!";
                }
                res = "delete done";
            }
            catch (Exception ex)
            {
                res = "delete failed";
            }

            return res;
        }

        [HttpPost]
        [Route("{id}")]
        public string Delete2(int id)
        {
            var res = string.Empty;
            try
            {
                var friend = _MyFriendList.Where(c => c.Id == id).FirstOrDefault();
                if (friend != null)
                {
                    _MyFriendList.Remove(friend);
                }
                else
                {
                    return "data not found!!";
                }
                res = "delete done";
            }
            catch (Exception ex)
            {
                res = "delete failed";
            }

            return res;
        }


        public class RequestDto
        {
            public int? Id { get; set; }
        }

        public class Info
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Mobile { get; set; }
            public string CrateTime { get; set; }
            public string ModifyTime { get; set; }
        }
    }
}
