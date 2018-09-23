namespace Forum.App_Start
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using DAL.Model.Entities;
    using Models;

    public class Mapping
    {
        public Mapping()
        {
            System.Action<IMapperConfigurationExpression> config = cfg => cfg.CreateMap<Role, RoleEditViewModel>();
            config += cfg => cfg.CreateMap<User, UserEditViewModel>();
            config += cfg => cfg.CreateMap<User, UserListViewModel>();//.ForMember("RegistrationDate", s => s.MapFrom(c => c.RegistrationDate));
            config += cfg => cfg.CreateMap<Role, RoleDropDownViewModel>();
            Mapper.Initialize(config);
        }
    }
}