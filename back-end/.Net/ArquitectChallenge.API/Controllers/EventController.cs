using ArquitectChallenge.Domain.Events;
using ArquitectChallenge.Interfaces.Services.Events;
using Microsoft.AspNetCore.Mvc;

namespace ArquitectChallenge.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class EventController : ApiController<EventData>
    {
        public EventController(IEventService service) : base(service)
        {
        }
    }
}