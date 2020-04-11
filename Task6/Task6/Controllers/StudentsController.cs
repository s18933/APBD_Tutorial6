using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using APBD_Task4.Models;
using Microsoft.AspNetCore.Mvc;
using Task6.Services;

namespace Task6.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        public SqlServerDbService service;
        public StudentsController()
        {
            service = new SqlServerDbService();
        }

        [HttpGet("{index}")]
        public IActionResult GetStudent(string index) 
        {
           
            var st = service.GetStudentByIndex(index);
            return Ok(st);
        }

    }
}