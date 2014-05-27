/*
 * Created by SharpDevelop.
 * User: guanxiang
 * Date: 2013/12/5
 * Time: 20:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Quartz;  
using Quartz.Impl; 
using Common.Logging;

namespace QuartzTest
{
	     public class JobExample : IJob  
   {  
       private int i = 0;  
       public void Execute(IJobExecutionContext context)  
       {  
           Console.WriteLine(++i);  
       }  
   }  
	class Program
	{
		public static void Main(string[] args)
		{
			//Console.WriteLine("Hello World!");
			
			// TODO: Implement Functionality Here
			
			//Console.Write("Press any key to continue . . . ");
			//Console.ReadKey(true);
			   //初始化调度器工厂    
          ISchedulerFactory sf = new StdSchedulerFactory();  
          //获取默认调度器    
          IScheduler scheduler = sf.GetScheduler();  

          //作业    
          IJobDetail job = JobBuilder  
                  .Create<JobExample>()  
                  .WithIdentity("计算作业", "组1")  
                  .RequestRecovery() // ask scheduler to re-execute this job if it was in progress when the scheduler went down...   
                  .Build();  


          //触发器    
          ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create()  
                                            .WithIdentity("触发器1", "触发器组1")  
                                            .StartAt(DateBuilder.FutureDate(1, IntervalUnit.Second))  
                                            .WithSimpleSchedule(x => x.WithRepeatCount(20).WithInterval(TimeSpan.FromSeconds(5)))  
                                            .Build();  

          //关联任务和触发器    
          scheduler.ScheduleJob(job, trigger);  
          //开始任务    
          scheduler.Start();  
          Console.Read();  

		}
	}
}