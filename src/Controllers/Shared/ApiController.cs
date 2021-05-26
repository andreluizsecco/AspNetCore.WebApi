using System;
using System.Collections.Generic;
using System.Net;
using AspNetCore.WebApi.Extensions;
using AspNetCore.WebApi.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.WebApi.Controllers.Shared
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected IActionResult ResponseOk(object result) =>
            Response(HttpStatusCode.OK, result);

        protected IActionResult ResponseOk() =>
            Response(HttpStatusCode.OK);

        protected IActionResult ResponseCreated() =>
            Response(HttpStatusCode.Created);
        
        protected IActionResult ResponseCreated(object data) =>
            Response(HttpStatusCode.Created, data);

        protected IActionResult ResponseNoContent() =>
            Response(HttpStatusCode.NoContent);

        protected IActionResult ResponseNotModified() =>
            Response(HttpStatusCode.NotModified);

        protected IActionResult ResponseBadRequest(string errorMessage) =>
            Response(HttpStatusCode.BadRequest, errorMessage: errorMessage);

        protected IActionResult ResponseBadRequest() =>
            Response(HttpStatusCode.BadRequest, errorMessage: "A requisição é inválida");

        protected IActionResult ResponseNotFound(string errorMessage) =>
            Response(HttpStatusCode.NotFound, errorMessage: errorMessage);

        protected IActionResult ResponseNotFound() =>
            Response(HttpStatusCode.NotFound, errorMessage: "O recurso não foi encontrado");

        protected IActionResult ResponseUnauthorized(string errorMessage) =>
            Response(HttpStatusCode.Unauthorized, errorMessage: errorMessage);

        protected IActionResult ResponseUnauthorized() =>
            Response(HttpStatusCode.Unauthorized, errorMessage:"Permissão negada");

        protected IActionResult ResponseInternalServerError() =>
            Response(HttpStatusCode.InternalServerError);
        
        protected IActionResult ResponseInternalServerError(string errorMessage) =>
            Response(HttpStatusCode.InternalServerError, errorMessage: errorMessage);

        protected IActionResult ResponseInternalServerError(Exception exception) =>
            Response(HttpStatusCode.InternalServerError, errorMessage: exception.Message);

        protected new JsonResult Response(HttpStatusCode statusCode, object data, string errorMessage)
        {
            CustomResult result = null;

            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                var success = statusCode.IsSuccess();

                if (data != null)
                    result = new CustomResult(statusCode, success, data);
                else
                    result = new CustomResult(statusCode, success);
            }
            else
            {
                var errors = new List<string>();

                if (!string.IsNullOrWhiteSpace(errorMessage))
                    errors.Add(errorMessage);

                result = new CustomResult(statusCode, false, errors);
            }
            return new JsonResult(result) { StatusCode = (int)result.StatusCode };
        }

        protected new JsonResult Response(HttpStatusCode statusCode, object result) =>
            Response(statusCode, result, null);

        protected new JsonResult Response(HttpStatusCode statusCode, string errorMessage) =>
            Response(statusCode, null, errorMessage);

        protected new JsonResult Response(HttpStatusCode statusCode) =>
            Response(statusCode, null, null);
    }
}