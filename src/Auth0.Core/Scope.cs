using Newtonsoft.Json;

namespace Auth0.Core
{
    /// <summary>
    /// Represents a resource server scope.
    /// </summary>
    public class Scope
    {
        /// <summary>
        /// Gets or set the description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or set the value.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
