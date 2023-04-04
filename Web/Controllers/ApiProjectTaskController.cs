using AutoMapper;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.ProjectsServ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Models.ProjectTaksVM;

namespace Web.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/task")]
    [ApiController]
    public class ApiProjectTaskController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectTasksService _projectTaskService;

        public ApiProjectTaskController(IMapper mapper, IProjectTasksService projectTaskService)
        {
            _mapper = mapper;
            _projectTaskService = projectTaskService;
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetById(Guid Id)
        {
            try
            {
                var dbResult = _mapper.Map<ProjectTaskViewModel>(_projectTaskService.GetById(Id));
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
                var dbResult = _mapper.Map<List<ProjectTaskViewModel>>(_projectTaskService.GetAll());
                return Ok(dbResult);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }

        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(AddProjectTaskViewModel _addData)
        {
            try
            {
                var _mapped = _mapper.Map<ProjectTask>(_addData);
                _projectTaskService.Insert(_mapped);
                return Ok(true);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(UpdateProjectTaskViewModel _updateData)
        {
            try
            {
                var dbResultById = _projectTaskService.GetById(_updateData.Id);
                var _mapped = _mapper.Map(_updateData, dbResultById);
                _projectTaskService.Update(_mapped);
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
                var dbResultById = _projectTaskService.GetById(Id);
                _projectTaskService.Delete(dbResultById);
                return Ok(true);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }



        [HttpGet]
        [Route("taskassignedme/{projectid}")]
        public IActionResult TaskForMe(Guid projectid)
        {
            try
            {
                var currentUserID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var dbResult = _mapper.Map<List<ProjectTaskViewModel>>(_projectTaskService.GetAllTaskAssignedMe(projectid, currentUserID));
                return Ok(dbResult);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }  
        
        
        [HttpPut]
        [Route("updatestatustask/{projectaskid}/{statusid}")]
        public IActionResult UpdateStatusTask(Guid projectaskid, Guid statusid)
        {
            try
            {
                var dbResult = _projectTaskService.GetById(projectaskid);
                dbResult.StatusId = statusid;
                _projectTaskService.Update(dbResult);
                return Ok(true);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }
    }
}
