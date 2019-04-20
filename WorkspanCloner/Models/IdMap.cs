namespace WorkspanCloner.Models
{
    public class IdMap
    {
        public int oldId;
        public int newId;

        public IdMap(int oldId, int newId)
        {
            this.oldId = oldId;
            this.newId = newId;
        }
    }
}
