using Domain.Models.Config;

namespace Domain.Models
{
    public class Settings
    {
        public BusinessCommunication BusinessCommunication { get; set; }
        public HttpSettings HttpSettings { get; set; }
        public CommunicationSettings CommunicationSettings { get; set; }
    }
}
