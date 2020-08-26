using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Huertos_Autosustentables.Models
{
    public class Clima
    {
        //Propiedades
        [Key]
        [DisplayName("Clima : ")]
        public int IdClima { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        [DisplayName("Nombre del Clima : ")]
        [Required(ErrorMessage = "Este campo es obligarorio")]
        public string NombreClima { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        [DisplayName("Caraterísticas : ")]
        [Required(ErrorMessage = "Este campo es obligarorio")]
        public string CaracteristicasClima { get; set; }

        [NotMapped]
        [DisplayName("Subir Imagen")]
        //[Required(ErrorMessage = "Este campo es obligarorio")]
        public IFormFile ImageFile { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Imagen")]
        //[Required(ErrorMessage = "Este campo es obligarorio")]
        public string ImageName { get; set; }

        //Relacion
        public virtual ICollection<Region> Regiones { get; set; }
    }
}
