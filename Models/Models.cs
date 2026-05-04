namespace MediaRake.Models;

public enum FieldType
{
    Score,
    Text,
    Image
}

public class FieldDefinition // This defines a Name-Type combo for defining fields
{
    public string Name { get; set; } = "";
    public FieldType Type { get; set; } = FieldType.Score;
}

public class FieldValue
{
    public int? Score { get; set; }
    public string? Text { get; set; }
}

public class Tag
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public string Color { get; set; } = "#FFFFFF";
}

public class MediaItem
{
    public Guid Id { get; set; } = Guid.NewGuid(); // This is a unique ID identifier
    public string Title { get; set; } = "";
    public Dictionary<string, FieldValue> Values { get; set; } = new();
    public List<Tag> Tags { get; set; } = new();
}

public class RatingList
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "";
    public List<MediaItem> Items { get; set; } = new();
    public List<FieldDefinition> Fields { get; set; } = new();
}

public class AppData
{
    public List<RatingList> Lists { get; set; } = new();
}

public class CreateListRequest
{
    public string Name { get; set; } = "";
    public List<FieldDefinition> Fields { get; set; } = new();
}

public class CreateTagRequest
{
    public string Name { get; set; } = "";
    public MediaItem? Item { get; set; }
}

public class DeleteTagRequest
{
    public Tag? Tag { get; set; }
    public MediaItem? Item { get; set; }
}