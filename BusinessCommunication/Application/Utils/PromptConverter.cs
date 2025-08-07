using Domain.Dtos.Response;
using System.Text;

namespace Application.Utils
{
    public static class PromptConverter
    {

        public static string GetProductsString(IList<IList<object>> products)
        {
            var stockText = new StringBuilder();

            List<string> headers = products[0].Select(product => (string)product).ToList();

            foreach (var product in products.Skip(1))
            {
                var productText = new StringBuilder();

                for (var item = 0; item < headers.Count; item++)
                {
                    productText.Append($"{headers[item]}: {product[item]} ");
                }

                stockText.AppendLine("- " + productText.ToString());
            }


            return stockText.ToString();
        }

        public static string GetMessageHistoryString(IEnumerable<MessageData> messageHistoryDto)
        {
            var historyText = new StringBuilder();

            foreach (var message in messageHistoryDto)
            {
                historyText.AppendLine($"Cliente: {message.ReceivedMessage}");
                historyText.AppendLine($"Bot: {message.ResponseMessage}");
            }

            return historyText.ToString();
        }

        public static string GetActualMessageString(string message)
        {
            var messageText = new StringBuilder();

            messageText.AppendLine(message);

            return messageText.ToString();
        }
    }
}
