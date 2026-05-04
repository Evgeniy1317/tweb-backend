using Microsoft.EntityFrameworkCore;
using SmashHub.BusinessLogic;
using SmashHub.BusinessLogic.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddDbContext<SmashHubContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IUser, UserBL>();
builder.Services.AddScoped<IProduct, ProductBL>();
builder.Services.AddScoped<ICourt, CourtBL>();
builder.Services.AddScoped<ITournament, TournamentBL>();
builder.Services.AddScoped<IStringingOrder, StringingOrderBL>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("FrontendPolicy");
app.UseAuthorization();
app.MapControllers();
app.Run();