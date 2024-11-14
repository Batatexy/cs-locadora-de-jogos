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
    public class ConsolesController : Controller
    {
        public async Task<IActionResult> ExportarConsolesExcel()
        {
            var item = await _context.Consoles.ToListAsync();
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Consoles");
            worksheet.Cell(1, 1).Value = "Tabela Consoles";

            // Define a última linha e coluna com base nos dados
            int ultimaLinha = item.Count + 2;
            int ultimaColuna = 5;

            // Borda externa e pintar o fundo inteiro de branco
            var tabelaRange = worksheet.Range(1, 1, ultimaLinha, ultimaColuna);
            tabelaRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            tabelaRange.Style.Fill.BackgroundColor=XLColor.White;

            // Depois de pintar toda a tabela de branca, pintar cabeçalho de uma outra cor:
            // Mudar cor do Cabeçalho
            worksheet.Cell(1, 1).Style.Font.FontColor=XLColor.Black;
            worksheet.Cell(1, 1).Style.Fill.BackgroundColor=XLColor.LightGray;

            // Mudar tamanho da fonte e outras configurações de formatação
            worksheet.Cell(1, 1).Style.Font.FontSize=20;
            worksheet.Row(1).Height = 20;

            worksheet.Range(1, 1,1,ultimaColuna).Style.Alignment.Vertical=XLAlignmentVerticalValues.Center;
            worksheet.Range(1, 1,1,ultimaColuna).Merge().Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;

            using (workbook)
            {
                // Cabeçalho
                worksheet.Cell(2, 1).Value = "ID";
                worksheet.Cell(2, 2).Value = "Nome";
                worksheet.Cell(2, 3).Value = "Fabricante";
                worksheet.Cell(2, 4).Value = "Geração";
                worksheet.Cell(2, 5).Value = "Ano de Lançamento";

                // Dados
                for (int i = 0; i < item.Count; i++)
                {
                    worksheet.Cell(i + 3, 1).Value = item[i].ID;
                    worksheet.Cell(i + 3, 2).Value = item[i].Nome;
                    worksheet.Cell(i + 3, 3).Value = item[i].Fabricante;
                    worksheet.Cell(i + 3, 4).Value = item[i].Geracao + "ª";
                    worksheet.Cell(i + 3, 5).Value = item[i].Ano;

                    // Arrumar algumas colunas
                    worksheet.Cell(i + 3, 1).Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(i + 3, 4).Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(i + 3, 5).Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;

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
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Tabela Consoles.xlsx");
                }
            }
        }

        private readonly ApplicationDbContext _context;

        public ConsolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Consoles
        public async Task<IActionResult> Index(string? Ordenar, string? Nome)
        {
            var applicationDbContext = _context.Consoles.OrderBy(j => j.Nome);

            if (Ordenar != null)
            {
                switch (Ordenar)
                {
                    case "Nome":
                        applicationDbContext = _context.Consoles.OrderBy(j => j.Nome);
                        break;

                    case "Fabricante":
                        applicationDbContext = _context.Consoles.OrderBy(j => j.Fabricante);
                        break;

                    case "Geracao":
                        applicationDbContext = _context.Consoles.OrderBy(j => j.Geracao);
                        break;

                    case "Ano":
                        applicationDbContext = _context.Consoles.OrderBy(j => j.Ano);
                        break;

                    default:
                        break;
                }
            }
            else
            {
                if (Nome != null)
                {
                    var applicationDbContextBuscar = _context.Consoles.Where(a => a.Nome.ToLower().Contains(Nome.ToLower()));
                    return View(await applicationDbContextBuscar.ToListAsync());
                }
            }

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Consoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                id = 1;
            }

            var consoles = await _context.Consoles
                .FirstOrDefaultAsync(m => m.ID == id);
            if (consoles == null)
            {
                return NotFound();
            }

            return View(consoles);
        }

        // GET: Consoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ImagemURL,WikipediaURL,Nome,Fabricante,Geracao,Ano")] Consoles consoles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consoles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consoles);
        }

        // GET: Consoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                id = 1;
            }

            var consoles = await _context.Consoles.FindAsync(id);
            if (consoles == null)
            {
                return NotFound();
            }
            return View(consoles);
        }

        // POST: Consoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ImagemURL,WikipediaURL,Nome,Fabricante,Geracao,Ano")] Consoles consoles)
        {
            if (id != consoles.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consoles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsolesExists(consoles.ID))
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
            return View(consoles);
        }

        // GET: Consoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                id = 1;
            }

            var consoles = await _context.Consoles
                .FirstOrDefaultAsync(m => m.ID == id);
            if (consoles == null)
            {
                return NotFound();
            }

            return View(consoles);
        }

        // POST: Consoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consoles = await _context.Consoles.FindAsync(id);
            if (consoles != null)
            {
                _context.Consoles.Remove(consoles);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsolesExists(int id)
        {
            return _context.Consoles.Any(e => e.ID == id);
        }
    }
}
