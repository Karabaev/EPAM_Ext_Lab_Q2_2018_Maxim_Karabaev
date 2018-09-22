namespace Forum.Models
{
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;

    public class RoleDropDownViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int? ID { get; set; }

        /// <summary>
        /// Название роли.
        /// </summary>
        //[Display(Name = "Role name")]
        // [Required(ErrorMessage = "Please, enter role name")]
        public string Name { get; set; }

        /// <summary>
        /// Уровень доступа.
        /// </summary>
        //[Display(Name = "Access level")]
        // [Range(0, 10, ErrorMessage = "Please, enter access level 0-10")]
        public int? AccessLevel { get; set; }
    }
}