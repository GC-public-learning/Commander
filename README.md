# .NET_Core_3.1_MVC_REST_API_project course

channel youtube of creator : Les Jackson https://www.youtube.com/channel/UCIMRGVXufHT69s1uaHHYJIA

link of the video course : https://www.youtube.com/watch?v=fmvcAzHpsk8

thanks to the youtuber "Les Jackson" ^^

## The goal
#### Make a MVC rest API asp.net core with 2021 professional conventional ways
#### use of GET, POST, PUT, PATCH, DELETE requests

## Technos
<img src="https://github.com/Geoffrey-Carpentier/Commander/blob/main/img/ASP_mvc_5.jpg" alt="asp net 5 mvc" height="100">&emsp;<img src="https://github.com/Geoffrey-Carpentier/Commander/blob/main/img/dotnetcore.png" alt="dotnet core" height="100">&emsp;<img src="https://github.com/Geoffrey-Carpentier/Commander/blob/main/img/c.png" alt="c#" height="100">&emsp;<img src="https://github.com/Geoffrey-Carpentier/Commander/blob/main/img/ef5.JPG" alt="Entity Framework 5" height="100">&emsp;<img src="https://github.com/Geoffrey-Carpentier/Commander/blob/main/img/sqlserver2019.jpg" alt="SQL server 2019" height="100">&emsp;<img src="https://github.com/Geoffrey-Carpentier/Commander/blob/main/img/open_api.png" alt="open Api" height="100">&emsp;<img src="https://github.com/Geoffrey-Carpentier/Commander/blob/main/img/vscode.png" alt="VS code" height="100">&emsp;<img src="https://github.com/Geoffrey-Carpentier/Commander/blob/main/img/automapper.png" alt="Automapper" height="100">

## Additional nuget packages : 
- "Microsoft.EntityFrameworkCore" to create the DB in code 1st and manage it
- "Microsoft.EntityFrameworkCore.Design" to use design time for "migration"
- "Microsoft.EntityFrameworkCore.SqlServer" to use "SqlServer"
- "Automapper" to map the objects with the DTO architecture
- "Microsoft.AspNetCore.JsonPatch" to modify the attributes from the objects separately thank to a json file (add, remove, replace, copy, move, test operations are available) ex format : 
~~~
[
    { "op": "add", "path": "/myPath", "value": ["myValue"] }
]
~~~
- "Microsoft.AspNetCore.Mvc.NewtonsoftJson" to customize the Json serialization and deserialization 
## Shemas
### Architecture
<img src="https://github.com/Geoffrey-Carpentier/Commander/blob/main/img/api_archi.jpg" alt="api archi" height="600"><br>
### End points
<img src="https://github.com/Geoffrey-Carpentier/Commander/blob/main/img/api_end_points.JPG" alt="api end points" height="600"><br>
### Cover points (titles and numbers has been adaptaded depending on commits) 
<img src="https://github.com/Geoffrey-Carpentier/Commander/blob/main/img/cover_points.jpg" alt="cover points" height="600"><br>
### Swagger uris
<img src="https://github.com/Geoffrey-Carpentier/Commander/blob/main/img/swagger_uris.JPG" alt="swagger uris" height="600"><br>
### Swagger Classes Shemas 
<img src="https://github.com/Geoffrey-Carpentier/Commander/blob/main/img/swagger_shemas.jpg" alt="swagger shemas" height="600">

## Notes
the project was generated with the .net 5.0.203
<br>with this new version, new features have been added like "open API" (Swagger) which with you'll easily test uris and automaticaly generate documentation

## Instructions

### Install

go vs studio code
<br/>go terminal
<br/><br/>&emsp;go your folder project
<br>&emsp;tape commands :
<br/>&emsp;&emsp;- dotnet new (see all available projects)
<br/>&emsp;&emsp;- dotnet new webapi -n Commander (Commander is the  project name)
<br/>&emsp;&emsp;- code -r Commander (open in existing window)

### Preparation
delete the forecast controller file
<br/>delete the model class weatherforecast.cs to

## 1) Setup the 2 "GET" requests, create 1st controller with route and fake data to retrieve

<br/>create the [Models/](https://github.com/Geoffrey-Carpentier/Commander/tree/main/Models) folder
<br/>&emsp;create "Command.cs" file in the "Models folder" to make the class : 
~~~
namespace Commander.Models {
    public class Command {
        public int Id { get; set; }
        public string HowTo { get; set; }
        public string Line { get; set; }
        public string Plateform { get; set; }
    }
}
~~~
<br/>create the [Data/](https://github.com/Geoffrey-Carpentier/Commander/tree/main/Data) folder for repository
<br/>&emsp;create "ICommanderRepo.cs" in the "Data" folder (make interface for the entire project) :
~~~
using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data {
    public interface ICommanderRepo {
        IEnumerable<Command> GetAppCommands();
        Command GetCommandById(int id);
    }
}
~~~

<br/>&emsp;create "MockCommanderRepo.cs" in the "Data" folder (replace the data from the database to test the controllers acces)
~~~
using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data{

    // classe made to simulate the data from the database in order
    // to test the controllers acces commands lines
    public class MockCommanderRepo : ICommanderRepo
    {
        public IEnumerable<Command> GetAppCommands()
        {
            var commands = new List<Command> {
                new Command{Id=0, HowTo="Boil an egg", Line="Boil water", Plateform="Kettle & Pan"},
                new Command{Id=1, HowTo="cut bread", Line="Get a knife", Plateform="knife"},
                new Command{Id=2, HowTo="Make cup of tea", Line="place teabag in cup", Plateform="Kettle & cup"}
            };
            return commands;
        }

        public Command GetCommandById(int id) {
            return new Command{Id=0, HowTo="Boil an egg", Line="Boil water", Plateform="Kettle & Pan"};
        }
    }
}
~~~
<br/> create "CommandsController.cs" in [Controllers/](https://github.com/Geoffrey-Carpentier/Commander/tree/main/Controllers) folder (configure the url and controllers)
~~~
using System.Collections.Generic;
using Commander.Data;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers{

    // api/commands
    [Route("api/[controller]")] 
    [ApiController]
    public class CommandsController : ControllerBase {
        private readonly MockCommanderRepo _repository = new MockCommanderRepo();
        [HttpGet]
        public ActionResult <IEnumerable<Command>> GetAllCommands() {
            var commandItems = _repository.GetAppCommands();
            return Ok(commandItems); // Ok -> code 200
        }

        // api/commands/{id} -> to get a JSON value
        [HttpGet("{id}")] 
        public ActionResult <Command> GetCommandById(int id) {
            var commandItem = _repository.GetCommandById(id);
            return Ok(commandItem);
        }
    }

}

~~~

### Test
<br/>&emsp; - go vs studio terminal (^ SHIFT ù) : 
~~~
dotnet run
~~~
<br/>&emsp; - go to postman to test urls
<br/>&emsp;&emsp;http://localhost:5000/api/commands
<br/>&emsp;&emsp;http://localhost:5000/api/commands/123

if you use the dotnet v 5... you can also use the page from "Swagger" service to test in your navigator the different urls of your API : 
~~~
http://localhost:5000/swagger/
https://localhost:5001/swagger/index.html (if the 1st url doesn't make work the calls)
~~~
if you want to use Swagger and you have an older version of dotnet you can install this one with the nuget package manager

## 2) Configure the dependency injection container

modify "ConfigureServices" method in the "Startup.cs" file :
~~~
public void ConfigureServices(IServiceCollection services)
{

    services.AddControllers();
    services.AddScoped<ICommanderRepo, MockCommanderRepo>(); // line added
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Commander", Version = "v1" });
    });
}
~~~
make the contructor for "CommandsController" and change the "\_repository" attribute value

~~~
private readonly ICommanderRepo _repository;

public CommandsController(ICommanderRepo repository) {
	_repository = repository;
}

~~~

### test :
in terminal : dotnet run then test the urls

## 3) Setup database and install entity framework packages

- connection on sql server with MSSMS : server name : localhost and sql server authentification with sa
- create a specific login for the app
<br/>&emsp;&emsp;- Security/login -> click right/new login
<br/>&emsp;&emsp;&emsp; Login name : CommanderAPI
<br/>&emsp;&emsp;&emsp; sql server authentification
<br/>&emsp;&emsp;&emsp;set up the passwords
<br/>&emsp;&emsp;&emsp; unpick "enforce password policy" and other options
<br/>&emsp;&emsp;&emsp; add sysadmin on Server Roles place
- disconnect and reconnect as CommanderAPI user
- go nuget.org and search entityframework
<br/>&emsp;&emsp;- select Microsoft entity frameworkCore
<br/>&emsp;&emsp;- copy the path .NET CLI
<br/>&emsp;&emsp;- paste the path on your vs terminal and execute, you don't have to specify the version ->
~~~
dotnet add package Microsoft.EntityFrameworkCore
~~~
- install these packages to in the same way : 
<br/>&emsp;"Microsoft.EntityFrameworkCore.Design" 
<br/>&emsp;"Microsoft.EntityFrameworkCore.SqlServer"

all the installed packages should appear on the .csproj file
~~~
<ItemGroup>
	<PackageReference Include="Microsoft.EntityFrameworkCore" Version="5.0.6" />
	<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="5.0.6">
	  <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
	  <PrivateAssets>all</PrivateAssets>
	</PackageReference>
	<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="5.0.6" />
	<PackageReference Include="Swashbuckle.AspNetCore" Version="5.6.3" />
</ItemGroup>
~~~

- tape this command line to install the Entity Framework Core .NET Command-line Tools
~~~
dotnet tool install --global dotnet-ef
~~~


## 4) Configure dbContext, ConnectionString and update de db with our model (not data yet)

- create "CommanderContext.cs" file on the Data folder :

~~~
using Commander.Models;
using Microsoft.EntityFrameworkCore;
namespace Commander.Data {
    
    public class CommanderContext : DbContext {
        //constructor
        public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt) {

        }
        public DbSet<Command> Commands { get; set; }
     
    } 
}
~~~
- add lines to configure the connection on the db on the "appsettings.json" file
~~~
"ConnectionStrings": {
    "CommanderConnection": "Server=localhost; Initial Catalog=CommanderDB; User ID=CommanderAPI; Password=passwd"
  }
~~~
- add line on "ConfigureServices" method from the "Startup.cs" file :
~~~
services.AddDbContext<CommanderContext>(opt => opt.UseSqlServer
    (Configuration.GetConnectionString("CommanderConnection")));
~~~
- tape commande line in your vs terminal : to generate de Migration folder with its files
~~~
dotnet ef migrations add InitialMigration
dotnet ef migrations remove 
~~~
- we remove the migrations files because we before have to add rules in our models Commands.cs 
~~~
using System.ComponentModel.DataAnnotations;

namespace Commander.Models {
    public class Command {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }
        [Required]
        public string Line { get; set; }
        [Required]
        public string Plateform { get; set; }
    }
}
~~~
- then regenerate de migration
- after execute command line in the terminal to update the database linked
~~~~
dotnet ef database update
~~~~
- check on MSSMS if the tables has been created


## 5) Add real data manually and handle it

- add manually 2 records on the "commands" table with "mssms"
- rename the "GetAppCommands()" by "GetAllCommands()" in the "ICommanderRepo" interface and 
update the files that use the old commmand with the new method name.
- create a new file in the Data/ folder : SqlCommanderRepo.cs :
~~~
using System.Collections.Generic;
using System.Linq;
using Commander.Models;

namespace Commander.Data {
    public class SqlCommanderRepo : ICommanderRepo {
        private readonly CommanderContext _context;
        public SqlCommanderRepo(CommanderContext context) {
            _context = context;
        }
        public IEnumerable<Command> GetAllCommands() {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int id) {
            return _context.Commands.FirstOrDefault(p => p.Id == id);
        }
    }
}
~~~
- on the "Startup.cs" file make the line that uses the Mock repo in commentary and add a new line in order to use the SqlCommanderRepo instead the MockCommanderRepo (fake data):
~~~
//services.AddScoped<ICommanderRepo, MockCommanderRepo>();
services.AddScoped<ICommanderRepo, SqlCommanderRepo>();
~~~
- test the urls after build and run on the vscode terminal :
~~~
dotnet build
dotnet run
~~~

## 6) Configure the DTO in order to map the serialised objects only with the information needed by the client

- install "AutoMapper.Extensions.Microsoft.DependencyInjection" via nuget.org in the same way than EF 5

- go startup.cs and add a the automapper service in the "ConfigureServices" method
~~~
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
~~~
- create a new folder "Dtos" on the project root
- create CommandReadDto.cs file in this folder
- copy the content of Command.cd from Models to the new created files and make some modifs :
~~~
namespace Commander.Dtos {
    public class CommandReadDto {
        public int Id { get; set; }
 
        public string HowTo { get; set; }
   
        public string Line { get; set; }
  
        // attribute deleted to don't expose this information on the client
        //public string Plateform { get; set; }
    }
}
~~~
- create a new folder "Profiles" ont the project root
- create "CommandsProfile.cs" on this new folder and fill it like that :
~~~
using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles {
    public class CommandsProfile : Profile {
        public CommandsProfile() {
            CreateMap<Command, CommandReadDto>();
        }
    }
}
~~~
modify "GetCommandByID"from the "CommandsController"  in order to return another error code when the id doesn't exist (204 replaced by notfound(404)) 
~~~
// api/commands/{id} -> to get a JSON value
        [HttpGet("{id}")] 
        public ActionResult <Command> GetCommandById(int id) {
            var commandItem = _repository.GetCommandById(id);
            if(commandItem != null){
                return Ok(commandItem);
            }
            return NotFound(); //404

        
        }
~~~
- modify the controller to use the new mapping library with the dto and modify the methods to use "CommandReadDto" instead "Command" : 
~~~
using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers{

    // api/commands
    [Route("api/[controller]")] 
    [ApiController]
    public class CommandsController : ControllerBase {

        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper) {
        _repository = repository;
        _mapper = mapper;
        }

        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDto>> GetAllCommands() {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems)); // Ok -> code 200
        }

        // api/commands/{id} -> to get a JSON value
        [HttpGet("{id}")] 
        public ActionResult <CommandReadDto> GetCommandById(int id) {
            var commandItem = _repository.GetCommandById(id);
            if(commandItem != null){
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            return NotFound(); //404
        }
    }
}

~~~
- when the urls are tested, the serialised objects should be displayed without the "plateform" attribute

## 7) Setup "POST" request -> add the create command way on the API

- add 2 lines in the "ICommanderRepo" from "Data/" folder in order to create commands :
~~~
bool SaveChanges();
void CreateCommand(Command cmd);
~~~
- reimplement de "ICommanderRepo" in "MockCommander.cs" and "SqlCommander.cs" from "Data/" -> "^;"
- modify the 2 news functions on "SqlCommanderRepo" from "Data/"
~~~
public bool SaveChanges() {
    return (_context.SaveChanges() >= 0);
}
public void CreateCommand(Command cmd) {
    if(cmd == null) {
        throw new ArgumentNullException(nameof(cmd));
    }
    _context.Commands.Add(cmd);
}
~~~
- create "CommandCreateDto.cs" in "Dtos/" :
~~~
namespace Commander.Dtos {
    public class CommandCreateDto {
        public string HowTo { get; set; }
        public string Line { get; set; }
        public string Plateform { get; set; }
    }
}
~~~
- add a new line in "CommandsProfile()" method in "CommandsProfile.cs from "Profiles/" :
~~~
CreateMap<CommandCreateDto, Command>();
~~~
- add new method in "CommandsController.cs" from "Controllers/" to create command :
~~~
// POST api/commands
[HttpPost]
public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto) {
    var commandModel = _mapper.Map<Command>(commandCreateDto);
    _repository.CreateCommand(commandModel);
    _repository.SaveChanges();

    // to show the object unless with needed attributes
    var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

    return Ok(commandReadDto);
}
~~~
- test the new url with a random json like that (easy with swagger): 
~~~
{
  "howTo": "string",
  "line": "string",
  "plateform": "string"
}
~~~
<br/>if everything is ok the api should return a json like that :
~~~{
  "id": 3,
  "howTo": "string",
  "line": "string",
}
~~~
<br/>a new record should be added on the db

## 8) Get the best suitable status codes from the uris

### get a 201 status code when a command is created by the post method

- complete the name of the header from "GetCommandById()" method on the commandController :
~~~
[HttpGet("{id}", Name ="GetCommandById")] 
~~~
- change the return of CreateCommand() method from the CommandController to return a 201 status :
~~~
// params : route name,  route value, content
return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto);
~~~
- test the post "/api/Commands" by creating a command. The response status code should be 201, with this
method you can retrieve the new oject created in a mapped json format on the body of the response and also 
get the path of the url to retrieve the command with the "get" method on the header from the response.

### get an appropriate error code

- add headers on "CommandCreateDto" from Dtos in order to retrieve a more suitable error response if the Json is incomplete when a command is created with the POST method. With this way a 400 code error is generated instead a 500 server error code :
~~~
using System.ComponentModel.DataAnnotations;

namespace Commander.Dtos {
    public class CommandCreateDto {
        [Required]
        [MaxLength(250)]
        public string HowTo { get; set; }
        [Required]
        public string Line { get; set; }
        [Required]
        public string Plateform { get; set; }
    }
}
~~~

## 9) Setup "PUT" request -> update a command (replace an old command by a new one)
- add new line on the "ICommanderRepo" interface from "Data/" folder :
~~~
void UpdateCommand(Command cmd);
~~~
- reimplement interface on the "MockCommanderRepo" from "Data/" folder (^; on the interface)
- reimplement interface on the "SqlCommanderRepo" from "Data/" folder and empty the update method
- create "CommandUpdateDto.cs" in the "Dtos" folder and copy the content from "CommandCreateDto" on it with replace the class name
- add new line on "CommandsProfile.cs" from "Profiles/" folder :
~~~
CreateMap<CommandUpdateDto, Command>();
~~~
add "UpdateCommand()" method on "CommandsController.cs" from "Controllers/" folder :
~~~
// PUT api/commands/{id}
[HttpPut("{id}")]
public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto) {
    var commandModelFromRepo = _repository.GetCommandById(id);
    if(commandModelFromRepo == null) {
        return NotFound();
    }
    _mapper.Map(commandUpdateDto, commandModelFromRepo);
    _repository.UpdateCommand(commandModelFromRepo);
    _repository.SaveChanges();
    return NoContent(); // 204
}
~~~
- test the new uri ("api/commands/{id}") by sending a new json object with any id. the status code should be 204

## 10) Setup "PATCH" request -> make a partial update on a record (choosing the attributes to replace)

- Install "Microsoft.AspNetCore.JsonPatch", go nuget.org to copy the ".net cli" path. (version doesn't needed)
~~~
dotnet add package Microsoft.AspNetCore.JsonPatch
~~~
- Install "Microsoft.AspNetCore.Mvc.NewtonsoftJson " with nuget.org to
~~~
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
~~~
- add new lines on "Startup.cs" file :
~~~
// header
using Newtonsoft.Json.Serialization;

// inside ConfigureServices() method
services.AddControllers().AddNewtonsoftJson(s => {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});
~~~
- addnew line on "CommandsProfile.cs" from "Profiles/" folder inside "CommandsProfile()"" method :
~~~
CreateMap<Command, CommandUpdateDto>();
~~~
- add the "PATCH" request on "CommandsController" : 
~~~
using Microsoft.AspNetCore.JsonPatch;

// PATCH api/commands/{id}
[HttpPatch("{id}")]
public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc) {
    var commandModelFromRepo = _repository.GetCommandById(id);
    if(commandModelFromRepo == null) {
        return NotFound();
    }
    
    var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
    patchDoc.ApplyTo(commandToPatch, ModelState);
    if(!TryValidateModel(commandToPatch)) {
        return ValidationProblem();
    }
    _mapper.Map(commandToPatch, commandModelFromRepo);
    _repository.UpdateCommand(commandModelFromRepo);
    _repository.SaveChanges();
    return NoContent();
}
~~~
- test the new uri, by replacing an attribute :
~~~
[
  {
    "path": "/howto",
    "op": "replace",
    "value": "replaced 26/06"
  }
]
~~~

## 11) Setup "DELETE" request -> Remove  a record on the "Commands"table from the db

- add new line on "ICommanderRepo.cd" from "Data/" folder :
~~~
void DeleteCommand(Command id);
~~~
- reimplement interface for "MockCommanderRepo.cs"
- reimplement interface for "SqlCommanderRepo.cs" and fill the "DeleteCommand()" method :
~~~
public void DeleteCommand(Command cmd) {
    if(cmd == null) {
        throw new ArgumentNullException(nameof(cmd));
    }
    _context.Commands.Remove(cmd);
}
~~~ 
- Add the last ActionResult on "CommandsController.cs" :
~~~
// DELETE api/commands/{id}
[HttpDelete("{id}")]
public ActionResult DeleteCommand(int id) {
    var commandModelFromRepo = _repository.GetCommandById(id);
    if(commandModelFromRepo == null) {
        return NotFound();
    }
    _repository.DeleteCommand(commandModelFromRepo);
    _repository.SaveChanges();
    return NoContent();
}
~~~
- test the new uri DELETE /api/Commands/{id} with a random id, the status code should be 204 and the data should be removed from the db



