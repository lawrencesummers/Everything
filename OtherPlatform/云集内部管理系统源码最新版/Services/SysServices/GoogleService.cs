using System;
using System.Threading.Tasks;
using Google.GData.Calendar;
using Google.GData.Extensions;
using IServices.ISysServices;

namespace Services.SysServices
{
    public class GoogleService : IGoogleService
    {
        private readonly ISysUserService _iSysUserService;

        public GoogleService(ISysUserService iSysUserService)
        {
            _iSysUserService = iSysUserService;
        }

        public  async Task<string> Insert(Guid userId, string title, string content, DateTime start, DateTime end)
        {
            try
            {
                //同步到Google日历
                var item = _iSysUserService.GetById(userId);

                if (string.IsNullOrEmpty(item.GoogleUserName) || string.IsNullOrEmpty(item.GooglePassword)) return "";

                var myService = new CalendarService(item.GoogleUserName);
                myService.setUserCredentials(item.GoogleUserName, item.GooglePassword);

                // Set the title and content of the entry.
                var entry = new EventEntry
                {
                    Title = { Text = "云集 " + title },
                    Content = { Content = content }
                };

                //计划时间
                var eventTime = new When(start, end);

                //判断是否为全天计划
                if (start.Date != end.Date)
                    eventTime.AllDay = true;

                entry.Times.Add(eventTime);

                var postUri = new Uri("https://www.google.com/calendar/feeds/default/private/full");

                // Send the request and receive the response:
                var eventEntry = myService.Insert(postUri, entry);
                return eventEntry.EventId;
            }
            catch
            {
                return "";
            }
        }

        public async Task Delete(Guid userId, string eventId)
        {
            try
            {
                var item = _iSysUserService.GetById(userId);

                if (string.IsNullOrEmpty(item.GoogleUserName) || string.IsNullOrEmpty(item.GooglePassword)) return;
                var myService = new CalendarService(item.GoogleUserName);
                myService.setUserCredentials(item.GoogleUserName, item.GooglePassword);

                var calendar =
                    myService.Get("https://www.google.com/calendar/feeds/default/private/full/" + eventId);

                foreach (var item1 in calendar.Feed.Entries)
                {
                    item1.Delete();
                }
            }
            catch
            {
            }
        }
    }
}