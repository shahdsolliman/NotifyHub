using AutoMapper;
using NotifyHub.Api.DTOs;
using NotifyHub.Application.Commands;
using NotifyHub.Domain.Entities;

namespace NotifyHub.Api.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Request DTO -> Command
        CreateMap<CreateNotificationRequest, CreateNotificationCommand>();

        // Entity -> Response DTO
        CreateMap<NotificationOutbox, NotificationResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Recipient, opt => opt.MapFrom(src => src.Recipient))
            .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.SentAt, opt => opt.MapFrom(src => src.LastAttemptAt))
            .ForMember(dest => dest.ErrorMessage, opt => opt.MapFrom(src => src.ErrorMessage));
    }
}
