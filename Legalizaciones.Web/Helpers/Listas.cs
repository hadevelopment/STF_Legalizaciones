using Legalizaciones.Interface;
using Legalizaciones.Interface.ISolicitud;
using Legalizaciones.Model;
using Legalizaciones.Model.Empresa;
using Legalizaciones.Model.Jerarquia;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legalizaciones.Web.Helpers
{
    public class Listas : Controller
    {
        public readonly IKactusEmpleadoRepository empleadoRepository;
        public readonly IMonedaRepository monedaRepository;
        public readonly IDestinoRepository destinoRepository;
        public readonly IZonaRepository zonaRepository;
        private readonly IBancoRepository bancoRepository;

        public Listas(
            IKactusEmpleadoRepository empleadoRepository,
            IMonedaRepository monedaRepository,
            IDestinoRepository destinoRepository,
            IZonaRepository zonaRepository,
            IBancoRepository bancoRepository
        )
        {
            this.empleadoRepository = empleadoRepository;
            this.monedaRepository = monedaRepository;
            this.destinoRepository = destinoRepository;
            this.zonaRepository = zonaRepository;
            this.bancoRepository = bancoRepository;
        }


        public List<Destino> getDestinos()
        {
            return destinoRepository.All().Where(a => a.Estatus == 1).ToList();
        }

        public List<Zona> getZonas() {
            return zonaRepository.All().Where(a => a.Estatus == 1).ToList();
        }

        public List<Banco> getBancos()
        {
            return bancoRepository.All().Where(a => a.Estatus == 1).ToList();
        }



    }
}
