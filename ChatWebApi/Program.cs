using FluentValidation;
using ChatWebApi.Context;
using ChatWebApi.Contracts.Member;
using ChatWebApi.Contracts.Message;
using ChatWebApi.Repositories.Member;
using ChatWebApi.Repositories.Message;
using ChatWebApi.Services.Member;
using ChatWebApi.Services.Message;
using ChatWebApi.Services.TokenGenerator;
using ChatWebApi.Validators.Member;
using ChatWebApi.Validators.Message;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => 
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddScoped<IMemberService, MemberService>();

builder.Services.AddScoped<IMemberRepository, MemberRepository>();

builder.Services.AddScoped<IValidator<RegisterMemberRequest>, RegisterMemberRequestValidator>();

builder.Services.AddScoped<IValidator<LoginMemberRequest>, LoginMemberRequestValidator>();

builder.Services.AddScoped<IValidator<GetMemberBySizeRequest>, GetMemberBySizeRequestValidator>();

builder.Services.AddScoped<IMessageRepository, MessageRepository>();

builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddScoped<IValidator<AddMessageRequest>, AddMessageRequestValidator>();

builder.Services.AddScoped<IValidator<GetMessageBySizeRequest>, GetMessageBySizeRequestValidator>();

builder.Services.AddScoped<IChatWebApiService, ChatWebApiService>();

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("AppSettings:Key").Value!))
    };

});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<ChatContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalDb"));
    options.UseLazyLoadingProxies();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
