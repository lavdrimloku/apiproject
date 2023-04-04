using AutoMapper;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.StatusServ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.StatusVM;

namespace Web.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/status")]
    [ApiController]
    public class ApiStatusController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IStatusService _statusService;

        public ApiStatusController(IMapper mapper, IStatusService statusService)
        {
            _mapper = mapper;
            _statusService = statusService;
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetById(Guid Id)
        {
            try
            {
                var dbResult = _mapper.Map<StatusViewModel>(_statusService.GetById(Id));
                return Ok(dbResult);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }

        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var dbResult = _mapper.Map<List<StatusViewModel>>(_statusService.GetAll());
                return Ok(dbResult);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }

        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(AddStatusViewModel _addData)
        {
            try
            {
                var _mapped = _mapper.Map<Status>(_addData);
                _statusService.Insert(_mapped);
                return Ok(true);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(UpdateStatusViewModel _updateData)
        {
            try
            {
                var dbResultById = _statusService.GetById(_updateData.Id);
                var _mapped = _mapper.Map(_updateData, dbResultById);
                _statusService.Update(_mapped);
                return Ok(true);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(Guid Id)
        {
            try
            {
                var dbResultById = _statusService.GetById(Id);
                _statusService.Delete(dbResultById);
                return Ok(true);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }
    }
}
