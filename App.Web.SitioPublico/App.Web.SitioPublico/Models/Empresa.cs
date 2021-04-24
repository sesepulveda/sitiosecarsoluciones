using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Web.SitioPublico.Models
{
    public class Empresa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmpresa { get; set; }
        public int Rut { get; set; }
        [StringLength(1)]
        public string Dv { get; set; }
        [StringLength(150)]
        public string RazonSocial { get; set; }
    }
}