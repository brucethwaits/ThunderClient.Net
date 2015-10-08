# ThunderClient.Net
.Net client for ThunderPush websockets server

##How to use

##Initialise

```csharp
ThunderClient.Net.Thunder thunder = new ThunderClient.Net.Thunder("[[serverNameOrIPAddress]]", "[[apikey]]", "[[secretkey]]");
```

##Get total number of users

```csharp
int userCount = thunder.GetUserCount();
```

##Send message

```csharp
var msg = new
{
    message = "test message"
};
int numberOfUsersWhoReceivedTheMessage = thunder.SendMessageToChannel(msg, "test");
```

##Get number of users in a channel

```csharp
List<string> usersInChannel = thunder.GetUsersInChannel("test");
```

##Send message to specific user

```csharp
var msg = new
{
    message = "test message"
};
int numberOfUsersWhoReceivedTheMessage = thunder.SendMessageToUser(msg, username);
```

##Check if specific user is online

```csharp
bool isOnline = thunder.IsUserOnline("[[username]]");
```

##Force a user to disconnect

```csharp
thunder.DisconnectUser("[[username]]");
```
