

using BFCD.Server.Domain;

namespace BFCD.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register the CustomerRepository as a dependency
            builder.Services.AddSingleton<ICustomerRepository, InMemoryCustomerRep>();

            // Register the SavingsAccountRepository as a dependency
            builder.Services.AddSingleton<ISavingsAccountRepository, InMemorySavingsAccountRep>();

            // Register the TransactionRepository as a dependency
            builder.Services.AddSingleton<ITransactionRepository, InMemoryTransactionRep>();

            builder.Services.AddSingleton<List<SavingsAccount>>();

            var app = builder.Build();

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
        }
    }
}
