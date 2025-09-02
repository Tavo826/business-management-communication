using Google.Apis.Auth.OAuth2;

namespace Application.Utils
{
    public static class GoogleAuthentication
    {
        public static GoogleCredential FromServiceAccountJson(string serviceAccountJson, string[] scopes)
        {
            var credential = GoogleCredential.FromJson(serviceAccountJson);
            if (credential.IsCreateScopedRequired)
                credential = credential.CreateScoped(scopes);
            return credential;
        }
    }
}
