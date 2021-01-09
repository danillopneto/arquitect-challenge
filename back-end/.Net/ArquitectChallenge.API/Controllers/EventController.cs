using ArquitectChallenge.Domain.Events;
using ArquitectChallenge.Interfaces.Services.Events;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace ArquitectChallenge.API.Controllers
{
    /// <summary>
    /// API of events.
    /// </summary>
    [Route("api/v1/[controller]")]
    public class EventController : ApiController<EventData, IEventService>
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="service">Service instance.</param>
        public EventController(IEventService service) : base(service)
        {
        }

        /// <summary>
        /// Get the list of events grouped by tag.
        /// </summary>
        /// <returns>The list of events grouped by tag.</returns>
        /// <response code="200">Success by getting the items.</response>
        /// <response code="408">Timeout by getting the items.</response>
        /// <response code="500">Internal error by getting the items.</response>
        [HttpGet]
        [Route("GetAllGroupedByTag")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult GetAllGroupedByTag()
        {
            try
            {
                var result = _service.GetAllGroupedByTag();
                return Ok(result);
            }
            catch (TimeoutException)
            {
                return StatusCode((int)HttpStatusCode.RequestTimeout);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
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