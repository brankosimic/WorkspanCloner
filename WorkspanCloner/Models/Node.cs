using System.Collections.Generic;

namespace WorkspanCloner.Models
{
    public class Node
    {
        public int Id { get; set; }
        public Dictionary<int, Node> Children { get; set; }
        public Dictionary<int, Node> Parents { get; set; }

        public Node(int id)
        {
            Children = new Dictionary<int, Node>();
            Parents = new Dictionary<int, Node>();
            Id = id;
        }

        /// <summary>
        /// Clone the specified Node and aggregates all cloned Nodes.
        /// </summary>
        /// <returns>Cloned node as a branch</returns>
        /// <param name="newId">Id of the Node to clone</param>
        /// <param name="clones">Aggregate of all cloned Nodes</param>
        public Node Clone(int newId, Dictionary<int, Node> clones)
        {
            var sequence = newId;
            var newNode = new Node(newId);

            if(!clones.ContainsKey(this.Id))
                clones.Add(this.Id, newNode);

            foreach (var item in this.Parents)
            {
                if (clones.ContainsKey(item.Key))
                    newNode.Parents.Add(clones[item.Key].Id, clones[item.Key]);
                else if (clones.Count == 1)
                    newNode.Parents.Add(item.Key, item.Value);
            }

            foreach (var item in this.Children)
            {
                if (!clones.ContainsKey(item.Key))
                    item.Value.Clone(sequence + 1, clones);

                newNode.Children.Add(clones[item.Key].Id, clones[item.Key]);
            }

            return newNode;
        }
    }
}
