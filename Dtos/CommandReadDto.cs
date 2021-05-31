namespace Commander.Dtos {
    public class CommandReadDto {
        public int Id { get; set; }
 
        public string HowTo { get; set; }
   
        public string Line { get; set; }
  
        // attribute deleted to don't expose this information on the client
        //public string Plateform { get; set; }
    }
}