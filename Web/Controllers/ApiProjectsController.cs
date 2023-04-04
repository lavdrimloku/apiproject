using AutoMapper;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.ProjectsServ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Web.Models.ProjectsVM;
namespace Web.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/project")]
    [ApiController]
    public class ApiProjectsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProjectService _projectService;

        public ApiProjectsController(IMapper mapper, IProjectService projectService)
        {
            _mapper = mapper;
            _projectService = projectService;
        }

        [HttpGet]
        [Route("{Id}")]
        public IActionResult GetById(Guid Id)
        {
            try
            {
                var dbResult = _mapper.Map<ProjectViewModels>(_projectService.GetById(Id));
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
                var dbResult = _mapper.Map<List<ProjectViewModels>>(_projectService.GetAll());
                return Ok(dbResult);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }

        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(AddProjectViewModels _addData)
        {
            try
            {
                var _mapped = _mapper.Map<Project>(_addData);
                _projectService.Insert(_mapped);
                return Ok(true);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(UpdateProjectViewModels _updateData)
        {
            try
            {
                var dbResultById = _projectService.GetById(_updateData.Id);
                var _mapped = _mapper.Map(_updateData, dbResultById);
                _projectService.Update(_mapped);
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
                var dbResultById = _projectService.GetById(Id);
                _projectService.Delete(dbResultById);
                return Ok(true);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong!");
            }
        }
    }
}
