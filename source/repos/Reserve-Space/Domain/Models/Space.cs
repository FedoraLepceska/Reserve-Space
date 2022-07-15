using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Space : BaseEntity
    {
        [Required]
        [Display(Name = "Space Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Display(Name = "Price per night")]
        public float price { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        [Display(Name = "Image")]
        public string image { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Date is required.")]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Date is required.")]
        public DateTime DateTo { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }

        public ICollection<ReservedSpaces> ReservedSpaces { get; set; }
        public virtual ICollection<SpacesInOrder> Orders { get; set; }

    }
}
