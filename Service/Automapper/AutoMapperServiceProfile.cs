using AutoMapper;
using AutoMapper.Internal;
using DomainEntity.AIEntities;
using DomainEntity.ChatEntities;
using DomainEntity.CustomerEntities;
using DomainEntity.Exchange;
using DomainEntity.FileEntities;
using DomainEntity.Request;

namespace Service.Automapper;

public class AutoMapperServiceProfile : Profile
{
    public AutoMapperServiceProfile()
    {
        this.Internal().ForAllMaps((obj, cnfg) =>
            cnfg.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)));

        CreateMap<CustomerRequest, Customer>()
            .ForMember(dest => dest.Password, opt => opt.Ignore());
        CreateMap<Customer, CustomerExchange>();

        CreateMap<AIRequest, AI>();
        CreateMap<AI, AIExchange>();

        CreateMap<AIFileChatRequest, AIFileChat>();
        CreateMap<AIFileChat, AIFileChatExchange>();

        CreateMap<ChatRequest, Chat>();
        CreateMap<Chat, ChatExchange>();

        CreateMap<MessageRequest, Message>()
            .ForMember(dest => dest.FileId, opt => opt.MapFrom(src =>
                src.FileId == Guid.Empty ? (Guid?)null : src.FileId))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src =>
                src.CustomerId == Guid.Empty ? (Guid?)null : src.CustomerId));
        CreateMap<Message, MessageExchange>();

        CreateMap<NureFileRequest, NureFile>();
        CreateMap<NureFile, NureFileExchange>();
    }
}
