using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.V1.Dtos
{
    public class CursoDto
    {        
        public int Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<DisciplinaDto> Disciplinas { get; set; }

    }
}
