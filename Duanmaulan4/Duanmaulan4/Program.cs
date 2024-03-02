using Duanmaulan4.DataView.Authentication;
using Duanmaulan4.Helpers;
using Duanmaulan4.Models;
using Duanmaulan4.Services;
using Duanmaulan4.Services.PhanQuyen;
using Duanmaulan4.Services.QuanLyKhoaDaoTao;
using Duanmaulan4.Services.QuanLyLoaiDiem;
/*using Duanmaulan4.Services;*/
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Book API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});






builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DuanmauConnection"));
});



// Life cycle DI: AddSingleton(), AddTransient(), AddScoped()
builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddScoped<IStudentServices, StudentServices>();
builder.Services.AddScoped<ILecturerServices, LecturerServices>();
builder.Services.AddScoped<INienKhoaService, NienKhoaService>();
builder.Services.AddScoped<ITobomonService, TobomonService>();
builder.Services.AddScoped<IMonhocService, MonhocService>();
builder.Services.AddScoped<ILopService, LopService>();
builder.Services.AddScoped<ILoaiDiemServices, LoaiDiemServices>();
builder.Services.AddScoped<ILichNghiService, LichNghiService>();
builder.Services.AddScoped<IDoanhThuService, DoanhThuService>();
builder.Services.AddScoped<ILuongNhanVienService, LuongNhanVienService>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<IKhoaKhoiService, KhoaKhoiService>();


/*builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ViewClassList", policy =>
    {
        policy.RequireClaim("Authorization", SD.Claim_ViewClassList);
    });
});
*/


/*builder.Services.AddAuthorization(options =>
{
    var authorizationService = builder.Services.BuildServiceProvider().GetService<IAuthorizationService>();

    foreach (var roleName in SD.GetRoles())
    {
        options.AddPolicy(roleName, policy =>
        {
            policy.RequireRole(roleName);

            // Điều chỉnh phần quyền linh hoạt tại đây
            var claims = authorizationService.GetClaimsForRoleAsync(roleName).Result;
            foreach (var claim in claims)
            {
                policy.RequireClaim(claim);
            }
        });
    }
});
*/



builder.Services.AddOptions();
var mailSettings = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<MailSettings>(mailSettings);



builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.SaveToken = true;
    options.RequireHttpsMetadata = builder.Environment.IsProduction();

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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

// Seed data
/*using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();

    // Apply database migrations
    dbContext.Database.Migrate();

    // Seed data
    dbContext.SeedData();
}*/
app.Run();
