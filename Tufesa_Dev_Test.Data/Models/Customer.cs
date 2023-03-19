using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Tufesa_Dev_Test.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace Tufesa_Dev_Test.Data.Models
{
    [Table("Customer")]
    [Index(nameof(RFC), IsUnique = true)]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID", ResourceType = typeof(Resources.Customer))]
        public int Id { get; set; }

        [Display(Name = "FIRST_NAME", ResourceType = typeof(Resources.Customer))]
        [Required(ErrorMessage = "Your first name is required.")]
        public string FirstName { get; set; }

        [Display(Name = "LAST_NAME", ResourceType = typeof(Resources.Customer))]
        [Required(ErrorMessage = "Your last name is required.")]
        public string LastName { get; set; }

        [Column("RFC")]
        [MaxLength(13)]
        [Display(Name = "RFC", ResourceType = typeof(Resources.Customer))]

        [Required(ErrorMessage = "RFC is required.")]
        [RegularExpression(@"^(((?!(([CcKk][Aa][CcKkGg][AaOo])|([Bb][Uu][Ee][YyIi])|([Kk][Oo](([Gg][Ee])|([Jj][Oo])))|([Cc][Oo](([Gg][Ee])|([Jj][AaEeIiOo])))|([QqCcKk][Uu][Ll][Oo])|((([Ff][Ee])|([Jj][Oo])|([Pp][Uu]))[Tt][Oo])|([Rr][Uu][Ii][Nn])|([Gg][Uu][Ee][Yy])|((([Pp][Uu])|([Rr][Aa]))[Tt][Aa])|([Pp][Ee](([Dd][Oo])|([Dd][Aa])|([Nn][Ee])))|([Mm](([Aa][Mm][OoEe])|([Ee][Aa][SsRr])|([Ii][Oo][Nn])|([Uu][Ll][Aa])|([Ee][Oo][Nn])|([Oo][Cc][Oo])))))[A-Za-zñÑ&][aeiouAEIOUxX]?[A-Za-zñÑ&]{2}(((([02468][048])|([13579][26]))0229)|(\d{2})((02((0[1-9])|1\d|2[0-8]))|((((0[13456789])|1[012]))((0[1-9])|((1|2)\d)|30))|(((0[13578])|(1[02]))31)))[a-zA-Z1-9]{2}[\dAa])|([Xx][AaEe][Xx]{2}010101000))$",
         ErrorMessage = "Invalid Format 'XXXX000000XX0'.")]
        public string RFC { get; set; }
        
        [Column("Email")]
        [Display(Name = "EMAIL", ResourceType = typeof(Resources.Customer))]
        [MaxLength(100)]
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
         ErrorMessage = "Invalid Format 'example@domain.com'.")]
        public string Email { get; set; }

        [Column("Birthdate")]
        [DataType(DataType.Date)]
        [Display(Name = "BORNDATE", ResourceType = typeof(Resources.Customer))]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? BornDate { get; set; }


        public enum CustomerStatus {
            NotSet,
            Active,
            Inactive
        };

        [System.ComponentModel.DefaultValue(CustomerStatus.NotSet)]
        [EnumHelper(typeof(CustomerStatus))]
        [Display(Name = "STATUS", ResourceType = typeof(Resources.Customer))]
        public CustomerStatus Status { get; set; } = CustomerStatus.NotSet;

        public string ToValuesString()
        {
            return @$"'Id': {Id}, 'FirstName': '{FirstName}','LastName': '{LastName}','RFC': '{RFC}','Email': '{Email}','BornDate': '{BornDate.Value.ToString("MM/dd/yyyy")}','Status': {((int)Status).ToString()}";
        }

    }
}
