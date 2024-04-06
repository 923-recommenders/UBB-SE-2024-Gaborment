using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UBB_SE_2024_Gaborment.FeedRepo
{
    public abstract class FeedConfiguration
{
    protected int id;
    protected Guid name;

    public int getId()
    {
        return id;
    }

    public abstract int sortComparisonFunction(Posts Post1, Posts Post2);
    public abstract Posts[] filterPosts(Posts[] posts);
}
}