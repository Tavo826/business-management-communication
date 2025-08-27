namespace Application.Interfaces
{
    public interface IStockAdapter
    {
        Task<IList<IList<object>>> GetStockData(string googleSpreadsheetId, string googleSpreadsheetName);
    }
}
