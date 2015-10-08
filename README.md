# ThunderClient.Net
.Net client for ThunderPush websockets server

##How to use

##Initialise

`ThunderClient.Net.Thunder thunder = new ThunderClient.Net.Thunder("[[serverNameOrIPAddress]]", "[[apikey]]", "[[secretkey]]");`

##Get total number of users

`int userCount = thunder.GetUserCount();`

##Send message

```
var msg = new
{
    message = "test message"
};
int numberOfUsersWhoReceivedTheMessage = thunder.SendMessageToChannel(msg, "test");
```

##Get number of users in a channel

`List<string> usersInChannel = thunder.GetUsersInChannel("test");`

##Send message to specific user

```
var msg = new
{
    message = "test message"
};
int numberOfUsersWhoReceivedTheMessage = thunder.SendMessageToUser(msg, username);
```

##Check if specific user is online

`bool isOnline = thunder.IsUserOnline("[[username]]");`

##Force a user to disconnect

`thunder.DisconnectUser("[[username]]");`
