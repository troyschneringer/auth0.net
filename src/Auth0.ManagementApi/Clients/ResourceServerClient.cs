using System.Collections.Generic;
using System.Threading.Tasks;
using Auth0.Core;
using Auth0.Core.Http;
using Auth0.ManagementApi.Models;

namespace Auth0.ManagementApi.Clients
{
    /// <summary>
    /// Contains all the methods to call the /resource-server endpoints.
    /// </summary>
    public class ResourceServerClient : ClientBase, IResourceServerClient
    {
        /// <summary>
        /// Creates a new instance of the ResourceServerClient class.
        /// </summary>
        /// <param name="connection">The <see cref="IApiConnection" /> which is used to communicate with the API.</param>
        public ResourceServerClient(IApiConnection connection)
            : base(connection)
        {

        }

        /// <summary>
        ///     Creates a new resource server.
        /// </summary>
        /// <param name="request">
        ///     The <see cref="ResourceServerCreateRequest" /> containing the properties of the resource server to create.
        /// </param>
        /// <returns>The created <see cref="ResourceServer"/></returns>
        public Task<ResourceServer> CreateAsync(ResourceServerCreateRequest request)
        {
            return Connection.PostAsync<ResourceServer>("resource-servers", request, null, null, null, null, null);
        }

        /// <summary>
        ///     Deletes a resource server.
        /// </summary>
        /// <param name="id">The id of the resource server to delete.</param>
        public Task DeleteAsync(string id)
        {
            return Connection.DeleteAsync<object>("resource-servers/{id}", 
                new Dictionary<string, string>
                {
                    {"id", id}
                });
        }

        /// <summary>
        ///     Retrieves a list of all resource servers. Accepts a list of fields to include or exclude.
        /// </summary>
        /// <param name="fields">
        ///     A comma separated list of fields to include or exclude (depending on includeFields) from the
        ///     result, empty to retrieve all fields
        /// </param>
        /// <param name="includeFields">
        ///     true if the fields specified are to be included in the result, false otherwise (defaults to true)
        /// </param>
        /// <returns></returns>
        public Task<IList<ResourceServer>> GetAllAsync(string fields = null, bool includeFields = true)
        {
            return Connection.GetAsync<IList<ResourceServer>>("resource-servers", null,
                new Dictionary<string, string>
                {
                    {"fields", fields},
                    {"include_fields", includeFields.ToString().ToLower()}
                }, null, null);
        }

        /// <summary>
        ///     Gets a resource server.
        /// </summary>
        /// <param name="id">The id of the resource server to retrieve.</param>
        /// <param name="fields">
        ///     A comma separated list of fields to include or exclude (depending on includeFields) from the
        ///     result, empty to retrieve all fields.
        /// </param>
        /// <param name="includeFields">
        ///     true if the fields specified are to be included in the result, false otherwise (defaults to true).
        /// </param>
        /// <returns>The <see cref="ResourceServer" />.</returns>
        public Task<ResourceServer> GetAsync(string id, string fields = null, bool includeFields = true)
        {
            return Connection.GetAsync<ResourceServer>("resource-servers/{id}",
                new Dictionary<string, string>
                {
                    {"id", id}
                },
                new Dictionary<string, string>
                {
                    {"fields", fields},
                    {"include_fields", includeFields.ToString().ToLower()}
                }, null, null);
        }

        /// <summary>
        ///     Updates a resource server.
        /// </summary>
        /// <param name="id">The id of the resource server to update.</param>
        /// <param name="request">A <see cref="ResourceServerUpdateRequest" /> containing the information to update.</param>
        /// <returns>The updated <see cref="ResourceServer" />.</returns>
        public Task<ResourceServer> UpdateAsync(string id, ResourceServerUpdateRequest request)
        {
            return Connection.PatchAsync<ResourceServer>("resource-servers/{id}", request, 
                new Dictionary<string, string>
                {
                    {"id", id}
                });
        }
    }
}
