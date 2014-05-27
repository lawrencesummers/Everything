using System;
using System.ServiceModel;

namespace LearnWCF
{
    [ServiceContract]
    public interface IHelloWCF
    {
        [OperationContract]
        string HelloWCF();
    }

    public class HelloWCFService : IHelloWCF
    {
        public string HelloWCF()
        {
            return "Hello WCF!";
        }
    }
}