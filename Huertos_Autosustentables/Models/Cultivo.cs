using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huertos_Autosustentables.Models
{
    public class Cultivo
    {
        //Propiedades
        [Key]
        public int IdCultivos { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Nombre : ")]
        [Required(ErrorMessage = "Este campo es obligarorio")]
        public string NombreCultivos { get; set; }

        [Column(TypeName = "nvarchar(1500)")]
        [DisplayName("Introducción : ")]
        [Required(ErrorMessage = "Este campo es obligarorio")]
        public string IntroduccionCultivos { get; set; }

        [Column(TypeName = "nvarchar(4000)")]
        [DisplayName("Cuerpo : ")]
        [Required(ErrorMessage = "Este campo es obligarorio")]
        public string CuerpoCultivos { get; set; }

        [Column(TypeName = "nvarchar(2000)")]
        [DisplayName("Recomendaciones : ")]
        [Required(ErrorMessage = "Este campo es obligarorio")]
        public string RecomendacionesCultivos { get; set; }

        [NotMapped]
        [DisplayName("Imagen :")]
        public IFormFile ImageFile { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Imagen")]
        public string ImageName { get; set; }

        //Relaciones
        [ForeignKey("Tipo Cultivo")]
        [DisplayName("Tipo Cultivo : ")]
        public int IdTipoCultivo { get; set; }
        public TipoCultivo Fk_ { get; set; }

        [ForeignKey("Región")]
        [DisplayName("Región : ")]
        public int IdRegiones { get; set; }
        public Region FK_ { get; set; }

        public virtual ICollection<DetalleUsersCultivo> Detalles { get; set; }
    }
}
