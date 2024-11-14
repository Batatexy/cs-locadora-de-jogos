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
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.Hosting.Internal;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace LocadoraDejogos.Controllers
{
    public class JogosController : Controller
    {
        public async Task<IActionResult> ExportarJogosExcel()
        {
            var item = await _context.Jogos.ToListAsync();
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Jogos");
            worksheet.Cell(1, 1).Value = "Tabela Jogos";

            // Define a última linha e coluna com base nos dados
            int ultimaLinha = item.Count + 2;
            int ultimaColuna = 9;

            // Borda externa e pintar o fundo inteiro de branco
            var tabelaRange = worksheet.Range(1, 1, ultimaLinha, ultimaColuna);
            tabelaRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            tabelaRange.Style.Fill.BackgroundColor=XLColor.White;

            // Depois de pintar toda a tabela de branca, pintar cabeçalho de uma outra cor:
            // Mudar cor do Cabeçalho
            worksheet.Cell(1, 1).Style.Font.FontColor=XLColor.White;
            worksheet.Cell(1, 1).Style.Fill.BackgroundColor=XLColor.RedNcs;

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
                worksheet.Cell(2, 3).Value = "Desenvolvedor";
                worksheet.Cell(2, 4).Value = "Distribuidora";
                worksheet.Cell(2, 5).Value = "Gênero";
                worksheet.Cell(2, 6).Value = "Ano de Lançamento";
                worksheet.Cell(2, 7).Value = "Console";
                worksheet.Cell(2, 8).Value = "Unidades";
                worksheet.Cell(2, 9).Value = "Preço";

                // Dados
                for (int i = 0; i < item.Count; i++)
                {
                    worksheet.Cell(i + 3, 1).Value = item[i].ID;
                    worksheet.Cell(i + 3, 2).Value = item[i].Nome;
                    worksheet.Cell(i + 3, 3).Value = item[i].Desenvolvedor;
                    worksheet.Cell(i + 3, 4).Value = item[i].Distribuidora;
                    worksheet.Cell(i + 3, 5).Value = item[i].Genero;
                    worksheet.Cell(i + 3, 6).Value = item[i].Ano;
                    worksheet.Cell(i + 3, 7).Value = item[i].ConsoleID;
                    worksheet.Cell(i + 3, 8).Value = item[i].Unidade;
                    worksheet.Cell(i + 3, 9).Value = "R$" + item[i].Preco;

                    // Arrumar algumas colunas
                    worksheet.Cell(i + 3, 9).Style.NumberFormat.Format = "0.00"; 
                    worksheet.Cell(i + 3, 1).Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(i + 3, 6).Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(i + 3, 7).Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(i + 3, 8).Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;

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
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Tabela Jogos.xlsx");
                }
            }
        }

        private readonly ApplicationDbContext _context;

        public JogosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Jogos
        public async Task<IActionResult> Index(string? Ordenar, string? Nome)
        {
            var applicationDbContext = _context.Jogos.Include(j => j.Consoles).OrderBy(j => j.Nome);

            if (Ordenar != null)
            {
                switch (Ordenar)
                {
                    case "Nome":
                        applicationDbContext = _context.Jogos.Include(j => j.Consoles).OrderBy(j => j.Nome);
                        break;

                    case "Desenvolvedor":
                        applicationDbContext = _context.Jogos.Include(j => j.Consoles).OrderBy(j => j.Desenvolvedor);
                        break;

                    case "Distribuidora":
                        applicationDbContext = _context.Jogos.Include(j => j.Consoles).OrderBy(j => j.Distribuidora);
                        break;

                    case "Genero":
                        applicationDbContext = _context.Jogos.Include(j => j.Consoles).OrderBy(j => j.Genero);
                        break;

                    case "Ano":
                        applicationDbContext = _context.Jogos.Include(j => j.Consoles).OrderBy(j => j.Ano);
                        break;

                    case "ConsoleID":
                        applicationDbContext = _context.Jogos.Include(j => j.Consoles).OrderBy(j => j.Consoles.Nome);
                        break;

                    case "Preco":
                        applicationDbContext = _context.Jogos.Include(j => j.Consoles).OrderBy(j => j.Preco);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (Nome != null)
                {
                    var applicationDbContextBuscar = _context.Jogos.Include(j => j.Consoles).Where(j => j.Nome.ToLower().Contains(Nome.ToLower())).Where(j => j.Unidade.HasValue && j.Unidade > 0);
                    return View(await applicationDbContextBuscar.ToListAsync());
                }
            }

            return View(await applicationDbContext.Where(j => j.Unidade.HasValue && j.Unidade > 0).ToListAsync());
        }
        
        // GET: Jogos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                id = 1;
            }

            var jogos = await _context.Jogos
                .Include(j => j.Consoles)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jogos == null)
            {
                return NotFound();
            }

            return View(jogos);
        }

        // GET: Jogos/Create
        public IActionResult Create()
        {
            ViewData["ConsoleID"] = new SelectList(_context.Consoles, "ID", "Nome");
            return View();
        }

        // POST: Jogos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CapaURL,FundoURL,LojaURL,Nome,Desenvolvedor,Distribuidora,Genero,Ano,ConsoleID,Unidade,Preco")] Jogos jogos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jogos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsoleID"] = new SelectList(_context.Consoles, "ID", "ID", jogos.ConsoleID);
            return View(jogos);
        }

        // GET: Jogos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                id = 1;
            }

            var jogos = await _context.Jogos.FindAsync(id);
            if (jogos == null)
            {
                return NotFound();
            }
            ViewData["ConsoleID"] = new SelectList(_context.Consoles, "ID", "ID", jogos.ConsoleID);
            return View(jogos);
        }

        // POST: Jogos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CapaURL,FundoURL,LojaURL,Nome,Desenvolvedor,Distribuidora,Genero,Ano,ConsoleID,Unidade,Preco")] Jogos jogos)
        {
            if (id != jogos.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jogos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogosExists(jogos.ID))
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
            ViewData["ConsoleID"] = new SelectList(_context.Consoles, "ID", "ID", jogos.ConsoleID);
            return View(jogos);
        }

        // GET: Jogos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                id = 1;
            }

            var jogos = await _context.Jogos
                .Include(j => j.Consoles)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jogos == null)
            {
                return NotFound();
            }

            return View(jogos);
        }

        // POST: Jogos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jogos = await _context.Jogos.FindAsync(id);
            if (jogos != null)
            {
                _context.Jogos.Remove(jogos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JogosExists(int id)
        {
            return _context.Jogos.Any(e => e.ID == id);
        }
    }
}
