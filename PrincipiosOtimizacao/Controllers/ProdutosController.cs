using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PrincipiosOtimizacao;

namespace ExemploCompressao.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    {
        [HttpGet("BuscaProdutos")]
        public async Task<IActionResult> Get()
        {
            List<Produto> produtos = new List<Produto>();

            Produto prod;
            for (int i = 1; i <= 100; i++)
            {
                prod = new Produto();
                prod.CodProduto = i.ToString("0000");
                prod.NomeProduto = string.Format("PRODUTO {0:0000}", i);
                prod.Preco = i / 10.0;

                produtos.Add(prod);
            }

            return Ok(produtos);
        }

        [HttpGet("BuscaProdutosComCache")]
        public async Task<IActionResult> Get([FromServices] IMemoryCache cache)
        {
            List<Produto> produtos = cache.GetOrCreate<List<Produto>>(
                "Produtos", context =>
                {
                    // setar o tempo de experiração e prioridade do cache
                    context.SetAbsoluteExpiration(TimeSpan.FromSeconds(30));
                    context.SetPriority(CacheItemPriority.High);

                    List<Produto> cacheProdutos = new List<Produto>();
                    Produto prod;

                    for (int i = 1; i <= 100; i++)
                    {
                        prod = new Produto();
                        prod.CodProduto = i.ToString("0000");
                        prod.NomeProduto = string.Format("PRODUTO {0:0000}", i);
                        prod.Preco = i / 10.0;

                        // Simular tempo de acesso a uma base de dados...
                        Thread.Sleep(TimeSpan.FromMilliseconds(300));
                        cacheProdutos.Add(prod);
                    }

                    return cacheProdutos;
                });

            return Ok(produtos);
        }
    }
}
