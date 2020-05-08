using System;
using System.Net;
using Common.Log;
using MAVN.Common.Encryption;
using Lykke.Common.ApiLibrary.Exceptions;
using Lykke.Common.Extensions;
using Lykke.Common.Log;
using MAVN.Service.CustomerProfile.Models;
using Microsoft.AspNetCore.Mvc;

namespace MAVN.Service.CustomerProfile.Controllers
{
    [Route("api/[controller]")]
    public class EncryptionKeyController : Controller
    {
        private readonly IAesSerializer _serializer;
        private readonly ILog _log;

        public EncryptionKeyController(IAesSerializer serializer, ILogFactory logFactory)
        {
            _serializer = serializer;
            _log = logFactory.CreateLog(this);
        }

        /// <summary>
        /// Set encryption key
        /// </summary>
        /// <param name="request">256 bit encryption key in base64-string format</param>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult SetKey([FromBody]SetKeyRequest request)
        {
            _log.Info("Request for set encryption key", context: Request.HttpContext.GetIp());

            try
            {
                _serializer.SetKey(Convert.FromBase64String(request.Key));
                return Ok();
            }
            catch (Exception e) when(e is ArgumentException ||
                                     e is InvalidOperationException ||
                                     e is FormatException)
            {
                throw new ValidationApiException(HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
