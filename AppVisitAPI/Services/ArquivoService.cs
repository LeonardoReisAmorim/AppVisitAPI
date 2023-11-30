﻿using AppVisitAPI.Data.Context;
using AppVisitAPI.DTOs.ArquivoDTO;
using AppVisitAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppVisitAPI.Services
{
    public class ArquivoService
    {
        private readonly Context _context;

        public ArquivoService(Context context)
        {
            _context = context;
        }

        public byte[] GetArquivoById(int id)
        {
            var arquivo = _context.Arquivos.AsNoTracking().Where(x => x.Id == id).Select(x => x.FilePlace).First();

            return arquivo;
        }

        public async Task<LerArquivoDTO> CreateArquivo(byte[] arquivoDTO)
        {
            
            var arquivo = new Arquivo
            {
                FilePlace = arquivoDTO
            };

            await _context.Arquivos.AddAsync(arquivo);
            _context.SaveChanges();

            var lerArquivo = new LerArquivoDTO
            {
                Id=arquivo.Id
            };

            return lerArquivo;
        }

        public async Task<bool> UpdateArquivo(int id, EditarArquivo editarArquivoDTO)
        {
            var arquivo = await _context.Arquivos.FirstOrDefaultAsync(arquivo => arquivo.Id == id);

            if (arquivo == null)
            {
                return false;
            }

            arquivo.FilePlace = editarArquivoDTO.Arquivo;
            _context.SaveChanges();

            return true;
        }

        public async Task<bool> DeleteArquivo(int id)
        {
            var arquivo = await _context.Arquivos.FirstOrDefaultAsync(arquivo => arquivo.Id == id);

            if (arquivo == null)
            {
                return false;
            }

            _context.Remove(arquivo);
            _context.SaveChanges();
            
            return true;
        }
    }
}
