namespace spiegazione_REST_epicode.DTO {
    public class StudentDTO {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<CourseDTO> Courses { get; set; }
    }
}
