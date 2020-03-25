namespace Lykke.Service.CustomerProfile.Client.Models
{
    /// <summary>
    /// Hold information about the Current page and the amount of items on each page
    /// </summary>
    public class PaginationModel
    {
        /// <summary>
        /// The Current Page
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// The amount of items that the page holds
        /// </summary>
        public int PageSize { get; set; }
    }
}
