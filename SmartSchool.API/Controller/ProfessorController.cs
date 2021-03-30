using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Context;
using SmartSchool.API.Models;

namespace SmartSchool.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {       
        public readonly IRepository _repository;

        public ProfessorController( IRepository repository)
        {            
            _repository = repository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var result = _repository.GetAllProfessores(true);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var professor = _repository.GetProfessoresById(id);
            if (professor == null)
                return BadRequest("Professor não encontrado.");
            return Ok(professor);
        }
       

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            _repository.Add(professor);
            if (_repository.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("Professor não cadastrado.");
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var prof = _repository.GetProfessoresById(id);
            if (prof == null)
            {
                return BadRequest("Professor não encontrado.");
            }
            _repository.Update(professor);
            if (_repository.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("Professor não atualizado.");
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Professor professor)
        {
            var prof = _repository.GetProfessoresById(id);
            if (prof == null)
            {
                return BadRequest("Professor não encontrado.");
            }

            _repository.Update(professor);
            if (_repository.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("Professor não atualizado.");
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = _repository.GetProfessoresById(id);
            if (professor == null)
            {
                return BadRequest("Professor não encontrado.");
            }
            _repository.Delete(professor);
            if (_repository.SaveChanges())
            {
                return Ok(professor);
            }
            return BadRequest("Professor não deletado.");
        }
    }
}
