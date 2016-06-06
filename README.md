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
_token.Subscribe(OnItemMessage);
....

private void OnItemMessage(YourObject message)
{
	Dispatcher.RequestMainThreadAction(() =>
	{
		//Do something with message;
	});
}

  
```
