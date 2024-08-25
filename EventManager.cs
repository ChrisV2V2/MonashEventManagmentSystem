using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WPFProg6221ICE
{
    public class EventManager
    {
        public List<Event> Events { get; set; }

        public EventManager()
        {
            Events = new List<Event>();
        }

        public void AddEvent(Event newEvent)
        {
            Events.Add(newEvent);
        }

        public IEnumerable<dynamic> FilterEvents(DateTime? startDate = null, string type = null, string department = null)
        {
            var query = Events.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(e => e.Date >= startDate.Value);

            if (!string.IsNullOrEmpty(type))
                query = query.Where(e => e.Type == type);

            if (!string.IsNullOrEmpty(department))
                query = query.Where(e => e.Department == department);

            return query.Select(e => new { e.Title, e.Date, e.Type, e.Department });
        }

        public void NotifyStudents()
        {
            Task.Run(() =>
            {
                foreach (var evt in Events)
                {
                    Console.WriteLine($"Notifying students about {evt.Title}");
                }
            });
        }

        public async Task<List<Event>> FetchEventDataAsync(string apiUrl)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(apiUrl);
                // Simulate parsing and returning events
                return new List<Event>(); // Replace with actual parsing logic
            }
        }

        public void UpdateEventStatus()
        {
            foreach (var evt in Events)
            {
                Task.Run(() =>
                {
                    Console.WriteLine($"Updating status for {evt.Title}");
                    evt.Status = "Updated";
                });
            }
        }
    }
}
