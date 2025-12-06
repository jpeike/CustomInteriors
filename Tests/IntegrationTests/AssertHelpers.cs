namespace Testing.IntegrationTests;

public class AssertHelpers
{
    public static void IsValidResponse(HttpResponseMessage response)
    {
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(response.Content.Headers.ContentType);
        Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        Assert.Equal("utf-8", response.Content.Headers.ContentType.CharSet);
    }
}