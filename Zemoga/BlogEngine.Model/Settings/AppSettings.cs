namespace BlogEngine.Model.Settings
{
    public class AppSettings
    {
        public ExternalService ExternalService { get; set; }
        public int TokenExpirationTime { get; set; }
        public Messages Messages { get; set; }
    }
    public class ExternalService
    {
        public FirebaseBaseSetting FirebaseSignIn { get; set; }
        public FirebaseBaseSetting FirebaseSignUp { get; set; }
    }

    public class FirebaseBaseSetting
    {
        public string Url { get; set; }
    }

    public class Messages
    {
        public string CommentCreationSucessfull { get; set; }
        public string CommentCreationFailed { get; set; }
        public string PostCreationSucessfull { get; set; }
        public string PostCreationFailed { get; set; }
        public string PostEditionNotFound { get; set; }
        public string PostEditionBadRequest { get; set; }
        public string PostEditionFailed { get; set; }
        public string PostEditionSucessfull { get; set; }
        public string UserCreationFailed { get; set; }
    }
}
