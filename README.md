1)	Prerequisites:
	Node.js, Angular CLI Tool
	.NET Core SDK
	Sql Server running
	
2)	a)	Open cmd from TransactionUploader.ServerSide\TransactionUploader.WebApi folder
	b)	Run "dotnet build"
	c)	Run "dotnet run"
	d)	Wait until app is running (There will be info message in console: "Now listening on: https://localhost:57601")

3)	a)	Open cmd from TransactionUploader.WebClient
	b)	Run "npm install"
	c)	Run "ng serve"
	d)	Wait until app is running (There will be message in console like: "Server is listening on localhost:4200, open your browser on http://localhost:4200/")

4)	a) Open browser on http://localhost:4200/
	b) There are several test input files with transaction in folder InputData which can be uploaded using "Upload Transaction" button.
