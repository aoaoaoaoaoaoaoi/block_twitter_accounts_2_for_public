# Automatic Twitter account blocking function
## Behavior
* Block the list owner if the list name of the list you are put in contains the specified string.
* Block the list owner if the list owner's self introduction of the list you are included in contains the string you specified.
* Block a follower(maybe the 100 most recent) if his or her self-introduction contains a string that you specify.
(Users you follow are not blocked.)

You can receive a list of users you have blocked in DM.

## Required
Register with Twitter Developer to receive tokens, etc.

## Usage
1. Register token, etc. as follows

| secret's name       | things to register  |
| ------------------- | ------------------- |
| ACCESS_TOKEN        | access token        |
| ACCESS_TOKEN_SECRET | access token secret |
| API_KEY             | api key             |
| API_KEY_SECRET      | api key secret      |

2. Put list name's ng words in ['ngWordText/list_name.csv'](https://github.com/aoaoaoaoaoaoaoi/block_twitter_accounts_2_for_public/blob/main/blockTwitterAccounts2/ngWordText/list_name.csv)
3. Put follower introduction's ng words in ['ngWordText/user_description.csv'](https://github.com/aoaoaoaoaoaoaoi/block_twitter_accounts_2_for_public/blob/main/blockTwitterAccounts2/ngWordText/user_description.csv)
4. Run GitHubActions manually or wait for a scheduled run (default is 9:00 UTC time)

## Code Explanation (In Japanese)
https://zenn.dev/aoaoaoaoaoaoaoi/articles/5e6a8262774f35
