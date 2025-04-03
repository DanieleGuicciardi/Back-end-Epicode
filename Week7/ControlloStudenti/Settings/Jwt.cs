namespace spiegazione_REST_epicode.Settings {
    public class Jwt {

        public string SecurityKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiresInMinutes { get; set; }
    }
}
