using System;

namespace recommenders_backend
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FollowTests.TestFollowClass();
            FollowTests.TestFollowRepositoryClass();
            BlockTests.TestBlockClass();
            BlockTests.TestBlockRepositoryClass();
            FollowTests.TestFollowServiceClass();
            BlockTests.TestBlockServiceClass();
            RequestTests.TestRequestClass();
            RequestTests.TestRequestRepositoryClass();
        }
    }
}
