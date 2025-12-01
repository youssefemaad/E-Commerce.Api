using Microsoft.Extensions.Configuration;

namespace Service.MappingProfile
{
    public abstract class BasePictureUrlResolver
    {
        protected readonly IConfiguration Configuration;

        protected BasePictureUrlResolver(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected string ResolveUrl(string? pictureUrl)
        {
            if (string.IsNullOrEmpty(pictureUrl))
            {
                return string.Empty;
            }

            return $"{Configuration.GetSection("Urls")["BaseUrl"]}{pictureUrl}";
        }
    }
}
