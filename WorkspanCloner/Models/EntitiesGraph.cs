using Newtonsoft.Json;
using System.Collections.Generic;

namespace WorkspanCloner.Models
{
    public class EntitiesGraph
    {
        [JsonProperty(PropertyName = "entities")]
        public List<EntityInfo> Entities;

        [JsonProperty(PropertyName = "links")]
        public List<Link> Links;
    }

    public class EntityInfo
    {
        [JsonProperty(PropertyName = "entity_id")]
        public int EntityId;

        [JsonProperty(PropertyName = "name")]
        public string Name;

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description;
    }

    public class Link
    {
        [JsonProperty(PropertyName = "from")]
        public int From;

        [JsonProperty(PropertyName = "to")]
        public int To;

        public override int GetHashCode() 
        {
            return string.Format("{0}-{1}", From, To).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var link = obj as Link;
            return this.From == link.From && this.To == link.To;
        }
    }
}
