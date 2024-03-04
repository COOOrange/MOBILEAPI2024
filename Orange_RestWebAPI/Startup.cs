using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Text;
using Orange_RestWebAPI.Connections;
using Orange_RestWebAPI.Model;
using static Orange_RestWebAPI.Model.PredefinedClasses.Response;
using System.Linq;

namespace Orange_RestWebAPI
{
    public class Startup
    {
        MyClass<LoginModel> singleResponse = new MyClass<LoginModel>();
        LogHelper logHelper = new LogHelper();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.



        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                //services.Configure<EmailSetting>(Configuration.GetSection("MailSettings"));
                services.AddHttpContextAccessor();
                //services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
                services.AddControllers();
                //services.AddHttpClient();
                // JWT Token Generation from Server Side.  
                services.AddMvc();
                // Enable Swagger   
                services.AddSwaggerGen(swagger =>
                {
                //This is to generate the Default UI of Swagger Documentation  
                swagger.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Orange Payroll Mobile API",
                        Description = "ASP.NET Core 5.0 Web API"
                    });
                // To Enable authorization using Swagger (JWT)  
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    });
                    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                    });
                });
                services.AddSession(options =>
                {
                    options.IdleTimeout = System.TimeSpan.FromMinutes(120);
                });

                services.AddAuthentication(option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                    //IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    //ValidIssuer = SiteKeys.WebSiteDomain,
                    ValidateAudience = true,
                    //ValidAudience = SiteKeys.WebSiteDomain,
                    RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ClockSkew = System.TimeSpan.Zero,
                    //ValidateIssuer = true,
                    //ValidateAudience = true,
                    //ValidateLifetime = false,
                    //ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) //Configuration["JwtToken:SecretKey"]  
                };
                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            // Call this to skip the default logic and avoid using the default response
                            context.HandleResponse();

                            // Write to the response in any way you wish
                            context.Response.StatusCode = 401;
                            context.Response.Headers.Append("You are not authorized", "");
                            context.Response.ContentType = "application/json";
                          await context.Response.WriteAsync("{\"Status Code\": \"401\", \"message\": \"You are not authorized \"}");
                           // await context.Response.WriteAsync("You are not authorized! (or some other custom message)");
                        }
                    };
                });
            }
            catch (System.Exception ex)
            {
                logHelper.Error("StartUp : " + ex.Message);
                throw;
            }
            }
        //public void ConfigureServices(IServiceCollection services)
        //{

        //    services.AddControllers();
        //    services.AddSwaggerGen(c =>
        //    {
        //        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Orange_RestWebAPI", Version = "v1" });
        //    });
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orange_RestWebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            //app.UseCookiePolicy();
            //app.UseSession();
            //app.Use(async (context, next) =>
            //{


            //    var JWToken = context.Session.GetString("JWToken");

            //    if (!string.IsNullOrEmpty(JWToken))
            //    {
            //        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
            //        //context.Response.StatusCode = 404;
            //    }
            //    //var isLoginController = context.Request.Path.StartsWithSegments("/Api/Login/Login");

            //    //if (isLoginController)
            //    //{
            //    //    await next();
            //    //    return;
            //    //}
            //    //// Check if the user is authenticated after login
            //    //var isAuthenticated = context.User.Identity.IsAuthenticated;

            //    //if (isAuthenticated)
            //    //{
            //    //    // User is authenticated after login, retrieve JWT token from session
            //    //    var JWToken = context.Session.GetString("JWToken");

            //    //    if (!string.IsNullOrEmpty(JWToken))
            //    //    {
            //    //        // If JWT token is available in session, add it to the request headers
            //    //        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
            //    //    }
            //    //}

            //    await next();
            //});
            //app.Use(async (context, next) =>
            //{
            //    var isLoginController = context.Request.Path.StartsWithSegments("/Api/Login/Login");

            //    if (isLoginController)
            //    {
            //        // If the request is for the login controller, proceed without further checks
            //        await next();
            //        return;
            //    }

            //    // Check if the user is authenticated after login
            //    var isAuthenticated = context.User.Identity.IsAuthenticated;

            //    if (isAuthenticated)
            //    {
            //        // User is authenticated after login, retrieve JWT token from session
            //        var JWToken = context.Session.GetString("JWToken");

            //        if (!string.IsNullOrEmpty(JWToken))
            //        {
            //            // If JWT token is available in session, add it to the request headers
            //            context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
            //        }
            //    }

            //    await next();
            //});
            //last commented 1
            //app.Use(async (context, next) =>
            //{
            //    // Check if the request is for the login controller
            //    var isLoginController = context.Request.Path.StartsWithSegments("/Api/Login/Login");

            //    if (isLoginController)
            //    {
            //        // If the request is for the login controller, proceed without further checks
            //        await next();
            //        return;
            //    }

            //    // Check if the user is authenticated after login
            //    var isAuthenticated = context.User.Identity.IsAuthenticated;

            //    if (isAuthenticated)
            //    {
            //        // User is authenticated after login, retrieve JWT token from session
            //        var JWToken = context.Session.GetString("JWToken");

            //        if (!string.IsNullOrEmpty(JWToken))
            //        {
            //            // If JWT token is available in session, add it to the request headers
            //            context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
            //        }
            //    }
            //    else
            //    {
            //        // If the user is not authenticated, return unauthorized access error
            //        context.Response.StatusCode = 401; // Unauthorized status code
            //        context.Response.ContentType = "application/json";
            //        await context.Response.WriteAsync("{\"error\": \"Unauthorized\", \"message\": \"User is not authenticated\"}");
            //        return; // Short-circuit the pipeline, don't call the next middleware
            //    }

            //    // Check if the session has expired
            //    if (context.Session.IsAvailable && !context.Session.Keys.Any())
            //    {
            //        // Session has expired, return session expired error
            //        context.Response.StatusCode = 401; // Unauthorized status code
            //        context.Response.ContentType = "application/json";
            //        await context.Response.WriteAsync("{\"error\": \"Unauthorized\", \"message\": \"Session expired\"}");
            //        return; // Short-circuit the pipeline, don't call the next middleware
            //    }

            //    await next();
            //});

            //last commented
            //app.Use(async (context, next) =>
            //{
            //    // Check if the request is for the login controller
            //    var isLoginController = context.Request.Path.StartsWithSegments("/Api/Login/Login");

            //    if (!isLoginController)
            //    {
            //        var JWToken = context.Session.GetString("JWToken");

            //        if (!string.IsNullOrEmpty(JWToken))
            //        {
            //            // If JWT token is available in session, add it to the request headers
            //            context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
            //        }
            //        else
            //        {
            //            // Check if the user is authenticated
            //            var isAuthenticated = context.User.Identity.IsAuthenticated;

            //            if (!isAuthenticated)
            //            {
            //                // If the user is not authenticated, return unauthorized access error
            //                context.Response.StatusCode = 401; // Unauthorized status code
            //                context.Response.ContentType = "application/json";
            //                await context.Response.WriteAsync("{\"error\": \"Unauthorized\", \"message\": \"User is not authenticated\"}");
            //                return; // Short-circuit the pipeline, don't call the next middleware
            //            }
            //        }
            //    }
            //    else
            //    {
            //        // Check if the user is authenticated after login
            //        var isAuthenticated = context.User.Identity.IsAuthenticated;

            //        if (isAuthenticated)
            //        {
            //            // User is authenticated after login, retrieve JWT token from session
            //            var JWToken = context.Session.GetString("JWToken");

            //            if (!string.IsNullOrEmpty(JWToken))
            //            {
            //                // If JWT token is available in session, add it to the request headers
            //                context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
            //            }
            //        }
            //    }

            //    await next();
            //});





            //Last Modified 27/02/2024
            //app.Use(async (context, next) =>
            //{
            //var isLoginController = context.Request.Path.StartsWithSegments("/Api/Login/Login");

            //    if (!isLoginController)
            //    {
            //        var JWToken = context.Session.GetString("JWToken");

            //        if (!string.IsNullOrEmpty(JWToken))
            //        {
            //            // If JWT token is available in session, add it to the request headers
            //            context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
            //        }
            //        else
            //        {
            //            //var isLoginController = context.Request.Path.StartsWithSegments("/Api/Login/Login");

            //            //    if (!isLoginController)
            //            //   {
            //            // Check if the user is authenticated
            //            var isAuthenticated = context.User.Identity.IsAuthenticated;

            //            if (!isAuthenticated)
            //            {
            //                // If the user is not authenticated, return unauthorized access error
            //                context.Response.StatusCode = 401; // Unauthorized status code
            //                context.Response.ContentType = "application/json";
            //                await context.Response.WriteAsync("{\"error\": \"Unauthorized\", \"message\": \"User is not authenticated\"}");
            //                return; // Short-circuit the pipeline, don't call the next middleware
            //            }
            //        }
            //    }
            //    await next();
            //});
            //last last modified
            //app.Use(async (context, next) =>
            //{
            //    // Check if the request is for the login controller
            //    var isLoginController = context.Request.Path.StartsWithSegments("/Api/Login/Login");

            //    if (!isLoginController)
            //    {
            //        // var JWToken = context.Session.GetString("JWToken");
            //        var JWToken = context.Session.GetString("JWToken");
            //        if (!string.IsNullOrEmpty(JWToken))
            //        {
            //            // If JWT token is available in session, add it to the request headers
            //            context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
            //        }
            //        else
            //        {
            //            // If JWT token is not available, check if the session has expired
            //            //if (context.Session.IsAvailable && !context.Session.Keys.Any())
            //            //{
            //            //    // Session has expired, return session expired error
            //            //    context.Response.StatusCode = 401; // Unauthorized status code
            //            //    context.Response.ContentType = "application/json";
            //            //    await context.Response.WriteAsync("{\"error\": \"Unauthorized\", \"message\": \"Session expired\"}");
            //            //    return; // Short-circuit the pipeline, don't call the next middleware
            //            //}

            //            // No JWT token and session not expired, return unauthorized access error
            //            context.Response.StatusCode = 401; // Unauthorized status code
            //            context.Response.ContentType = "application/json";
            //            await context.Response.WriteAsync("{\"error\": \"Unauthorized\", \"message\": \"Authorization token not provided\"}");
            //            return; // Short-circuit the pipeline, don't call the next middleware
            //        }
            //    }

            //    await next();
            //});
            //app.Use(async (context, next) =>
            //{

            //    var JWToken = context.Session.GetString("JWToken");

            //    if (!string.IsNullOrEmpty(JWToken))
            //    {
            //        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
            //        //context.Response.StatusCode = 404;
            //    }

            //    else
            //    {
            //        // Check if session has expired
            //     //   if (context.Session.IsAvailable && !context.Session.Keys.Any())
            //      //  {
            //          //  context.Response.StatusCode = 401; // Unauthorized status code

            //            // Set response content
            //           // context.Response.ContentType = "application/json";
            //           // await context.Response.WriteAsync("{\"error\": \"Unauthorized\", \"message\": \"Session expired\",\"StatusCode\": \"404\"}");
            //           // return; // Short-circuit the pipeline, don't call the next middleware
            //      //  }

            //        // No JWT token and session not expired
            //        //context.Response.StatusCode = 401; // Unauthorized status code

            //        // Set response content
            //        context.Response.ContentType = "application/json";
            //        await context.Response.WriteAsync("{\"error\": \"Unauthorized\", \"message\": \"Authorization token not provided\",\"StatusCode\": \"404\"}");
            //        //await context.Response.WriteAsync("{\"error\": \"Unauthorized\", \"message\": \"Authorization token not provided\"}");
            //        return; // Short-circuit the pipeline, don't call the next middleware
            //    }


















            //    await next();
            //});
            app.UseAuthentication();
           app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseSession();
            //app.Use(async (context, next) =>
            //{


            //    var JWToken = context.Session.GetString("JWToken");

            //    if (!string.IsNullOrEmpty(JWToken))
            //    {
            //        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
            //        //context.Response.StatusCode = 404;
            //    }
            //    //var isLoginController = context.Request.Path.StartsWithSegments("/Api/Login/Login");

            //    //if (isLoginController)
            //    //{
            //    //    await next();
            //    //    return;
            //    //}
            //    //// Check if the user is authenticated after login
            //    //var isAuthenticated = context.User.Identity.IsAuthenticated;

            //    //if (isAuthenticated)
            //    //{
            //    //    // User is authenticated after login, retrieve JWT token from session
            //    //    var JWToken = context.Session.GetString("JWToken");

            //    //    if (!string.IsNullOrEmpty(JWToken))
            //    //    {
            //    //        // If JWT token is available in session, add it to the request headers
            //    //        context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
            //    //    }
            //    //}

            //    await next();
            //});
            //app.Use(async (context, next) =>
            //{
            //    // Check if the user is authenticated
            //    var isAuthenticated = context.User.Identity.IsAuthenticated;

            //    if (!isAuthenticated)
            //    {
            //        // If the user is not authenticated, return unauthorized access error
            //        context.Response.StatusCode = 401; // Unauthorized status code
            //        context.Response.ContentType = "application/json";
            //        await context.Response.WriteAsync("{\"error\": \"Unauthorized\", \"message\": \"User is not authenticated\"}");
            //        return; // Short-circuit the pipeline, don't call the next middleware
            //    }

            //    // Check if the session has expired
            //    if (context.Session.IsAvailable && !context.Session.Keys.Any())
            //    {
            //        // Session has expired, return session expired error
            //        context.Response.StatusCode = 401; // Unauthorized status code
            //        context.Response.ContentType = "application/json";
            //        await context.Response.WriteAsync("{\"error\": \"Unauthorized\", \"message\": \"Session expired\"}");
            //        return; // Short-circuit the pipeline, don't call the next middleware
            //    }

            //    await next();
            //});
            //app.Use(async (context, next) =>
            //{
            //    // Check if the session has expired
            //    if (context.Session.IsAvailable && !context.Session.Keys.Any())
            //    {
            //        // Session has expired, return session expired error
            //        context.Response.StatusCode = 401; // Unauthorized status code
            //        context.Response.ContentType = "application/json";
            //        await context.Response.WriteAsync("{\"error\": \"Unauthorized\", \"message\": \"Session expired\"}");
            //        return; // Short-circuit the pipeline, don't call the next middleware
            //    }

            //    await next();
            //});
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
