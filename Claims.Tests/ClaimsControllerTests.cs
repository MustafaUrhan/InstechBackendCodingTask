using Claims.Domain.Entities;
using Claims.Domain.Repositories.Claims;
using Claims.Domain.Repositories.Covers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Claims.Tests;

public class ClaimsControllerTests
{

    [Fact]
    public async Task Get_Claims()
    {
        var application = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
        var _client = application.CreateClient();

        var response = await _client.GetAsync("/api/v1/claims");

        response.EnsureSuccessStatusCode();

        //TODO: Apart from ensuring 200 OK being returned, what else can be asserted?
        var claims = await response.Content.ReadFromJsonAsync<List<ClaimResponseItem>>();
        Assert.NotNull(claims);
        Assert.NotEmpty(claims);

        foreach (var claim in claims)
        {
            Assert.False(string.IsNullOrEmpty(claim.ClaimId));
            Assert.False(string.IsNullOrEmpty(claim.CoverId));
            Assert.False(string.IsNullOrEmpty(claim.Name));
            Assert.InRange(claim.DamageCost, 0, 100000);
        }
    }

    [Fact]
    public async Task Create_Claim()
    {
        var application = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
        var _client = application.CreateClient();
        var createClaimRequest = new CreateClaimRequest
        {
            CoverId = "6ae89ee5-3550-409e-fe5a-08dce219470b",
            Created = "2024-09-02T10:00:00Z",
            Name = "Fire Damage",
            Type = 2,
            DamageCost = 5000
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/claims", createClaimRequest);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var createdClaim = await response.Content.ReadFromJsonAsync<CreateClaimResponse>();
        Assert.NotNull(createdClaim);
        Assert.Equal(createClaimRequest.CoverId, createdClaim.CoverId);
        Assert.Equal(createClaimRequest.Name, createdClaim.Name);
        Assert.Equal(createClaimRequest.Type, createdClaim.Type);
        Assert.Equal(createClaimRequest.DamageCost, createdClaim.DamageCost);
    }

    [Fact]
    public async Task Create_Claim_ShouldFail_WhenDamageCostExceedsLimit()
    {
        var application = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
        var _client = application.CreateClient();
        // Arrange
        var createClaimRequest = new CreateClaimRequest
        {
            CoverId = "6ae89ee5-3550-409e-fe5a-08dce219470b",
            Created = "2024-09-02T10:00:00Z",
            Name = "Fire Damage",
            Type = 2,
            DamageCost = 150000
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/claims", createClaimRequest);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var errorMessage = await response.Content.ReadAsStringAsync();
        Assert.Contains("Damage cost cannot exceed 100000", errorMessage);
    }

    [Fact]
    public async Task Create_Claim_ShouldFail_WhenClaimDateNotWithinCoverPeriod()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(ICoverQueryRepository));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    var mockCoverQueryRepository = Substitute.For<ICoverQueryRepository>();
                    mockCoverQueryRepository
                    .GetSingleAsync(Arg.Any<Expression<Func<Cover, bool>>>(),
                                    null, null, null, true, false, false, false, CancellationToken.None)
                    .Returns(callInfo =>
                    {
                        var predicate = callInfo.Arg<Expression<Func<Cover, bool>>>();
                        var cover = new Cover
                        {
                            Id = Guid.Parse("6ae89ee5-3550-409e-fe5a-08dce2194700"),
                            StartDate = DateTime.Parse("2024-01-01T00:00:00Z"),
                            EndDate = DateTime.Parse("2024-12-31T23:59:59Z")
                        };

                        return predicate.Compile()(cover) ? cover : null;
                    });

                    services.AddScoped(_ => mockCoverQueryRepository);
                });
            });

        var _client = application.CreateClient();

        // Arrange
        var createClaimRequest = new CreateClaimRequest
        {
            CoverId = "6ae89ee5-3550-409e-fe5a-08dce2194700",
            Created = "2023-01-01T10:00:00Z", // Outside the cover period
            Name = "Fire Damage",
            Type = 2,
            DamageCost = 5000
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/claims", createClaimRequest);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var errorMessage = await response.Content.ReadAsStringAsync();
        Assert.Contains("Claim date is not within the cover period", errorMessage);
    }

    [Fact]
    public async Task Create_Claim_ShouldFail_WhenCoverIdNotFound()
    {
        var application = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
        var _client = application.CreateClient();
        // Arrange
        var createClaimRequest = new CreateClaimRequest
        {
            CoverId = "2F6E1B67-38AE-4347-FE56-08DCE2194700",
            Created = "2024-09-02T10:00:00Z",
            Name = "Fire Damage",
            Type = 2,
            DamageCost = 5000
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/claims", createClaimRequest);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        var errorMessage = await response.Content.ReadAsStringAsync();
        Assert.Contains($"Cover with id {createClaimRequest.CoverId} not found", errorMessage);
    }

    [Fact]
    public async Task Delete_Claim()
    {
        var application = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
        var _client = application.CreateClient();

        // Arrange
        var createClaimRequest = new CreateClaimRequest
        {
            CoverId = "6ae89ee5-3550-409e-fe5a-08dce219470b",
            Created = "2024-09-02T10:00:00Z",
            Name = "Fire Damage",
            Type = 2,
            DamageCost = 5000
        };

        var createResponse = await _client.PostAsJsonAsync("/api/v1/claims", createClaimRequest);
        var createdClaim = await createResponse.Content.ReadFromJsonAsync<CreateClaimResponse>();

        // Act
        var response = await _client.DeleteAsync($"/api/v1/claims/{createdClaim.Id}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var deletedClaimIdString = await response.Content.ReadAsStringAsync();
        var deletedClaimId = deletedClaimIdString.Trim('"');
        Assert.Equal(createdClaim.Id, deletedClaimId);
    }

    [Fact]
    public async Task Delete_Claim_ShouldReturnNotFound_WhenClaimDoesNotExist()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(IClaimQueryRepository));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    var mockClaimQueryRepository = Substitute.For<IClaimQueryRepository>();
                    mockClaimQueryRepository
                    .GetSingleAsync(Arg.Any<Expression<Func<Claim, bool>>>(),
                                    null, null, null, true, false, false, false, CancellationToken.None)
                    .Returns(callInfo =>
                    {
                        var predicate = callInfo.Arg<Expression<Func<Claim, bool>>>();
                        return predicate.Compile()(null) ? new Claim() : null;
                    });

                    services.AddScoped(_ => mockClaimQueryRepository);
                });
            });

        var _client = application.CreateClient();

        // Arrange
        var claimId = Guid.NewGuid();

        // Act
        var response = await _client.DeleteAsync($"/api/v1/claims/{claimId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        var errorMessage = await response.Content.ReadAsStringAsync();
        Assert.Contains($"Claim with id {claimId} not found", errorMessage);
    }

}

#region Get_Claims
public class ClaimResponseItem
{
    public string ClaimId { get; set; }
    public string CoverId { get; set; }
    public string Created { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public decimal DamageCost { get; set; }
}
#endregion

#region Create_Claim
public class CreateClaimRequest
{
    public string CoverId { get; set; }
    public string Created { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public decimal DamageCost { get; set; }
}

public class CreateClaimResponse
{
    public string Id { get; set; }
    public string CoverId { get; set; }
    public string Created { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public decimal DamageCost { get; set; }
}
#endregion

