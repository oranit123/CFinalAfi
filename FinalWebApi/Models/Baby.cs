namespace FinalWebApi.Models
{
    public class Baby
    {
        public string idChild { get; set; }
        public string childName { get; set; }
        public DateTime birthdate { get; set; }
        public string parentId { get; set; }  // New property for Parent ID
    }

}
