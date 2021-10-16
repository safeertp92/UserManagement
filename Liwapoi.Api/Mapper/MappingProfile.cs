using AutoMapper;
using Liwapoi.Api.Models.RequestModel;
using Liwapoi.DB.Models;
using Liwapoi.Models.BLL;
using Liwapoi.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liwapoi.Api.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            #region UserMapping
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Name,
                    opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Id,
                    opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.ImgSrc,
                    opt => opt.MapFrom(src => src.FileName))
                .AfterMap((src, dest) =>
                {
                    if (src.UserRoles != null && src.UserRoles.Any())
                    {
                        var role = src.UserRoles.FirstOrDefault();
                        if (role != null)
                        {
                            dest.RoleId = role.RoleId;
                            dest.RoleName = role.Role.RoleName;
                        }
                        else
                        {
                            dest.RoleId = 0;
                            dest.RoleName = string.Empty;
                        }
                    }
                });

            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UserId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FileName,
                    opt => opt.MapFrom(src => src.ImgSrc));

            CreateMap<LoginRequest, LoginModel>();
            CreateMap<UserModel, UserDTO>();
            CreateMap<UserDTO, UserModel>();
            CreateMap<UserAddRequest, UserModel>();
            #endregion

            #region RoleMapping
            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>();

            CreateMap<RoleDTO, RoleModel>();
            CreateMap<RoleModel, RoleDTO>();
            #endregion
        }
    }
}
