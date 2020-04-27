using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Kolokwium.DTOs;
using Kolokwium.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        readonly IDbService _service;
       

        public TasksController(IDbService service1)
        {
            _service = service1;
            
        }

        [HttpGet("{id}")]
        public IActionResult getTasks(int id)
        {

            try
            {
                return Ok(_service.GetTasks(id));
                
            }
            catch (SqlException)
            {
                return BadRequest("sql blad");
            }

        }


        [HttpPost]
        public IActionResult AddTask(AddTaskRequest request)
        {
            try
            {
                return Ok(_service.AddTask(request));

              
            }
            catch (SqlException)
            {

                return NotFound("blad sql"); //tak wiem ale nwm jak inaczej bo jest za pozno pewnie trza w tej metodzie

            }

        }



    }
}