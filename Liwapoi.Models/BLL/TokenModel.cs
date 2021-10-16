using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liwapoi.Models.BLL
{
    public class TokenModel
    {
        public int RoleId { get; set; }
        public long UserId { get; set; }
        public string Token { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
