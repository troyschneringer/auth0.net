using Newtonsoft.Json;

namespace Auth0.Core
{
    /// <summary>
    /// Represents a resource server (api) in Auth0.
    /// </summary>
    public class ResourceServer : ResourceServerBase
    {
        /// <summary>
        /// The id of the resource server.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}