using Microsoft.AspNetCore.Mvc;

namespace RestAPICV.Endpoints
{
    public static class BaseEndpoints
    {
        public static void RegisterEndpoints(this WebApplication app)
        {
            PersonalInfoEndpoints.RegisterEndpoints(app);
            EducationEndpoints.RegisterEndpoints(app);
            WorkExperienceEndpoints.RegisterEndpoints(app);
            GitHubEndpoints.RegisterEndpoints(app);
        }
    }
}