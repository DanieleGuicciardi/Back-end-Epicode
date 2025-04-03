namespace spiegazione_REST_epicode.Models {
    public class Course {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        // Navigazione molti-a-molti verso gli studenti
        public ICollection<Student> Students { get; set; }
    }
}
