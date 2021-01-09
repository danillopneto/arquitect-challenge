using ArquitectChallenge.Domain;
using ArquitectChallenge.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace ArquitectChallenge.API.Controllers
{
    /// <summary>
    /// Generic API controller.
    /// </summary>
    /// <typeparam name="T">Type of the model.</typeparam>
    [ApiController]
    public abstract class ApiController<T> : Controller where T : BaseObject
    {
        private readonly IBaseService _service;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="service">Service instance.</param>
        public ApiController(IBaseService service)
        {
            _service = service;
        }

        #region " APIS "

        /// <summary>
        /// Delete the item from database.
        /// </summary>
        /// <param name="id">Identifier of the item.</param>
        /// <returns>Result of the deletion.</returns>
        /// <response code="200">Success by deleting the products.</response>
        /// <response code="408">Timeout by deleting the items.</response>
        /// <response code="500">Internal error by deleting the product.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public virtual ActionResult Delete(int id)
        {
            try
            {
                _service.DeleteById<T>(id);
                return Ok();
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
        /// Get the list of all items.
        /// </summary>
        /// <returns>The list of the item.</returns>
        /// <response code="200">Success by getting the items.</response>
        /// <response code="408">Timeout by getting the items.</response>
        /// <response code="500">Internal error by getting the items.</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public virtual ActionResult<IEnumerable<T>> Get()
        {
            try
            {
                var result = _service.GetList<T>();
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
        /// Get an specific item.
        /// </summary>
        /// <param name="id">Identifier of the item.</param>
        /// <returns>The item with the Identifier informed.</returns>
        /// <response code="200">Success by getting the item.</response>
        /// <response code="408">Timeout by getting the items.</response>
        /// <response code="500">Internal error by getting the item.</response>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public virtual ActionResult<T> Get(int id)
        {
            try
            {
                var result = _service.GetById<T>(id);
                if (result == null)
                {
                    return NotFound(new { message = "The item was not found." });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }

        /// <summary>
        /// Insert the item into database.
        /// </summary>
        /// <param name="model">Model to be saved.</param>
        /// <returns>The item that was saved with its Id.</returns>
        /// <response code="200">Success by inserting the item.</response>
        /// <response code="408">Timeout by getting the items.</response>
        /// <response code="500">Internal error by inserting the item.</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public virtual ActionResult Post([FromBody] T model)
        {
            return Save(model);
        }

        /// <summary>
        /// Update a specific item.
        /// </summary>
        /// <param name="id">Identifier of the item.</param>
        /// <param name="model">Model to be saved.</param>
        /// <returns>The item that was saved.</returns>
        /// <response code="200">Success by getting the items.</response>
        /// <response code="408">Timeout by getting the items.</response>
        /// <response code="500">Internal error by getting the items.</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public virtual ActionResult Put(int id, [FromBody] T model)
        {
            return Save(model);
        }

        #endregion " APIS "

        #region " PROTECTED METHODS "

        /// <summary>
        /// Save the item into database.
        /// </summary>
        /// <param name="model">Model to be saved.</param>
        /// <returns>Result of saving the model.</returns>
        protected virtual ActionResult Save(T model)
        {
            try
            {
                var result = _service.Save(model);
                return Ok(model);
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

        #endregion " PROTECTED METHODS "
    }
}