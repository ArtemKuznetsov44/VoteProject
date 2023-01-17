using System.ServiceModel;

namespace VoteService_WCF
{
    internal class ServerClient 
    {
        public int ID { get; set; }
       
        public OperationContext OperationContext { get; set; }

    }
}
