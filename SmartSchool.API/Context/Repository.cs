using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Helpers;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Context
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;
        public Repository(SmartContext context)
        {
             _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity); ;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
        public async Task<PageList<Aluno>> GetAllAlunosAsync(PageParams pageParams, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(aluno => aluno.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(disciplina => disciplina.Professor);
            }
            query = query.AsNoTracking().OrderBy(aluno => aluno.Id);

            //return await query.ToListAsync();
            return await PageList<Aluno>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }

        public Aluno[] GetAllAlunos(bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(aluno => aluno.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(disciplina => disciplina.Professor);
            }
            query = query.AsNoTracking().OrderBy(aluno => aluno.Id);

            return query.ToArray();
        }
        

        public Aluno[] GetAllAlunosByDisciplinaId(int disciplinaId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(aluno => aluno.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(disciplina => disciplina.Professor);
            }
            query = query.AsNoTracking()
                         .OrderBy(aluno => aluno.Id)
                         .Where(a => a.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId));

            return query.ToArray();
        }

        public Aluno GetAlunosById(int alunoId, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(aluno => aluno.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Disciplina)
                             .ThenInclude(disciplina => disciplina.Professor);
            }
            query = query.AsNoTracking()
                         .OrderBy(aluno => aluno.Id)
                         .Where(a => a.Id == alunoId);

            return query.FirstOrDefault();
        }

        public Professor[] GetAllProfessores(bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(professor => professor.Disciplinas)
                             .ThenInclude(disciplina => disciplina.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }
            query = query.AsNoTracking().OrderBy(Professor => Professor.Id);

            return query.ToArray();
        }
        public Professor[] GetAllProfessoresByDisciplinaId(int disciplinaId, bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(professor => professor.Disciplinas)
                             .ThenInclude(disciplina => disciplina.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }
            query = query.AsNoTracking()
                         .OrderBy(aluno => aluno.Id)
                         .Where(a => a.Disciplinas.Any(d => d.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId)));

            return query.ToArray();
        }


        public Professor GetProfessoresById(int professorId, bool includeAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (includeAluno)
            {
                query = query.Include(professor => professor.Disciplinas)
                             .ThenInclude(disciplina => disciplina.AlunosDisciplinas)
                             .ThenInclude(ad => ad.Aluno);
            }
            query = query.AsNoTracking()
                         .OrderBy(professor => professor.Id)
                         .Where(p => p.Id == professorId);

            return query.FirstOrDefault();
        }
    }
}
