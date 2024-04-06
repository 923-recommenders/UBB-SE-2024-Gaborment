using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recommenders_backend.Relationships
{
    internal interface Relationship
    {
       
        string getSender();
        string getReceiver();
    }
}
