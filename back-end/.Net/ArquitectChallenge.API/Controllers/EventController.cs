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
        /// Get the number of events grouped by tag.
        /// </summary>
        /// <returns>The number of events grouped by tag.</returns>
        /// <response code="200">Success by getting the groups.</response>
        /// <response code="408">Timeout by getting the groups.</response>
        /// <response code="500">Internal error by getting the groups.</response>
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
        /// Get events groupped by date and hour.
        /// </summary>
        /// <returns>The list of events groupped by date and hour.</returns>
        /// <response code="200">Success by getting the events.</response>
        /// <response code="408">Timeout by getting the events.</response>
        /// <response code="500">Internal error by getting the events.</response>
        [HttpGet]
        [Route("GetEventsGroupedByHour")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult GetEventsGroupedByHour()
        {
            try
            {
                var result = _service.GetEventsGroupedByHour();
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
        /// Get the newest events based on a id.
        /// </summary>
        /// <param name="lastEventId">The id of last event.</param>
        /// <response code="200">Success by getting the newest events.</response>
        /// <response code="408">Timeout by getting the newest events.</response>
        /// <response code="500">Internal error by getting the newest events.</response>
        /// <returns>The list of newest events.</returns>
        [HttpGet]
        [Route("GetNewestEvents")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult GetNewestEvents(int lastEventId)
        {
            try
            {
                var result = _service.GetNewestEvents(lastEventId);
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
        /// Get the numeric events.
        /// </summary>
        /// <returns>The list of numeric events.</returns>
        /// <response code="200">Success by getting the events.</response>
        /// <response code="408">Timeout by getting the events.</response>
        /// <response code="500">Internal error by getting the events.</response>
        [HttpGet]
        [Route("GetNumericEvents")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public ActionResult GetNumericEvents()
        {
            try
            {
                var result = _service.GetNumericEvents();
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
            return base.Save(model);
        }
    }
}