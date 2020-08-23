using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Huertos_Autosustentables.Models
{
    public class DetalleUsersCultivo
    {
        //Propiedades
        [Key]
        public int IdDetalle { get; set; }

        [DisplayName("Metros Cuadrados (m²): ")]
        public float MetrosCuadradosDetalle { get; set; }

        [DisplayName("Precio aproximado de semillas (Sobre): ")]
        public float PrecioSemillasDetalle { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        [DisplayName("Lugar de Cultivo : ")]
        public string LugarCultivoDetalle { get; set; }

        //Relaciones
        [ForeignKey("Cultivo")]
        [DisplayName("Cultivo : ")]
        public int IdCultivo { get; set; }
        public Cultivo FK_ { get; set; }

        [ForeignKey("UseId")]
        [DisplayName("Usuario : ")]
        public string UserId { get; set; }
        public  IdentityUser User { get; set; }
    }
}
