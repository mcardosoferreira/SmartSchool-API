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
        private readonly SmartContext _context;

        public readonly IRepository _repository;

        public ProfessorController(SmartContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Professores);
        }
        [HttpGet("ById")]
        public IActionResult GetById(int id)
        {
            var professor = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (professor == null)
                return BadRequest("Professor não encontrado.");
            return Ok(professor);
        }

        [HttpGet("ByName")]
        public IActionResult GetByName(string nome)
        {
            var professor = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Nome.Contains(nome));
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
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
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
            var prof = _context.Professores.AsNoTracking().FirstOrDefault(p => p.Id == id);
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
            var professor = _context.Professores.FirstOrDefault(p => p.Id == id);
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
