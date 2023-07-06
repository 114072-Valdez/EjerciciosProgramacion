using ClubNauticoDBFirst.Business.SocioBusiness.Commands;
using ClubNauticoDBFirst.Business.SocioBusiness.Queries;
using ClubNauticoDBFirst.Models;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//IMPORTANTE:
builder.Services.AddControllers()
    .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<PostNuevoSocio>());

//IMPORTANTE:
builder.Services.AddDbContext<ClubNauticoDbfirstContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionDatabase")));
builder.Services.AddMediatR(typeof(GetSocioByID).Assembly); //un solo metodo y listo
builder.Services.AddMediatR(typeof(PostNuevoSocio.PostNuevoSocioCommandHandler).Assembly);


//IMPORTANTE:
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
        });
});


var app = builder.Build();


//IMPORTANTE:
AppContext.SetSwitch("Npgsql.EnableLegacyTimesTampBehavior", true);



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//IMPORTANTE
app.UseCors();





app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();