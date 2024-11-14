using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LocadoraDejogos.Data;
using LocadoraDejogos.Models;
using ClosedXML.Excel;

namespace LocadoraDejogos.Controllers
{
    public class FuncionariosController : Controller
    {
        public async Task<IActionResult> ExportarFuncionariosExcel()
        {
            var item = await _context.Funcionarios.ToListAsync();
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Funcionários");
            worksheet.Cell(1, 1).Value = "Tabela Funcionários";

            // Define a última linha e coluna com base nos dados
            int ultimaLinha = item.Count + 2;
            int ultimaColuna = 6;

            // Borda externa e pintar o fundo inteiro de branco
            var tabelaRange = worksheet.Range(1, 1, ultimaLinha, ultimaColuna);
            tabelaRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            tabelaRange.Style.Fill.BackgroundColor=XLColor.White;

            // Depois de pintar toda a tabela de branca, pintar cabeçalho de uma outra cor:
            // Mudar cor do Cabeçalho
            worksheet.Cell(1, 1).Style.Font.FontColor=XLColor.Black;
            worksheet.Cell(1, 1).Style.Fill.BackgroundColor=XLColor.Sandstorm;

            // Mudar tamanho da fonte e outras configurações de formatação
            worksheet.Cell(1, 1).Style.Font.FontSize=20;
            worksheet.Row(1).Height = 20;

            worksheet.Range(1, 1,1,ultimaColuna).Style.Alignment.Vertical=XLAlignmentVerticalValues.Center;
            worksheet.Range(1, 1,1,ultimaColuna).Merge().Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;

            using (workbook)
            {
                // Cabeçalho
                worksheet.Cell(2, 1).Value = "ID";
                worksheet.Cell(2, 2).Value = "Login";
                worksheet.Cell(2, 3).Value = "Senha";
                worksheet.Cell(2, 4).Value = "CPF";
                worksheet.Cell(2, 5).Value = "Nome";
                worksheet.Cell(2, 6).Value = "Data de Nascimento";

                // Dados
                for (int i = 0; i < item.Count; i++)
                {
                    worksheet.Cell(i + 3, 1).Value = item[i].ID;
                    worksheet.Cell(i + 3, 2).Value = item[i].Login;
                    worksheet.Cell(i + 3, 3).Value = item[i].Senha;
                    worksheet.Cell(i + 3, 4).Value = item[i].CPF;
                    worksheet.Cell(i + 3, 5).Value = item[i].Nome;
                    worksheet.Cell(i + 3, 6).Value = item[i].DataNascimento;

                    // Arrumar algumas colunas
                    worksheet.Cell(i + 3, 1).Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;

                    // Adicionar apenas bordas horizontais internas (remove verticais internas)
                    for (int j = 1; j <= ultimaColuna; j++)
                    {
                        worksheet.Cell(i + 2, j).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Cell(i + 2, j).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    }
                }

                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Tabela Funcionários.xlsx");
                }
            }
        }

        private readonly ApplicationDbContext _context;

        public FuncionariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index(string? Ordenar, string? Nome)
        {
            var applicationDbContext = _context.Funcionarios.OrderBy(j => j.Nome);

            if (Ordenar != null)
            {
                switch (Ordenar)
                {
                    case "Login":
                        applicationDbContext = _context.Funcionarios.OrderBy(j => j.Login);
                        break;

                    case "Senha":
                        applicationDbContext = _context.Funcionarios.OrderBy(j => j.Senha);
                        break;

                    case "CPF":
                        applicationDbContext = _context.Funcionarios.OrderBy(j => j.CPF);
                        break;

                    case "DataNascimento":
                        applicationDbContext = _context.Funcionarios.OrderBy(j => j.DataNascimento);
                        break;

                    default:
                        break;
                }
            }
            else
            {
                if (Nome != null)
                {
                    var applicationDbContextBuscar = _context.Funcionarios.Where(a => a.Nome.ToLower().Contains(Nome.ToLower()));
                    return View(await applicationDbContextBuscar.ToListAsync());
                }
            }

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                id = 1;
            }

            var funcionarios = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.ID == id);
            if (funcionarios == null)
            {
                return NotFound();
            }

            return View(funcionarios);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Login,Senha,CPF,Nome,DataNascimento")] Funcionarios funcionarios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(funcionarios);
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                id = 1;
            }

            var funcionarios = await _context.Funcionarios.FindAsync(id);
            if (funcionarios == null)
            {
                return NotFound();
            }
            return View(funcionarios);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Login,Senha,CPF,Nome,DataNascimento")] Funcionarios funcionarios)
        {
            if (id != funcionarios.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionariosExists(funcionarios.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(funcionarios);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                id = 1;
            }

            var funcionarios = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.ID == id);
            if (funcionarios == null)
            {
                return NotFound();
            }

            return View(funcionarios);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionarios = await _context.Funcionarios.FindAsync(id);
            if (funcionarios != null)
            {
                _context.Funcionarios.Remove(funcionarios);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionariosExists(int id)
        {
            return _context.Funcionarios.Any(e => e.ID == id);
        }
    }
}
