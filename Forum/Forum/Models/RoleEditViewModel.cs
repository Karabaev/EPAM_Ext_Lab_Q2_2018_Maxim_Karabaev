namespace Forum.Models
{
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;
    using DAL.Model.Entities;
    /// <summary>
    /// Вью модель для Role.
    /// </summary>
    public class RoleEditViewModel
    {
        public RoleEditViewModel(Role role)
        {
            this.ID = role.ID;
            this.Name = role.Name;
            this.AccessLevel = role.AccessLevel;
        }

        public RoleEditViewModel() { }

        [HiddenInput(DisplayValue = false)]
        public int? ID { get; set; }

        /// <summary>
        /// Название роли.
        /// </summary>
        [Display(Name = "Role name")]
       // [Required(ErrorMessage = "Please, enter role name")]
        public string Name { get; set; }

        /// <summary>
        /// Уровень доступа.
        /// </summary>
        [Display(Name = "Access level")]
       // [Range(0, 10, ErrorMessage = "Please, enter access level 0-10")]
        public int? AccessLevel { get; set; }

        public static implicit operator RoleEditViewModel(Role role)
        {
            return new RoleEditViewModel(role);
        }
    }
}