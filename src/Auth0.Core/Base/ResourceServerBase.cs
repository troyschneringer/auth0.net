using Newtonsoft.Json;

namespace Auth0.Core
{
    /// <summary>
    /// Base class for resource servers.
    /// </summary>
    public abstract class ResourceServerBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets a list of scopes associated with the resource server.
        /// </summary>
        [JsonProperty("scopes")]
        public Scope[] Scopes { get; set; }

        /// <summary>
        /// Gets or sets the signing algorithm.
        /// </summary>
        [JsonProperty("signing_alg")]
        public string SigningAlgorithm { get; set; }

        /// <summary>
        /// Gets or sets the signing secret.
        /// </summary>
        [JsonProperty("signing_secret")]
        public string SigningSecret { get; set; }

        /// <summary>
        /// Gets or sets the token lifetime (in seconds).
        /// </summary>
        [JsonProperty("token_lifetime")]
        public int? TokenLifetime { get; set; }
    }
}