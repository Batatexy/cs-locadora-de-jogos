﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LocadoraDejogos.Data;
using LocadoraDejogos.Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;

namespace LocadoraDejogos.Controllers
{

    public class AlugueisController : Controller
    {
        public async Task<IActionResult> ExportarAlugueisExcel()
        {
            var item = await _context.Alugueis.ToListAsync();
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Alugueis");
            worksheet.Cell(1, 1).Value = "Tabela Alugueis";

            // Define a última linha e coluna com base nos dados
            int ultimaLinha = item.Count + 2;
            int ultimaColuna = 4;

            // Borda externa e pintar o fundo inteiro de branco
            var tabelaRange = worksheet.Range(1, 1, ultimaLinha, ultimaColuna);
            tabelaRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            tabelaRange.Style.Fill.BackgroundColor=XLColor.White;

            // Depois de pintar toda a tabela de branca, pintar cabeçalho de uma outra cor:
            // Mudar cor do Cabeçalho
            worksheet.Cell(1, 1).Style.Font.FontColor=XLColor.Black;
            worksheet.Cell(1, 1).Style.Fill.BackgroundColor=XLColor.GreenPigment;

            // Mudar tamanho da fonte e outras configurações de formatação
            worksheet.Cell(1, 1).Style.Font.FontSize=20;
            worksheet.Row(1).Height = 20;

            worksheet.Range(1, 1,1,ultimaColuna).Style.Alignment.Vertical=XLAlignmentVerticalValues.Center;
            worksheet.Range(1, 1,1,ultimaColuna).Merge().Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;

            using (workbook)
            {
                // Cabeçalho
                worksheet.Cell(2, 1).Value = "ID";

                worksheet.Cell(2, 2).Value = "ID do Cliente";
                //worksheet.Cell(2, 3).Value = "Nome do Cliente";

                worksheet.Cell(2, 3).Value = "ID do Jogo";
                //worksheet.Cell(2, 5).Value = "Nome do Jogo";

                worksheet.Cell(2, 4).Value = "ID do Funcionário";
                //worksheet.Cell(2, 7).Value = "Nome do Funcionário";

                // Dados
                for (int i = 0; i < item.Count; i++)
                {
                    worksheet.Cell(i + 3, 1).Value = item[i].ID;
                    worksheet.Cell(i + 3, 2).Value = item[i].ClienteID;
                    //worksheet.Cell(i + 3, 3).Value = item[i].ClienteID.Nome;

                    worksheet.Cell(i + 3, 3).Value = item[i].JogoID;
                    //worksheet.Cell(i + 3, 5).Value = item[i].JogoID.Nome;

                    worksheet.Cell(i + 3, 4).Value = item[i].FuncionarioID;
                    //worksheet.Cell(i + 3, 7).Value = item[i].FuncionarioID.Nome;

                    // Arrumar algumas colunas
                    worksheet.Cell(i + 3, 1).Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(i + 3, 2).Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(i + 3, 3).Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;
                    worksheet.Cell(i + 3, 4).Style.Alignment.Horizontal=XLAlignmentHorizontalValues.Center;

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
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Tabela Alugueis.xlsx");
                }
            }
        }

        private readonly ApplicationDbContext _context;

        public AlugueisController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? Valor)
        {
            var applicationDbContext = _context.Alugueis.Include(a => a.Clientes).Include(a => a.Funcionarios).Include(a => a.Jogos);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Alugueis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alugueis = await _context.Alugueis
                .Include(a => a.Clientes)
                .Include(a => a.Funcionarios)
                .Include(a => a.Jogos)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (alugueis == null)
            {
                return NotFound();
            }

            return View(alugueis);
        }

        // GET: Alugueis/Create
        public IActionResult Create(int? Jogo, string? Cliente)
        {
            Random random = new Random();

            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ID", "Nome", Cliente);
                
            //Já abre a tela com o jogo desejado selecionado
            ViewData["JogoID"] = new SelectList(_context.Jogos, "ID", "Nome", Jogo);

            //Randomizar funcionários
            var Funcionario = random.Next(1, 4);
            ViewData["FuncionarioID"] = new SelectList(_context.Funcionarios, "ID", "Nome", Funcionario);

            return View();
        }

        // POST: Alugueis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ClienteID,JogoID,FuncionarioID")] Alugueis alugueis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alugueis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ID", "Nome", alugueis.ClienteID);
            ViewData["FuncionarioID"] = new SelectList(_context.Funcionarios, "ID", "Nome", alugueis.FuncionarioID);
            ViewData["JogoID"] = new SelectList(_context.Jogos, "ID", "Nome", alugueis.JogoID);
            return View(alugueis);
        }

        // GET: Alugueis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alugueis = await _context.Alugueis.FindAsync(id);
            if (alugueis == null)
            {
                return NotFound();
            }
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ID", "Nome", alugueis.ClienteID);
            ViewData["FuncionarioID"] = new SelectList(_context.Funcionarios, "ID", "Nome", alugueis.FuncionarioID);
            ViewData["JogoID"] = new SelectList(_context.Jogos, "ID", "Nome", alugueis.JogoID);
            return View(alugueis);
        }

        // POST: Alugueis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ClienteID,JogoID,FuncionarioID")] Alugueis alugueis)
        {
            if (id != alugueis.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alugueis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlugueisExists(alugueis.ID))
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
            ViewData["ClienteID"] = new SelectList(_context.Clientes, "ID", "Nome", alugueis.ClienteID);
            ViewData["FuncionarioID"] = new SelectList(_context.Funcionarios, "ID", "Nome", alugueis.FuncionarioID);
            ViewData["JogoID"] = new SelectList(_context.Jogos, "ID", "Nome", alugueis.JogoID);
            return View(alugueis);
        }

        // GET: Alugueis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alugueis = await _context.Alugueis
                .Include(a => a.Clientes)
                .Include(a => a.Funcionarios)
                .Include(a => a.Jogos)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (alugueis == null)
            {
                return NotFound();
            }

            return View(alugueis);
        }

        // POST: Alugueis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alugueis = await _context.Alugueis.FindAsync(id);
            if (alugueis != null)
            {
                _context.Alugueis.Remove(alugueis);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlugueisExists(int id)
        {
            return _context.Alugueis.Any(e => e.ID == id);
        }
    }
}
