namespace FinalWebApi.Models
{
    public class Vaccination
    {
        public string idChild { get; set; }
        public DateTime timeDate { get; set; }
        public int typeVaccination { get; set; }
        public string nodes { get; set; }
        public string descreption { get; set; } // Added this property

    }

}
