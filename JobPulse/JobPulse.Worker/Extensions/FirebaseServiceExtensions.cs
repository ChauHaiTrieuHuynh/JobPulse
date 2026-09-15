using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using JobPulse.Worker.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Worker.Extensions
{
    public static class FirebaseServiceExtensions
    {
        public static IServiceCollection AddFirebaseService(this IServiceCollection services, IConfiguration configuration)
        {
            try {
                services.Configure<FirebaseSetting>(
                    configuration.GetSection("Firebase"));

                services.AddSingleton<FirestoreDb>(sp => {
                    var setting = sp.GetRequiredService<IOptions<FirebaseSetting>>().Value;
                    var env = sp.GetRequiredService<IHostEnvironment>();

                    var credentialPath = Path.Combine(env.ContentRootPath, "Keys", "firebase.json");

                    if (credentialPath == null || !File.Exists(credentialPath))
                    {
                        throw new FileNotFoundException($"Firebase credential file not found at path: {credentialPath}");
                    }
                    var credential = GoogleCredential.FromFile(credentialPath);

                    return new FirestoreDbBuilder
                    {
                        ProjectId = setting.ProjectId,
                        Credential = credential
                    }.Build();
                });

                return services;
            }
            catch(Exception ex) {
                throw;
            }
        }
    }
}
