using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Context;
using SmartSchool.API.V1.Dtos;
using SmartSchool.API.Models;

namespace SmartSchool.API.V1.Controller
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {       
        public readonly IRepository _repository;
        public readonly IMapper _mapper;

        public ProfessorController( IRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var professores = _repository.GetAllProfessores(true);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professores));
        }
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var professor = _repository.GetProfessoresById(id);
            if (professor == null)
                return BadRequest("Professor não encontrado.");

            var professorDto = _mapper.Map<ProfessorDto>(professor);

            return Ok(professorDto);
        }
       

        [HttpPost]
        public IActionResult Post(ProfessorRegistrarDto model)
        {
            var professor = _mapper.Map<Professor>(model);

            _repository.Add(professor);
            if (_repository.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não cadastrado.");
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProfessorRegistrarDto model)
        {
            var professor = _repository.GetProfessoresById(id);
            if (professor == null)
            {
                return BadRequest("Professor não encontrado.");
            }

            _mapper.Map(model, professor);

            _repository.Update(professor);
            if (_repository.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não atualizado.");
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorRegistrarDto model)
        {
            var professor = _repository.GetProfessoresById(id);
            if (professor== null)
            {
                return BadRequest("Professor não encontrado.");
            }

            _mapper.Map(model, professor);

            _repository.Update(professor);
            if (_repository.SaveChanges())
            {
                return Created($"/api/aluno/{model.Id}", _mapper.Map<ProfessorDto>(professor));
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
