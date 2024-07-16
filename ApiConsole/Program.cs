using ApiConsole.Data;
using ApiConsole.Repository.Implementation;
using ApiConsole.Service;

Console.WriteLine("Starting data migration...");

var context = new AppDbContext();
var migrations = new Migrations(context);
var repository = new PositionRepository(context);
var service = new SyncData(repository);

// Execute ef migrations
await migrations.ExecuteMigrations();

// Execute script
await service.GetPositions();
