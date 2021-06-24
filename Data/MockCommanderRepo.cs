using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data{

    // classe made to simulate the data from the database in order
    // to test the controllers acces commands lines
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
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

        public bool SaveChanges() {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd) {
            throw new System.NotImplementedException();
        }
    }
}