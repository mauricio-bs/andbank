using System.Diagnostics;
using System.Net.Http.Headers;
using ApiConsole.Constants;
using ApiConsole.Model;
using ApiConsole.Repository;
using Newtonsoft.Json;

namespace ApiConsole.Service;

public class SyncData(IPositionRepository repository)
{
    private readonly IPositionRepository _repository = repository;
    static readonly HttpClient _client = new();
    static readonly ExternalApiConstants _contsants = new();

    private int totalRead = 0;

    private int totalSaved = 0;

    public async Task GetPositions()
    {
        var sw = new Stopwatch();
        sw.Start();

        // Set base url
        _client.BaseAddress = new Uri(_contsants.ExternalApiUrl);
        // Set headers
        _client.DefaultRequestHeaders.Add(
            _contsants.ExternalApiAuthHeader,
            _contsants.ExternalApiKey
        );
        _client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json")
        );

        // Make request
        Stream response = await _client.GetStreamAsync("/candidate/positions");
        Console.WriteLine($"Data fetched in, start processing...");
        await ProcessData(response);

        sw.Stop();
        Console.WriteLine();
        Console.WriteLine($"Finished! Data processed in {sw.Elapsed}");
        Console.WriteLine($"Memory: {Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024} mb");
    }

    private async Task ProcessData(Stream streamData)
    {
        const int batchSize = 1000;
        List<Position> buffer = [];

        var serializer = new JsonSerializer();
        using var timer = new Timer((_) => GenerateLog(), null, 1000, 1000 * 3);

        // Read stream chunks
        using JsonTextReader reader = new(new StreamReader(streamData));
        while (reader.Read())
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                // Get each object
                var data = serializer.Deserialize<Position>(reader);

                data.Id = $"{data.PositionId}{data.Date:o}";
                buffer.Add(data);

                // Logs
                totalRead += 1;
            }

            // Verify buffer size
            if (buffer.Count >= batchSize)
            {
                await _repository.BulkSave(buffer);
                totalSaved += buffer.Count();
                buffer.Clear();
            }
        }

        // Handle remaining buffer if have
        if (buffer.Count > 0)
        {
            await _repository.BulkSave(buffer);
            totalSaved += buffer.Count();
        }
    }

    private void GenerateLog()
    {
        Console.Clear();
        Console.WriteLine($"{totalRead} registries red");
        Console.WriteLine($"{totalSaved} saved registries");
    }
}
