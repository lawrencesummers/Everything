/*
 * Created by SharpDevelop.
 * User: guanxiang
 * Date: 2013/12/6
 * Time: 9:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Windows.Forms;


namespace WCFPublisher
{

	[ServiceContract(CallbackContract=typeof(IPublisherEvents))]
    public interface IPublisher
    {
        [OperationContract]
        void Subscriber(Guid id);    //订阅
        [OperationContract]
        void UnSubscriber(Guid id);  //取消订阅

        [OperationContract]
        void Trigger(); 
    }

    public interface IPublisherEvents
    {
        [OperationContract(IsOneWay = true)]
        void Notify();               //发布消息

    }

    [ServiceBehavior]
    public class Publisher:IPublisher
    {


        Dictionary<Guid, IPublisherEvents> dic = new Dictionary<Guid, IPublisherEvents>();

        //public void AddSubscriber(Guid id, IPublisherEvents callback)
        //{
        //    if (!dic.ContainsKey(id))
        //    {
        //        dic.Add(id, callback);
        //        //lsbSubscribers.Items.Add(id);
        //    }
        //}

        //public void RemoveSubscriber(Guid id, IPublisherEvents callback)
        //{
        //    if (dic.ContainsKey(id))
        //    {
        //        dic.Remove(id);
        //        //lsbSubscribers.Items.Remove(id);
        //    }
        //}

        public void Subscriber(Guid id)
        {
            IPublisherEvents callback = OperationContext.Current.GetCallbackChannel<IPublisherEvents>();
            //MainForm form=Application.OpenForms[0] as MainForm;
            //form.AddSubscriber(id,callback);
            dic.Add(id, callback);
        }

        public void UnSubscriber(Guid id)
        {
            IPublisherEvents callback = OperationContext.Current.GetCallbackChannel<IPublisherEvents>();
            //MainForm form = Application.OpenForms[0] as MainForm;
            //form.RemoveSubscriber(id, callback);
        }
        public void Trigger()
        {
            foreach (KeyValuePair<Guid, IPublisherEvents> pair in dic)
            {
                pair.Value.Notify();
            }
        }

    }

}
