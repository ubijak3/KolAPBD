namespace Example.DTOs
{
    public class PrescriptionPOST
    {
        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public int IdPatient { get; set; }

        public int IdDoctor { get; set; }
    }
}
