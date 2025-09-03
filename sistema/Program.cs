using sistema.Repositories;

using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ClienteRepository>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()   // ou .WithOrigins("https://localhost:5002") para restringir
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// (Andre) TODO: capturar excecoes nao tratadas e trata-las usando Resultado<T>

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();