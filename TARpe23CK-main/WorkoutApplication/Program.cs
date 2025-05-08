var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(optionsBuilderConfigurationExtentions =>
 OptionsBuilderConfigurationExtensions.UseInMemoryDatabase ("WorkoutDb"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope =((IApplicationBuilder)app).ApplicationServices.GetRequiredServices<IServicesScopeFactory>().CreateScope())
using (var context = scope.ServicesProvider.GetService<DataContext>()) context?.Databas.EnsureCreated();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
