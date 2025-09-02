using Application.Interfaces;
using Application.Utils;
using Domain.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Microsoft.Extensions.Options;

namespace Adapter
{
    public class StockAdapter : IStockAdapter
    {
        private readonly Settings _settings;
        private readonly GoogleCredential _googleCredential;
        private readonly string[] _scopes = { SheetsService.Scope.Spreadsheets };

        public StockAdapter(IOptions<Settings> settings)
        {
            _settings = settings.Value;

            _googleCredential = GoogleAuthentication.FromServiceAccountJson(
                _settings.CommunicationSettings.GoogleSheetsSettings.ServiceAccountJson,
                _scopes
            );
        }

        public async Task<IList<IList<object>>> GetStockData(string googleSpreadsheetId, string googleSpreadsheetName)
        {

            try
            {
                using (var sheetsService = new SheetsService(new BaseClientService.Initializer() { HttpClientInitializer = _googleCredential }))
                {
                    var getValueRequest = sheetsService.Spreadsheets.Values.Get(googleSpreadsheetId, googleSpreadsheetName);

                    var result = await getValueRequest.ExecuteAsync();

                    var values = result.Values;

                    if (values == null || values.Count == 0)
                    {
                        throw new Exception("Stock is empty");
                    }

                    return values;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
