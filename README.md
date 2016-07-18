# Firebase.Xamarin
Light weight wrapper for Firebase Realtime Database REST API.
## Installation
```csharp
// Install release version
Install-Package Firebase.Xamarin

```

## Supported frameworks
* .NET 4.5+
* ASP.Net Core 1.0
* Xamarin Android
* Xamarin iOS 
* Xamarin iOS Classic
* Windows 8

## Usage

### Querying

```csharp
var firebase = new FirebaseClient("https://yourdatabase.firebaseio.com/");
var items = await firebase
  .Child("yourentity")
  .OrderByKey()
  .LimitToFirst(2)
  .OnceAsync<YourObject>();
  
foreach (var item in items)
{
  Console.WriteLine($"{item.Key} name is {item.Object.Name}");
}
```

### Saving data

```csharp
var firebase = new FirebaseClient("https://yourdatabase.firebaseio.com/");

// add new item to list of data 
var item = await firebase
  .Child("yourentity")
  .PostAsync(new YourObject());
  
// note that there is another overload for the PostAsync method which delegates the new key generation to the client
  
Console.WriteLine($"Key for the new item: {item.Key}");  

// add new item directly to the specified location (this will overwrite whatever data already exists at that location)
var item = await firebase
  .Child("yourentity")
  .Child("Ricardo")
  .PutAsync(new YourObject());

```

### Realtime streaming

```csharp
FirebaseClient<YourObject> _client = new FirebaseClient<YourObject>("https://yourdatabase.firebaseio.com/", "");
StreamToken<YourObject> _token = _client.GetStreamToken("yourentity");
_token.Where(r=> r.Object.id > 10) // optional
      .Subscribe(OnItemMessage);
....

private void OnItemMessage(FirebaseEvent<YourObject> message)
{
  	Dispatcher.RequestMainThreadAction(() =>
    {
        if (report.EventType == FirebaseEventType.InsertOrUpdate)
        {
            //Do Somethig
        }
        else
        {
            //Do Something else
        }
    });
}
```
## Generating Tokens

To generate tokens, you'll need your Firebase Secret which you can find by entering your Firebase
URL into a browser and clicking the "Secrets" tab on the left-hand navigation menu.

Once you've downloaded the library and grabbed your Firebase Secret, you can generate a token with
this snippet of .Net code:

```
var tokenGenerator = new Firebase.TokenGenerator("<YOUR_FIREBASE_SECRET>");
var authPayload = new Dictionary<string, object>()
{
  { "uid", "1" },
  { "some", "arbitrary" },
  { "data", "here" }
};
string token = tokenGenerator.CreateToken(authPayload);
```

The payload object passed into `CreateToken()` is then available for use within your
security rules via the [`auth` variable](https://www.firebase.com/docs/security/api/rule/auth.html).
This is how you pass trusted authentication details (e.g. the client's user ID) to your
Firebase rules. The payload can contain any data of your choosing, however it
must contain a "uid" key, which must be a string of less than 256 characters. The
generated token must be less than 1024 characters in total.


## Token Options

A second `options` argument can be passed to `CreateToken()` to modify how Firebase treats the
token. Available options are:

* **expires** (DateTime) - A timestamp denoting the time after which this token should no longer
be valid.

* **notBefore** (DateTime) - A timestamp denoting the time before which this token should be
rejected by the server.

* **admin** (bool) - Set to `true` if you want to disable all security rules for this client. This
will provide the client with read and write access to your entire Firebase.

* **debug** (bool) - Set to `true` to enable debug output from your security rules. You should
generally *not* leave this set to `true` in production (as it slows down the rules implementation
and gives your users visibility into your rules), but it can be helpful for debugging.

Here is an example of how to use the second `options` argument:

```
var tokenGenerator = new Firebase.TokenGenerator("<YOUR_FIREBASE_SECRET>");
var authPayload = new Dictionary<string, object>()
{
  { "uid", "1" },
  { "some", "arbitrary" },
  { "data", "here" }
};
string token = tokenGenerator.CreateToken(authPayload, new Firebase.TokenOptions(admin: true));
  
```
## Thanks
Special thanks to [bezysoftware](https://github.com/bezysoftware) for the original [firebase-database-dotnet] (https://github.com/step-up-labs/firebase-database-dotnet) code that is the core for this Xamarin adaptation. Also thanks to  [mikelehen](https://github.com/mikelehen) for the original [Firebase Token Generator - .NET] (https://github.com/firebase/firebase-token-generator-dotNet)
