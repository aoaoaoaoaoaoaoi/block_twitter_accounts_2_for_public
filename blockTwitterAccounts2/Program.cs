using Tweetinvi;
using System.Text;
using System.Linq;
using Tweetinvi.Parameters;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlockTwitterAccouts
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var userClient = new TwitterClient(args[0], args[1], args[2], args[3]);
            var user = await userClient.Users.GetAuthenticatedUserAsync();
            var followUserIds = await userClient.Users.GetFriendIdsAsync(user.Id);
            var followUserIdsSet = new HashSet<long>(followUserIds);
            
            var blockingUserList = new List<string>();

            var blockingUserFromList = await blockUserFromList(userClient, followUserIdsSet);
            blockingUserList.AddRange(blockingUserFromList);

            var blockingUserFromFollow = await blockUserFromFollowers(userClient, user.Id, followUserIdsSet);
            blockingUserList.AddRange(blockingUserFromFollow);

            if(blockingUserList.Any()){
                var now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                var dmMessage = $"【自動ブロック機能】\n{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}\n以下のアカウントをブロックしました\n\n@{string.Join( "\n@", blockingUserList)}";
                await userClient.Messages.PublishMessageAsync(dmMessage, user.Id);
            }
        }

        private static async Task<List<string>> blockUserFromFollowers(TwitterClient userClient, long userId, HashSet<long> followUserIdsSet){
            var blockUserList = new List<string>();
            var followers = await userClient.Users.GetFollowersAsync(userId);
            foreach(var follower in followers){
                if(followUserIdsSet.Contains(follower.Id)){
                    continue;
                }
                if(isNgByUserDescription(follower.Description)){
                    await userClient.Users.BlockUserAsync(follower.Id);
                    blockUserList.Add(follower.ScreenName);
                }
            }
            return blockUserList;
        }

        private static async Task<List<string>> blockUserFromList(TwitterClient userClient, HashSet<long> followUserIds){
            var lists = await userClient.Lists.GetAccountListMembershipsAsync();
            var blockUserList = new List<string>();
            foreach(var list in lists){
                if(followUserIds.Contains(list.Owner.Id)){
                    continue;
                }
                if(isNgByName(list.Name) || isNgByUserDescription(list.Owner.Description)){
                    await userClient.Users.BlockUserAsync(list.Owner.Id);
                    blockUserList.Add(list.Owner.ScreenName);
                }
            }
            return blockUserList;
        }

        private static bool isNgByName(string listName){
            var ngWordList = getCsvData("./ngWordText/list_name.csv");
            foreach(var ngWord in ngWordList){
                if(listName.Contains(ngWord)){
                    return true;
                }
            }
            return false;
        } 

        private static bool isNgByUserDescription(string userDescription){
            var ngWordList = getCsvData("./ngWordText/user_description.csv");
            foreach(var ngWord in ngWordList){
                if(userDescription.Contains(ngWord)){
                    return true;
                }
            }
            return false;
        }

        private static string[] getCsvData(string filePath){
            var file = File.ReadAllText(filePath, Encoding.UTF8);
            return file.Split(',');
        } 
    }
}