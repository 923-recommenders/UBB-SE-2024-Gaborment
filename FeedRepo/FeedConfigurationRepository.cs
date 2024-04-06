using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.FeedRepo
{
    public class FeedConfigurationRepository
{
    private List<FeedConfiguration> feedList;
    private int freeId;

    /*
    public FeedConfigurationRepository(List<FeedConfiguration> newFeedList, int newFreeId)
    {
        this.feedList = newFeedList;
        this.freeId = newFreeId;
    }
    */
    public FeedConfigurationRepository()
    {
        feedList = new List<FeedConfiguration>();
        HomePageFeedBuilderMock homeBuilder = new HomePageFeedBuilderMock();
        FeedConfiguration homeFeed = homeBuilder.Builder();

        TrendingFactoryMock trendingFeedBuilder = new TrendingFactoryMock();
        FeedConfiguration trendingFeed = trendingFeedBuilder.Builder();

        ControversialFeedFactoryMock controversialFeedBuilder = new ControversialFeedFactoryMock();
        FeedConfiguration controversialFeed = controversialFeedBuilder.Builder();

        FollowingFeedFactory followingFeedBuilder = new FollowingFeedFactory();
        FeedConfiguration followingFeed = followingFeedBuilder.Builder();

        feedList.Add(homeFeed);
        feedList.Add(trendingFeed);
        feedList.Add(controversialFeed);
        feedList.Add(followingFeed);

        freeId = 4;
    }
    public List<FeedConfiguration> GetFeedList()
    {
        return feedList;
    }

    public FeedConfiguration getFeed(int searchedId)
    {
        if(feedList == null)
        {
            throw new ArgumentNullException("the feed list from the repository is empty, no feed found");

        }
        else
        {
            FeedConfiguration searchedFeed = null!;
            
            var templateFeed = feedList
                                .Where(feed => feed != null && feed.getId() == searchedId)
                                .SingleOrDefault();
            if (templateFeed != null)
            {
                searchedFeed = templateFeed;
            }
            if (templateFeed == null)
            {
                throw new ArgumentException($"no feed found with id {searchedId}");
            }
           
            if(searchedFeed == null)
            {
                throw new ArgumentException($"no feed found with id {searchedId}");
            }
            return searchedFeed;
        }
    }

    public void addCustomFeedToRepository(string[] ?hashtagsList, string[]? locations, string[]? users)
    {
        FeedConfigurationFactoryMock director = new FeedConfigurationFactoryMock();
        
        FeedConfiguration newFeed = director.makeCustomFeed(hashtagsList != null ? hashtagsList : new string[0],
                                        locations != null ? locations : new string[0],
                                        users != null ? users : new string[0]);
        if(newFeed == null)
        {
            throw new ArgumentNullException("custom feed failed to be passed in the Repository");
        }
        else
        {
            feedList.Add(newFeed);
        }
        freeId++;
    }
    public void updateCustomFeed(int id, string[] ? hashtagsList, string[]? locations, string[]? users)
    {
        FeedConfiguration toBeUpdatedFeed = getFeed(id);
        if(toBeUpdatedFeed.GetType() == typeof(CustomFeedMock))
        {
            FeedConfigurationFactoryMock director = new FeedConfigurationFactoryMock();
            FeedConfiguration newFeed = director.makeCustomFeed(hashtagsList != null ? hashtagsList : new string[0],
                                            locations != null ? locations : new string[0],
                                            users != null ? users : new string[0]);
            toBeUpdatedFeed = newFeed;
        }
        else
        {
            throw new Exception("feed must be of type 'CUSTOM' in order to be updated from repo");
        }
    }

    public void deleteCustomFeed(int id)
    {
         FeedConfiguration toBeDeletedFeed = getFeed(id);
        if (toBeDeletedFeed.GetType() == typeof(CustomFeedMock))
        {
            feedList.Remove(toBeDeletedFeed);
        }
        else
        {
            throw new Exception("feed must be of type 'CUSTOM' in order to be deleted from repo");
        }
    }

    private int getFreeFeedId()
    {
        return freeId;
    }
}
}
