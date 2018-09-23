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
        [HiddenInput(DisplayValue = false)]
        public int? ID { get; set; }

        /// <summary>
        /// Название роли.
        /// </summary>
        [Display(Name = "Role name")]
        public string Name { get; set; }

        /// <summary>
        /// Уровень доступа.
        /// </summary>
        [Display(Name = "Access level")]
        public int? AccessLevel { get; set; }
    }
}