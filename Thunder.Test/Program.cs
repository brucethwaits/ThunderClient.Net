using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thunder.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ThunderClient.Net.Thunder thunder = new ThunderClient.Net.Thunder("[[server]]", "[[apikey]]", "[[secretkey]]");
            int userCount = thunder.GetUserCount();
            var msg = new
            {
                message = "test message"
            };

            int receivedMessage = thunder.SendMessageToChannel(msg, "test");

            List<string> usersInChannel = thunder.GetUsersInChannel("test");

            string username = "test";
            if (usersInChannel.Count > 0)
            {
                username = usersInChannel.First();
            }

            int test = thunder.SendMessageToUser(msg, username);

            bool test1 = thunder.IsUserOnline(username);
            bool test2 = thunder.IsUserOnline("not-test");

            thunder.DisconnectUser(username);
            thunder.DisconnectUser("not-test");
        }
    }
}

