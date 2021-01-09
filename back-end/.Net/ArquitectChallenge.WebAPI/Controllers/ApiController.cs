using ArquitectChallenge.Domain;
using ArquitectChallenge.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace ArquitectChallenge.WebAPI.Controllers
{
    public abstract class ApiController<T> : Controller where T : BaseObject
    {
        private readonly IBaseRepository _mapping;

        public ApiController(IBaseRepository mapping)
        {
            _mapping = mapping;
        }

        #region " APIS "

        /// <summary>
        /// Delete the item from database.
        /// </summary>
        /// <param name="id">Identifier of the item.</param>
        /// <returns>Result of the deletion.</returns>
        /// <response code="200">Success by getting the products.</response>
        /// <response code="408">Timeout by gettint the items.</response>
        /// <response code="500">Internal error by getting the product.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public virtual ActionResult Delete(Guid id)
        {
            try
            {
                _mapping.DeleteById(id.ToString());
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
        /// <response code="408">Timeout by gettint the items.</response>
        /// <response code="500">Internal error by getting the items.</response>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public virtual ActionResult<IEnumerable<T>> Get()
        {
            try
            {
                var result = _mapping.GetList<T>();
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
        /// <response code="408">Timeout by gettint the items.</response>
        /// <response code="500">Internal error by getting the item.</response>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public virtual ActionResult<T> Get(Guid id)
        {
            try
            {
                var result = _mapping.GetById<T>(id.ToString());
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
        /// <response code="408">Timeout by gettint the items.</response>
        /// <response code="500">Internal error by inserting the item.</response>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public virtual ActionResult Insert([FromBody] T model)
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
        /// <response code="408">Timeout by gettint the items.</response>
        /// <response code="500">Internal error by getting the items.</response>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public virtual ActionResult Update(Guid id, [FromBody] T model)
        {
            return Save(model);
        }

        #endregion " APIS "

        #region " PRIVATE METHODS "

        private ActionResult Save(T model)
        {
            try
            {
                var result = _mapping.Save(model);
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

        #endregion " PRIVATE METHODS "
    }
}