using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace EventosIO.Application.ViewModels
{
    public class CategoriaViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }



        public SelectList Categorias()
        {
            return new SelectList(ListarCategorias(), "Id", "Nome");
        }

        public List<CategoriaViewModel> ListarCategorias()
        {
            var categorias = new List<CategoriaViewModel>()
            {
                new CategoriaViewModel() { Id = new Guid("409ab097-66f0-41bd-bcc4-3d5feb0b4acc"), Nome = "Congresso" },
                new CategoriaViewModel() { Id = new Guid("c9b68cc6-bd2d-4497-a3fd-ec7272b6b5e8"), Nome = "Meetup" },
                new CategoriaViewModel() { Id = new Guid("22365f44-1c2e-4741-a3cb-255c084bb36e"), Nome = "Workshop" }
            };

            return categorias;
        }


    }
}
