namespace Core;

public static class QuoteRequestExtensions
{
    public static QuoteRequestModel ToModel(this QuoteRequest entity) => new QuoteRequestModel
    {
        QuoteId = entity.QuoteId,
        JobId = entity.JobId,
        RequestDate = entity.RequestDate,
        DescriptionOfWork = entity.DescriptionOfWork,
        EstimatedPrice = entity.EstimatedPrice,
        Job = entity.Job.ToModel()
    };

    public static IEnumerable<QuoteRequestModel> ToModels(this IEnumerable<QuoteRequest> entity)
    {
        return entity.Select(e => e.ToModel());
    }

    public static QuoteRequest ToEntity(this QuoteRequestModel model) => new QuoteRequest
    {
        QuoteId = model.QuoteId,
        JobId = model.JobId,
        RequestDate = model.RequestDate,
        DescriptionOfWork = model.DescriptionOfWork,
        EstimatedPrice = model.EstimatedPrice,
        Job = model.Job.ToEntity()
    };
}