using Microsoft.AspNetCore.Mvc;
using Projeto_dio_api.Context;
using Projeto_dio_api.Models;
using Projeto_dio_api.Controllers;
using System.Linq;

namespace Projeto_dio_api.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObeterPorId(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            return Ok(tarefa);
        }
        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var todasTarefas = _context.Tarefas.ToList();

            return Ok(todasTarefas); 
        }
        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var tarefaTitulo = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));

            return Ok(tarefaTitulo);
        }
        [HttpGet("ObeterPorData")]
        public IActionResult ObeterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date).ToList();

            if(tarefa.Any())
            {
                return Ok(tarefa);
            }
            else
            {
                return NotFound();
            }
            
        }
        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            var tarefa = _context.Tarefas.Where(x =>x.Status == status);
            return Ok(tarefa);
        }
        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if(tarefa == null)
                return NotFound();
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { erro = "Data não pode ser vazia!" });

            _context.Add(tarefa);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObeterPorId),new {Id = tarefa.Id}, tarefa);
        }
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { erro = "A data da tarefa não pode estar vazia!" });
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Status = tarefa.Status;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            _context.Update(tarefaBanco);
            _context.SaveChanges();

            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);
            if(tarefaBanco == null)
                return NotFound();
            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
