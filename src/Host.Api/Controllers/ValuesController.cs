using System;
using System.Threading.Tasks;
using Domain.Entities;
using Host.Api.Requests.Commands.Values;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Host.Api.Controllers
{
    /* Looking for a token?
     * http://localhost:52836/api/JwtTest
     */
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IMediator _mediator;

        public ValuesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/values
        [HttpGet]
        [Authorize("PolicyUserTypeA")]
        [Authorize("PolicyUserTypeB")]
        public async Task<Guid> Get()
        {
            var item = new Item
            {
                Description = "asas"
            };
            var response = await _mediator.Send(new GetCommandRequest(item));

            return response.UniqueId;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
