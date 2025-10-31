namespace Core;

public static class PhoneExtensions
{
    public static PhoneModel ToModel(this Phone entity) => new PhoneModel
    {
        PhoneId = entity.PhoneId,
        CustomerId = entity.CustomerId,
        PhoneNumber = entity.PhoneNumber,
        PhoneType = entity.PhoneType,
        Customer = entity.Customer.ToModel()
    };

    public static IEnumerable<PhoneModel> ToModels(this IEnumerable<Phone> entity)
    {
        return entity.Select(e => e.ToModel());
    }

    public static Phone ToEntity(this PhoneModel model) => new Phone
    {
        PhoneId = model.PhoneId,
        CustomerId = model.CustomerId,
        PhoneNumber = model.PhoneNumber,
        PhoneType = model.PhoneType,
        Customer = model.Customer.ToEntity()
    };
}