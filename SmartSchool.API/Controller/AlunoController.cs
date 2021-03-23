﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        public List<Aluno> Alunos = new List<Aluno>()
       {
           new Aluno()
           {
               Id = 1,
               Nome = "Marcos",
               Sobrenome = "assunção",
               Telefone = "123154"
           },
           new Aluno()
           {
               Id = 2,
               Nome = "Marta",
               Sobrenome = "Santos",
               Telefone = "1231542"
           },
           new Aluno()
           {
               Id = 3,
               Nome = "Laura",
               Sobrenome = "Almeida",
               Telefone = "1231543"
           }
       };
        public AlunoController() { }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Alunos);
        }
        [HttpGet("byId")]
        public IActionResult GetById(int id)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Id == id);
            if (aluno == null)
                return BadRequest("Aluno não encontrado");
            return Ok(aluno);
        }
        
        [HttpGet("byName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno = Alunos.FirstOrDefault(a => a.Nome.Contains(nome) && a.Sobrenome.Contains(sobrenome));
            if (aluno == null)
                return BadRequest("Aluno não encontrado");
            return Ok(aluno);
        }
        
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {            
            return Ok(aluno);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {            
            return Ok(aluno);
        }
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            return Ok(aluno);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {            
            return Ok();
        }
    }
}
