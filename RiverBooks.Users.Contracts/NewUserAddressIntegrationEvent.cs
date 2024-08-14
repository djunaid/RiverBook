
namespace RiverBooks.Users.Contracts;

public record NewUserAddressIntegrationEvent(UserAddressDetails userAddressDetails) : IntegrationEventBase;
