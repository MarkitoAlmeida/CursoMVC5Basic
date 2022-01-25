using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CursoMVC5Basic.Models;

namespace CursoMVC5Basic.Controllers
{
    public class AlunosController : Controller
    {
        private readonly ApplicationDbContext context = new ApplicationDbContext();

        [HttpGet]
        [Route("listar-alunos")]
        public async Task<ActionResult> Index()
        {
            return View(await context.Alunos.ToListAsync());
        }

        [HttpGet]
        [Route("aluno-detalhe/{id:int}")]
        public async Task<ActionResult> Details(int id)
        {
            Aluno aluno = await context.Alunos.FindAsync(id);

            if (aluno == null)
                return HttpNotFound();

            return View(aluno);
        }

        [HttpGet]
        [Route("novo-aluno")]
        public ActionResult Create()
        {
            return View();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("novo-aluno")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome,Email,CPF,DataMatricula,Ativo")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                context.Alunos.Add(aluno);
                await context.SaveChangesAsync();

                //Usado principalmente quando precisa passar um estado de uma controller para outra e depois para uma view.
                //Enquanto o TempData não é lido, o estado permanece, mas depois de lido ele se perde.
                TempData["Mensagem"] = "Aluno cadastrado com sucesso.";

                return RedirectToAction("Index");
            }

            return View(aluno);
        }

        [HttpGet]
        [Route("editar-aluno/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            Aluno aluno = await context.Alunos.FindAsync(id);

            if (aluno == null)
                return HttpNotFound();

            //Usado quando passa de uma Controller direto para uma View
            ViewBag.Mensagem = "Não esqueça que esta ação é irreversível.";

            return View(aluno);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("editar-aluno")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,Email,CPF,DataMatricula,Ativo")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                context.Entry(aluno).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(aluno);
        }

        [HttpGet]
        [Route("excluir-aluno/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            Aluno aluno = await context.Alunos.FindAsync(id);

            if (aluno == null)
                return HttpNotFound();

            return View(aluno);
        }

        [HttpPost]
        [Route("excluir-aluno/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Aluno aluno = await context.Alunos.FindAsync(id);

            context.Alunos.Remove(aluno);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                context.Dispose();

            base.Dispose(disposing);
        }
    }
}
