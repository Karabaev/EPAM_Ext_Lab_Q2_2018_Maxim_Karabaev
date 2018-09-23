namespace Forum.Models
{
    using System;
    using DAL.Core;
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using DAL.Model.Entities;

    public class UserEditViewModel
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public int? ID { get; set; }
        /// <summary>
        /// Логин.
        /// </summary>
        [Display(Name = "Login")]
        public string Login { get; set; }
        /// <summary>
        /// Хэш пароля.
        /// </summary>
        [Display(Name = "Password hash")]
        public string PasswordHash { get; set; }
        /// <summary>
        /// Публичное имя пользователя.
        /// </summary>
        [Display(Name = "Public name")]
        public string PublicName { get; set; }
        /// <summary>
        /// Роль.
        /// </summary>
        public int UserRoleID { get; set; }
        /// <summary>
        /// Заблокирован пользователь?
        /// </summary>
        [Display(Name = "Banned")]
        public bool? IsBanned { get; set; }
        /// <summary>
        /// Почта.
        /// </summary>
        [Display(Name = "Email")]
        public string Email { get; set; }

     //   [HiddenInput(DisplayValue = false)]
        public DateTime RegistrationDate { get; set; }
        public Role UserRole { get; set; }
    }
}