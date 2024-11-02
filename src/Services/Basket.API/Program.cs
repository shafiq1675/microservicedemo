using Basket.API.GRPCServices;
using Basket.API.Repositories;
using Discount.GRPC.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(option => option.Address = new Uri(builder.Configuration.GetValue<string>("GRPCSettings:GRPCUrl")));
builder.Services.AddScoped<DiscountGRPCService>();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetConnectionString("BasketDB");
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
