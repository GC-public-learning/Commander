# .NET_Core_3.1_MVC_REST_API_project course

channel youtube of creator : Les Jackson https://www.youtube.com/channel/UCIMRGVXufHT69s1uaHHYJIA

link of the video course : https://www.youtube.com/watch?v=fmvcAzHpsk8

thanks to the youtuber "Les Jackson" ^^


## notes

the project was generated with the .net 5.0.203
<br>with this new version, new features have been added like "open API" (Swagger) which with you'll easily test urls and automaticaly generate documentation

## instructions

### install

go vs studio code
<br/>go terminal
<br/><br/>&emsp;go your folder project
&emsp;tape commands
<br/>&emsp;&emsp;- dotnet new (see all available projects)
<br/>&emsp;&emsp;- dotnet new webapi -n Commander (Commander is the  project name)
<br/>&emsp;&emsp;- code -r Commander (open in existing window)

### preparation
delete the forecast controler file
<br/>delete the model class weatherforecast.cs to

## 1) create 1st controller with route and fake data to retrieve

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

### test
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

## 2) configure the dependency injection container

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

## 3) set up database and install entity framework packages

- connection on sql server with MSSMS : server name : localhost and sql server authentification with sa
- create a specific login for the app
</br>&emsp;&emsp;- Security/login -> click right/new login
</br>&emsp;&emsp;&emsp; Login name : CommanderAPI
</br>&emsp;&emsp;&emsp; sql server authentification
</br>&emsp;&emsp;&emsp;set up the passwords
</br>&emsp;&emsp;&emsp; unpick "enforce password policy" and other options
</br>&emsp;&emsp;&emsp; add sysadmin on Server Roles place
- disconnect and reconnect as CommanderAPI user
- go nuget.org and search entityframework
</br>&emsp;&emsp;- select Microsoft entity frameworkCore
</br>&emsp;&emsp;- copy the path .NET CLI
</br>&emsp;&emsp;- paste the path on your vs terminal and execute, you don't have to specify the version ->
~~~
dotnet add package Microsoft.EntityFrameworkCore
~~~
- install these packages to in the same way : 
<br>&emsp;"Microsoft.EntityFrameworkCore.Design" 
<br>&emsp;"Microsoft.EntityFrameworkCore.SqlServer"

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


## 4) configure dbContext, ConnectionString and update de db with our model (not data yet)

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
