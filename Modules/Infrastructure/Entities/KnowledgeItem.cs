namespace CareTrack.Server.Modules.Infrastructure.Entities;

public class KnowledgeItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public KnowledgeItemType Type { get; set; }
    public string Url { get; set; } = string.Empty; 
    public string Author { get; set; } = string.Empty;
}

public enum KnowledgeItemType
{
    Article,
    Video,
    Infographic
}
