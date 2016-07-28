using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Auth0.Core;
using Auth0.Core.Exceptions;
using Auth0.ManagementApi.Models;
using Auth0.Tests.Shared;
using FluentAssertions;
using NUnit.Framework;

namespace Auth0.ManagementApi.IntegrationTests
{
    [TestFixture]
    public class ResourceServerTests : TestBase
    {
        [Test]
        public async Task Test_resource_server_crud_sequence()
        {
            var scopes = new
            {
                resource_servers = new
                {
                    actions = new string[] { "read", "create", "delete", "update" }
                }
            };
            string token = GenerateToken(scopes);

            var apiClient = new ManagementApiClient(token, new Uri(GetVariable("AUTH0_MANAGEMENT_API_URL")));

            // Get all resource servers
            var resourceServersBefore = await apiClient.Clients.GetAllAsync();

            // Add a new resource server
            var newResourceServer = await CreateAndAssertAsync(apiClient);

            // Get all resource servers and ensure we have one more
            await GetAllAndAssertAsync(apiClient, resourceServersBefore, newResourceServer.Id);

            // Update the resource server
            var updatedResourceServe = await UpdateAndAssertAsync(apiClient, newResourceServer.Id);

            // Get a single resource server
            var resourceServer = await GetAndAssertAsync(apiClient, newResourceServer.Id, updatedResourceServe.Name);

            // Delete the resource server
            await DeleteAndAssertASync(apiClient, resourceServer.Id);
        }
        
        private async Task<ResourceServer> CreateAndAssertAsync(IManagementApiClient apiClient)
        {
            var request = new ResourceServerCreateRequest
            {
                Name = $"Integration testing {Guid.NewGuid().ToString("N")}",
                Identifier = "https://integration-test.com/",
                Scopes = new[]
                {
                    new Scope { Value = "read:resource", Description = "Read the resource" }
                },
                SigningAlgorithm = "RS256",
                TokenLifetime = 72000
            };

            var response = await apiClient.ResourceServers.CreateAsync(request);

            response.Should().NotBeNull();
            response.Name.Should().Be(request.Name);
            response.Identifier.Should().Be(request.Identifier);
            response.SigningAlgorithm.Should().Be(request.SigningAlgorithm);
            response.TokenLifetime.Should().Be(request.TokenLifetime);
            response.Scopes.Should()
                .HaveSameCount(request.Scopes)
                .And
                .OnlyContain(s => s.Value == "read:resource" && s.Description == "Read the resource");

            return response;
        }

        private async Task DeleteAndAssertASync(IManagementApiClient apiClient, string id)
        {
            await apiClient.ResourceServers.DeleteAsync(id);
            Func<Task> getFunc = async () => await apiClient.ResourceServers.GetAsync(id);
            getFunc
                .ShouldThrow<ApiException>()
                .And
                .ApiError.ErrorCode.Should().Be("inexistent_resource_server");
        }

        private static async Task GetAllAndAssertAsync(IManagementApiClient apiClient, IList<Client> before, string id)
        {
            var after = await apiClient.ResourceServers.GetAllAsync();
            after.Count.Should().Be(before.Count + 1);
            after.Should().Contain(s => s.Id == id);
        }

        private async Task<ResourceServer> GetAndAssertAsync(IManagementApiClient apiClient, string id, string expectedName)
        {
            var resourceServer = await apiClient.ResourceServers.GetAsync(id);

            resourceServer.Should().NotBeNull();
            resourceServer.Name.Should().Be(expectedName);

            return resourceServer;
        }

        private async Task<ResourceServer> UpdateAndAssertAsync(IManagementApiClient apiClient, string id)
        {
            var request = new ResourceServerUpdateRequest
            {
                Name = $"Integration testing {Guid.NewGuid().ToString("N")}",
                Scopes = new[]
                {
                    new Scope { Value = "create:resource", Description = "Create the resource" }
                }
            };

            var response = await apiClient.ResourceServers.UpdateAsync(id, request);

            response.Should().NotBeNull();
            response.Name.Should().Be(request.Name);
            response.Scopes.Should()
                .HaveSameCount(request.Scopes)
                .And
                .OnlyContain(s => s.Value == "create:resource" && s.Description == "Create the resource");

            return response;
        }
    }
}
