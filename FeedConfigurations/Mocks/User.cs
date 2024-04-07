using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBB_SE_2024_Gaborment.FeedConfigurations;

namespace FeedConfigurations.Mocks
{
    public class User
    {
        Guid ID { get; set; }
        String Username { get; set; }

        public User(String username)
        {
            this.ID = Guid.NewGuid();
            this.Username = username;
        }

        public String GetUsername()
        {
            return Username;
        }

       public void SetUsername(String username)
        {
            Username = username;
        }

    }
}