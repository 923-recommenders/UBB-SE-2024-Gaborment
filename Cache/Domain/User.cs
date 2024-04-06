using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recommenders_backend
{
    public class User
    {
        public string Id { get; set; }
        public List<string> Following { get; set; } = new List<string>();
        public List<string> Preferences { get; set; } = new List<string>();
    }
}