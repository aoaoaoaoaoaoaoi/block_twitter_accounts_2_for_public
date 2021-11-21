using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Tweetinvi;
using System.IO;
using System.Text;

namespace BlockTwitterAccouts
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var userClient = new TwitterClient(args[0], args[1], args[2], args[3]);
            var lists = await userClient.Lists.GetAccountListMembershipsAsync();
            var now = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            var blockUserList = new List<string>();
            foreach(var list in lists){
                if(list.Owner.Following != null && (bool)list.Owner.Following){
                    continue;
                }
                if(isNgByName(list.Name) || isNgByUserDescription(list.Owner.Description)){
                    await userClient.Users.BlockUserAsync(list.Owner.Id);
                    blockUserList.Add(list.Owner.ScreenName);
                }
            }
            if(blockUserList.Any()){
                var dmMessage = $"【自動ブロック機能】{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}\n以下のアカウントをブロックしました\n{string.Join( "\n@", blockUserList)}";
                var user = await userClient.Users.GetAuthenticatedUserAsync();
                await userClient.Messages.PublishMessageAsync(dmMessage, user.Id);
            }
        }

        private static bool isNgByName(string listName){
            var file = File.ReadAllText("./ngWordText/list_name.csv", Encoding.UTF8);
            var ngWordList = file.Split(',');
            foreach(var ngWord in ngWordList){
                if(listName.Contains(ngWord)){
                    return true;
                }
            }
            return false;
        } 

        private static bool isNgByUserDescription(string userDescription){
            var file = File.ReadAllText("./ngWordText/user_description.csv", Encoding.UTF8);
            var ngWordList = file.Split(',');
            foreach(var ngWord in ngWordList){
                if(userDescription.Contains(ngWord)){
                    return true;
                }
            }
            return false;
        } 
    }
}