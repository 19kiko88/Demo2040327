using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class MyCRUDController : ControllerBase
    {
        public static List<Info> MyFriendList = new List<Info>
        {
            new Info(){ Id = 1001, Mobile = "111", Name = "JoJo"},
        };


        [HttpPost("{name}/{mobile}")]
        public string Create_FromRoute(string name, string mobile)
        {
            var rnd = new Random();
            var rndNo = rnd.Next(1, 1000);
            var res = string.Empty;

            try
            {
                MyFriendList.Add(new Info()
                {
                    Id = rndNo,
                    Name = name,
                    Mobile = mobile,
                    CreateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                });
                res = $"create done(id:{rndNo})";
            }
            catch (Exception ex)
            {
                res = $"create failed. {ex.Message}";
            }

            return res;
        }

        [HttpPost]
        public string Create_FromQuery([FromQuery] string? name, string? mobile)
        {
            var rnd = new Random();
            var rndNo = rnd.Next(1, 1000);
            var res = string.Empty;

            try
            {
                MyFriendList.Add(new Info()
                {
                    Id = rndNo,
                    Name = name,
                    Mobile = mobile,
                    CreateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                });
                res = $"create done(id:{rndNo})";
            }
            catch (Exception ex)
            {
                res = $"create failed. {ex.Message}";
            }

            return res;
        }


        [HttpPost]
        public string Create_FromBody([FromBody] RequestDto data)
        {
            var rnd = new Random();
            var rndNo = rnd.Next(1, 1000);
            var res = string.Empty;

            try
            {
                MyFriendList.Add(new Info()
                {
                    Id = rndNo,
                    Name = data.Name,
                    Mobile = data.Mobile,
                    CreateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
                });
                res = $"create done(id:{rndNo})";
            }
            catch (Exception ex)
            {
                res = $"create failed. {ex.Message}";
            }

            return res;
        }

        public List<Info> Read()
        {
            return MyFriendList;
        }

        public ActionResult Read_ReturnOK()
        {
            return Ok();
        }


        /*
         * FromRoute
         * https://localhost:44359/MyCRUD/Read_FromRoute/JoJo
         */
        [HttpGet("{id}")]
        public List<Info> Read_FromRoute(int id)
        {
            return MyFriendList;
        }

        /*
         * FromQueryString
         * https://localhost:44359/MyCRUD/Read_FromQuery?id=1001&name=
         */
        [HttpGet]
        public List<Info> Read_FromQuery([FromQuery] int? id, string? name)
        {
            var list = MyFriendList.AsEnumerable();

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
        public string Update([FromBody] RequestDto data)
        {
            var res = string.Empty;
            try
            {
                var friend = MyFriendList.Where(c => c.Id == data.Id).FirstOrDefault();
                if (friend != null)
                {
                    if (!string.IsNullOrEmpty(data.Mobile))
                    {
                        res += $"{friend.Mobile} => {data.Mobile}, ";
                        friend.Mobile = data.Mobile;
                    }

                    if (!string.IsNullOrEmpty(data.Name))
                    {
                        res += $"{friend.Name} => {data.Name}, ";
                        friend.Name = data.Name;
                    }
                    
                    friend.ModifyTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                }
                else
                {
                    return "data not found!!";
                }

                res = $"update done. {res}";
            }
            catch (Exception ex)
            {
                res = "update failed";
            }

            return res;
        }


        [HttpPost("{id}")]
        public string Delete_FromRoute(int id)
        {
            var res = string.Empty;
            try
            {
                var friend = MyFriendList.Where(c => c.Id == id).FirstOrDefault();
                if (friend != null)
                {
                    MyFriendList.Remove(friend);
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
        public string Delete_FromBody([FromBody]RequestDto data)
        {
            var res = string.Empty;
            try
            {
                var friend = MyFriendList.Where(c => c.Id == data.Id).FirstOrDefault();
                if (friend != null)
                {
                    MyFriendList.Remove(friend);
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

        [HttpGet]
        public string DeleteWithGetMehod(int id)
        {
            var res = string.Empty;
            try
            {
                var friend = MyFriendList.Where(c => c.Id == id).FirstOrDefault();
                if (friend != null)
                {
                    MyFriendList.Remove(friend);
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
            public string? Name { get; set; }
            public string? Mobile { get; set; }
        }

        public class Info
        {
            public int? Id { get; set; }
            public string Name { get; set; }
            public string Mobile { get; set; }
            public string CreateTime { get; set; }
            public string ModifyTime { get; set; }
        }
    }
}
