using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace WorkspanCloner.Models
{
    public class Index
    {
        private EntitiesGraph data { get; set; }
        private int sequence { get; set; }
        private readonly Dictionary<int, Node> allNodes;

        public Index(EntitiesGraph entitiesInput)
        {
            allNodes = new Dictionary<int, Node>();

            data = entitiesInput;
            var duplicates = entitiesInput.Entities.GroupBy(x => x.EntityId).Any(x => x.Count() > 1);

            if (!duplicates)
            {
                sequence = entitiesInput.Entities.Max(x => x.EntityId) + 1;
                entitiesInput.Links.ForEach(item => Add(item.From, item.To));
            }
            else
            {
                throw new DuplicateWaitObjectException();
            }
        }

        /// <summary>
        /// Clone the specified branch starting at Node Id.
        /// </summary>
        /// <param name="toClone">Id of the node from which to start cloning.</param>
        public void Clone(int toClone)
        {
            if (allNodes.ContainsKey(toClone))
            {
                var clonedMap = new Dictionary<int, Node>();
                var cloned = this.allNodes[toClone].Clone(sequence, clonedMap);

                clonedMap.ToList().ForEach(x => this.allNodes.Add(x.Value.Id, x.Value));
                var linksFrom = cloned.Parents.Select(x => new Link { From = x.Value.Id, To = cloned.Id });
                var relatedIds = Traverse(cloned.Id, new HashSet<Link>());

                var relatedEntities = clonedMap.ToList()
                    .Join(data.Entities,
                        x => x.Key,
                        y => y.EntityId,
                        (x, y) => new EntityInfo { EntityId = x.Value.Id, Name = y.Name, Description = y.Description });

                data.Links.AddRange(linksFrom.Concat(relatedIds));
                data.Entities.AddRange(relatedEntities);
            }
            else
            {
                throw new KeyNotFoundException("The entity does not exist. Nothing is cloned.");
            }
        }

        /// <summary>
        /// Converts the entities graph to json string
        /// </summary>
        /// <returns>json string</returns>
        public string GetOutput()
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }

        /// <summary>
        /// Adds a new node to node list and updates children and parents based on links
        /// </summary>
        /// <param name="from">Entity Id linking from</param>
        /// <param name="to">Entity Id linking to</param>
        private void Add(int from, int to)
        {
            if (!allNodes.ContainsKey(from))
                allNodes.Add(from, new Node(from));

            if (!allNodes.ContainsKey(to))
                allNodes.Add(to, new Node(to));


            if (!allNodes[from].Children.ContainsKey(to))
            {
                allNodes[from].Children.Add(to, allNodes[to]);
                allNodes[to].Parents.Add(from, allNodes[from]);
            }
        }

        /// <summary>
        /// Traverse the nodes beginning at specified id.
        /// </summary>
        /// <returns>The traverse.</returns>
        /// <param name="id">Id of the Node</param>
        /// <param name="result">Aggregate list of all Links inside the branch</param>
        private IEnumerable<Link> Traverse(int id, HashSet<Link> result)
        {
            foreach (var childId in this.allNodes[id].Children.Keys)
            {
                var isAdded = result.Add(new Link { From = id, To = childId });

                // check for circular reference
                if (isAdded)
                {
                    Traverse(childId, result).ToList().ForEach(x => result.Add(x));
                }
            }
            return result;
        }
    }
}
