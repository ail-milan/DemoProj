using JsonApiDotNetCore.Resources;
using JsonApiDotNetCore.Resources.Annotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DemoProj.DataLayer.Models
{
    [Resource]
    [Comment("A collection of ORGANIZATIONS records")]
    public class ORGANIZATIONS : Identifiable<int>
    {
        [Attr]
        [StringLength(150)]
        [Unicode(false)]
        public string Name { get; set; } = string.Empty;

        [Attr]
        public bool IsActive { get; set; } = true;

        [Required]
        public byte TypeId { get; set; }

        [Attr]
        [Required]
        public DateTime Created { get; set; }

        [Attr]
        public DateTime? Updated { get; set; }
    }
}
