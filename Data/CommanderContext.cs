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

