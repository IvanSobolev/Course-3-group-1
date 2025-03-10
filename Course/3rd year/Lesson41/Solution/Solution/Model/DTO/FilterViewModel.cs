using System.Text.Json.Serialization;

namespace Solution.Model.DTO;

public class FilterViewModel
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    [JsonPropertyName("filterLogin")]
    public string FilterLogin { get; set; } = string.Empty;

    [JsonPropertyName("filterPasswordHash")]
    public string FilterPasswordHash { get; set; } = string.Empty;

    [JsonPropertyName("sortedByLogin")] 
    public bool SortedByLogin { get; set; } = false;
}