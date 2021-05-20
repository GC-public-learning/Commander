# .NET_Core_3.1_MVC_REST_API_project course

channel youtube of creator : Les Jackson https://www.youtube.com/channel/UCIMRGVXufHT69s1uaHHYJIA

link of the video course : https://www.youtube.com/watch?v=fmvcAzHpsk8

thanks to the youtuber "Les Jackson" ^^


## instructions

### install

go vs studio code
<br/>go terminal
<br/><br/>&emsp;go your folder project
&emsp;tape commands
<br/>&emsp;&emsp;- dotnet new (see all available projects)
<br/>&emsp;&emsp;- dotnet new webapi -n .Commander (Commander is the  project name)
<br/>&emsp;&emsp;- code -r .Commander (open in existing window)

### preparation
delete the forecast controler file
<br/>delete the model class weatherforecast.cs to

## create 1st controller with route and fake data to retrieve

(content of files created is not described !)

<br/>create the "Models" folder
<br/>&emsp;create "Command.cs" file in the "Models folder" 
<br/>create a "Data" folder for repository
<br/>&emsp;create "ICommanderRepo.cs" in the "Data" folder (make interface for the entire project)
<br/>&emsp;create "MockCommanderRepo.cs" in the "Data" folder (replace the data from the database to test the controllers acces)
<br/> create "CommandsController.cs" in "Controllers" folder (configure the url and controllers)

### test
<br/>&emsp; - go vs studio terminal (^ SHIFT ù) -> dotnet run
<br/>&emsp; - go to postman to test urls
<br/>&emsp;&emsp;http://localhost:5000/api/commands
<br/>&emsp;&emsp;http://localhost:5000/api/commands/123

## create the dependecy injection controller

video : 51:00









