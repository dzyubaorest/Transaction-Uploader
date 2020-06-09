## Prerequisites:
	* Node.js, Angular CLI Tool
	* .NET Core SDK
	* Sql Server running

## Steps
	
### Open cmd from TransactionUploader.ServerSide\TransactionUploader.WebApi folder
```
* Run "dotnet build"
* Run "dotnet run"
*  Wait until app is running (There will be info message in console: "Now listening on: https://localhost:57601")
```
###	Open cmd from TransactionUploader.WebClient
```
* Run "npm install"
* Run "ng serve"
* Wait until app is running (There will be message in console like: "Server is listening on localhost:4200, open your browser on http://localhost:4200/")
```

### Open browser on http://localhost:4200/
There are several test input files with transaction in folder InputData which can be uploaded using "Upload Transaction" button.
