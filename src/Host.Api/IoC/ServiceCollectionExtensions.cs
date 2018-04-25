using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Host.Api.IoC
{
    /*
     * https://fullstackmark.com/post/13/jwt-authentication-with-aspnet-core-2-web-api-angular-5-net-core-identity-and-facebook-login
     * https://joonasw.net/view/adding-custom-claims-aspnet-core-2
     * https://joonasw.net/view/apply-authz-by-default
     */
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));

            var signingKey = new X509SecurityKey(
                new X509Certificate2(
                    jwtAppSettingOptions[nameof(JwtIssuerOptions.CertificateName)],
                    jwtAppSettingOptions[nameof(JwtIssuerOptions.CertificatePassword)]
                )
            );

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.CertificateName = jwtAppSettingOptions[nameof(JwtIssuerOptions.CertificateName)];
                options.CertificatePassword = jwtAppSettingOptions[nameof(JwtIssuerOptions.CertificatePassword)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.RsaSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };


            services
                .AddAuthentication(o =>
                {
                    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                    o.TokenValidationParameters = tokenValidationParameters;
                    o.SaveToken = true;
                    o.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = async context =>
                        {
                            
                        },
                        //OnTokenValidated = async ctx =>
                        //{
                        //    //var db = ctx.HttpContext.RequestServices.GetRequiredService<AuthorizationDbContext>();

                        //    var claims = new List<Claim>
                        //    {
                        //        new Claim("CustomExtraClaim", "1")
                        //    };
                        //    var appIdentity = new ClaimsIdentity(claims);

                        //    ctx.Principal.AddIdentity(appIdentity);
                        //}
                    };
                });

            return services;
        }

        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc(o =>
            {
                //Let's make sure ANY api call should be Authenticated unless otherwise specified by [AllowAnonymous]
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                o.Filters.Add(new AuthorizeFilter(policy));
                o.Conventions.Add(new AddAuthorizeFiltersControllerConvention());
            });

            return services;
        }

        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(o =>
            {
                o.AddPolicy("AccessThisServicePolicy", b =>
                {
                    b.RequireAuthenticatedUser();
                    b.RequireClaim(ClaimTypes.Role, configuration.GetValue<string>(Constants.Configuration.ServiceName));
                    b.AuthenticationSchemes = new List<string> { JwtBearerDefaults.AuthenticationScheme };
                });

                o.AddPolicy("PolicyUserTypeA", policy => policy.RequireClaim(ClaimTypes.Role, "UserTypeA"));
                o.AddPolicy("PolicyUserTypeB", policy => policy.RequireClaim(ClaimTypes.Role, "UserTypeB"));
            });

            return services;
        }
    }

    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string CertificateName { get; set; }
        public string CertificatePassword { get; set; }
        public DateTime IssuedAt => DateTime.UtcNow;
        public DateTime Expiration => IssuedAt.Add(ValidFor);
        public DateTime NotBefore => DateTime.UtcNow;
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(30);
        public SigningCredentials SigningCredentials { get; set; }

        public Func<Task<string>> JtiGenerator =>
            () => Task.FromResult(Guid.NewGuid().ToString());

        /// <summary>
        /// Convert to Unix format
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        public static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() -
                                 new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
    }

    public class AddAuthorizeFiltersControllerConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            controller.Filters.Add(new AuthorizeFilter("AccessThisServicePolicy"));
        }
    }
}