using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCRUD2.Models
{
    public class UserViewModel
    {
        public long Id { get; set; } 
        public string Name { get; set; }
        public string Nick { get; set; }
        public string Pwd { get; set; }
        public string Lastname { get; set; }
        public int Status { get; set; }
    }
}