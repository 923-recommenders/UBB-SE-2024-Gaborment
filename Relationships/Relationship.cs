using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recommenders_backend.Relationships
{
    internal interface Relationship
    {
        //nu am vorbit de setteri dar poate merge in caz ca e vreo implementare de schimbare de username din partea echipelor de conturi
        string getSender();
        string getReceiver();
    }
}
