using System;
using System.Text;

namespace recommenders
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            InternationalizationManager internationalizationManager = new InternationalizationManager();

            Console.WriteLine(internationalizationManager.GetTranslationForIdentifier("NoFriendRequestMessage"));
            Console.WriteLine(internationalizationManager.GetTranslationForIdentifier("ConfirmRemoveFriendMessage"));
            Console.WriteLine($"{internationalizationManager.GetTranslationForIdentifier("FriendRequestConfirmedNotification")}", "X");
            Console.WriteLine($"{internationalizationManager.GetTranslationForIdentifier("NumberMutualFriendsLabel")}", "100");
            Console.WriteLine(internationalizationManager.GetTranslationForIdentifier("FriendRequestsTitle"));
            Console.WriteLine(internationalizationManager.GetTranslationForIdentifier("FollowBackFriendRequestButton") + "\n");

            internationalizationManager.SetLanguage("es-ES");

            Console.WriteLine(internationalizationManager.GetTranslationForIdentifier("NoFriendRequestMessage"));
            Console.WriteLine(internationalizationManager.GetTranslationForIdentifier("ConfirmRemoveFriendMessage"));
            Console.WriteLine($"{internationalizationManager.GetTranslationForIdentifier("FriendRequestConfirmedNotification")}", "X");
            Console.WriteLine($"{internationalizationManager.GetTranslationForIdentifier("NumberMutualFriendsLabel")}", "100");
            Console.WriteLine(internationalizationManager.GetTranslationForIdentifier("FriendRequestsTitle"));
            Console.WriteLine(internationalizationManager.GetTranslationForIdentifier("FollowBackFriendRequestButton") + "\n");

            internationalizationManager.SetLanguage("ro-RO");

            Console.WriteLine(internationalizationManager.GetTranslationForIdentifier("NoFriendRequestMessage"));
            Console.WriteLine(internationalizationManager.GetTranslationForIdentifier("ConfirmRemoveFriendMessage"));
            Console.WriteLine($"{internationalizationManager.GetTranslationForIdentifier("FriendRequestConfirmedNotification")}", "X");
            Console.WriteLine($"{internationalizationManager.GetTranslationForIdentifier("NumberMutualFriendsLabel")}", "100");
            Console.WriteLine(internationalizationManager.GetTranslationForIdentifier("FriendRequestsTitle"));
            Console.WriteLine(internationalizationManager.GetTranslationForIdentifier("FollowBackFriendRequestButton") + "\n");

            internationalizationManager.SetLanguage("ca-ES");

            Console.WriteLine(internationalizationManager.GetTranslationForIdentifier("NoFriendRequestMessage"));
            Console.WriteLine(internationalizationManager.GetTranslationForIdentifier("ConfirmRemoveFriendMessage"));
            Console.WriteLine($"{internationalizationManager.GetTranslationForIdentifier("FriendRequestConfirmedNotification")}", "X");
            Console.WriteLine($"{internationalizationManager.GetTranslationForIdentifier("NumberMutualFriendsLabel")}", "100");
            Console.WriteLine(internationalizationManager.GetTranslationForIdentifier("FriendRequestsTitle"));
            Console.WriteLine(internationalizationManager.GetTranslationForIdentifier("FollowBackFriendRequestButton") + "\n");

            try
            {
                Console.WriteLine(internationalizationManager.GetTranslationForIdentifier("IdentifierThatDoesNotExist"));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString() + "\n");
            }

            Console.ReadKey();
        }
    }
}
