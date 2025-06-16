using Domain.Responses;

namespace GrpcServiceUser.Interface
{
    public interface IShopRepository
    {
        /// <summary>
        ///     Get list of all shops in database.
        /// </summary>
        /// <returns>IEnumerable of shop</returns>
        Task<IEnumerable<ResponseShop>> GetAll();
        /// <summary>
        ///     Get data of shop by id.
        /// </summary>
        /// <param name="shopId">Id of shop need to find.</param>
        /// <returns>This object.</returns>
        Task<ResponseShop?> GetOne(string shopId);
        /// <summary>
        ///     Get data of shop which user has by user's id.
        /// </summary>
        /// <param name="userId">user's id has shop</param>
        /// <returns>This object.</returns>
        Task<ResponseShop?> GetOfUser(string userId);
        /// <summary>
        ///     Add new shop into database.
        /// </summary>
        /// <param name="shop">Data create new shop.</param>
        /// <returns>Response status add shop.</returns>
        Task<Response> CreateShop(Domain.Requests.RequestCreateShop shop);
        /// <summary>
        ///     Update data of a shop in database
        /// </summary>
        /// <param name="shop">Data update shop.</param>
        /// <returns>Response status update shop.</returns>
        Task<Response> UpdateShop(Domain.Requests.RequestUpdateShop shop);
        /// <summary>
        ///     Delete one shop in database.
        /// </summary>
        /// <param name="shopId">Id of a shop.</param>
        /// <returns>Response status delete shop.</returns>
        Task<Response> DeleteShop(string shopId);

    }
}
