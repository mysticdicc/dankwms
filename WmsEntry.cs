namespace Dankwms
{
    public class WmsEntry
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Picture { get; set; }
        public string Keywords { get; set; }

        public WmsEntry(string Id, string Name, string Description, string Location, string Picture, string Keywords)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Location = Location;
            this.Picture = Picture;
            this.Keywords = Keywords;
        }
    }
}
