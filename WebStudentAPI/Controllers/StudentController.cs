﻿using Microsoft.AspNetCore.Mvc;
using WebStudentAPI.Dtos;
using WebStudentAPI.Entities;
using WebStudentAPI.Services.Abstract;

namespace WebStudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }



        [HttpGet]
        public IActionResult Get()
        {
            var items = _studentService.GetAll();

            return Ok(items);
        }


        // GET: api/<StudentController>
        [HttpGet("Best")]
        public IEnumerable<StudentDto> GetBestStudents()
        {
            var items = _studentService.GetAll().Where(s => s.Score>90);
            var dataToReturn = items.Select(a =>
            {
                return new StudentDto
                {
                    Id = a.Id,
                    Age = a.Age,
                    Fullname = a.Fullname,
                    Score = (int)a.Score,
                    SeriaNo = a.SeriaNo
                };
            });
            return dataToReturn;
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public StudentDto Get(int id)
        {
            var item = _studentService.Get(id);
            var dataToReturn = new StudentDto
            {
                Id = item.Id,
                SeriaNo = item.SeriaNo,
                Age = item.Age,
                Fullname = item.Fullname,
                Score = (int)item.Score
            };
            return dataToReturn;
        }

        // POST api/<StudentController>
        [HttpPost]
        public IActionResult Add([FromBody] StudentDto value)
        {
            try
            {
                var obj = new Student
                {
                    Age = value.Age,
                    Score = value.Score,
                    Fullname = value.Fullname,
                    SeriaNo = value.SeriaNo
                };
                _studentService.Add(obj);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] StudentDto value)
        {
            try
            {
                var item = _studentService.Get(id);
                item.SeriaNo = value.SeriaNo;
                item.Age = value.Age;
                item.Fullname = value.Fullname;
                item.Score = value.Score;
                _studentService.Update(item);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _studentService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}