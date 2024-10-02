using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Claims.Tests;

public class CoverControllerTests
{
    [Fact]
    public async Task Get_Covers()
    {

        var application = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
        var _client = application.CreateClient();

        var response = await _client.GetAsync("/api/v1/covers");
        response.EnsureSuccessStatusCode();

        var covers = await response.Content.ReadFromJsonAsync<IEnumerable<CoverResponseItem>>();
        Assert.NotNull(covers);
        Assert.NotEmpty(covers);

        foreach (var cover in covers)
        {
            Assert.NotEqual(Guid.Empty, cover.Id);
            Assert.NotEqual(DateTime.MinValue, cover.StartDate);
            Assert.NotEqual(DateTime.MinValue, cover.EndDate);
            Assert.InRange(cover.Type, 0, 5);
            Assert.NotEqual(0, cover.Premium);
            Assert.NotEqual(DateTime.MinValue, cover.CreatedAt);
        }
    }

    [Fact]
    public async Task Create_Cover()
    {
        var application = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
        var _client = application.CreateClient();

        var request = new CreateCoverRequest
        {
            StartDate = DateTime.Now.AddMonths(3),
            EndDate = DateTime.Now.AddYears(1),
            Type = 1
        };

        var response = await _client.PostAsJsonAsync("/api/v1/covers", request);
        response.EnsureSuccessStatusCode();

        var cover = await response.Content.ReadFromJsonAsync<CoverResponseItem>();
        Assert.NotNull(cover);
        Assert.NotEqual(Guid.Empty, cover.Id);
        Assert.Equal(request.StartDate, cover.StartDate);
        Assert.Equal(request.EndDate, cover.EndDate);
        Assert.Equal(request.Type, cover.Type);
    }

    [Fact]
    public async Task Create_Cover_Invalid_StartDate()
    {
        var application = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
        var _client = application.CreateClient();

        var request = new CreateCoverRequest
        {
            StartDate = DateTime.Now.AddMonths(-3),
            EndDate = DateTime.Now.AddYears(1),
            Type = 1
        };

        var response = await _client.PostAsJsonAsync("/api/v1/covers", request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Create_Cover_Invalid_EndDate()
    {
        var application = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
        var _client = application.CreateClient();

        var request = new CreateCoverRequest
        {
            StartDate = DateTime.Now.AddMonths(3),
            EndDate = DateTime.Now.AddMonths(1),
            Type = 1
        };

        var response = await _client.PostAsJsonAsync("/api/v1/covers", request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Create_Cover_Invalid_Insurance_Period()
    {
        var application = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
        var _client = application.CreateClient();

        var request = new CreateCoverRequest
        {
            StartDate = DateTime.Now.AddMonths(3),
            EndDate = DateTime.Now.AddYears(2),
            Type = 1
        };

        var response = await _client.PostAsJsonAsync("/api/v1/covers", request);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

//     public sealed class DeleteCoverCommandHandler : IRequestHandler<DeleteCoverCommand, ErrorOr<Guid>>
// {

//     private readonly ICoverCommandRepository _coverCommandRepository;
//     private readonly ICoverQueryRepository _coverQueryRepository;
//     private readonly IPublisher _publisher;
//     public DeleteCoverCommandHandler(ICoverCommandRepository coverCommandRepository, ICoverQueryRepository coverQueryRepository, IPublisher publisher)
//     {
//         _coverCommandRepository = coverCommandRepository;
//         _coverQueryRepository = coverQueryRepository;
//         _publisher = publisher;
//     }

//     public async Task<ErrorOr<Guid>> Handle(DeleteCoverCommand request, CancellationToken cancellationToken)
//     {
//         var cover = await _coverQueryRepository.GetSingleAsync(predicate: s => s.Id == request.Id,
//                                                                include: s => s.Include(x => x.Claims),
//                                                                cancellationToken: cancellationToken);
//         if (cover is null)
//         {
//             return Errors.Business.ResultNotFound($"Cover with id {request.Id} not found");
//         }


//         cover.IsActive = false;
//         cover.IsDeleted = true;
//         cover.ModifiedAt = DateTime.UtcNow;
//         await _coverCommandRepository.UpdateAsync(cover, cancellationToken);

//         var auditNotification = new CreateOrDeleteCoverNotification(cover.Id.ToString(), "DELETE");
//         await _publisher.Publish(auditNotification);
        
//         return request.Id;
//     }
// }

    [Fact]
    public async Task Delete_Cover()
    {
        var application = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
        var _client = application.CreateClient();

        var request = new CreateCoverRequest
        {
            StartDate = DateTime.Now.AddMonths(3),
            EndDate = DateTime.Now.AddYears(1),
            Type = 1
        };

        var response = await _client.PostAsJsonAsync("/api/v1/covers", request);
        response.EnsureSuccessStatusCode();

        var cover = await response.Content.ReadFromJsonAsync<CoverResponseItem>();
        Assert.NotNull(cover);
        Assert.NotEqual(Guid.Empty, cover.Id);

        var deleteResponse = await _client.DeleteAsync($"/api/v1/covers/{cover.Id}");
        deleteResponse.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Delete_Cover_Not_Found()
    {
        var application = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
        var _client = application.CreateClient();

        var deleteResponse = await _client.DeleteAsync($"/api/v1/covers/{Guid.NewGuid()}");
        Assert.Equal(HttpStatusCode.NotFound, deleteResponse.StatusCode);
    }
}

#region Get_Covers
public class CoverResponseItem
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Type { get; set; }
    public decimal Premium { get; set; }
    public DateTime CreatedAt { get; set; }
}
#endregion

#region Create_Cover
public class CreateCoverRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Type { get; set; }
}
#endregion