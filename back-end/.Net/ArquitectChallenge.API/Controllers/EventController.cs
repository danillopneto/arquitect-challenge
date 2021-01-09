using ArquitectChallenge.Domain.Events;
using ArquitectChallenge.Interfaces.Services.Events;
using Microsoft.AspNetCore.Mvc;

namespace ArquitectChallenge.API.Controllers
{
    /// <summary>
    /// API of events.
    /// </summary>
    [Route("api/v1/[controller]")]
    public class EventController : ApiController<EventData>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="service">Service instance.</param>
        public EventController(IEventService service) : base(service)
        {
        }

        /// <summary>
        /// Saving the event.
        /// </summary>
        /// <param name="model">Event data.</param>
        /// <returns>Result of saving the event.</returns>
        protected override ActionResult Save(EventData model)
        {
            model.FillStatus();
            return base.Save(model);
        }
    }
}